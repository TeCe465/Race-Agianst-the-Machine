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
    Rigidbody rigidbody;

    private CapsuleCollider capsuleCollider;
    private PlayerCharacter player;
    GameObject conditions;
    private float speed = 3.0f;
    private float SpeedMultiplier;
    private float deltaX;
    private float deltaZ;
    public float gravity = 35f;
    public bool isGrounded;

    public float maxVelocityChange = 10.0f;
    // this will allow other scripts to alter the speed
    public float defaultSpeed;
    public float defaultSpeedMultiplier;
    //i need a way for the speed to stop changing since i want it to be more modular. so i should put a bool trigger

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
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
            // Inverse if camera is flipped
            if (conditions.GetComponent<CameraFlip>().flipped)
                deltaX = -(Input.GetAxis("Horizontal"));
            else
                deltaX = Input.GetAxis("Horizontal");

            deltaZ = Input.GetAxis("Vertical");


            // Movement Script
            Vector3 targetVelocity = new Vector3(deltaX, 0f, deltaZ);
            targetVelocity *= speed;
            targetVelocity = transform.TransformDirection(targetVelocity);
            Vector3 velocityChange = (targetVelocity - rigidbody.velocity);
            velocityChange.y = 0;
            rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
            rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));


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
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
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
            speed = 0;
            isCrouching = false;
            isWalkingBackwards = false;
        }
    }
    void OnCollisionStay(Collision collision)
    {
        // This prevents the player from sliding down a slope when idling
        if (!Input.anyKeyDown)
            gravity = 0f;
        else
            gravity = 35f;

        isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        gravity = 35f;
        isGrounded = false;
    }
}