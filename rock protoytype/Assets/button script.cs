using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class buttonscript : MonoBehaviour
{
    public bool pressed = false;
    public GameObject doorITopens;
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    private CircleCollider2D cc;
    private Rigidbody2D rb;


    public bool opens_circle_colider = false;
    public bool opens_Phisics_Part = false;
    public bool Needs_to_be_Continualy_Pressed = true;
    public bool Open_by_defalt = false;
    // Start is called before the first frame update
    void Start()
    {
        if (Open_by_defalt == true)
        {
            sr = doorITopens.GetComponent<SpriteRenderer>();
            sr.enabled = false;
            if (opens_circle_colider == true)
            {
                cc = doorITopens.GetComponent<CircleCollider2D>();
                cc.enabled = false;
            }
            else
            {
                bc = doorITopens.GetComponent<BoxCollider2D>();
                bc.enabled = false;
            }

            if (opens_Phisics_Part == true)
            {
                rb = doorITopens.GetComponent<Rigidbody2D>();
                rb.simulated = false;
            }
        }
        else
        {
            sr = doorITopens.GetComponent<SpriteRenderer>();
            sr.enabled = true;
            if (opens_circle_colider == true)
            {
                cc = doorITopens.GetComponent<CircleCollider2D>();
                cc.enabled = true;
            }
            else
            {
                bc = doorITopens.GetComponent<BoxCollider2D>();
                bc.enabled = true;
            }

            if (opens_Phisics_Part == true)
            {
                rb = doorITopens.GetComponent<Rigidbody2D>();
                rb.simulated = true;
            }
        }
          

    }

    // Update is called once per frame
    void Update()
    {
        if (Open_by_defalt == true)
        {
            if (pressed == true)
            {
                sr.enabled = true;
                if (opens_circle_colider == true)
                {
                    cc.enabled = true;
                }
                else
                {
                    bc.enabled = true;
                }

                if (opens_Phisics_Part == true)
                {
                    rb.simulated = true;
                }
            }
            else
            {
                sr.enabled = false;
                if (opens_circle_colider == true)
                {
                    cc.enabled = false;
                }
                else
                {
                    bc.enabled = false;
                }

                if (opens_Phisics_Part == true)
                {
                    rb.simulated = false;
                }
            }
        }
        else
        {

            if (pressed == true)
            {
                sr.enabled = false;
                if (opens_circle_colider == true)
                {
                    cc.enabled = false;
                }
                else
                {
                    bc.enabled = false;
                }

                if (opens_Phisics_Part == true)
                {
                    rb.simulated = false;
                }
            }
            else
            {
                sr.enabled = true;
                if (opens_circle_colider == true)
                {
                    cc.enabled = true;
                }
                else
                {
                    bc.enabled = true;
                }

                if (opens_Phisics_Part == true)
                {
                    rb.simulated = true;
                }
            }
        }

      
    }
  
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            

        }
        else
        {
            pressed = true;

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Needs_to_be_Continualy_Pressed == true)
        {
            if (collision.gameObject.CompareTag("floor"))
            {


            }
            else
            {
                pressed = true;

            }
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {

        }
        else
        {
            if (Needs_to_be_Continualy_Pressed == true)
            {
                pressed = false;

            }

        }
    }
}
