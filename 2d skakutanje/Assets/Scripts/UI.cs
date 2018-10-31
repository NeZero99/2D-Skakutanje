using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Text score;
    public Text inGameNHS;
    public Text nhs;

    public int UIbrojac;


    private void Start()
    {
        score.text = UIbrojac.ToString();
        if (inGameNHS.enabled == true)
        {
            nhs.enabled = true;
        }
    }

    public void Again()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void exitApp()
    {
        Application.Quit();
    }

}
