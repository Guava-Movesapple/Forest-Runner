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
    private CapsuleCollider col;
    private float rollTimer = 0;
    public bool isAlive;
    private Vector3 move = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        rb = GetComponent<Rigidbody>(); 
        animator = GetComponent<Animator>(); 
        col = GetComponent<CapsuleCollider>();
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
            move += Vector3.down * jumpSpeed;
            animator.SetBool("isRolling", true);
            rollTimer = 0.77f;
            col.height = 0.9f;
            col.center = new Vector3(col.center.x,0.5f,col.center.z); 
        }
        else
        {
            animator.SetBool("isRolling",false);
        }

        if(rollTimer > 0)
        {
            rollTimer -= Time.deltaTime;
            if(rollTimer < 0)
            {
                col.height = 1.651325f;
                col.center = new Vector3(col.center.x, 0.8464648f, col.center.z);
            }
        }

     

    }

    private void FixedUpdate()
    {
        if (transform.position.x != lane * 5)
        {
            changeLane();
        }
        isGrounded = Physics.Raycast(transform.position + Vector3.up , Vector3.down, 1.1f);
        animator.SetBool("IsJumping", !isGrounded);


    }

    private void changeLane() {
        switch (lane)
        {
            case -1:
                transform.position=Vector3.MoveTowards(transform.position, new Vector3(-5, transform.position.y, transform.position.z), playerSpeed);
                break;

            case -0:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, transform.position.y, transform.position.z), playerSpeed);
                break;

            case 1:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(5, transform.position.y, transform.position.z), playerSpeed);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Hit");
        }
    }

}
