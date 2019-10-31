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
    public float JumpPower = 13f;
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
    private float maxSpeed;

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
        maxSpeed = speed + 3f;
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

            Vector3 targetVelocity = new Vector3(deltaX, 0f, deltaZ);
            Vector3 velocityChange = new Vector3();

            targetVelocity *= speed;
            targetVelocity = transform.TransformDirection(targetVelocity);
            velocityChange = (targetVelocity - GetComponent<Rigidbody>().velocity);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {

                float distance = Vector3.Distance(transform.position, hit.point);
                //this checks if the player is airbourne
                //Debug.Log(distance + ": " + "isColliding: " + isColliding);

                if (distance > 1.3f)
                {
                    GetComponent<Collider>().material.staticFriction = 0.0f;
                    GetComponent<Collider>().material.dynamicFriction = 0.0f;
                    GetComponent<Collider>().material = GetComponent<Collider>().material; // WTF
                    isGrounded = false;
                }
                else
                {
                    GetComponent<Collider>().material.staticFriction = 0.6f;
                    GetComponent<Collider>().material.dynamicFriction = 0.6f;
                    GetComponent<Collider>().material = GetComponent<Collider>().material; // WTF
                    isGrounded = true;
                }
            }

            Debug.Log(GetComponent<Rigidbody>().velocity.y);
            // Setting max vertical speed
            if (GetComponent<Rigidbody>().velocity.y > maxSpeed)
            {
                velocityChange.y = -3f;
            }
            else
                velocityChange.y = 0;
            //Apply changes
            GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);


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
                GetComponent<Rigidbody>().AddForce(new Vector3(0, JumpPower, 0), ForceMode.Impulse);
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
            Debug.Log("Velocity: " + GetComponent<Rigidbody>().velocity.magnitude);


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

        // stop player from sliding
        if (GetComponent<Rigidbody>().velocity.magnitude < .02)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

    }
}