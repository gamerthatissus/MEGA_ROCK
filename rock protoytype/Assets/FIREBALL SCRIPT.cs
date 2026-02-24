using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIREBALLSCRIPT : MonoBehaviour
{
    public boss_FIGHT_SCRIPT eee;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("floor") && name== "FIREBALL")
        {
            eee.attacking = false;
            Destroy(gameObject, 0.5f);
        }
    }

}
