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


            rotacija.smrt = true;
            //collision.gameObject.SetActive(false);
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>;
            rb.isKinematic();
            Debug.Log("Game Over");
        }
    }
}
