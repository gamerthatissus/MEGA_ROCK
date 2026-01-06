using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemyscript : MonoBehaviour
{
    //enemy types:   1=fall  2=normal 3=burried    4=smasher
    public int enemyType=0;

    private int canfall=0;
    public Rigidbody2D Player_RB;
    public Rigidbody2D Enemy_RB;
    public PointEffector2D enemy_knockback;
    public int state = 0;
    public int go = 0;
    void Start()
    {
        
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

    void Update()
    {
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
                        enemy_knockback.enabled = true;
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
