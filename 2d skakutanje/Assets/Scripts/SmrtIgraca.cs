using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmrtIgraca : MonoBehaviour {

    private Rotacija rotacija;
    private Pomeranje pomeranje;
    

    private void Start()
    {
        rotacija = GameObject.FindObjectOfType<Rotacija>();
        pomeranje = GameObject.FindObjectOfType<Pomeranje>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rotacija.smrt = true;
            pomeranje.smrt = true;
            //collision.gameObject.SetActive(false);
            Debug.Log("Game Over");
        }
    }
}
