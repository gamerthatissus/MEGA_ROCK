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
    
    IEnumerator waitDesableAcchevments()
    {
        //610, 462.32
        a_get_pos.localPosition = new Vector3(-133.22f, 430, 0);
        yield return new WaitForSeconds(0.1f);

        a_get_pos.localPosition = new Vector3(-133.22f, 430, 0); 
        if (Camera.main.orthographicSize >5.5)
        {
            a_get_pos.localPosition = new Vector3(-133.22f, 600, 0); 

        }

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

    }
    public void GiveAchievement(string achievement)
    {
        a_get_pannel.SetActive(true);
        a_get_text.text = achievement;
        StartCoroutine(waitDesableAcchevments());

        string currentAchievements = PlayerPrefs.GetString("achievements");

        if (!currentAchievements.Split(";").Contains(achievement))
        {
            PlayerPrefs.SetString("achievements", currentAchievements + achievement + ";");
            PlayerPrefs.Save();

            Debug.Log(achievement);
        }
    }
    //610, 462.32
    private void Start()
    {
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

        if (!moveScript && (thisSceneName == "game" || thisSceneName == "LevelOne" || thisSceneName == "LevelTwo" || thisSceneName == "LevelThree" || thisSceneName == "THE BOSS FIGHT"))
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
            if (lastSceneName == "game")
            {
                if (thisSceneName == "LevelOne")
                {
                    GiveAchievement("Tutorial completed!");

                    if (violent)
                    {
                        GiveAchievement("Tutorial completed after defeating all enemies!");
                    }
                }
                else if (thisSceneName == "THE BOSS FIGHT")
                {
                    GiveAchievement("Secret portal found!");
                }
            }
            else if (lastSceneName == "LevelOne" && thisSceneName == "LevelTwo")
            {
                GiveAchievement("Level 1 completed!");

                if (hitless)
                {
                    GiveAchievement("Level 1 completed without taking damage!");
                }

                if (violent)
                {
                    GiveAchievement("Level 1 completed after defeating all enemies!");
                }
            }
            else if (lastSceneName == "LevelTwo" && thisSceneName == "LevelThree")
            {
                GiveAchievement("Level 2 completed!");

                if (hitless)
                {
                    GiveAchievement("Level 2 completed without taking damage!");
                }

                if (violent)
                {
                    GiveAchievement("Level 2 completed after defeating all enemies!");
                }
            }
            else if (lastSceneName == "LevelThree" && thisSceneName == "THE BOSS FIGHT")
            {
                GiveAchievement("Level 3 completed!");

                if (hitless)
                {
                    GiveAchievement("Level 3 completed without taking damage!");
                }

                if (violent)
                {
                    GiveAchievement("Level 3 completed after defeating all enemies!");
                }
            }

            hitless = true;
            violent = false;
            lastSceneName = thisSceneName;
        }
    }
}
