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
    public bool isGrounded = true;
    public Animator animator;
    private CapsuleCollider col;
    private float rollTimer = 0;
    public bool isAlive;
    private RaycastHit hit;
    public float downForce = 1;
    public GameObject logic;
    private AudioSource coinCollectFX;
    private bool called = false;
    public bool isSneaking = false;
    public float deathForce;


    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        rb = GetComponent<Rigidbody>(); 
        animator = GetComponent<Animator>(); 
        col = GetComponent<CapsuleCollider>();
        coinCollectFX = GetComponent<AudioSource>();
        called = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            controls();
        }

        if(isSneaking && Input.anyKeyDown)
        {
            isAlive = false;
            animator.SetBool("isAlive",false);


        }
   


    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, (Vector3.ProjectOnPlane(transform.forward, hit.normal).normalized.z * playerSpeed));
            changeLane();
        }
        isGrounded = Physics.Raycast(transform.position + Vector3.up , Vector3.down,out hit, 1.3f);
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
            rb.AddForce(Vector3.back * deathForce, ForceMode.Impulse);
            isAlive = false;
            animator.SetBool("isAlive", false);
        }
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if(trigger.gameObject.tag == "Coin")
        {
            coinCollectFX.Play();
            logic.GetComponent<LogicManagerScript>().coinIncrease();
        }
        else if (trigger.gameObject.tag == "Enter")
        {
            animator.SetBool("isSneaking", true);
            isSneaking = true;
            playerSpeed = 8;
        }
        else if (trigger.gameObject.tag == "Exit")
        {
            animator.SetBool("isSneaking", false);
            isSneaking = false;
            playerSpeed = 18;
        }
    }

    private void controls()
    {
        if (Input.GetKeyDown(KeyCode.A) && lane > -1)
        {
            lane--;
        }

        if (Input.GetKeyDown(KeyCode.D) && lane < 1 )
        {
            lane++;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            animator.SetBool("isRolling", false);
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            called = true;
        }

        if (Input.GetKeyDown(KeyCode.S) && !animator.GetBool("isRolling"))
        {

            if (!isGrounded)
            {
                rb.AddForce(Vector3.down * jumpSpeed, ForceMode.Impulse);
            }
            animator.SetBool("isRolling", true);
            rollTimer = 1.150f;
            col.height = 0.9f;
            col.center = new Vector3(col.center.x, 0.5f, col.center.z);
        }



        if (rollTimer > 0)
        {
            rollTimer -= Time.deltaTime;
            if (rollTimer < 0 || animator.GetBool("IsJumping") || !isAlive)
            {
                col.height = 1.651325f;
                col.center = new Vector3(col.center.x, 0.8464648f, col.center.z);
                animator.SetBool("isRolling", false);

            }
        }

        if (transform.position.y > 3f && called)
        {
            called = false;
            rb.velocity = new Vector3(rb.velocity.x, 2, rb.velocity.z);
        }

    }

}
