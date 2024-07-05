using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManage : MonoBehaviour
{
    public float laneSpeed = 0.33f;
    public float playerSpeed = 17f;
    public float jumpSpeed = 6;
    public float rollSpeed = 6;
    public Rigidbody rb;
    public int lane = 0;
    private bool isGrounded = true;
    public Animator animator;
    private CapsuleCollider col;
    private float rollTimer = 0;
    public bool isAlive;
    private RaycastHit hit;
    public float downForce = 1;
    public GameObject logic;


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
        logic.GetComponent<LogicManagerScript>().coinIncrease();

        transform.Translate(Vector3.forward * 4 *Time.deltaTime, Space.Self);
   
        if (Input.GetKeyDown(KeyCode.A) && lane > -1 && isAlive)
        {
            lane--;
        }

        if (Input.GetKeyDown(KeyCode.D)  && lane < 1 && isAlive)
        {
            lane++;             
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isGrounded && isAlive)
        {
            rb.AddForce(Vector3.up * jumpSpeed,ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.S) && !animator.GetBool("isRolling") && isAlive)
        {
            rb.AddForce( Vector3.down * jumpSpeed, ForceMode.Impulse);
            animator.SetBool("isRolling", true);
            rollTimer = 1.150f;
            col.height = 0.9f;
            col.center = new Vector3(col.center.x,0.5f,col.center.z);
        }


        if(rollTimer > 0)
        {
            rollTimer -= Time.deltaTime;
            if(rollTimer < 0)
            {
                col.height = 1.651325f;
                col.center = new Vector3(col.center.x, 0.8464648f, col.center.z);
                animator.SetBool("isRolling",false);

            }
        }



    }

    private void FixedUpdate()
    {
            
        rb.AddForce((Vector3.ProjectOnPlane(transform.forward,hit.normal).normalized * playerSpeed) - rb.velocity);     
        changeLane();
        isGrounded = Physics.Raycast(transform.position + Vector3.up , Vector3.down,out hit, 1.2f);
        animator.SetBool("IsJumping", !isGrounded);

        if(rb.velocity.y < 0 && !isGrounded)
        {
            rb.AddForce(Vector3.down * downForce);
        }


    }

    private void changeLane() {
        switch (lane)
        {
            case -1:
                transform.position=Vector3.MoveTowards(transform.position, new Vector3(-5, transform.position.y, transform.position.z), laneSpeed);
                break;

            case -0:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, transform.position.y, transform.position.z), laneSpeed);
                break;

            case 1:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(5, transform.position.y, transform.position.z), laneSpeed);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            isAlive = false;
            animator.SetBool("isAlive", false);
        }
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if(trigger.gameObject.tag == "Coin")
        {
            logic.GetComponent<LogicManagerScript>().coinIncrease();
        }
    }

}
