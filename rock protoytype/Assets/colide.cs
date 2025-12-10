using JetBrains.Annotations;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colide : MonoBehaviour
{
    public move22 thingyyyy;




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name=="OW")
        {
            thingyyyy.Oww();

        }
    }


}
