using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class timerendscript : MonoBehaviour
{
    public TextMeshProUGUI timerrrr;
    public timeleftscript wwwwwww;
    public AudioClip defalt_music;
    
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        wwwwwww.awardEScapebadge();
        timerrrr.enabled = false;
        wwwwwww.Timeer = 100;
        wwwwwww.cannumber = false;

        AudioSource cam_music = Camera.main.GetComponent<AudioSource>();
        cam_music.Stop();
        cam_music.loop = true;
        cam_music.clip = defalt_music;
        cam_music.Play();
    }
}
