using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class timeleftscript : MonoBehaviour
{
    public ParticleSystem BOOM;
    public TextMeshProUGUI timer;
    public GameObject[] colapse;
    public int Timeer=100;
    public bool cannumber;
    public FindObjectsSortMode e;
    public bool colapse1 = false;
    public move22 mainnnScriiipt;

    public int stage = 0;
    public AudioClip startmusic;
public AudioClip middlemisic;
    public AudioClip endMusic;
    public AudioClip CRASH;
    public AudioSource audio_sorceee;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "FIRST SAND CAVE")
        {
            colapse1 = true;
        }
        else
        {
            colapse1 = false;

        }

        Timeer = 100;
        if (colapse1 == true)
        {
            colapse = GameObject.FindGameObjectsWithTag("colapse1");
            cannumber = false;
            timer.enabled = false;

            foreach (GameObject tinghy in colapse)
            {
                Rigidbody2D ee = tinghy.GetComponent<Rigidbody2D>();
                ee.simulated = false;
            }
        }
        else
        {
            colapse = GameObject.FindGameObjectsWithTag("colapse");
            cannumber = true;
            timer.enabled = false;

            foreach (GameObject tinghy in colapse)
            {
                Rigidbody2D ee = tinghy.GetComponent<Rigidbody2D>();
                ee.simulated = false;
            }
        }
           
    }
    IEnumerator waitsec()
    {
        cannumber = false;
        yield return new WaitForSeconds(1f);
        cannumber = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            cannumber = true;

            AudioSource cam_music = Camera.main.GetComponent<AudioSource>();
            stage = 1;
            cam_music.Stop();
            cam_music.loop = true;
            cam_music.clip = startmusic;
            cam_music.Play();

            Timeer = 100;
            timer.enabled = true;
            Timeer = 100;

        }
    }
    // Update is called once per frame

    public void awardEScapebadge()
    {
        if (timer.enabled==true)
        {
            mainnnScriiipt.beat_sand_cave = true;

        }
    }

    void Update()
    {
        if (cannumber == true && timer.enabled==true)
        {
            timer.text = "" + Timeer;

            cannumber = false;
            Timeer -= 1;

            if (Timeer <= 85 && stage==1)
            {
                stage = 2;

                AudioSource cam_music = Camera.main.GetComponent<AudioSource>();
                cam_music.clip = middlemisic;
                cam_music.Play();

            }

            if (Timeer <= 25 && stage == 2)
            {
                stage = 3;

                AudioSource cam_music = Camera.main.GetComponent<AudioSource>();
                cam_music.clip = endMusic;
                cam_music.Play();

            }


            if (Timeer == 0)
            {
                mainnnScriiipt.discovered_sand_collapse = true;

                foreach (GameObject tinghy in colapse)
                {
                    audio_sorceee.clip = CRASH;
                    audio_sorceee.Play();
                    BOOM.Play();
                    Rigidbody2D ee = tinghy.GetComponent<Rigidbody2D>();
                    ee.simulated = true;
                    timer.enabled = false;
                }
            }
            StartCoroutine(waitsec());
        }

    }
}
