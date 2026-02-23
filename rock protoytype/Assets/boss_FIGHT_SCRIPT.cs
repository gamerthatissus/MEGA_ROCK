using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class boss_FIGHT_SCRIPT : MonoBehaviour
{
    private bool canattack = true;
    private bool attacking = false;
    public move22 MAIN;
    public Rigidbody2D Player_RB;
    public GameObject OG_fireball;
    public GameObject LAVA;
    public GameObject golem_Normal;
    public GameObject golem_Sand;
    public GameObject golem_Cave;



    public float Boss_Hp = 3000;
    // Start is called before the first frame update
    void Start()
    {
        canattack = true;
        attacking = false;

        Boss_Hp = 3000;
        canattack = false;

        StartCoroutine(Beweenattacks());

    }

    IEnumerator Beweenattacks()
    {
        canattack = false;
        yield return new WaitForSeconds(UnityEngine.Random.Range(3,12));
        canattack = true;
    }
    IEnumerator summonFIREBALL()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0,1.5f));
        GameObject FIREball_CLONE = Instantiate(OG_fireball);
        Rigidbody2D fire_RB = FIREball_CLONE.GetComponent<Rigidbody2D>();
        fire_RB.position = new Vector2(840 + UnityEngine.Random.Range(-20, 20f), 90+UnityEngine.Random.Range(-5, 15f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canattack == true && attacking==false)
        {
            float A_rand = UnityEngine.Random.Range(1, 20);

           if (A_rand > 8)
            {
                attacking = true;
                int numb_fireballs =  Mathf.RoundToInt(UnityEngine.Random.Range(3,8));
                for (int i = 0; i < numb_fireballs; i++)
                {
                    StartCoroutine(summonFIREBALL());
                }
                StartCoroutine(Beweenattacks());

            }

        }

    }
}
