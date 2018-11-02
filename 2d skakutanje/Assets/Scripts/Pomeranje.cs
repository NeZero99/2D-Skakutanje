﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pomeranje : MonoBehaviour {

    private Rigidbody2D rb;
    public GameObject efekat;
    public int brzina;
    public int jacinaSkoka;
    private bool skociti = true;
    public float brzinaPada = 2.5f;

    public Text score;
    private int scoreBrojac;
    public Text nhs;

    public Animator animator;

    public bool smrt;
    private GameObject SmrtEkran;
    private GameObject InGameEkran;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        scoreBrojac = 0;
        score.text = scoreBrojac.ToString();
        nhs.enabled = false;

        smrt = false;
        SmrtEkran = GameObject.Find("/Canvas/GameOver");
        SmrtEkran.gameObject.SetActive(false);
        InGameEkran = GameObject.Find("/Canvas/InGame");
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void FixedUpdate()
    {
        if (!smrt)
        {
            //float ver = Input.GetAxis("Vertical");

            if (Input.GetButton("Jump") && skociti)
            {
                rb.velocity = new Vector2(rb.velocity.x, jacinaSkoka);
                //rb.AddForce((Vector2.up * jacinaSkoka), ForceMode2D.Impulse);
                //rb.AddForce(new Vector2(rb.velocity.x, jacinaSkoka), ForceMode2D.Impulse);
                rb.gravityScale = 0.001f;
                Instantiate(efekat, transform.position, Quaternion.identity);
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
                rb.gravityScale = 20;
            }
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Static;

            animator.SetBool("Smrt", true);

            SmrtEkran.GetComponent<UI>().UIbrojac = scoreBrojac;
            SmrtEkran.gameObject.SetActive(true);
            InGameEkran.gameObject.SetActive(false);
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
            }
            score.text = scoreBrojac.ToString();
            Debug.Log(scoreBrojac.ToString());

            if (scoreBrojac > PlayerPrefs.GetInt("HighScore"))//dodati restart hs-a
            {
                PlayerPrefs.SetInt("HighScore", scoreBrojac);
                nhs.enabled = true;
            }
        }
    }
}
