using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class timerendscript : MonoBehaviour
{
    public TextMeshProUGUI timerrrr;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        timerrrr.enabled = false;
    }
}
