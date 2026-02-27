using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIREBALLSCRIPT : MonoBehaviour
{
    public AudioSource sound;
    public AudioClip hitfloor;
    public bool MegaFireball = false;
    public boss_FIGHT_SCRIPT eee;
    public LayerMask player;
    public move22 main;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        IEnumerator explode(UnityEngine.Transform EXPLOSHION, SpriteRenderer spriteEXPLOSHION)
        {
            yield return new WaitForSeconds(0.1f);
            Rigidbody2D fireRB=gameObject.GetComponent<Rigidbody2D>(); ;
            Collider2D[] kaboom = Physics2D.OverlapCircleAll(fireRB.position, 4f, player);
            foreach (Collider2D thingy in kaboom)
            {
                if (thingy.gameObject.name == "player")
                {
                    main.hp -= 5;

                    Rigidbody2D playerRB = thingy.gameObject.GetComponent<Rigidbody2D>();
                    if (playerRB.position.x > fireRB.position.x)
                    {
                        playerRB.velocity = new Vector2(fireRB.velocity.x + 8, playerRB.velocity.y + 3);
                    }
                    else
                    {
                        playerRB.velocity = new Vector2(playerRB.velocity.x - 8, playerRB.velocity.y + 3);

                    }
                }
            }
            for (float grow = 0; grow < 3.5f; grow += 0.1f)
            {
                EXPLOSHION.localScale = new Vector2(0.1f + grow, 0.1f + grow);
                spriteEXPLOSHION.color = new Color(200, 0, 0, 0.8f - (grow / 5));

                yield return null;
            }
            Destroy(EXPLOSHION.gameObject);
        }
        
        if (collision.gameObject.CompareTag("floor") && name== "FIREBALL")
        {
            if (MegaFireball == false)
            {
                sound.loop = false;
                sound.Stop();
                sound.clip = hitfloor;
                sound.Play();
                eee.attacking = false;
                Destroy(gameObject, 0.4f);
            }
            else
            {
                sound.loop = false;
                sound.Stop();
                sound.clip = hitfloor;
                sound.Play();
                eee.attacking = false;
                GameObject OBJECT_exploshion = gameObject.transform.Find("EXPLOSHION").gameObject;
                UnityEngine.Transform EXPLOSHION = OBJECT_exploshion.GetComponent<UnityEngine.Transform>();
                SpriteRenderer spriteEXPLOSHION = OBJECT_exploshion.GetComponent<SpriteRenderer>();
                spriteEXPLOSHION.enabled = true;
                Destroy(gameObject, 0.3f);
                StartCoroutine(explode(EXPLOSHION, spriteEXPLOSHION));
                
            }
                
        }
        
        
    }

}
