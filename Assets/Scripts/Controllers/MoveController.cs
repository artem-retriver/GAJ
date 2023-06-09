using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private int speed;
    [SerializeField] private int laneSpeed;
    /*[SerializeField] private float jumpLenght;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float slideLenght;*/

    private Animator anim;
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private int currentLane = 135;
    private Vector3 verticalTargetPosition = new(135, 10);
    /*private bool jumping = false;
    private float jumpStart;
    private bool sliding = false;
    private float slideStart;
    private Vector3 boxColliderSize;*/

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        //boxColliderSize = boxCollider.size;
    }

    public void InputHandler()
    {
        if (SwipeController.swipeLeft)
        {
            ChangeLane(15);
        }
        else if (SwipeController.swipeRight)
        {
            ChangeLane(-15);
        }
        /*else if (SwipeController.swipeUp)
        {
            Jump();
        }
        else if (SwipeController.swipeDown)
        {
            Slide();
        }*/
    }

    public void Movebale()
    {
        /*if (jumping)
        {
            float ratio = (transform.position.z - jumpStart) / jumpLenght;
            if (ratio >= 1f)
            {
                jumping = false;
                anim.SetBool("Jumping", false);
            }
            else
            {
                verticalTargetPosition.y = Mathf.Sin(ratio * Mathf.PI) * jumpHeight;
            }
        }
        else
        {
            verticalTargetPosition.y = Mathf.MoveTowards(verticalTargetPosition.y, 0, 5 * Time.deltaTime);
        }

        if (sliding)
        {
            float ratio = (transform.position.z - slideStart) / slideLenght;
            if (ratio >= 1f)
            {

                sliding = false;
                anim.SetBool("Sliding", false);
                boxCollider.size = boxColliderSize;
            }
        }*/

        Vector3 targetPosition = new Vector3(verticalTargetPosition.x, verticalTargetPosition.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneSpeed * Time.deltaTime);
    }

    public void Move()
    {
        rb.velocity = Vector3.back * speed;
    }

    public void UnMove()
    {
        rb.velocity = Vector3.forward * 0;
    }

    private void ChangeLane(int direction)
    {
        int targetLane = currentLane + direction;

        if (targetLane < 120 || targetLane > 150)
            return;

        currentLane = targetLane;
        verticalTargetPosition = new Vector3(currentLane, 10, 0);
    }

    /*private void Jump()
    {
        if (!jumping)
        {
            jumpStart = transform.position.z;
            anim.SetFloat("JumpSpeed", speed / jumpLenght);
            anim.SetBool("Jumping", true);
            jumping = true;
        }
    }

    private void Slide()
    {
        if (!jumping && !sliding)
        {
            slideStart = transform.position.z;
            anim.SetFloat("JumpSpeed", speed / slideLenght);
            anim.SetBool("Sliding", true);
            Vector3 newSize = boxCollider.size;
            newSize.y = newSize.y / 2;
            boxCollider.size = newSize;
            sliding = true;
        }
    }*/
}
