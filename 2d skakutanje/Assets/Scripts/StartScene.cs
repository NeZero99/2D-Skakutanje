﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour {

    public Text highscore;
    public Text reseted;

    private AudioSource pocetak;

	// Use this for initialization
	void Start () {
        reseted.enabled = false;
        highscore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();

        pocetak = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Nova Scena");
            pocetak.Play();
            Pomeranje.stScreen = false;
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
        {
            PlayerPrefs.SetInt("HighScore", 0);
            Debug.Log("Resetovan High Score");
            highscore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
            reseted.enabled = true;
            Invoke("vracanjeTeksta", 3f);
        }
    }

    void vracanjeTeksta()
    {
        reseted.enabled = false;
    }
}
