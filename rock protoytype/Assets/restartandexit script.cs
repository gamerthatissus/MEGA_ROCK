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
        SceneManager.LoadScene("100%menu");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


        
      

    }

  


}
