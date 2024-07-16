using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.rotation.x, 180 * Time.deltaTime,transform.rotation.z);
    }
}
