using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartScene : MonoBehaviour {

    public TextMeshProUGUI highscore;
    public Text reseted;

    private AudioSource pocetak;

    private bool pusteno = false;

	// Use this for initialization
	void Start () {
        reseted.enabled = false;
        highscore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();

        pocetak = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount == 1 && !pocetak.isPlaying && !pusteno)
        {
            Debug.Log("Nova Scena");
            pocetak.Play();
            Pomeranje.stScreen = false;
            pusteno = true;
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
        {
            resetovanjeSkora();
        }
    }

    private void vracanjeTeksta()
    {
        reseted.enabled = false;
    }

    private void resetovanjeSkora()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        Debug.Log("Resetovan High Score");
        highscore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        reseted.enabled = true;
        Invoke("vracanjeTeksta", 3f);
    }
}
