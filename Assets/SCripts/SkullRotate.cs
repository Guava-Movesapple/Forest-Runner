using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullRotate : MonoBehaviour
{

    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.rotation.x, speed * Time.deltaTime,transform.rotation.z);
    }
}
