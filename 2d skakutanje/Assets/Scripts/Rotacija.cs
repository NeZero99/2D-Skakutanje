using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacija : MonoBehaviour {

    public float brzinaRotacije = 0.5f;
    public GameObject zaPracenje;
    private float razlika;
    public bool smrt = false;

    private bool dostigao;

    // Use this for initialization
    void Start () {
        brzinaRotacije = 0.1f;

        dostigao = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!smrt)
        {
            Vector2 offset = new Vector2(Time.time * brzinaRotacije, 0);

            GetComponent<Renderer>().material.mainTextureOffset = offset;

            //Debug.Break();
        }

        if (zaPracenje.transform.position.x > -5.85 && !dostigao)
        {
            razlika = transform.position.x - zaPracenje.transform.position.x;
            dostigao = true;
        }
    }

    private void LateUpdate()
    {
        if (dostigao)
        {
            transform.position = new Vector3((razlika + zaPracenje.transform.position.x), 2, 13);
        }
    }

    public void GasenjeRotacije()
    {
        brzinaRotacije = 0.0f;
        Vector2 offset = new Vector2(Time.time * brzinaRotacije, 0);

        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
}
