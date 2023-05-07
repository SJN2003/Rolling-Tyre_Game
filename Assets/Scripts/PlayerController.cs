using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;

    private int desiredLane = 1;
    public float laneDistance = 4;

    public float jumpForce;
    public float Gravity = -20;

    public Animator animator;
    private bool isSliding = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.isGameStarted)
            return;
        
        if(forwardSpeed < maxSpeed)
            forwardSpeed += 0.1f * Time.deltaTime;

        animator.SetBool("isGameStarted", true);
        direction.z = forwardSpeed;

        
        animator.SetBool("isGrounded", true);

        if(controller.isGrounded)
        {
            direction.y = -1;
            if(SwipeManager.swipeUp)
            {
                Jump();
            }
        }
        else
        {
            direction.y += Gravity * Time.deltaTime;
        }

        if(SwipeManager.swipeDown && !isSliding)
        {
            StartCoroutine(Slide());
        }

        if(SwipeManager.swipeRight)
        {
            desiredLane++;
            if(desiredLane == 3)
                desiredLane = 2;
        }

        if(SwipeManager.swipeLeft)
        {
            desiredLane--;
            if(desiredLane == -1)
                desiredLane = 0;
        }

        Vector3 tragetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if(desiredLane == 0)
        {
            tragetPosition += Vector3.left * laneDistance;
        }else if(desiredLane == 2)
        {
            tragetPosition += Vector3.right * laneDistance;
        }

        if(transform.position == tragetPosition)
            return;
        Vector3 diff = tragetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if(moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }
    
    private void FixedUpdate()
    {
        if(!PlayerManager.isGameStarted)
            return;
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;
        yield return new WaitForSeconds(1.2f);

        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;
        animator.SetBool("isSliding", false);
        isSliding = false;
    }
}
