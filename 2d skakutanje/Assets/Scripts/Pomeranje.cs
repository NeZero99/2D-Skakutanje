﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;
using TMPro;

public class Pomeranje : MonoBehaviour {

    private Rigidbody2D rb;
    public GameObject efekat;
    public float brzina;
    public float jacinaSkoka;
    [HideInInspector]
    public bool skociti = true;
    public float brzinaPada = 2.5f;
    [HideInInspector]
    public bool skokUnet;

    public TextMeshProUGUI score;
    [HideInInspector]
    public int scoreBrojac;
    public TextMeshProUGUI nhs;

    public Animator animator;
     [HideInInspector]
    public bool smrt;
    private GameObject SmrtEkran;
    private GameObject InGameEkran;
    private bool prozvanaSmrt;

    private AudioSource aus;
    private AudioSource[] brojac;
    private bool pustenAUS = false;

    public GameObject zvezdice;

    [HideInInspector]
    public static bool stScreen = true;
    private bool iskljucenstScreen;

    public GameObject StartSC;
    //private AudioSource audioSSC;

    private AnalyticsEventTracker analitikaScora;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        scoreBrojac = 0;
        score.text = scoreBrojac.ToString();
        nhs.enabled = false;
        skokUnet = false;

        smrt = false;
        SmrtEkran = GameObject.Find("/Canvas/GameOver");
        SmrtEkran.gameObject.SetActive(false);
        InGameEkran = GameObject.Find("/Canvas/InGame");

        prozvanaSmrt = false;

        aus = GetComponent<AudioSource>();
        brojac = GameObject.Find("/Canvas/InGame").GetComponents<AudioSource>();

        analitikaScora = GetComponent<AnalyticsEventTracker>();


        if (stScreen)
        {
            StartSC.SetActive(true);
            //audioSSC = StartSC.GetComponent<AudioSource>();
            InGameEkran.SetActive(false);
            iskljucenstScreen = false;
        }
        else
        {
            StartSC.SetActive(false);
        }

        //Advertisement.Show("banner");
    }
	
	// Update is called once per frame
	void Update () {
        if (smrt)
        {
            rb.bodyType = RigidbodyType2D.Static;

            animator.SetBool("Smrt", true);
            if (!prozvanaSmrt)
            {
                Instantiate(zvezdice, new Vector3(transform.position.x, transform.position.y + 1), transform.rotation);
                //goscr.Play();
                prozvanaSmrt = true;
            }

            SmrtEkran.GetComponent<UI>().UIbrojac = scoreBrojac;
            SmrtEkran.gameObject.SetActive(true);
            InGameEkran.gameObject.SetActive(false);
        }

        if(transform.position.y >= 1.6)
        {
            pustenAUS = false;
        }

        if (Input.touchCount == 1 && skociti && !aus.isPlaying && !pustenAUS && transform.position.x > -5.85)
        {
            aus.Play();
            pustenAUS = true;
            Debug.Log("Zvuk emitovan");
            skokUnet = true;
        }
        if (!stScreen && !iskljucenstScreen)
        {
            //StartSC.SetActive(false);
            Destroy(StartSC, 0.1f);
            InGameEkran.SetActive(true);
            iskljucenstScreen = true;
        }

        animator.SetFloat("yVel", rb.velocity.y);
	}

    private void FixedUpdate()
    {
        if (!smrt && !stScreen && !Advertisement.isShowing)
        {
            //float ver = Input.GetAxis("Vertical");

            if (skokUnet)
            {
                rb.velocity = new Vector2(rb.velocity.x, jacinaSkoka);
                //rb.AddForce((Vector2.up * jacinaSkoka), ForceMode2D.Impulse);
                //rb.AddForce(new Vector2(rb.velocity.x, jacinaSkoka), ForceMode2D.Impulse);
                rb.gravityScale = 0.001f;
                Instantiate(efekat, transform.position, Quaternion.identity);

                skokUnet = false;
            }

            rb.velocity = new Vector2(brzina, rb.velocity.y);
            //rb.AddForce((new Vector2(hor, 0) * brzina));

            if (rb.velocity.y < 0)
            {
                rb.gravityScale = 1;
                rb.velocity += Vector2.up * Physics2D.gravity.y * (brzinaPada - 1) * Time.deltaTime;
            }
            else if (transform.position.y >= 1.6)
            {
                //rb.gravityScale = 20;
                rb.velocity = Vector2.up * 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zemlja"))
        {
            Debug.Log("moze");
            skociti = true;
            animator.SetBool("MogucSkok", skociti);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zemlja"))
        {
            Debug.Log("ne moze");
            skociti = false;
            animator.SetBool("MogucSkok", skociti);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Neprijatelj"))
        {
            if (rb.bodyType == RigidbodyType2D.Dynamic)
            {
                scoreBrojac++;
                analitikaScora.TriggerEvent();
                if (scoreBrojac % 10 == 0)
                {
                    brojac[0].Play();
                }

                if(scoreBrojac > 5)
                {
                    brzina += 0.15f;
                    //brzinaPada += 0.09f;
                    //jacinaSkoka += 0.01f;
                }
            }
            score.text = scoreBrojac.ToString();
            Debug.Log(scoreBrojac.ToString());

            if (scoreBrojac > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", scoreBrojac);
                PlayServices.servisi.dodavanjeSkoraUTabelu("CggIxK7n5R8QAhAB", scoreBrojac);
                PlayServices.servisi.OpenSave(true);

                if (nhs.enabled == false)
                {
                    brojac[1].Play();
                }
                nhs.enabled = true;
            }
        }
    }
}
