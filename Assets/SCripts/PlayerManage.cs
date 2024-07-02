using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManage : MonoBehaviour
{
    public float playerSpeed = 0.9f;
    public float jumpSpeed = 6;
    public float rollSpeed = 6;
    public Rigidbody rb;
    private int lane = 0;
    private bool isGrounded = true;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && lane > -1)
        {
            lane--;
        }

        if (Input.GetKeyDown(KeyCode.D)  && lane < 1)
        {
            lane++;             
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            rb.velocity = Vector3.up * jumpSpeed;

        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            rb.velocity = Vector3.down * jumpSpeed;
            animator.SetBool("isRolling", true);
        }
        else
        {
            animator.SetBool("isRolling",false);
        }

     

    }

    private void FixedUpdate()
    {

        changeLane();
        isGrounded = Physics.Raycast(transform.position + Vector3.up , Vector3.down, 1.1f);
        animator.SetBool("IsJumping", !isGrounded);
    }

    private void changeLane() {
        switch (lane)
        {
            case -1:
                transform.position=Vector3.MoveTowards(transform.position, new Vector3(-5, transform.position.y, 0), playerSpeed);
                break;

            case -0:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, transform.position.y, 0), playerSpeed);
                break;

            case 1:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(5, transform.position.y, 0), playerSpeed);
                break;
        }
    }

}
