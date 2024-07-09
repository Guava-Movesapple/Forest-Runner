using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject soul;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,180*Time.deltaTime,0);
       
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<AudioSource>().Play();
        Destroy(soul);
    }
}
