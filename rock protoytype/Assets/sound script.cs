using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class soundscript : MonoBehaviour
{
    public AudioClip water;
    public AudioSource A_Sorse;
    public AudioSource A_Sorse2;

    public float AudioClipLength;
    public float Volume;
    public int tick = 0;
    // Start is called before the first frame update
    void Start()
    {
        AudioClipLength = water.length;
        A_Sorse.loop = false;
        A_Sorse2.loop = true;
        A_Sorse2.playOnAwake = true;
        A_Sorse.playOnAwake = false;
        A_Sorse.volume = 1;
        StartCoroutine(LOAD());
    }

    IEnumerator LOAD()
    {
        while (true)
        {
            if (tick < 20)
            {
                tick++;
                A_Sorse.volume -= 0.05f;
                A_Sorse2.volume += 0.05f;
            }
            else if (tick>80)
            {
                tick++;
                A_Sorse.volume += 0.05f;
                A_Sorse2.volume -= 0.05f;
            }
            else
            {
                tick++;
            }
           

            if (tick >= 100)
            {
                tick = 0;
                A_Sorse.volume = 1;
                A_Sorse2.volume = 0;
                A_Sorse.Play();
            }

            yield return null;

        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
