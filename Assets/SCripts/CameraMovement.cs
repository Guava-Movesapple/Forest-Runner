using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject playerObject;
    private Vector3 position;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        position = transform.position;
        position.z = playerObject.transform.position.z - 13f;
        transform.position = position;
        changeLane();
    }

    private void changeLane()
    {
        switch (playerObject.GetComponent<PlayerManage>().lane)
        {
            case -1:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(-3.3f, transform.position.y, transform.position.z), 0.5f);
                break;

            case 0:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, transform.position.y, transform.position.z), 0.5f);
                break;

            case 1:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(3.3f, transform.position.y, transform.position.z), 0.5f);
                break;
        }
    }
}
