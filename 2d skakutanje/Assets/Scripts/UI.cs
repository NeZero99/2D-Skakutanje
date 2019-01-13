using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;
using TMPro;

public class UI : MonoBehaviour {

    public TextMeshProUGUI score;
    public TextMeshProUGUI inGameNHS;
    public TextMeshProUGUI nhs;
    public Text reseted;
    public Text trenutniHSprikaz;
    [HideInInspector]
    public int UIbrojac;

    private AudioSource[] zvuci;

    static int brPlayAg = 0;

    private AnalyticsEventTracker analitikaHighScora;

    private void Start()
    {
        analitikaHighScora = GetComponent<AnalyticsEventTracker>();
        score.text = UIbrojac.ToString();
        if (inGameNHS.enabled == true)
        {
            nhs.enabled = true;
            analitikaHighScora.TriggerEvent();

            PlayServices.servisi.dodavanjeSkoraUTabelu("CggIxK7n5R8QAhAB", UIbrojac);
        }
        else
        {
            nhs.enabled = false;
        }

        reseted.enabled = false;

        zvuci = GetComponents<AudioSource>();

        brPlayAg++;
    }

    void Update()
    {
        trenutniHSprikaz.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();

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
        Debug.Log("broj replaya: " + brPlayAg.ToString() + " ostatak: " + (brPlayAg % 3).ToString());
        if (brPlayAg % 3 == 0)// ne radii ovaj deo, treba da prikazuje reklamuu svaki treci put
        {
            if (Advertisement.IsReady("video"))
            {
                Advertisement.Show("video");
                //brPlayAg++;
            }
        }
        zvuci[1].Play();
        SceneManager.LoadScene("MainScene");
    }

    public void exitApp()
    {
        zvuci[2].Play();
        Application.Quit();
    }

}
