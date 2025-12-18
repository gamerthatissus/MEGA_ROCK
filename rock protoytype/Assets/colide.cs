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
                thingyyyy.spike();
                canspike = false;
                StartCoroutine(waitforspike());

            }

        }

      
    }

    IEnumerator waitforspike()
    {

        yield return new WaitForSeconds(0.1f);
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
    }


}
