using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{

    public GameObject  loadingScreen;
    public Image loadBar;
    public TMP_Text highscore;

    // Start is called before the first frame update
    void Start()
    {
        if(highscore != null)
        {
            highscore.text = "HIGHSCORE: " + PlayerPrefs.GetInt("highscore");
        }
        loadingScreen.SetActive(false);

        if (!PlayerPrefs.HasKey("highscore"))
        {
            PlayerPrefs.SetInt("highscore", 0);
        }
    }


    public void LoadGame(string Scene)
    {
        StartCoroutine(LoadSceneAsync(Scene));
    }

    IEnumerator LoadSceneAsync(string Scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(Scene);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            loadBar.fillAmount = Mathf.Clamp01( operation.progress/0.9f);
            yield return null;
        }
        
    }
}
