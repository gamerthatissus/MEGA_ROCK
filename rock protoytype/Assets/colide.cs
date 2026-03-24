using JetBrains.Annotations;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colide : MonoBehaviour
{
  
    public bool canspike = true;
    public move22 thingyyyy;
    public bool cansand = true;
    private void Start()
    {
             canspike = true;

}

    IEnumerator waitBREAK(GameObject colliidderrrr)
    {
        yield return new WaitForSeconds(2.5f);
        colliidderrrr.SetActive(false);
        yield return new WaitForSeconds(5f);
        colliidderrrr.SetActive(true);

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("colapse"))
        {
            if (cansand == true)
            {
                cansand = false;
                thingyyyy.MILD_OW();
                StartCoroutine(waitforSAND(0.2f));
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "CRACKED_SAND")
        {
            StartCoroutine(waitBREAK(collision.gameObject));
        }

        if (collision.gameObject.CompareTag("TNT"))
        {
            thingyyyy.TnT ++;
            Destroy(collision.gameObject);
            
        }
        if (collision.gameObject.CompareTag("colapse"))
        {
            if (cansand == true)
            {
               cansand = false;
              thingyyyy.MILD_OW();
               StartCoroutine(waitforSAND(0.2f));
            }

        }
        if (collision.gameObject.CompareTag("SPIKE"))
        {
            if (canspike == true)
            {
                Rigidbody2D ecolide = collision.gameObject.GetComponent<Rigidbody2D>();

                thingyyyy.spike(collision.relativeVelocity.magnitude);
                canspike = false;
                StartCoroutine(waitforspike(0.1f));

            }

        }
       
        


    }

    IEnumerator waitforspike(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        canspike = true;


    }

    IEnumerator waitforSAND(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        cansand = true;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LAVA"))
        {
            thingyyyy.KILL();

        }
        if (collision.gameObject.CompareTag("ROCK_PILE"))
        {
            thingyyyy.stone = thingyyyy.stone_MAX;

        }
        if (collision.gameObject.CompareTag("HOLE"))
        {
            thingyyyy.HOLE();

        }
        if (collision.gameObject.CompareTag("dmg"))
        {
            if (canspike == true)
            {
                canspike = false;
                thingyyyy.dmg();
                StartCoroutine(waitforspike(0.1f));
            }

        }
        
    }


}
