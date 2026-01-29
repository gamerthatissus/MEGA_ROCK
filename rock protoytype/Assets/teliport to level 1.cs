using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teliporttolevel1 : MonoBehaviour
{
    public int mode = 1;
    public move22 move2222;
    public GameObject pathRememberer;
    // Start is called before the first frame update
    

    // Update is called once per frame
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.gameObject.name =="player")
        {
            if (mode == 1)
            {
                move2222.choosepath();

            }
            else if (mode==2)
            {
                DontDestroyOnLoad(pathRememberer.gameObject);
                SceneManager.LoadScene("LevelOne");
            }
            else if (mode == 3)
            {
                SceneManager.LoadScene("LevelTwo");
            }
            else if (mode == 4)
            {
                SceneManager.LoadScene("LevelThree");
            }
            else if (mode == 5)
            {
                SceneManager.LoadScene("Boss");
            }
        }

    }
}
