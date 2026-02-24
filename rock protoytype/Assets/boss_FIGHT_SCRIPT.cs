using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class boss_FIGHT_SCRIPT : MonoBehaviour
{
    public GameObject METIOR_WARNING;
    private bool canattack = true;
    public bool attacking = false;
    public move22 MAIN;
    public Rigidbody2D Player_RB;
    public GameObject OG_fireball;
    public GameObject LAVA;
    public GameObject WARNING_LAVA;

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
        yield return new WaitForSeconds(UnityEngine.Random.Range(3,8));
        canattack = true;
    }
    IEnumerator summonFIREBALL()
    {

        float randx = UnityEngine.Random.Range(-20, 20f);
      

        yield return new WaitForSeconds(UnityEngine.Random.Range(0,1.5f));
        GameObject WARNING_CLONE = Instantiate(METIOR_WARNING);
        WARNING_CLONE.transform.position = new Vector2(840 + randx, 70);
        Destroy(WARNING_CLONE, 0.4f);
        GameObject FIREball_CLONE = Instantiate(OG_fireball);

        FIREball_CLONE.name = "FIREBALL";
        Rigidbody2D fire_RB = FIREball_CLONE.GetComponent<Rigidbody2D>();
        fire_RB.position = new Vector2(840 + randx, 100+UnityEngine.Random.Range(0,1f));
    }
    IEnumerator LAVA_RISE()
    {

        yield return new WaitForSeconds(1f);

        for (float i = 0; i < 5; i+=Time.deltaTime)
        {  
            LAVA.transform.position = new Vector2(LAVA.transform.position.x, LAVA.transform.position.y + (2f*Time.deltaTime));
            yield return null;

        }
        for (float i = 0; i < 5; i += Time.deltaTime)
        {
            LAVA.transform.position = new Vector2(LAVA.transform.position.x, LAVA.transform.position.y - (2f * Time.deltaTime));
            yield return null;

        }
        attacking = false;
    }

    IEnumerator WARNING_LAVA_RISE()
    {


        for (float i = 0; i < 1; i+= Time.deltaTime)
        {
            WARNING_LAVA.transform.position = new Vector2(WARNING_LAVA.transform.position.x, WARNING_LAVA.transform.position.y + (10f * Time.deltaTime));
            yield return null;


        }
        yield return new WaitForSeconds(1f);

        for (float i = 0; i < 1; i+=Time.deltaTime)
        {
            WARNING_LAVA.transform.position = new Vector2(WARNING_LAVA.transform.position.x, WARNING_LAVA.transform.position.y - (10f * Time.deltaTime));
           
                yield return null;

            

        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (canattack == true && attacking==false)
        {
            float A_rand = UnityEngine.Random.Range(1, 20);

           if (A_rand < 8)
            {
                attacking = true;
                canattack = false;
                int numb_fireballs =  Mathf.RoundToInt(UnityEngine.Random.Range(5,15));
                for (int i = 0; i < numb_fireballs; i++)
                {
                    StartCoroutine(summonFIREBALL());
                }
                StartCoroutine(Beweenattacks());
            }
            
            
            if (A_rand > 8 && A_rand<11)
            {
                attacking = true;
                canattack = false;
                StartCoroutine(Beweenattacks());
                StartCoroutine(LAVA_RISE());
                StartCoroutine(WARNING_LAVA_RISE());

            }
        }

    }
}
