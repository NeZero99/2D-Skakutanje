using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour {

    public GameObject zaPracenje;
    private Vector3 razlika;

    private bool dostigao;

    //public float horizontalResolution = 1920;

    // Use this for initialization
    void Start () {
        dostigao = false;
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = new Vector3(zaPracenje.transform.position.x, 1, 1);

        if (zaPracenje.transform.position.x > -5.85)
        {
            if (!dostigao)
            {
                razlika = transform.position - zaPracenje.transform.position;
            }
            dostigao = true;
        }
    }

    private void LateUpdate()
    {
        if (dostigao)
        {
            transform.position = new Vector3((razlika.x + zaPracenje.transform.position.x), 0, (razlika.z + zaPracenje.transform.position.z));
        }
    }
    /* kao ono za optimizaciju kamere kada se menja rezolucija
    private void OnGUI()
    {
        float currentAspect = (float)Screen.width / (float)Screen.height;
        Camera.main.orthographicSize = horizontalResolution / currentAspect / 200;
    }*/
}
