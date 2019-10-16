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
    public float speed = 3.0f;
    public float gravity = -9.8f;
    public bool isGrounded;

    void Start()
    {
        player = GetComponent<PlayerCharacter>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        //_charController = GetComponent<CharacterController>();
        isCrouching = false;
        isWalkingBackwards = false;
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
            //movement.y = gravity;



            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            //_charController.Move(movement);

            if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftShift))
            {
                speed = 2.0f;
                isWalkingBackwards = true;
            }
            else
            {
                isWalkingBackwards = false;
            }

            if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.S))
            {

                speed = 7.0f;
            }
            else
            {
                speed = 3.0f;
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Debug.Log("Jumping");
                movement.y = 10f;
                //rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                //isGrounded = false;
            }
            else
                movement.y = gravity;

            //make sure they dont cheat by pressing run and croutch at the same time
            if ((!Input.GetKeyDown(KeyCode.LeftShift)) && (Input.GetKey(KeyCode.LeftControl)))
            {
                isCrouching = true;
                capsuleCollider.height = 0.8f;
                speed = 2.0f;
            }
            else
            {
                isCrouching = false;
                capsuleCollider.height = 2.1f;
            }

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