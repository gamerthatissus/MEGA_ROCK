using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class timeleftscript : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public GameObject[] colapse;
    public int Timeer=300;
    public bool cannumber;
    public FindObjectsSortMode e;
    // Start is called before the first frame update
    void Start()
    {
        colapse = GameObject.FindGameObjectsWithTag("colapse");
        cannumber = true;
        timer.enabled = false;
        
        foreach (GameObject tinghy in colapse)
        {
          Rigidbody2D ee=  tinghy.GetComponent<Rigidbody2D>();
            ee.simulated = false;
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
            timer.enabled = true;
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        timer.text = ""+Timeer;
        if (cannumber == true && timer.enabled==true)
        {
            cannumber = false;
            Timeer -= 1;
            if (Timeer == 0)
            {
                foreach (GameObject tinghy in colapse)
                {
                    Rigidbody2D ee = tinghy.GetComponent<Rigidbody2D>();
                    ee.simulated = true;
                }
            }
            StartCoroutine(waitsec());
        }

    }
}
