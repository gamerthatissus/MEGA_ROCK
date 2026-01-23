using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fakestonescript : MonoBehaviour
{
    public SpriteRenderer HIDEN_until_in_area;
    public SpriteRenderer HIDEN_until_in_area2;
    public SpriteRenderer HIDEN_until_in_area3;

    // Start is called before the first frame update
    void Start()
    {
        HIDEN_until_in_area.enabled = false;
        HIDEN_until_in_area2.enabled = false;
        HIDEN_until_in_area3.enabled = false;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            HIDEN_until_in_area.enabled = true;
            HIDEN_until_in_area2.enabled = true;
            HIDEN_until_in_area3.enabled = true;



        }
    }

}
