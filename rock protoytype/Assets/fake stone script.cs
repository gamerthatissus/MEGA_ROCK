using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class fakestonescript : MonoBehaviour
{
    public SpriteRenderer HIDEN_until_in_area;
    public SpriteRenderer HIDEN_until_in_area2;
    public SpriteRenderer HIDEN_until_in_area3;
    public Light2D HIDEN_until_in_area4;
    public Light2D HIDEN_until_in_area7;
    public SpriteRenderer HIDEN_until_in_area5;
    public ParticleSystem HIDEN_until_in_area6;
    public move22 moveScript;
    public bool givebadge = false;
    public bool giveBadge2 = false;
    public bool giveSAND_Badge1 = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!moveScript )
        {
            GameObject move = GameObject.Find("move");
            if (move)
                moveScript = move.GetComponent<move22>();
        }

        if (HIDEN_until_in_area != null)
        {
            HIDEN_until_in_area.enabled = false;

        }
        if (HIDEN_until_in_area2 != null)
        {
            HIDEN_until_in_area2.enabled = false;

        }
        if (HIDEN_until_in_area3 != null)
        {
            HIDEN_until_in_area3.enabled = false;

        }
        if (HIDEN_until_in_area4 != null)
        {
            HIDEN_until_in_area4.enabled = true;
            HIDEN_until_in_area4.intensity = 0.05f;
        }
        if (HIDEN_until_in_area7 != null)
        {
            HIDEN_until_in_area7.enabled = true;
            HIDEN_until_in_area7.intensity = 0.05f;
        }
        if (HIDEN_until_in_area5 != null)
        {
            HIDEN_until_in_area5.enabled = false;

        }
        if (HIDEN_until_in_area6 != null)
        {
            HIDEN_until_in_area6.Stop(); 

        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            if (moveScript != null && givebadge==true)
            {
                moveScript.discovedSecret = true;
            }
            if (moveScript != null && giveSAND_Badge1 == true)
            {
                moveScript.discovered_sand_cave = true;
            }
            if (moveScript != null && giveBadge2 == true)
            {
                moveScript.discovedSecret2 = true;
            }
            if (HIDEN_until_in_area != null)
            {
                HIDEN_until_in_area.enabled = true;

            }
            if (HIDEN_until_in_area2 != null)
            {
                HIDEN_until_in_area2.enabled = true;

            }
            if (HIDEN_until_in_area3 != null)
            {
                HIDEN_until_in_area3.enabled = true;

            }
            if (HIDEN_until_in_area4 != null)
            {
                HIDEN_until_in_area4.enabled = true;
                HIDEN_until_in_area4.intensity = 0.5f;
            }
            if (HIDEN_until_in_area7 != null)
            {
                HIDEN_until_in_area7.enabled = true;
                HIDEN_until_in_area7.intensity = 0.5f;
            }
            if (HIDEN_until_in_area5 != null)
            {
                HIDEN_until_in_area5.enabled = true;

            }
            if (HIDEN_until_in_area6 != null)
            {
                HIDEN_until_in_area6.Play();

            }


        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            if (HIDEN_until_in_area != null)
            {
                if (HIDEN_until_in_area.gameObject.name == "hiden stalagtite")
                {

                }
                else
                {
                    HIDEN_until_in_area.enabled = false;

                }

            }
            if (HIDEN_until_in_area2 != null)
            {
                HIDEN_until_in_area2.enabled = false;

            }
            if (HIDEN_until_in_area3 != null)
            {
                HIDEN_until_in_area3.enabled = false;

            }

            if (HIDEN_until_in_area4 != null)
            {
                HIDEN_until_in_area4.enabled = true;
                HIDEN_until_in_area4.intensity = 0.05f;
            }
            if (HIDEN_until_in_area7 != null)
            {
                HIDEN_until_in_area7.enabled = true;
                HIDEN_until_in_area7.intensity = 0.05f;
            }

            if (HIDEN_until_in_area5 != null)
            {
                HIDEN_until_in_area5.enabled = false;

            }
            if (HIDEN_until_in_area6 != null)
            {
                HIDEN_until_in_area6.Stop();

            }
        }
    }
  
        
    

}
