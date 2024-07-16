using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class satanScript : MonoBehaviour
{
    private bool entered = false;
    [SerializeField] float rotateSpeed = 180f;
    [SerializeField] float downSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (entered)
        {
            transform.Rotate(0,rotateSpeed * Time.deltaTime, 0);
            transform.position += Vector3.down * downSpeed * Time.deltaTime ;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("eNTERED");
            entered = true;
        }
    }
}
