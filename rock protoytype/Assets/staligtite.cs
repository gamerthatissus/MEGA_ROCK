using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class staligtite : MonoBehaviour
{
    public Vector3 mouseposstart;
    public Camera maincam;
    public Rigidbody2D stalag;
    public Vector3 mousepos;
    public SpriteRenderer stalag_sprite;
    public Rigidbody2D player;
    // Start is called before the first frame update
    void Start()
    {
        stalag.simulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        mouseposstart = Input.mousePosition;
        mousepos = maincam.ScreenToWorldPoint(mouseposstart);
        mousepos.z = 0;

        float distanceY = Mathf.Abs(mousepos.y - stalag.position.y);
        float distanceX = Mathf.Abs(mousepos.x - stalag.position.x);

        float distance = Mathf.Sqrt((distanceX * distanceX) + (distanceY * distanceY));

        float distanceY2 = Mathf.Abs(player.position.y - stalag.position.y);
        float distanceX2 = Mathf.Abs(player.position.x - stalag.position.x);

        float distance2 = Mathf.Sqrt((distanceX2 * distanceX2) + (distanceY2 * distanceY2));

        if (distance <= 0.5 && distance2<=6.5)
        {
            stalag_sprite.color = Color.green;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                stalag.simulated = true;

            }

        }
        else
        {
            stalag_sprite.color = Color.gray;


        }

    }
}
