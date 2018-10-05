using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMesto : MonoBehaviour {

    public GameObject zaStvaranje;

	// Use this for initialization
	void Start () {
        Instantiate(zaStvaranje, transform.position, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
