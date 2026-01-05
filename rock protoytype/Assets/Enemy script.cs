using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemyscript : MonoBehaviour
{
    //enemy types:   1=fall  2=normal 3=burried
    public int enemyType=0;

    public Rigidbody2D Player_RB;
    public Rigidbody2D Enemy_RB;
    public PointEffector2D enemy_knockback;
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

    void Update()
    {
        float distanceY = Mathf.Abs(Player_RB.position.y - Enemy_RB.position.y);
        float distanceX = Mathf.Abs(Player_RB.position.x - Enemy_RB.position.x);

        float distance = Mathf.Sqrt(  (distanceX * distanceX) + (distanceY * distanceY)   );
    }
}
