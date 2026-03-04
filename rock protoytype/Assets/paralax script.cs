using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralaxscript : MonoBehaviour
{
    // Start is called before the first frame update
    public float OG_x;
    public float OG_y;

    public Camera maincam;
    public float paralax_x;
    public float paralax_y;

    public Vector2 OG_campos;
    void Start()
    {
        OG_x = gameObject.transform.position.x;
        OG_y = gameObject.transform.position.y;

        OG_campos = maincam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float Ofset = (maincam.transform.position.x-OG_campos.x) * paralax_x;
        float Ofset2 = (maincam.transform.position.y - OG_campos.y) * paralax_y;

        gameObject.transform.position = new Vector2(OG_x + Ofset,OG_y+Ofset2);
    }
}
