using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UI;
using TMPro;

public class LogicManagerScript : MonoBehaviour
{
    public GameObject player;
    private int coinCollected = 0;
    public float scoreMultiplier = 1.02f;
    private float score = 0;
    public TMP_Text scoreText;
    public TMP_Text coinText;
    public Image meter;
    [SerializeField] bool isUsingKen = false;
    [SerializeField] float abilityTimer = 0;
    [SerializeField] float kenTime = 10;
    [SerializeField] float meterFill = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(0, 6, false);
        meter.fillAmount = meterFill;


    }

    // Update is called once per frame

    private void Update()
    {

        if(meter.fillAmount == 1)
        {
            Debug.Log("Can use ken");
        }
        if (meter.fillAmount == 1 && Input.GetKeyDown(KeyCode.E) && !isUsingKen)
        {
            isUsingKen = true;
            Physics.IgnoreLayerCollision(0, 6, true);
            abilityTimer = kenTime;
        }

        if (isUsingKen)
        {
            kenTimer();
        }
    }

    void FixedUpdate()
    {
        if (player.GetComponent<PlayerManage>().isAlive)
        {
            score += (player.GetComponent<Rigidbody>().velocity.z/10) * scoreMultiplier;
            scoreText.text = math.trunc(score).ToString();
        }


    }

    public void coinIncrease()
    {
        coinCollected += 1;
        meterFill += 1;
        coinText.text = coinCollected.ToString();
        meter.fillAmount = meterFill/100f;
    }

    private void kenTimer()
    {
        abilityTimer -= Time.deltaTime;
        meter.fillAmount = abilityTimer/10;

        if(abilityTimer < 0)
        {
            Physics.IgnoreLayerCollision(0, 6, false);
            meter.fillAmount = 0;
            meterFill = 0;
            isUsingKen = false;
        }
    }

}
