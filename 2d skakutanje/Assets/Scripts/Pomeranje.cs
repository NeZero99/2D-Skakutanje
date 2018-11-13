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
    private bool skokUnet;

    public Text score;
    private int scoreBrojac;
    public Text nhs;

    public Animator animator;
     [HideInInspector]
    public bool smrt;
    private GameObject SmrtEkran;
    private GameObject InGameEkran;
    private bool prozvanaSmrt;

    private AudioSource aus;
    private AudioSource brojac;

    public GameObject zvezdice;

    [HideInInspector]
    public static bool stScreen = true;
    private bool iskljucenstScreen;

    public GameObject StartSC;
    private AudioSource audioSSC;

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
        brojac = GameObject.Find("/Canvas/InGame").GetComponent<AudioSource>();

        
        if(stScreen)
        {
            StartSC.SetActive(true);
            audioSSC = StartSC.GetComponent<AudioSource>();
            InGameEkran.SetActive(false);
            iskljucenstScreen = false;
        }
        else
        {
            StartSC.SetActive(false);
        }
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

        if (Input.GetButton("Jump") && skociti && !aus.isPlaying && transform.position.x > -5.85)
        {
            aus.Play();
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
	}

    private void FixedUpdate()
    {
        if (!smrt && !stScreen)
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
                rb.gravityScale = 20;
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
                brojac.Play();
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
