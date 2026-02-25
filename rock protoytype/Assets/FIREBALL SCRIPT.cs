using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIREBALLSCRIPT : MonoBehaviour
{
    public AudioSource sound;
    public AudioClip hitfloor;

    public boss_FIGHT_SCRIPT eee;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("floor") && name== "FIREBALL")
        {
            sound.loop = false;
            sound.Stop();
            sound.clip = hitfloor;
            sound.Play();
            eee.attacking = false;
            Destroy(gameObject, 0.5f);
        }
    }

}
