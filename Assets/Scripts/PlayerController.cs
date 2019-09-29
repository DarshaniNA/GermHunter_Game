using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private bool isRunning = false;

    // animation
    private Animator anim;

    // movement
    private CharacterController controller;
    private int desiredLane = 1; // 0-left, 1-middle, 2-right
    private float gravity = 12.0f;
    private float verticalVelocity;
    private float jumpForce = 10.0f;

    //speed Modifier
    private float originalSpeed = 6.0f;
    private float speed;
    private float speedIncreaseLastTick;
    private float speedIncreaseTime = 2.5f;
    private float speedIncreaseAmount = 0.1f;
    private Vector3 moveDirection = Vector3.zero;

    // Use this for initialization
    void Start () {
        speed = originalSpeed;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        if (!isRunning)
            return;

        if(Time.time - speedIncreaseLastTick > speedIncreaseTime)
        {
            speedIncreaseLastTick = Time.time;
            speed += speedIncreaseAmount;
            //chamge the modifier text
            GameManager.Instance.UpdateModifier(speed - originalSpeed);
        }
        // gather the inputs on which lne we shoud be
        //if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        //    MoveLane(false);
        //if (Input.GetKeyDown(KeyCode.RightArrow)) 
        //    MoveLane(true);
        if (MobileInput.Instance.SwipeLeft)
            MoveLane(false);
        if (MobileInput.Instance.SwipeRight)
            MoveLane(true);

        //calculate where we should be in the future
        Vector3 targetPositiion = transform.position.z * Vector3.forward;
        if (desiredLane == 0) {
            targetPositiion += Vector3.left * Constant.LANE_DISTANCE;
        }
        else if (desiredLane == 2)
        {
            targetPositiion += Vector3.right * Constant.LANE_DISTANCE;
        }

        //let's calculate our move delta
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPositiion - transform.position).normalized.x * 12;

        bool isGrounded = IsGrounded();
        anim.SetBool("Grounded", isGrounded);

        //calculate Y
        if (IsGrounded()) // if grounded
        {
            verticalVelocity = -0.1f;

            moveDirection = new Vector3(Input.acceleration.x * 30, 0.0f, 0.0f);
            moveDirection = transform.TransformDirection(moveDirection);
            //if (Input.GetKeyDown(KeyCode.Space))
            if (MobileInput.Instance.SwipeUp)
            {
                //Jump
                anim.SetTrigger("Jump");
                verticalVelocity = jumpForce;
            }

        else if (MobileInput.Instance.SwipeDown)
            {
                //slide
                StartSliding();
                Invoke("StopSliding", 1.0f);
            }
        }
        else {
            verticalVelocity -= (gravity * Time.deltaTime);
            //fast falling
            //if (Input.GetKeyDown(KeyCode.Space)) {
            if (MobileInput.Instance.SwipeDown) { 
                    verticalVelocity = -jumpForce;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;       //Apply gravity  
        controller.Move(moveDirection * Time.deltaTime);

        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        //move the player
        controller.Move(moveVector * Time.deltaTime);

        //rotate the player where he is going
        Vector3 dir = controller.velocity;
        if (dir != Vector3.zero)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, Constant.TURN_SPEED);
        }
    }

    private void StartSliding()
    {
        anim.SetBool("Sliding", true);
        controller.height /= 1.2f;
        controller.center = new Vector3(controller.center.x, controller.center.y * 1.2f, controller.center.z);

    }

    private void StopSliding()
    {
        anim.SetBool("Sliding", false);
        controller.height *= 1.2f;
        controller.center = new Vector3(controller.center.x, controller.center.y / 1.2f, controller.center.z);
    }

    public void MoveLane(bool goingRight) {
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }

    private bool IsGrounded() {
        Ray groundRay = new Ray( new Vector3(
                controller.bounds.center.x,
                (controller.bounds.center.y - controller.bounds.extents.y) + 0.2f,
                controller.bounds.center.z),
            Vector3.down);
        Debug.DrawRay(groundRay.origin, groundRay.direction, Color.cyan, 1.0f);

        return Physics.Raycast(groundRay, 0.2f + 0.1f);
    }

    public void StartRunning()
    {
        isRunning = true;
        anim.SetTrigger("StartRunning");
    }

    private void Crash()
    {
        anim.SetTrigger("Death");
        isRunning = false;
        GameManager.Instance.OnDeath();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch(hit.gameObject.tag)
        {
            case "Obstacle":
                Crash();
                break;
        }
    }
}
