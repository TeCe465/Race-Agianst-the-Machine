using UnityEngine;
using System.Collections;

// basic WASD-style movement control
// commented out line demonstrates that transform.Translate instead of charController.Move doesn't have collision detection

//[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{

    //[System.NonSerialized] public Vector3 movement;
    [System.NonSerialized] public bool isCrouching;
    [System.NonSerialized] public bool lightStatus;
    [System.NonSerialized] public bool isWalkingBackwards;

    private CapsuleCollider capsuleCollider;
    private PlayerCharacter player;
    GameObject conditions;
    private float speed = 3.0f;
    private float SpeedMultiplier;
    private float deltaX;
    private float deltaZ;
    private bool stopped = false;
    public float gravity = 35f;
    public bool isGrounded;
    private bool isColliding = false;

    public float maxVelocityChange = 10.0f;
    // this will allow other scripts to alter the speed
    public float defaultSpeed;
    public float defaultSpeedMultiplier;
    //i need a way for the speed to stop changing since i want it to be more modular. so i should put a bool trigger

    void Start()
    {
        conditions = GameObject.Find("Conditions");
        player = GetComponent<PlayerCharacter>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        isCrouching = false;
        isWalkingBackwards = false;
        speed = defaultSpeed;
        SpeedMultiplier = defaultSpeedMultiplier;
    }


    void FixedUpdate()
    {


        //this looks at the parent's playerCharacter and checks its health which allows me to disable movement when they die
        if (player.isAlive)
        {
            //if the player is revived somehow, disable stopped
            stopped = false;

            // Inverse if camera is flipped
            if (conditions.GetComponent<CameraFlip>().flipped)
                deltaX = -(Input.GetAxis("Horizontal"));
            else
                deltaX = Input.GetAxis("Horizontal");

            deltaZ = Input.GetAxis("Vertical");

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                float distance = Vector3.Distance(transform.position, hit.point);
                //this checks if the player is airbourne
                if (distance < 1.5f && isColliding)
                {
                    // Movement Script
                    Vector3 targetVelocity = new Vector3(deltaX, 0f, deltaZ);
                    targetVelocity *= speed;
                    targetVelocity = transform.TransformDirection(targetVelocity);
                    Vector3 velocityChange = (targetVelocity - GetComponent<Rigidbody>().velocity);
                    velocityChange.y = 0;
                    GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);
                    isGrounded = true;
                }
                // This checks if the player is airbourne and not pushing agianst a wall
                else if (distance > 1.5f && !isColliding)
                {
                    // Movement Script
                    Vector3 targetVelocity = new Vector3(deltaX, 0f, deltaZ);
                    targetVelocity *= speed;
                    targetVelocity = transform.TransformDirection(targetVelocity);
                    Vector3 velocityChange = (targetVelocity - GetComponent<Rigidbody>().velocity);
                    velocityChange.y = 0;
                    GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);
                    isGrounded = false;

                }
                else
                    isGrounded = false;
            }

            // Movement Key Inputs
            if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftShift))
            {
                SpeedMultiplier = defaultSpeedMultiplier / 1.5f;
            }
            else
            {
                isWalkingBackwards = false;
            }

            if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.S))
            {
                SpeedMultiplier = defaultSpeedMultiplier * 2f;
            }
            else
            {
            }

            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                gravity = 35f;
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 12, 0), ForceMode.Impulse);
            }
            else

            //make sure they dont cheat by pressing run and croutch at the same time
            if ((!Input.GetKey(KeyCode.LeftShift)) && (Input.GetKey(KeyCode.LeftControl)))
            {
                SpeedMultiplier = defaultSpeedMultiplier / 1.5f;
                isCrouching = true;
                capsuleCollider.height = 0.8f;
            }
            else
            {
                isCrouching = false;
                capsuleCollider.height = 2.1f;
            }

            //reset speed modified if player isnt using either crouch or run
            if ((!Input.GetKey(KeyCode.LeftShift)) && (!Input.GetKey(KeyCode.LeftControl)))
            {
                SpeedMultiplier = defaultSpeedMultiplier;
            }

            // after everything has been adjusted, alter the speed!
            speed = defaultSpeed * SpeedMultiplier;


        }
        else
        {

            if (!stopped)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                stopped = true;
            }
            gravity = 35f;
            speed = 0;
            isCrouching = false;
            isWalkingBackwards = false;
        }

        // gravity is always applied
        GetComponent<Rigidbody>().AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0));
    }
    void OnCollisionStay(Collision collision)
    {
        //if player is colliding with something, stop it
        isColliding = true;
    }
    //    // This prevents the player from sliding down a slope when idling
    //    if (!Input.anyKey)
    //    {
    //        if (GetComponent<Rigidbody>().velocity.magnitude < .02)
    //        {
    //            GetComponent<Rigidbody>().velocity = Vector3.zero;
    //            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    //        }

    //    }
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, Vector3.down, out hit))
    //    {
    //        //GameObject hitObj = hit.transform.gameObject;
    //        float distance = Vector3.Distance(transform.position, hit.point);
    //        if (distance > 3f)
    //            isGrounded = false;
    //        else
    //            isGrounded = true;

    //    }

    //}
    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }
}