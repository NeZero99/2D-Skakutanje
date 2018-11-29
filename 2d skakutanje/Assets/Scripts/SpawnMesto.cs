using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMesto : MonoBehaviour {

    public GameObject zaStvaranje;
    //private Pomeranje pom;

	// Use this for initialization
	void Start () {
        Instantiate(zaStvaranje, transform.position, Quaternion.identity);
        //pom = FindObjectOfType<Pomeranje>();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*Debug.Log("Treba da skoci");
        if (pom.skociti)
        {
            //Debug.Log("Treba da skoci");
            pom.skokUnet = true;
        }*/
    }
}
