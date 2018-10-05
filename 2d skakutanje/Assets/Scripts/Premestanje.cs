using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Premestanje : MonoBehaviour {

    public GameObject premestiti;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            premestiti.transform.position += new Vector3(60, 0);
        }
    }
}
