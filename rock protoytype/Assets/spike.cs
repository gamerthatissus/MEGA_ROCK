using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spi : MonoBehaviour
{
    public PolygonCollider2D collide;
    public int hits = 2;
    public LayerMask enemys;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator secondHit(Enemyscript E_S)
    {

        yield return new WaitForSeconds(0.1f);
      
        if (hits >= 1)
        {
            E_S.HP -= 30;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer==7 && hits>=1)
        {
            hits -= 1;

            Enemyscript ENemy_script = collision.gameObject.GetComponent<Enemyscript>();
            ENemy_script.HP -= 40;
            StartCoroutine(secondHit(ENemy_script));
        }
    }
}
