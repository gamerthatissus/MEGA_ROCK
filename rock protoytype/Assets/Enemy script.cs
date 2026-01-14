using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class Enemyscript : MonoBehaviour
{
    //enemy types:   1=fall  2=normal 3=burried    4=smasher
    public int enemyType=0;

    private bool awakened = false;
    public float HP = 100;
    public float maxHp = 100;
    private int canfall=0;
    public Rigidbody2D Player_RB;
    public Rigidbody2D Enemy_RB;
    public AudioClip enter;
    public AudioClip attack;
    public AudioClip die;
    private bool ded = false;
    public PointEffector2D enemy_knockback;
    public BoxCollider2D enemy_box;
    public move22 move;

    public int state = 0;
    public int go = 0;
    void Start()
    {

        HP = maxHp;

        

        enemy_box.enabled = true;
        if (enemyType==0)
        {
            //sets enemy type to normal if no enemy type is sellected
            enemyType = 2;
        }

        if (enemyType == 1)
        {
            enemy_knockback.enabled = false;
            Enemy_RB.simulated = false;
        }


    }
  
    

    IEnumerator waittick()
    {
        go = 3;
        yield return new WaitForSeconds(2);
          if (state==1)
        {
            state = 0;
            go = 1;

        }
        else if (state==0)
        {
            state = 1;
            go = 1;
        }
    }
    IEnumerator waitfall()
    {

        yield return new WaitForSeconds(0.1f);
        canfall = 1;

    }

    IEnumerator waitColide()
    {

        yield return new WaitForSeconds(0.3f);
        enemy_box.enabled = true;
        Enemy_RB.gravityScale *= 10;
        enemy_box.gameObject.layer = 7;
        enemyType = 2;
    }

    void Update()
    {
        if (HP <= maxHp * 0.75f)
        {
            SpriteRenderer renddd = Enemy_RB.gameObject.GetComponent<SpriteRenderer>();

            renddd.color = new Color32(200, 200, 200, 255);

        }
        if (HP <= maxHp*0.5f)
        {
          
            SpriteRenderer renddd = Enemy_RB.gameObject.GetComponent<SpriteRenderer>();

            renddd.color = new Color32(100,100,100,255);
        }
        if (HP <= maxHp*0.25f)
        {
            SpriteRenderer renddd = Enemy_RB.gameObject.GetComponent<SpriteRenderer>();

            renddd.color = new Color32(50, 50, 50, 255);
        }
        if (HP <= 0)
        {
            if (ded == false)
            {
                ded = true;
                AudioSource musicc = Enemy_RB.gameObject.GetComponent<AudioSource>();

                musicc.clip = die;
                musicc.Play();
                Enemy_RB.position = new Vector2(99999, 87532);
                Destroy(Enemy_RB.gameObject, 1);
                move.stone += 2;
            }
          

        }
        if (enemyType == 2)
        {
            float distanceY = Mathf.Abs(Player_RB.position.y - Enemy_RB.position.y);
            float distanceX = Mathf.Abs(Player_RB.position.x - Enemy_RB.position.x);

            float distance = Mathf.Sqrt((distanceX * distanceX) + (distanceY * distanceY));

            if (distance <= 20)
            {

                if (awakened == false)
                {
                    if (Enemy_RB.mass == 50)
                    {

                    }
                    else
                    {
                        awakened = true;
                        AudioSource musicc = Enemy_RB.gameObject.GetComponent<AudioSource>();

                        musicc.clip = enter;
                        musicc.Play();
                    }


                }
                if (Mathf.Abs(Enemy_RB.angularVelocity) < 500)
                {
                    if (Player_RB.position.x > Enemy_RB.position.x)
                    {
                        if (Enemy_RB.mass == 50)
                        {
                            if (Mathf.Abs(Enemy_RB.angularVelocity) < 100)
                            {
                                Enemy_RB.AddTorque(-250);

                            }


                        }
                        else
                        {
                            Enemy_RB.AddTorque(2 * (-1));

                        }

                    }
                    else
                    {
                        if (Enemy_RB.mass == 50)
                        {
                            if (Mathf.Abs(Enemy_RB.angularVelocity) < 100)
                            {
                                Enemy_RB.AddTorque(250);

                            }
                        }
                        else
                        {
                            Enemy_RB.AddTorque(2);

                        }

                    }
                }

            }
            else
            {
                awakened = false;
            }
            
        }

        if (enemyType == 1 && Enemy_RB.simulated==false)
        {
            float distanceY = Mathf.Abs(Player_RB.position.y - Enemy_RB.position.y);
            float distanceX = Mathf.Abs(Player_RB.position.x - Enemy_RB.position.x);

            float distance = Mathf.Sqrt((distanceX * distanceX) + (distanceY * distanceY));

            if (distanceX <= 1)
            {
                Enemy_RB.simulated = true;
                canfall = 0;
                StartCoroutine(waitfall());


            }

        }

        if (enemyType == 4)
        {
            if (go==0)
            {
                StartCoroutine(waittick());
            }

            if (go == 1)
            {
                Enemy_RB.gravityScale *= -1;
                go = 99;
            }
        }
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (enemyType == 2 && collision.gameObject.CompareTag("player"))
        {
            awakened = true;
            AudioSource musicc = Enemy_RB.gameObject.GetComponent<AudioSource>();
            
            musicc.clip = attack;
            musicc.Play();

            if ( Mathf.Abs(Player_RB.velocity.magnitude)>15)
            {
                move.stone += 1;
                Destroy(Enemy_RB.gameObject);
                
            }
        } 
        if (collision.gameObject.CompareTag("floor"))
        {
            
            if (enemyType == 1)
            {
               if (enemy_knockback == null)
                {
                    
                }
               else
                {
                    if (Enemy_RB.simulated == true && canfall==1 )
                    {
                        enemy_box.enabled = false;
                        Enemy_RB.gravityScale *= 0.1f;
                        enemy_knockback.enabled = true;
                        StartCoroutine(waitColide());
                        Destroy(enemy_knockback, 0.1f);

                    }

                }


                
                  



            }
            if (enemyType == 4)
            {
                if (go == 99)
                {
                    go = 0;

                }
            }

        }

    }
}
