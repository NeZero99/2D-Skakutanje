using System.Collections;
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

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        scoreBrojac = 0;
        score.text = scoreBrojac.ToString();
        nhs.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
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

        if(rb.velocity.y < 0)
        {
            rb.gravityScale = 1;
            rb.velocity += Vector2.up * Physics2D.gravity.y * (brzinaPada - 1) * Time.deltaTime;
        }
        else if(transform.position.y >= 1.6)
        {
            rb.gravityScale = 20;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zemlja"))
        {
            Debug.Log("moze");
            skociti = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zemlja"))
        {
            Debug.Log("ne moze");
            skociti = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Neprijatelj"))
        {
            scoreBrojac++;
            score.text = scoreBrojac.ToString();
            Debug.Log(scoreBrojac.ToString());

            if(scoreBrojac > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", scoreBrojac);
                nhs.enabled = true;
            }
        }
    }
}
