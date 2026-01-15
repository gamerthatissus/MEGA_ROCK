using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class quicksandscript : MonoBehaviour
{
    public bool sink = false;
    private float OG_PosY;
    public Transform solidTransform;
    private bool cansink=true;
     void Start()
    {
        OG_PosY = solidTransform.position.y;
    }
  
    void FixedUpdate()
    {
        if (sink == true)
        {
            solidTransform.position = new Vector2(solidTransform.position.x, solidTransform.position.y-0.01f);
        }
        else
        {
            if (solidTransform.position.y >= OG_PosY)
            {
                solidTransform.position = new Vector2(solidTransform.position.x, OG_PosY);
            }
            else
            {
                solidTransform.position = new Vector2(solidTransform.position.x, solidTransform.position.y + 0.01f);
            }
        }
    }

    IEnumerator waitforsink()
    {
        cansink = false;

        yield return new WaitForSeconds(0.1f);
        cansink = true;


    }
    IEnumerator waitSINK()
    {

        yield return new WaitForSeconds(0.25f);
        sink = false;


    }
    //StartCoroutine(spendstone(2));

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == ("player") && cansink == true)
        {
            StartCoroutine(waitforsink());
            sink = true;

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == ("player") && cansink == true)
        {
            StartCoroutine(waitforsink());
            sink = true;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == ("player") && cansink==true)
        {
            StartCoroutine(waitforsink());
            sink = false;

        }
        else
        {
            if (collision.gameObject.name == ("player"))
            {
                StartCoroutine(waitSINK());

            }
        }
    }
}
