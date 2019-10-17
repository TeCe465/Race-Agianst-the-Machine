using UnityEngine;
using System.Collections;

// basic WASD-style movement control
// commented out line demonstrates that transform.Translate instead of charController.Move doesn't have collision detection

//[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{

    [System.NonSerialized] public Vector3 movement;
    [System.NonSerialized] public bool isCrouching;
    [System.NonSerialized] public bool lightStatus;
    [System.NonSerialized] public bool isWalkingBackwards;
    //private CharacterController _charController;

    private CapsuleCollider capsuleCollider;
    private PlayerCharacter player;
    private float speed = 3.0f;
    private float SpeedMultiplier;
    public float gravity = -9.8f;
    public bool isGrounded;

    // this will allow other scripts to alter the speed
    public float defaultSpeed;
    public float defaultSpeedMultiplier;
    //i need a way for the speed to stop changing since i want it to be more modular. so i should put a bool trigger

    void Start()
    {
        player = GetComponent<PlayerCharacter>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        isCrouching = false;
        isWalkingBackwards = false;
        speed = defaultSpeed;
        SpeedMultiplier = defaultSpeedMultiplier;
    }


    void Update()
    {


        //this looks at the parent's playerCharacter and checks its health which allows me to disable movement when they die
        if (player.isAlive)
        {
            transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime);
            float deltaX = Input.GetAxis("Horizontal") * speed;
            float deltaZ = Input.GetAxis("Vertical") * speed;
            movement = new Vector3(deltaX, 0, deltaZ);
            movement = Vector3.ClampMagnitude(movement, speed);
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);

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
                //SpeedMultiplier = defaultSpeedMultiplier;
            }

            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                Debug.Log("Jumping");
                movement.y = 10f;
                //rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                //isGrounded = false;
            }
            else
                movement.y = gravity;

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
    void OnCollisionStay()
    {
        isGrounded = true;
    }
}