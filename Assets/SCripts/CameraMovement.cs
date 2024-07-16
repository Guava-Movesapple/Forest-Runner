using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject playerObject;
    private Vector3 position;
    public float laneChangeSpeed = 0.5f;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        position = transform.position;
        position.z = playerObject.transform.position.z - 6.10619f;
        transform.position = position;




    }


    private void FixedUpdate()
    {
        if (playerObject.transform.position.y > 0 && playerObject.GetComponent<PlayerManage>().isGrounded)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 9, transform.position.z), 0.06f);
        }
        else if (playerObject.transform.position.y < 3.85f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 6, transform.position.z), 0.6f);
        }

        if (playerObject.GetComponent<PlayerManage>().isAlive)
        {
            changeLane();
        }
    }

    private void changeLane()
    {
        switch (playerObject.GetComponent<PlayerManage>().lane)
        {
            case -1:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(-3.3f, transform.position.y, transform.position.z), laneChangeSpeed);
                break;

            case 0:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, transform.position.y, transform.position.z), laneChangeSpeed);
                break;

            case 1:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(3.3f, transform.position.y, transform.position.z), laneChangeSpeed);
                break;
        }
    }
}
