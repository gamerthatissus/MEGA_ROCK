using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class quicksandscript : MonoBehaviour
{
    public bool sink = false;
    private float OG_PosY;
    private float OG_PosY_NoNSOLID;

    public Transform solidTransform;
    public Transform Non_solidTransform;
    private bool cansink=true;
     void Start()
    {
        OG_PosY_NoNSOLID = Non_solidTransform.position.y;
        OG_PosY = solidTransform.position.y;
    }
  
    void FixedUpdate()
    {
        if (sink == true)
        {
          Non_solidTransform.position = new Vector2(Non_solidTransform.position.x, Non_solidTransform.position.y - 0.01f);
            solidTransform.position = new Vector2(solidTransform.position.x, solidTransform.position.y-0.01f);
        }
        else
        {
            if (solidTransform.position.y >= OG_PosY)
            {
                Non_solidTransform.position = new Vector2(Non_solidTransform.position.x, OG_PosY_NoNSOLID);

                solidTransform.position = new Vector2(solidTransform.position.x, OG_PosY);
            }
            else
            {
                Non_solidTransform.position = new Vector2(Non_solidTransform.position.x, Non_solidTransform.position.y + 0.01f);

                solidTransform.position = new Vector2(solidTransform.position.x, solidTransform.position.y + 0.01f);
            }
        }
    }

    //IEnumerator waitforsink()
    //{
      //  cansink = false;

        //yield return new WaitForSeconds(0.1f);
        //cansink = true;


//    }
   // IEnumerator waitSINK()
   // {

        //yield return new WaitForSeconds(0.25f);
       // sink = false;


    //}
    //StartCoroutine(spendstone(2));

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == ("player") && cansink == true)
        {
            sink = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == ("player") && cansink == true)
        {
            sink = false;

        }
       
    }

    

   
}
