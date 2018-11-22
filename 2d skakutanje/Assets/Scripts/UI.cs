using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class UI : MonoBehaviour {

    public Text score;
    public Text inGameNHS;
    public Text nhs;
    public Text reseted;
    public Text trenutniHSprikaz;

    public int UIbrojac;

    private AudioSource[] zvuci;

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

        zvuci = GetComponents<AudioSource>();
    }

    void Update()
    {
        trenutniHSprikaz.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();

        /*Touch dodir = Input.GetTouch(0);

        if (dodir.phase == TouchPhase.Began)
        {
            Again();
        }*/

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
        {
            PlayerPrefs.SetInt("HighScore", 0);
            Debug.Log("Resetovan High Score");
            trenutniHSprikaz.enabled = false;
            reseted.enabled = true;
            Invoke("vracanjeTeksta", 3f);
        }
    }

    void vracanjeTeksta()
    {
        reseted.enabled = false;
        trenutniHSprikaz.enabled = true;
    }

    public void Again()
    {
        /*var opcije = new ShowOptions
        {
            resultCallback = 
        }
        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video", opcije);
        }
        if (opcije.resultCallback == ShowResult.Finished)
        {
            zvuci[1].Play();
            SceneManager.LoadScene("MainScene");
        }*/
        zvuci[1].Play();
        SceneManager.LoadScene("MainScene");
    }

    public void exitApp()
    {
        zvuci[2].Play();
        Application.Quit();
    }

}
