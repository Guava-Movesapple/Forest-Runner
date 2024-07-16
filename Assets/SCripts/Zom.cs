using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zom : MonoBehaviour
{
    private Animator zom;
    private bool entered = false;

    // Start is called before the first frame update
    void Start()
    {
        zom = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (entered && Input.anyKeyDown)
        {
            zom.SetBool("isAlive", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            entered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            entered = false;
        }
    }
}
