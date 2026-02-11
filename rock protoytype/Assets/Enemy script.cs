using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Enemyscript : MonoBehaviour
{
    //enemy types:   1=fall  2=normal 3=burried    4=smasher
    public int enemyType=0;

    public GameObject GolemAnimator;
    private int ISdiigingUp = 0;
    public LayerMask playerMASK;
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
    public bool enemy_boxE=true;
    public move22 move;

    private bool canattack = true;

    public bool slowed = false;

    public int state = 0;
    public int go = 0;
    void Start()
    {
        StartCoroutine(LOAD());

        canattack = true;
        HP = maxHp;

        
        if (enemy_boxE == true)
        {
            BoxCollider2D enemy_box = Enemy_RB.gameObject.GetComponent<BoxCollider2D>();

            enemy_box.enabled = true;

        }
        if (enemyType==0)
        {
            //sets enemy type to normal if no enemy type is sellected
            enemyType = 2;
        }

        if (enemyType == 1)
        {
            PointEffector2D enemy_knockback = Enemy_RB.gameObject.GetComponent<PointEffector2D>();

            enemy_knockback.enabled = false;
            Enemy_RB.simulated = false;
        }


    }


    IEnumerator LOAD()
    {
        while (Player_RB == null)
        {
            Player_RB = GameObject.FindWithTag("player").GetComponent<Rigidbody2D>();
            yield return null;

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
        if (enemy_boxE == true)
        {
            BoxCollider2D enemy_box = Enemy_RB.gameObject.GetComponent<BoxCollider2D>();
            yield return new WaitForSeconds(0.1f);
            enemy_box.enabled = true;
            Enemy_RB.gravityScale *= 10;
            yield return new WaitForSeconds(0.2f);
            enemy_box.gameObject.layer = 7;
            enemyType = 2;
        }
 
    }

    IEnumerator waitAttack()
    {
        canattack = false;
        slowed = true;
        Enemy_RB.velocity = new Vector2(Enemy_RB.velocity.x / 2, Enemy_RB.velocity.y / 2);
        Enemy_RB.angularVelocity = Enemy_RB.angularVelocity / 2;
        yield return new WaitForSeconds(0.4f);
        slowed = false;
        yield return new WaitForSeconds(0.4f);
        canattack = true;

    }

    IEnumerator risefromsand()
    {
        Transform ET= Enemy_RB.gameObject.GetComponent<Transform>();
 for (int i = 0; i < 35; i++)
        {
            yield return new WaitForSeconds(0.03f);

            ET.position = new Vector2(ET.position.x, ET.position.y + 0.05f);

        }
        Enemy_RB.simulated = true;
        enemyType = 2;


    }
    IEnumerator SinkINToSand()
    {
        Enemy_RB.simulated = false;
        enemyType = 6;
        ISdiigingUp = 5;

        Transform ET = Enemy_RB.gameObject.GetComponent<Transform>();
        float oldx = ET.position.x;
        float oldy = ET.position.y;
        Collider2D[] enemyePunch = Physics2D.OverlapCircleAll(new Vector2(Player_RB.position.x, Player_RB.position.y-1.6f),0.3f );

        bool canDig = false;
            
        foreach (Collider2D enemyObject in enemyePunch)
        {

            if (enemyObject.gameObject.CompareTag("floor"))
            {
                canDig = true;

            }
            else
            {
                ET.position = new Vector2(oldx, oldy);

            }
        }

        if (canDig == true)
        {
            for (int i = 0; i < 16; i++)
            {
                yield return new WaitForSeconds(0.03f);



                ET.position = new Vector2(ET.position.x, ET.position.y - 0.1f);



            }
            ET.position = new Vector2(Player_RB.position.x, Player_RB.position.y - 1.6f);
            for (int i = 0; i < 16; i++)
            {
                yield return new WaitForSeconds(0.03f);

                ET.position = new Vector2(ET.position.x, ET.position.y + 0.1f);

            }

            Enemy_RB.simulated = true;
            enemyType = 2;
            yield return new WaitForSeconds(3f);
            ISdiigingUp = 1;
        }
        else
        {
            Enemy_RB.simulated = true;
            enemyType = 2; 
            ISdiigingUp = 1;

        }





    }

    IEnumerator waitPUNCH()
    {
        SpriteRenderer Enmy_spriteREND = Enemy_RB.gameObject.GetComponent<SpriteRenderer>();

        if (Enemy_RB.mass == 50)
        {
                GameObject animator_CLONE = Instantiate(GolemAnimator);
            Transform CLone_transform = animator_CLONE.GetComponent<Transform>();

            CLone_transform.position = Enemy_RB.position;
            Enmy_spriteREND.enabled = false;
            if (Player_RB.position.x > Enemy_RB.position.x)
            {
                CLone_transform.localScale = new Vector3(-0.3f, 0.3f,0.3f);
            }
            else
            {
                CLone_transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

            }

            Destroy(animator_CLONE, 0.3f); 
        }


        yield return new WaitForSeconds(0.2f);
        Enmy_spriteREND.enabled = true;
        Collider2D[] enemyPunch = Physics2D.OverlapCircleAll(Enemy_RB.position, 1.2f, playerMASK);
        
        foreach (Collider2D enemyObject in enemyPunch)
        {
            if (enemyObject.gameObject.name == "player")
            {
                if (Enemy_RB.mass == 50)
                {
                    move.hp -= 15*move.blockMultiplier;

                }
                else if (Enemy_RB.mass == 6)
                {
                    move.hp -= 10 * move.blockMultiplier;

                }
                else
                {
                    move.hp -= 20 * move.blockMultiplier;

                }
                if (Player_RB.position.x > Enemy_RB.position.x)
                {
                    Player_RB.velocity = new Vector2(Player_RB.velocity.x + 8, Player_RB.velocity.y + 3);
                }
                else
                {
                    Player_RB.velocity = new Vector2(Player_RB.velocity.x - 8, Player_RB.velocity.y + 3);

                }
            }

        }

    }

    void FixedUpdate()
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
                move.stone += 4;
                move.hp += 10;
                if (move.hp > 100)
                    move.hp = 100;
            }
          

        }

     


        if (enemyType == 2)
        {
            float distanceY = Mathf.Abs(Player_RB.position.y - Enemy_RB.position.y);
            float distanceX = Mathf.Abs(Player_RB.position.x - Enemy_RB.position.x);

            float distance = Mathf.Sqrt((distanceX * distanceX) + (distanceY * distanceY));

            if (distance <= 20 && slowed == false)
            {
                Enemy_RB.WakeUp();

                if (awakened == false || Mathf.Abs(Enemy_RB.angularVelocity) < 5 )
                {
                    if (Enemy_RB.mass == 50)
                    {
                        if (Mathf.Abs(Enemy_RB.angularVelocity) < 500)
                        {
                            if (Player_RB.position.x > Enemy_RB.position.x)
                            {
                                
                                    if (Mathf.Abs(Enemy_RB.angularVelocity) < 100)
                                    {
                                        Enemy_RB.AddTorque(-1500);

                                    }



                            }
                            else
                            {
                               
                                    if (Mathf.Abs(Enemy_RB.angularVelocity) < 100)
                                    {
                                        Enemy_RB.AddTorque(1500);

                                    }
                                

                            }
                        }
                    }
                   else if (Enemy_RB.mass == 6 && distance >= 8 && ISdiigingUp == 1)
                        {
                            StartCoroutine(SinkINToSand());
                        }
                     else
                    {

                        if (Mathf.Abs(Enemy_RB.angularVelocity) < 500)
                        {
                            if (Player_RB.position.x > Enemy_RB.position.x)
                            {
                                if (Enemy_RB.mass == 6)
                                {

                                    Enemy_RB.AddTorque(-120);




                                }
                                else
                                {

                                    Enemy_RB.AddTorque(20 * (-1));

                                }

                            }
                            else
                            {
                                if (Enemy_RB.mass == 6)
                                {

                                    Enemy_RB.AddTorque(120);


                                }
                                else
                                {

                                    Enemy_RB.AddTorque(20);

                                }

                            }
                        }
                        AudioSource musicc = Enemy_RB.gameObject.GetComponent<AudioSource>();

                        musicc.clip = enter;
                        if (awakened == false)
                        {
                            musicc.Play();

                        }

                        awakened = true;


                    }


                }

                if (Mathf.Abs(Enemy_RB.angularVelocity) < 500)
                {
                    if (Player_RB.position.x > Enemy_RB.position.x)
                    {
                        if (Enemy_RB.mass == 50)
                        {
                            if (Mathf.Abs(Enemy_RB.angularVelocity) < 100 )
                            {
                                Enemy_RB.AddTorque(-450);

                            }


                        }
                        else
                        {
                            if (Enemy_RB.mass == 6)
                            {
                                if (Mathf.Abs(Enemy_RB.angularVelocity) < 250)
                                {
                                    Enemy_RB.AddTorque(-20);

                                }

                            }
                            else
                            {
                                Enemy_RB.AddTorque(4 * (-1));

                            }

                        }

                    }
                    else
                    {
                        if (Enemy_RB.mass == 50)
                        {
                            if (Mathf.Abs(Enemy_RB.angularVelocity) < 100)
                            {
                                Enemy_RB.AddTorque(450);

                            }
                        }
                        else
                        {
                            if (Mathf.Abs(Enemy_RB.angularVelocity) < 250)
                            {
                                Enemy_RB.AddTorque(20);

                            }
                            else
                            {
                                Enemy_RB.AddTorque(4);

                            }

                        }

                    }
                }

            }
            else
            {
                awakened = false;
            }
            
        }

        if (enemyType == 6 && Enemy_RB.simulated == false)
        {
            float distanceY = Mathf.Abs(Player_RB.position.y - Enemy_RB.position.y);
            float distanceX = Mathf.Abs(Player_RB.position.x - Enemy_RB.position.x);

            float distance = Mathf.Sqrt((distanceX * distanceX) + (distanceY * distanceY));

            if (distanceX <= 4 && distanceY<=6 && ISdiigingUp==0)
            {
                ISdiigingUp = 1;
                StartCoroutine(risefromsand());

            }

        }



        if (enemyType == 1 && Enemy_RB.simulated==false)
        {
            float distanceY = Mathf.Abs(Player_RB.position.y - Enemy_RB.position.y);
            float distanceX = Mathf.Abs(Player_RB.position.x - Enemy_RB.position.x);

            float distance = Mathf.Sqrt((distanceX * distanceX) + (distanceY * distanceY));

            if (distanceX <= 1.1f)
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
    private void OnCollisionStay2D(Collision2D collision)
    {
       

        if (enemyType == 2 && collision.gameObject.CompareTag("player") && canattack==true)
        {
            
             StartCoroutine(waitAttack());

            awakened = true;
            AudioSource musicc = Enemy_RB.gameObject.GetComponent<AudioSource>();

            musicc.clip = attack;
            musicc.Play();

            StartCoroutine(waitPUNCH());





        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (enemyType == 2 && collision.gameObject.CompareTag("player"))
        {
            if (canattack==true)
            {

                StartCoroutine(waitAttack());

                awakened = true;
                AudioSource musicc = Enemy_RB.gameObject.GetComponent<AudioSource>();

                musicc.clip = attack;
                musicc.Play();

                StartCoroutine(waitPUNCH());






            }

            if (Mathf.Abs(Player_RB.velocity.magnitude) > 15)
            {
                move.stone += 4;
                move.hp += 10;
                if (move.hp > 100)
                    move.hp = 100;
                Destroy(Enemy_RB.gameObject);

            }
        } 
        if (collision.gameObject.CompareTag("floor") )
        {
            
            if (enemyType == 1)
            {
                PointEffector2D enemy_knockback = Enemy_RB.gameObject.GetComponent<PointEffector2D>();

                if (enemy_knockback == null)
                {
                    
                }
               else
                {
                    if (Enemy_RB.simulated == true && canfall==1 )
                    {
                        if (enemy_boxE == true)
                        {
                            BoxCollider2D enemy_box = Enemy_RB.gameObject.GetComponent<BoxCollider2D>();
                            enemy_box.enabled = false;
                            Enemy_RB.gravityScale *= 0.1f;
                            enemy_knockback.enabled = true;
                            StartCoroutine(waitColide());
                            Destroy(enemy_knockback, 0.2f);
                        }

                       

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
