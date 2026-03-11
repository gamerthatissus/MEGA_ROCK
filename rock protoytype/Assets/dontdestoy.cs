using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Canvas cannnvas;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

    }

    private void Update()
    {
        cannnvas.worldCamera = Camera.main;   
    }


}
