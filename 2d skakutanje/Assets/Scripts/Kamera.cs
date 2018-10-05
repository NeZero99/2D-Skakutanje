using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour {

    public GameObject zaPracenje;
    private Vector3 razlika;

	// Use this for initialization
	void Start () {
        razlika = transform.position - zaPracenje.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = new Vector3(zaPracenje.transform.position.x, 1, 1);
	}

    private void LateUpdate()
    {
        //transform.position = razlika + zaPracenje.transform.position;
        transform.position = new Vector3((razlika.x + zaPracenje.transform.position.x), 0, (razlika.z + zaPracenje.transform.position.z));
    }
}
