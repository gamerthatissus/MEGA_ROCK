using JetBrains.Annotations;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colide : MonoBehaviour
{
  
    public bool canspike = true;
    public move22 thingyyyy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LAVA"))
        {
            thingyyyy.KILL();

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
                StartCoroutine(waitforspike(0.4f));
            }

        }
    }


}
