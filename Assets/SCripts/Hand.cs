using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] float multiplier;
    [SerializeField] float dist = 6;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3((Mathf.PingPong(Time.time, 1) * multiplier)-dist,transform.position.y,transform.position.z);
    }
}
