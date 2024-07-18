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
    public TMP_Text abiltyText;
    private bool isDeathCalled = false;
    public GameObject bgm;
    public GameObject kenSphere;
    public GameObject pausePanel;
    public GameObject deathPanel;
    public float waitSec = 4;
    public TMP_Text deathScore;


    

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(0, 6, false);
        meter.fillAmount = meterFill;
        abiltyText.enabled = false;
        pausePanel.SetActive(false);
        deathPanel.SetActive(false);


    }

    // Update is called once per frame

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && player.GetComponent<PlayerManage>().isAlive)
        {
            Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
            pausePanel.SetActive(!pausePanel.activeSelf);
        }

        if ( !player.GetComponent<PlayerManage>().isAlive && !isDeathCalled)
        {

            OnDeath();
        }

        if (meter.fillAmount == 1)
        {
            abiltyText.enabled = true;
        }
        if (meter.fillAmount == 1 && Input.GetKeyDown(KeyCode.E) && !isUsingKen)
        {
            isUsingKen = true;
            kenSphere.SetActive(true);
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
            kenSphere.SetActive(false);
            abiltyText.enabled = false;
        }
    }

    void OnDeath()
    {
        isDeathCalled = true;
        GetComponent<AudioSource>().Play();
        bgm.GetComponent<AudioSource>().Stop();
        if(PlayerPrefs.GetInt("highscore") < score)
        {
            PlayerPrefs.SetInt("highscore", (int) score);
        }
        StartCoroutine(enableDeathscreenAfterSec());
        

    }

    IEnumerator enableDeathscreenAfterSec()
    {
        yield return new WaitForSeconds(waitSec);
        deathPanel.SetActive(true);
        deathScore.text = "Score: " + (int)score;
    }

}
