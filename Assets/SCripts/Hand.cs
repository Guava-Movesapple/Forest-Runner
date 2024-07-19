using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] float multiplier;
    [SerializeField] float dist = 6;
    [SerializeField] int rev = 1;
    [SerializeField] float speed ;


    private void Start()
    {
        speed = Random.Range(0.5f, 1.6f);
    }

    void Update()
    {
        transform.position = new Vector3(((Mathf.PingPong(Time.time * speed, 1) * multiplier)-dist) * rev,transform.position.y,transform.position.z);
    }
}
