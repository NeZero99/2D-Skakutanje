using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Text score;
    public Text inGameNHS;
    public Text nhs;
    public Text reseted;

    public int UIbrojac;


    private void Start()
    {
        score.text = UIbrojac.ToString();
        if (inGameNHS.enabled == true)
        {
            nhs.enabled = true;
        }
        else
        {
            nhs.enabled = false;
        }

        reseted.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Again();
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
        {
            PlayerPrefs.SetInt("HighScore", 0);
            Debug.Log("Resetovan High Score");
            reseted.enabled = true;
            Invoke("vracanjeTeksta", 3f);
        }
    }

    void vracanjeTeksta()
    {
        reseted.enabled = false;
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
