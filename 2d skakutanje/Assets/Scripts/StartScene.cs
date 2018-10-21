using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour {

    public Text highscore;

	// Use this for initialization
	void Start () {
        highscore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Nova Scena");
            SceneManager.LoadScene("MainScene");
        }
	}
}
