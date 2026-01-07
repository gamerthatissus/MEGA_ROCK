using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class launchscript : MonoBehaviour
{
    public int insidefloor = 0;
    public SpriteRenderer spriteRend;
    public BoxCollider2D spriteBox;
    public move22 thingyyyy;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRend.enabled = false;
        spriteBox.enabled = false;
     insidefloor = 0;
        StartCoroutine(pashence());

    }

  

    IEnumerator pashence()
    {

        yield return new WaitForSeconds(0.02f);
       
        if (insidefloor == 0)
        {
            if (gameObject.name == "launch")
            {
                spriteRend.enabled = true;
                spriteBox.enabled = true;

            }
            else
            {
                thingyyyy.refundStone();
                Destroy(gameObject);

            }

        }
        else
        {
            spriteRend.enabled = true;
            spriteBox.enabled = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        
           
 if (collision.gameObject.CompareTag("floor"))
        {
            insidefloor += 1;

        }
        else
        {


        }


    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("floor"))
        {
            if (insidefloor == 0)
            {
                insidefloor = 1;

            }

        }
        else
        {


        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            insidefloor -= 1;

        }
        else
        {


        }
    }
}
