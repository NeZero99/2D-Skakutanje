using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrisanjeNeprijatelja : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Neprijatelj") || collision.gameObject.CompareTag("Efekti"))
        {
            Destroy(collision.gameObject);
        }
    }
}
