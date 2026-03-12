using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementScript : MonoBehaviour
{
    public GameObject a_get_pannel;
    public TextMeshProUGUI a_get_text;
    private string lastSceneName;
    private bool hitless;
    private bool violent;
    private move22 moveScript;
    private GameObject enemyList;
    public RectTransform a_get_pos;
    public bool ACEIVEMENTING=false;
    IEnumerator waitdie()
    {
        yield return new WaitForSeconds(1.6f);
        GiveAchievement("moten rock-y: note to self, lava is not fun to swim in");

    }
    IEnumerator waitNEXT(string ashevmenttt_text)
    {
       for (int i = 0; i < 2; i--)
        {
            if (ACEIVEMENTING == false)
            {
                GiveAchievement(ashevmenttt_text);
                yield return null;


            }
            yield return null;
        }
    }
    IEnumerator delayedachevment(string ashevmenttt_text, float delaytime)
    {
        yield return new WaitForSeconds(delaytime);
                GiveAchievement(ashevmenttt_text);

    }
    IEnumerator waitDesableAcchevments()
    {
        //610, 462.32
        a_get_pos.localPosition = new Vector3(-133.22f, 600, 0);
        

        for (float i = 0; i < 1.1f; i += Time.deltaTime)
          {
           
                a_get_pos.localPosition = new Vector2(-133.22f, a_get_pos.localPosition.y-(150*Time.deltaTime));
            yield return null;
            
        }

        yield return new WaitForSeconds(5);

        for (float i = 0; i < 1.1f; i += Time.deltaTime)
        {
            
                a_get_pos.localPosition = new Vector2(-133.22f, a_get_pos.localPosition.y + (150 * Time.deltaTime));

            
            yield return null;
        }
        a_get_pos.localPosition = new Vector3(-133.22f, 600, 0);
        yield return null;
        ACEIVEMENTING = false;
    }
    public void GiveAchievement(string achievement)
    {
        if (ACEIVEMENTING == true)
        {
            StartCoroutine(waitNEXT(achievement));
        }
        else
        {
            string currentAchievements = PlayerPrefs.GetString("achievements");

            if (!currentAchievements.Split(";").Contains(achievement))
            {
                ACEIVEMENTING = true;

                a_get_pannel.SetActive(true);
                a_get_text.text = achievement;
                StartCoroutine(waitDesableAcchevments());

                PlayerPrefs.SetString("achievements", currentAchievements + achievement + ";");
                PlayerPrefs.Save();

                Debug.Log(achievement);
            }
        }

       
      
    }
    //610, 462.32
    private void Start()
    {
        PlayerPrefs.DeleteKey("achievements");
        PlayerPrefs.Save();


        if (GameObject.Find("achevment panel") && GameObject.Find("achevment text"))
        {
            a_get_pannel = GameObject.Find("achevment panel");
            a_get_text = GameObject.Find("achevment text").GetComponent<TextMeshProUGUI>();
        }
        DontDestroyOnLoad(gameObject);



        a_get_pannel.SetActive(true);

        

        if (GameObject.Find("AchievementHandler"))
        {
            Destroy(gameObject);
        }

        gameObject.name = "AchievementHandler";


    }

    private void Update()
    {
        if (moveScript != null)
        {
            if (moveScript.stone == 0 && moveScript.ranout==false)
            {
                moveScript.ranout = true;
                GiveAchievement("BROKE: run out of stone");

            }
            if (moveScript.unlockedLAUNCH == true)
            {
                moveScript.unlockedLAUNCH = false;
                GiveAchievement("SPACE TO LAUNCH: unlock the rock launch ability!");

            }
            if (moveScript.discovedSecret == true)
            {
                moveScript.discovedSecret = false;
                GiveAchievement("SECRET ROOM: discover your first secret room");

            }
            if (moveScript.BURNT == true)
            {
                moveScript.BURNT = false;
                StartCoroutine(waitdie());

            }
        }
        if (GameObject.Find("achevment panel") && GameObject.Find("achevment text"))
        {
            a_get_pannel = GameObject.Find("achevment panel");
            a_get_text = GameObject.Find("achevment text").GetComponent<TextMeshProUGUI>();
            a_get_pos = a_get_pannel.GetComponent<RectTransform>();
            
        }

        string thisSceneName = SceneManager.GetActiveScene().name;

        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteKey("achievements");
            PlayerPrefs.Save();

            Debug.Log("achievements cleared");
        }

        if (!moveScript && (thisSceneName == "game" || thisSceneName == "LevelOne" || thisSceneName == "LevelTwo" || thisSceneName == "LevelThree" || thisSceneName == "THE BOSS FIGHT" || thisSceneName == "2 THE BOSS FIGHT"))
        {
            GameObject move = GameObject.Find("move");
            if (move)
                moveScript = move.GetComponent<move22>();
        }

        if (!enemyList && (thisSceneName == "game" || thisSceneName == "LevelOne" || thisSceneName == "LevelTwo" || thisSceneName == "LevelThree"))
        {
            enemyList = GameObject.Find("enemyList");
        }

        if (hitless && moveScript && moveScript.hp < 120)
        {
            hitless = false;
        }

        if (!violent && enemyList && enemyList.transform.hierarchyCount == 1)
        {
            violent = true;
        }

        if (thisSceneName != lastSceneName)
        {
            if (lastSceneName != "Titlescreen" || lastSceneName != "MainMenu")
            {
                if (moveScript.hp < 5)
                {
                    StartCoroutine(delayedachevment("tis but a scratch: compelte a level with less then 5hp left", 0.1f));
                   

                }
            }
            if (lastSceneName == "game")
            {
                if (thisSceneName == "LevelOne")
                {
                    GiveAchievement("BEGINER: complete the totoreal!");
                    StartCoroutine(delayedachevment("2 TO SURGE!: unlock the stone surge ability!", 0.3f));

                    if (violent)
                    {
                        StartCoroutine(delayedachevment("MURDERHOBO BEGINER: complete the totorial after defeating all enemies!", 0.15f));

                    }
                }
                else if (thisSceneName == "THE BOSS FIGHT")
                {
                    GiveAchievement("???: find the secret in level 1!");
                }
            }
            else if (lastSceneName == "LevelOne" && thisSceneName == "LevelTwo")
            {
                GiveAchievement("NOVICE: complete level 1!");

                if (hitless)
                {
                    StartCoroutine(delayedachevment("CANT TOTCH THIS: complete Level 1 without taking damage!", 0.13f));

                }

                if (violent)
                {
                    StartCoroutine(delayedachevment("MUDERHOBO NOVICE: complete level one after defeating all enemys", 0.08f));

                   
                }
            }
            else if (lastSceneName == "LevelTwo" && thisSceneName == "LevelThree")
            {
                GiveAchievement("EXPERIENCED: completed level 2!");

                if (hitless)
                {
                    StartCoroutine(delayedachevment("IMMAGINE BEING HIT: complete level 2 without taking damage!", 0.05f));

                }

                if (violent)
                {
                    StartCoroutine(delayedachevment("EXPERIENCED MURDERHOBO: complete level 2 after defeating all enemies!", 0.2f));

                }
            }
            else if (lastSceneName == "LevelThree" && thisSceneName == "THE BOSS FIGHT")
            {
                GiveAchievement("PROFESHONAL: complete level 3!");


                if (hitless)
                {
                    StartCoroutine(delayedachevment("THE UNTUTCHABLE ONE: complete level 3 without taking damage!", 0.09f));

                }

                if (violent)
                {
                    StartCoroutine(delayedachevment("PROFESHONAL MURDERHOBO: complete level 3 after deating all enemys!", 0.04f));

                }
            }

            hitless = true;
            violent = false;
            lastSceneName = thisSceneName;
        }
    }
}
