using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemy_finder : MonoBehaviour
{
    public GameObject enemy;
    private GameObject FINDER;
    public Camera Mmaincamera;
    private Vector2 enemySreenPos;
    private SpriteRenderer find_render;
    public bool invisLastframe=false;
    // Start is called before the first frame update
    void Start()
    {
        FINDER = gameObject;
        find_render = FINDER.GetComponent<SpriteRenderer>();
        enemySreenPos = Mmaincamera.WorldToScreenPoint( enemy.transform.position);
        FINDER.transform.position = enemy.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 enemySreenPos = Mmaincamera.WorldToScreenPoint(enemy.transform.position);

        float newy = Mathf.Clamp(enemySreenPos.y, 0, 600);

        float newx = Mathf.Clamp(enemySreenPos.x, 0, 1100);


        Vector3 almostfinalpos= Mmaincamera.ScreenToWorldPoint(new Vector3(newx, newy,0));
        Vector3 finalpos = new Vector3(almostfinalpos.x, almostfinalpos.y, 0);

       if (finalpos == enemy.transform.position )
        {
            FINDER.transform.position = finalpos;

            find_render.enabled = false;
        }
        else
        {
            FINDER.transform.position = finalpos;
            if (enemy.name.Contains("OG"))
            {
                find_render.enabled = false;
                invisLastframe = true;
            }
            else
            {
                if (invisLastframe == true)
                {
                    invisLastframe = false;
                }
                else
                {
                    find_render.enabled = true;

                }

            }

        }


    }
}
