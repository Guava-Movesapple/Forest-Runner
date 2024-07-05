using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UI;

public class LogicManagerScript : MonoBehaviour
{
    public GameObject player;
    private int coinCollected = 0;
    public float scoreMultiplier = 1.02f;
    private float score = 0;
    public Text scoreText;
    public Text coinText;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerManage>().isAlive)
        {
            score += Time.deltaTime * scoreMultiplier;
        }

       // scoreText.text = "Run: " + math.trunc(score);
    }

    public void coinIncrease()
    {
        coinCollected += 1;
       // coinText.text = coinCollected.ToString();
    }

}
