using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementScript : MonoBehaviour
{
    private string lastSceneName;
    private bool hitless;
    private move22 moveScript;

    private void GiveAchievement(string achievement)
    {
        string currentAchievements = PlayerPrefs.GetString("achievements");

        if (!currentAchievements.Split(";").Contains(achievement))
        {
            PlayerPrefs.SetString("achievements", currentAchievements + achievement + ";");
            PlayerPrefs.Save();

            Debug.Log(achievement);
        }
    }

    private void Start()
    {
        if (GameObject.Find("AchievementHandler"))
        {
            Destroy(gameObject);
        }

        gameObject.name = "AchievementHandler";
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        string thisSceneName = SceneManager.GetActiveScene().name;

        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteKey("achievements");
            PlayerPrefs.Save();

            Debug.Log("achievements cleared");
        }

        if (hitless && moveScript && moveScript.hp < 120)
        {
            hitless = false;
        }

        if (thisSceneName != lastSceneName)
        {
            if (lastSceneName == "game")
            {
                if (thisSceneName == "LevelOne")
                {
                    GiveAchievement("Tutorial completed!");
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
            }
            else if (lastSceneName == "LevelTwo" && thisSceneName == "LevelThree")
            {
                GiveAchievement("Level 2 completed!");

                if (hitless)
                {
                    GiveAchievement("Level 2 completed without taking damage!");
                }
            }
            else if (lastSceneName == "LevelThree" && thisSceneName == "THE BOSS FIGHT")
            {
                GiveAchievement("Level 3 completed!");

                if (hitless)
                {
                    GiveAchievement("Level 3 completed without taking damage!");
                }
            }

            GameObject move = GameObject.Find("move");
            if (move)
                moveScript = move.GetComponent<move22>();

            hitless = true;
            lastSceneName = thisSceneName;
        }
    }
}
