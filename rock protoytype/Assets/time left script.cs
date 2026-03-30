using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class timeleftscript : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public GameObject[] colapse;
    public int Timeer=100;
    public bool cannumber;
    public FindObjectsSortMode e;
    public bool colapse1 = false;
    // Start is called before the first frame update
    void Start()
    {
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

            Timeer = 100;
            timer.enabled = true;
            Timeer = 100;

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (cannumber == true && timer.enabled==true)
        {
            timer.text = "" + Timeer;

            cannumber = false;
            Timeer -= 1;
            if (Timeer == 0)
            {
                foreach (GameObject tinghy in colapse)
                {
                    Rigidbody2D ee = tinghy.GetComponent<Rigidbody2D>();
                    ee.simulated = true;
                    timer.enabled = false;
                }
            }
            StartCoroutine(waitsec());
        }

    }
}
