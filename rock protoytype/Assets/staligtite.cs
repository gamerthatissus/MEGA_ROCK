using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class staligtite : MonoBehaviour
{
    public Vector3 mouseposstart;
    public Camera maincam;
    public Rigidbody2D stalag;
    public Vector3 mousepos;
    public SpriteRenderer stalag_sprite;
    public Rigidbody2D player;
    public GameObject move;

    private move22 moveScript;
    private Vector2 aimDirection;

    // Start is called before the first frame update
    void Start()
    {
        stalag.simulated = false;
        moveScript = move.GetComponent<move22>();
    }

    // Update is called once per frame
    void Update()
    {
        aimDirection = moveScript.aimDirection;

        mouseposstart = Input.mousePosition;
        mousepos = maincam.ScreenToWorldPoint(mouseposstart);
        mousepos.z = 0;

        float distanceY = Mathf.Abs(mousepos.y - stalag.position.y);
        float distanceX = Mathf.Abs(mousepos.x - stalag.position.x);

        float distance = Mathf.Sqrt((distanceX * distanceX) + (distanceY * distanceY));

        float distanceY2 = Mathf.Abs(player.position.y - stalag.position.y);
        float distanceX2 = Mathf.Abs(player.position.x - stalag.position.x);

        float distance2 = Mathf.Sqrt((distanceX2 * distanceX2) + (distanceY2 * distanceY2));

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(player.position.x, player.position.y), aimDirection, 20f, 1 << 10);
        if (hit.collider != null)
            Debug.Log(hit.collider);

        if (distance <= 0.5 && distance2<=6.5)
        {
            stalag_sprite.color = Color.green;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                stalag.simulated = true;
                AudioSource st_sound = stalag.gameObject.GetComponent<AudioSource>();
                st_sound.Play();
            }

        }
        else
        {
            stalag_sprite.color = Color.gray;


        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("cracked"))
        {
            AudioSource st_sound = stalag.gameObject.GetComponent<AudioSource>();
            st_sound.Play();
            Destroy(collision.gameObject,0.3f);
        }
    }
}
