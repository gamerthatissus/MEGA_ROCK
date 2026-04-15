using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teliporttolevel1 : MonoBehaviour
{
    public bool boss = false;
    public int mode = 1;
    public move22 move2222;
    public GameObject pathRememberer;
    // Start is called before the first frame update
    

    // Update is called once per frame
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.gameObject.name =="player")
        {

            if (boss == true)
            {
                SceneManager.LoadScene("THE BOSS FIGHT");

            }
            else
            {
                if (mode == 1)
                {
                    move2222.RIGID();
                    
                    if (move2222.showedLAUNCH == false)
                    {
                        move2222.SHOW_LAUNCH();

                    }
                }
                else if (mode == 2)
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
                    SceneManager.LoadScene("THE BOSS FIGHT");
                }
                else if (mode == 500)
                {
                    move2222.JustRespawned = false;
                    move2222.insidemove.position = new Vector2(2,0);
                    move2222.outsidemove.position = new Vector2(2,0);
                    Camera.main.transform.parent.gameObject.transform.position = new Vector2(2,0);
                    
                }
                else if (mode == 200 && move2222.showedTnt == false)
                {
                    move2222.SHOW_TNT();
                }
                else if (mode==100 && move2222.showedCOMbat==false)
                {
                    move2222.SHOW_COMBAT();
                }
            }


          
        }

    }
}
