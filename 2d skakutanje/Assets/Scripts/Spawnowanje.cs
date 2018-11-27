using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Spawnowanje : MonoBehaviour {

    public GameObject[] mestaZaStvaranje;

    private float vremeStvaranja;
    public float pocetnoVremeStvaranja;
    public float opadajuceVreme;
    public float minVreme = 0.65f;
    private int brojVisokih = 0;
    private int rand;

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
        if (!rotacija.smrt && !Pomeranje.stScreen && !Advertisement.isShowing)
        {
            if (vremeStvaranja <= 0)
            {
                
                if(brojVisokih == 2)
                {
                    Debug.Log("Ne moze visokoooooooooooooooooooooo");
                    Instantiate(mestaZaStvaranje[1], transform.position, Quaternion.identity);
                    brojVisokih = 0;
                }
                else
                {
                    rand = Random.Range(0, mestaZaStvaranje.Length);
                    Instantiate(mestaZaStvaranje[rand], transform.position, Quaternion.identity);
                }
                vremeStvaranja = pocetnoVremeStvaranja;

                if (rand == 0)
                {
                    brojVisokih++;
                }
                else
                {
                    brojVisokih = 0;
                }
                if (pocetnoVremeStvaranja > minVreme)
                {
                    pocetnoVremeStvaranja -= opadajuceVreme;
                }
                else
                {
                    Debug.Log("Minimalno vreme dostignuto");
                }
            }
            else
            {
                vremeStvaranja -= Time.deltaTime;
            }
        }
	}
}
