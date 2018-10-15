using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmrtIgraca : MonoBehaviour {

    private Rotacija rotacija;
    

    private void Start()
    {
        rotacija = GameObject.FindObjectOfType<Rotacija>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            rotacija.smrt = true;
            Debug.Log("Game Over");
        }
    }
}
