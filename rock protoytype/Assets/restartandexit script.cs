using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class eeee : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void GOtoMENU ()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("TEST");

        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene("game");

        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene("LevelOne");

        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            SceneManager.LoadScene("LevelTwo");

        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene("LevelThree");

        }



    }

  


}
