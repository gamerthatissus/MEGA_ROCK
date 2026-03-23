using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;


public class boss_FIGHT_SCRIPT : MonoBehaviour
{
    public int gggolemeeed = 4;
    public int laved = 5;
    public int meitiored = 3;
    private float lavastart;
    public bool skipdiologe = false;
    public TextMeshProUGUI diologe;
    public int typeCount = 0;
    public Camera MAINCAMERA;

    public bool FIGHT_started = false;
    public AudioClip audioSUMMON;
    public AudioSource sound;
    public AudioClip lavarise;
    public AudioClip PRE_lavarise;
    public TextMeshProUGUI TEXT_HP;
    public AudioSource CAM;
    public AudioClip INTENSE;
    public AudioClip normal;
    private bool textdone=true;
    public AudioClip errupt;
    public AudioClip PRE_errupt;
    public GameObject boss;
    public GameObject METIOR_WARNING;
    public bool canattack = true;
    public bool attacking = false;
    public move22 MAIN;
    public Rigidbody2D Player_RB;
    public GameObject OG_fireball;
    public GameObject OG_fireballMEGA;
    public int talkstage=0;
    public GameObject LAVA;
    public GameObject WARNING_LAVA;
    public Button diologe_button;
    public GameObject golem_Normal;
    public GameObject golem_Sand;
    public GameObject golem_Cave;

    public bool ULTUIMANTFAISE = false;

    public float Boss_Hp = 1000;
    public AudioClip TALK_SOUND;
    public AudioSource soooounnnnd;
    
    private bool talked;

    public void NextDialogue(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
            talked = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        lavastart = LAVA.transform.position.y;
        laved = 0;
        meitiored = 0;
        gggolemeeed = 0;
        ULTUIMANTFAISE = false;
        canattack = false;
        attacking = false;

        Boss_Hp = 1000;
        canattack = false;

        //5.5
        MAINCAMERA.orthographicSize = 9;
        StartCoroutine(SLOW_TEXT(("so, we finaly meet face to face"),0.05f,true,false));

    }


    IEnumerator SLOW_TEXT(string textt, float Wait_time, bool clear, bool delay)
    {
        
        if (delay == true)
        {

            yield return new WaitForSeconds(1f);
        }

        int faketime = typeCount;
        if (textdone == false)
        {
            for (int i = 0; i < 2; i--)
            {
                if (textdone == true && typeCount == faketime + 1)
                {
                    i = 5;
                    
                }
                yield return null;

            }
        }

       

        textdone = false;

        



        char[] textSLOW = textt.ToCharArray();
        if (clear == true)
        {
            string text_thusfarr = " ";
            for (int i = 0; i < textSLOW.Length; i++)
            {
                soooounnnnd.Stop();

                soooounnnnd.clip = TALK_SOUND;
                soooounnnnd.loop = false;
                soooounnnnd.Play();
                text_thusfarr = text_thusfarr + textSLOW[i];
                diologe.text = text_thusfarr+" ";
                yield return new WaitForSeconds(Wait_time);

            }
        }
        else
        {
            string text_thusfarr = diologe.text;
            for (int i = 0; i < textSLOW.Length; i++)
            {
                soooounnnnd.Stop();
                soooounnnnd.clip = TALK_SOUND;
                soooounnnnd.loop = false;
                soooounnnnd.Play();
                text_thusfarr = text_thusfarr + textSLOW[i];
                diologe.text = text_thusfarr;
                yield return new WaitForSeconds(Wait_time);

            }
        }
      
        textdone = true;
        
            typeCount++;
       
    }


    IEnumerator Beweenattacks()
    {
        canattack = false;
        if (ULTUIMANTFAISE == false)
        {
            canattack = false;
            yield return new WaitForSeconds(UnityEngine.Random.Range(4, 13));
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
        LAVA.transform.position = new Vector2(LAVA.transform.position.x, lavastart);

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
            if (LAVA.transform.position.y < -12.5)
            {
                LAVA.transform.position = new Vector2(LAVA.transform.position.x, LAVA.transform.position.y + (2f * Time.deltaTime));
                yield return null;
            }
            

        }
        for (float i = 0; i < 5; i += Time.deltaTime)
        {
            LAVA.transform.position = new Vector2(LAVA.transform.position.x, LAVA.transform.position.y - (2f * Time.deltaTime));
            yield return null;

        }
        LAVA.transform.position = new Vector2(LAVA.transform.position.x, lavastart);

        
        attacking = false;
    }

    IEnumerator WARNING_LAVA_RISE()
    {

        WARNING_LAVA.transform.position = new Vector2(WARNING_LAVA.transform.position.x, lavastart);

        for (float i = 0; i < 1; i+= Time.deltaTime)
        {

            if (WARNING_LAVA.transform.position.y < -12)
            {
                WARNING_LAVA.transform.position = new Vector2(WARNING_LAVA.transform.position.x, WARNING_LAVA.transform.position.y + (10f * Time.deltaTime));
                yield return null;
            }
          


        }
        yield return new WaitForSeconds(1f);

        for (float i = 0; i < 1; i+=Time.deltaTime)
        {
            WARNING_LAVA.transform.position = new Vector2(WARNING_LAVA.transform.position.x, WARNING_LAVA.transform.position.y - (10f * Time.deltaTime));
           
                yield return null;

            

        }
        WARNING_LAVA.transform.position = new Vector2(WARNING_LAVA.transform.position.x, lavastart);
    }
    // Update is called once per frame
    private void Update()
    {
        if (FIGHT_started == false)
        {
            if (skipdiologe == true)
            {
                FIGHT_started = true;
                talkstage++;
                Destroy(diologe.transform.parent.gameObject);


                CAM.Stop();
                FIGHT_started = true;
                CAM.clip = normal;
                CAM.loop = true;
                CAM.Play();
                MAINCAMERA.orthographicSize = 5.5f;
                StartCoroutine(Beweenattacks());
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && textdone == true || talked && textdone == true)
            {
                talked = false;
               

                switch (talkstage)
                {
                    case 0:
                        StartCoroutine(SLOW_TEXT(("What?"), 0.05f, true, false));
                        talkstage++;
                        textdone = false;
                        StartCoroutine(SLOW_TEXT(("you dont know who i am?"), 0.05f, false, true));
                        textdone = false;

                        typeCount -= 1;

                        break;

                    case 1:
                        StartCoroutine(SLOW_TEXT(("Well, who do you think has been sommoning all of those golems you have been fighting?"), 0.05f, true, false));

                        talkstage++;
                        break;
                    case 2:
                        StartCoroutine(SLOW_TEXT(("i am"), 0.03f, true, false));

                        StartCoroutine(SLOW_TEXT((" VOLCANOY"), 0.1f, false, false));
                        typeCount++;
                        StartCoroutine(SLOW_TEXT((" the"), 0.03f, false, false));

                        typeCount++;
                        StartCoroutine(SLOW_TEXT((" Strongest"), 0.06f, false, false));
                        typeCount++;
                        StartCoroutine(SLOW_TEXT((" pet rock in the"), 0.03f, false, false));
                        typeCount++;
                        StartCoroutine(SLOW_TEXT((" WHOLE WORLD"), 0.1f, false, false));
                        typeCount -= 4;
                        talkstage++;
                        break;
                    case 3:
                        StartCoroutine(SLOW_TEXT(("and i will"), 0.03f, true, false));

                        StartCoroutine(SLOW_TEXT((" DESTROY"), 0.1f, false, false));
                        typeCount++;
                        StartCoroutine(SLOW_TEXT((" you for"), 0.03f, false, false));

                        typeCount++;
                        StartCoroutine(SLOW_TEXT((" DARING"), 0.1f, false, false));
                        typeCount++;
                        StartCoroutine(SLOW_TEXT((" to defeat me!"), 0.03f, false, false));
                        typeCount -= 3;







                        talkstage++;
                        break;
                    case 4:
                        talkstage++;
                        Destroy(diologe.transform.parent.gameObject);


                        CAM.Stop();
                        FIGHT_started = true;
                        CAM.clip = normal;
                        CAM.loop = true;
                        CAM.Play();
                        MAINCAMERA.orthographicSize = 5.5f;
                        StartCoroutine(Beweenattacks());



                        break;


                }



            }
        }
       
    }
    void FixedUpdate()
    {
    
        if (Boss_Hp <= 0)
        {
            Destroy(boss);
            CAM.Stop();
            Boss_Hp = 0;
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

            float A_rand;

            if (gggolemeeed >= 4)
            {
                
                A_rand = 20;
            }
            else if (laved >= 5)
            {
                A_rand = 12;

            }
            else if (meitiored>=3)
            {
                A_rand = 1;
            }
            else
            {
                A_rand = UnityEngine.Random.Range(1, 20);

            }



            if (A_rand <= 9)
            {
                laved += 1;
                gggolemeeed += 1;
                meitiored = 0;

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
                gggolemeeed += 1;
                meitiored += 1;
                laved = 0;

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
                meitiored += 1;
                laved += 1;
                gggolemeeed = 0;

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
