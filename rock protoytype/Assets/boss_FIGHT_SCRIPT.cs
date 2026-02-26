using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class boss_FIGHT_SCRIPT : MonoBehaviour
{
    public AudioClip audioSUMMON;
    public AudioSource sound;
    public AudioClip lavarise;
    public AudioClip PRE_lavarise;
    public TextMeshProUGUI TEXT_HP;
    public AudioSource CAM;
    public AudioClip INTENSE;
    public AudioClip errupt;
    public AudioClip PRE_errupt;
    public GameObject boss;
    public GameObject METIOR_WARNING;
    private bool canattack = true;
    public bool attacking = false;
    public move22 MAIN;
    public Rigidbody2D Player_RB;
    public GameObject OG_fireball;
    public GameObject OG_fireballMEGA;

    public GameObject LAVA;
    public GameObject WARNING_LAVA;

    public GameObject golem_Normal;
    public GameObject golem_Sand;
    public GameObject golem_Cave;

    public bool ULTUIMANTFAISE = false;

    public float Boss_Hp = 1000;
    // Start is called before the first frame update
    void Start()
    {
        ULTUIMANTFAISE = false;
        canattack = true;
        attacking = false;

        Boss_Hp = 1000;
        canattack = false;

        StartCoroutine(Beweenattacks());

    }

    IEnumerator Beweenattacks()
    {
        if (ULTUIMANTFAISE == false)
        {
            canattack = false;
            yield return new WaitForSeconds(UnityEngine.Random.Range(4, 12));
            canattack = true;
        }
        else
        {
            canattack = false;
            yield return new WaitForSeconds(UnityEngine.Random.Range(3, 8));
            canattack = true;
        }

       
    }
    IEnumerator summonFIREBALL()
    {

        float randx = UnityEngine.Random.Range(-20, 20f);
      

        yield return new WaitForSeconds(UnityEngine.Random.Range(0,1.5f));
        GameObject WARNING_CLONE = Instantiate(METIOR_WARNING);
        WARNING_CLONE.transform.position = new Vector2(840 + randx, 70);
    if (ULTUIMANTFAISE == false)
        {
            Destroy(WARNING_CLONE, 0.5f);

        }
        else
        {
            Destroy(WARNING_CLONE, 0.2f);

        }
    if (ULTUIMANTFAISE == false)
        {
            GameObject FIREball_CLONE = Instantiate(OG_fireball);
           
            FIREball_CLONE.name = "FIREBALL";
            Rigidbody2D fire_RB = FIREball_CLONE.GetComponent<Rigidbody2D>();
            fire_RB.position = new Vector2(840 + randx, 100 + UnityEngine.Random.Range(0, 1f));
        }
        else
        {
            GameObject FIREball_CLONE = Instantiate(OG_fireballMEGA);
          
            FIREball_CLONE.name = "FIREBALL";
            Rigidbody2D fire_RB = FIREball_CLONE.GetComponent<Rigidbody2D>();
            fire_RB.position = new Vector2(840 + randx, 100 + UnityEngine.Random.Range(0, 1f));
        }

       
    }
    IEnumerator LAVA_RISE()
    {
        if (ULTUIMANTFAISE == false)
        {
            yield return new WaitForSeconds(1f);

        }
        else
        {
            yield return new WaitForSeconds(0.2f);

        }

        sound.loop = false;
        sound.Stop();
        sound.clip = lavarise;
        sound.Play();

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
        if (Boss_Hp <= 0)
        {
            Destroy(boss);
        }
        TEXT_HP.text = "HP: " + Boss_Hp + "/1000";
        if (Boss_Hp <= 400 && ULTUIMANTFAISE==false)
        {
          

            CAM.Stop();
            CAM.clip = INTENSE;
            CAM.loop = true;
            CAM.Play();
            ULTUIMANTFAISE = true;

        }
        if (canattack == true && attacking==false && Boss_Hp>0)
        {
            float A_rand = UnityEngine.Random.Range(1, 20);

           if (A_rand <= 9)
            {
                sound.loop = false;
                sound.Stop();
                sound.clip = PRE_errupt;
                sound.Play();

                attacking = true;
                canattack = false;
                if (ULTUIMANTFAISE == false)
                {
                    int numb_fireballs = Mathf.RoundToInt(UnityEngine.Random.Range(5, 15));
                    for (int i = 0; i < numb_fireballs; i++)
                    {
                        StartCoroutine(summonFIREBALL());
                    }
                    StartCoroutine(Beweenattacks());
                }
                else
                {
                    int numb_fireballs = Mathf.RoundToInt(UnityEngine.Random.Range(7, 16));
                    for (int i = 0; i < numb_fireballs; i++)
                    
                        StartCoroutine(summonFIREBALL());
                    }
                    StartCoroutine(Beweenattacks());
                }
            else if (A_rand > 9 && A_rand<=14)
            {
                sound.loop = false;
                sound.Stop();
                sound.clip = PRE_lavarise;
                sound.Play();

                attacking = true;
                canattack = false;
                StartCoroutine(Beweenattacks());
                StartCoroutine(LAVA_RISE());
                StartCoroutine(WARNING_LAVA_RISE());

            }
           else
            {
                sound.loop = false;
                sound.Stop();
                sound.clip = audioSUMMON;
                sound.Play();

                attacking = true;
                canattack = false;

                StartCoroutine(Beweenattacks());

                //840,70.1
                int rand_golem = Mathf.RoundToInt(UnityEngine.Random.Range(1, 4));
                int rand_place = Mathf.RoundToInt(UnityEngine.Random.Range(1, 4));

                if (rand_place == 1)
                {
                    if (rand_golem == 1)
                    {
                        GameObject enemyclone = Instantiate<GameObject>(golem_Cave);
                        enemyclone.name = "MINION";
                        Rigidbody2D moveee = enemyclone.GetComponent<Rigidbody2D>();
                        enemyclone.transform.position = new Vector2(840 + UnityEngine.Random.Range(-5, 5), 74);
                    }
                    else if (rand_golem == 2)
                    {
                        GameObject enemyclone = Instantiate<GameObject>(golem_Sand);
                        enemyclone.name = "MINION";
                        enemyclone.transform.position = new Vector2(840 + UnityEngine.Random.Range(-5, 5), 69.2f);
                    }
                    else
                    {
                        GameObject enemyclone = Instantiate<GameObject>(golem_Normal);
                        enemyclone.name = "MINION";
                        enemyclone.transform.position = new Vector2(840 + UnityEngine.Random.Range(-5, 5), 70);
                    }
                }
                else if (rand_place == 2)
                {
                    if (rand_golem == 1)
                    {
                        
                    }
                    else if (rand_golem == 2)
                    {
                        GameObject enemyclone = Instantiate<GameObject>(golem_Sand);
                        enemyclone.name = "MINION";
                        enemyclone.transform.position = new Vector2(840 + UnityEngine.Random.Range(-5, 5), 76.2f);
                    }
                    else
                    {
                        GameObject enemyclone = Instantiate<GameObject>(golem_Normal);
                        enemyclone.name = "MINION";
                        enemyclone.transform.position = new Vector2(840 + UnityEngine.Random.Range(-5, 5), 77);
                    }
                }
                else if(rand_place==3)
                {

                    if (rand_golem == 1)
                    {
                        GameObject enemyclone = Instantiate<GameObject>(golem_Cave);
                        enemyclone.name = "MINION";
                        Rigidbody2D moveee = enemyclone.GetComponent<Rigidbody2D>();
                        enemyclone.transform.position = new Vector2(829 + UnityEngine.Random.Range(-2.5f, 2.5f), 80);
                    }
                    else if (rand_golem == 2)
                    {
                        GameObject enemyclone = Instantiate<GameObject>(golem_Sand);
                        enemyclone.name = "MINION";
                        enemyclone.transform.position = new Vector2(829 + UnityEngine.Random.Range(-2.5f, 2.5f), 81);
                    }
                    else
                    {
                        GameObject enemyclone = Instantiate<GameObject>(golem_Normal);
                        enemyclone.name = "MINION";
                        enemyclone.transform.position = new Vector2(829 + UnityEngine.Random.Range(-2.5f, 2.5f), 80);
                    }




                }
                else
                {
                    if (rand_golem == 1)
                    {
                        GameObject enemyclone = Instantiate<GameObject>(golem_Cave);
                        enemyclone.name = "MINION";
                        Rigidbody2D moveee = enemyclone.GetComponent<Rigidbody2D>();
                        enemyclone.transform.position = new Vector2(850 + UnityEngine.Random.Range(-2.5f, 2.5f), 80);
                    }
                    else if (rand_golem == 2)
                    {
                        GameObject enemyclone = Instantiate<GameObject>(golem_Sand);
                        enemyclone.name = "MINION";
                        enemyclone.transform.position = new Vector2(850 + UnityEngine.Random.Range(-2.5f, 2.5f), 81);
                    }
                    else
                    {
                        GameObject enemyclone = Instantiate<GameObject>(golem_Normal);
                        enemyclone.name = "MINION";
                        enemyclone.transform.position = new Vector2(850 + UnityEngine.Random.Range(-2.5f, 2.5f), 80);
                    }
                }

                if (UnityEngine.Random.Range(0, 10) > 6)
                {
                    rand_golem = Mathf.RoundToInt(UnityEngine.Random.Range(1, 4));
                    rand_place = Mathf.RoundToInt(UnityEngine.Random.Range(1, 4));

                    if (rand_place == 1)
                    {
                        if (rand_golem == 1)
                        {
                            GameObject enemyclone = Instantiate<GameObject>(golem_Cave);
                            enemyclone.name = "MINION";
                            Rigidbody2D moveee = enemyclone.GetComponent<Rigidbody2D>();
                            enemyclone.transform.position = new Vector2(840 + UnityEngine.Random.Range(-5, 5), 74);
                        }
                        else if (rand_golem == 2)
                        {
                            GameObject enemyclone = Instantiate<GameObject>(golem_Sand);
                            enemyclone.name = "MINION";
                            enemyclone.transform.position = new Vector2(840 + UnityEngine.Random.Range(-5, 5), 69.2f);
                        }
                        else
                        {
                            GameObject enemyclone = Instantiate<GameObject>(golem_Normal);
                            enemyclone.name = "MINION";
                            enemyclone.transform.position = new Vector2(840 + UnityEngine.Random.Range(-5, 5), 70);
                        }
                    }
                    else if (rand_place == 2)
                    {
                        if (rand_golem == 1)
                        {

                        }
                        else if (rand_golem == 2)
                        {
                            GameObject enemyclone = Instantiate<GameObject>(golem_Sand);
                            enemyclone.name = "MINION";
                            enemyclone.transform.position = new Vector2(840 + UnityEngine.Random.Range(-5, 5), 76.2f);
                        }
                        else
                        {
                            GameObject enemyclone = Instantiate<GameObject>(golem_Normal);
                            enemyclone.name = "MINION";
                            enemyclone.transform.position = new Vector2(840 + UnityEngine.Random.Range(-5, 5), 77);
                        }
                    }
                    else if (rand_place == 3)
                    {

                        if (rand_golem == 1)
                        {
                            GameObject enemyclone = Instantiate<GameObject>(golem_Cave);
                            enemyclone.name = "MINION";
                            Rigidbody2D moveee = enemyclone.GetComponent<Rigidbody2D>();
                            enemyclone.transform.position = new Vector2(829 + UnityEngine.Random.Range(-2.5f, 2.5f), 80);
                        }
                        else if (rand_golem == 2)
                        {
                            GameObject enemyclone = Instantiate<GameObject>(golem_Sand);
                            enemyclone.name = "MINION";
                            enemyclone.transform.position = new Vector2(829 + UnityEngine.Random.Range(-2.5f, 2.5f), 81);
                        }
                        else
                        {
                            GameObject enemyclone = Instantiate<GameObject>(golem_Normal);
                            enemyclone.name = "MINION";
                            enemyclone.transform.position = new Vector2(829 + UnityEngine.Random.Range(-2.5f, 2.5f), 80);
                        }




                    }
                    else
                    {
                        if (rand_golem == 1)
                        {
                            GameObject enemyclone = Instantiate<GameObject>(golem_Cave);
                            enemyclone.name = "MINION";
                            Rigidbody2D moveee = enemyclone.GetComponent<Rigidbody2D>();
                            enemyclone.transform.position = new Vector2(850 + UnityEngine.Random.Range(-2.5f, 2.5f), 80);
                        }
                        else if (rand_golem == 2)
                        {
                            GameObject enemyclone = Instantiate<GameObject>(golem_Sand);
                            enemyclone.name = "MINION";
                            enemyclone.transform.position = new Vector2(850 + UnityEngine.Random.Range(-2.5f, 2.5f), 81);
                        }
                        else
                        {
                            GameObject enemyclone = Instantiate<GameObject>(golem_Normal);
                            enemyclone.name = "MINION";
                            enemyclone.transform.position = new Vector2(850 + UnityEngine.Random.Range(-2.5f, 2.5f), 80);
                        }
                    }
                }
                

                attacking = false;





            }


        }

    }
}
