using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnowanje : MonoBehaviour {

    public GameObject[] mestaZaStvaranje;

    private float vremeStvaranja;
    public float pocetnoVremeStvaranja;
    public float opadajuceVreme;
    public float minVreme = 0.65f;

    private Rotacija rotacija;
    //private Pomeranje pomeranje;

    // Use this for initialization
    void Start () {
        rotacija = GameObject.FindObjectOfType<Rotacija>();
        //pomeranje = GameObject.FindObjectOfType<Pomeranje>();

        //Debug.Log(gameObject.name);
    }
	
	// Update is called once per frame
	void Update () {
        if (!rotacija.smrt && !Pomeranje.stScreen)
        {
            if (vremeStvaranja <= 0)
            {
                int rand = Random.Range(0, mestaZaStvaranje.Length);
                Instantiate(mestaZaStvaranje[rand], transform.position, Quaternion.identity);
                vremeStvaranja = pocetnoVremeStvaranja;
                if (pocetnoVremeStvaranja > minVreme)
                {
                    pocetnoVremeStvaranja -= opadajuceVreme;
                }
            }
            else
            {
                vremeStvaranja -= Time.deltaTime;
            }
        }
	}
}
