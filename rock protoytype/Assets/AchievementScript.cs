using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementScript : MonoBehaviour
{
    private string lastSceneName;

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

        if (thisSceneName != lastSceneName)
        {
            if (lastSceneName == "game")
            {
                if (thisSceneName == "LevelOne")
                {
                    Debug.Log("Completed the tutorial!");
                }
                else if (thisSceneName == "THE BOSS FIGHT")
                {
                    Debug.Log("Found the secret portal!");
                }
            }
            else if (lastSceneName == "LevelOne" && thisSceneName == "LevelTwo")
            {
                Debug.Log("Completed level 1!");
            }
            else if (lastSceneName == "LevelTwo" && thisSceneName == "LevelThree")
            {
                Debug.Log("Completed level 2!");
            }
            else if (lastSceneName == "LevelThree" && thisSceneName == "THE BOSS FIGHT")
            {
                Debug.Log("Completed level 3!");
            }

            lastSceneName = thisSceneName;
        }
    }
}
