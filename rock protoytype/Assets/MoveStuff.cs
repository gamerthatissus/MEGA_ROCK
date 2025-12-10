using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class move22 : MonoBehaviour
{
    public TextMeshProUGUI mana1;
    public TextMeshProUGUI mana2;
    public TextMeshProUGUI mana3;
    public TextMeshProUGUI mana4;

    public TextMeshProUGUI move1;
    public TextMeshProUGUI move2;
    public TextMeshProUGUI move3;
    public TextMeshProUGUI move4;
    
    public float hp;
    public Slider hpbar;

    

    public TextMeshProUGUI stoneTEXT;
    public Rigidbody2D insidemove;
    public Rigidbody2D outsidemove;
    public PhysicsMaterial2D phisics;
    public Camera maincam;
    public float maxmas = 0.9f;
    public float minmas = 0.3f;
    public Object launcher;
    public float minfriction = 0.4f;
    public float maxfriction = 1.2f;
    public int amountoflanchers=0;
    public int stone=5;


    
    public Vector3 mouseposstart;
    public Vector3 mousepos;


    // Start is called before the first frame update
    void Start()
    {
        move1.text = "stone launch";
        mana1.text = "space (2 stone)";
        
        hp = 100;
        
        maxmas = 1.2f;
        minmas = 0.3f;

        minfriction = 1f;
        maxfriction = 5f;
        stoneTEXT.text = "stone: " + stone;
    }

    // Update is called once per frame
    void Update()
    {
        hpbar.value = hp;
        stoneTEXT.text = "stone: " + stone;

        mouseposstart = Input.mousePosition;
        mousepos = maincam.ScreenToWorldPoint(mouseposstart);
        mousepos.z = 0;

        if (Input.GetKey(KeyCode.D))
        {
            insidemove.AddForce(Vector2.right * 600f * insidemove.mass * Time.deltaTime, ForceMode2D.Force);

        }

        if (Input.GetKey(KeyCode.A))
        {
            insidemove.AddForce(Vector2.right * -600f * insidemove.mass * Time.deltaTime, ForceMode2D.Force);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (move1.text=="stone launch")
            {
                if (stone >= 2)
                {
                    stone -= 2;
                    Object block = Instantiate(launcher);

                    Object thingy = GameObject.Find("pow10");
                    if (thingy != null)
                    {
                        Destroy(thingy);
                        block.name = "pow" + amountoflanchers;

                    }
                    else
                    {
                        amountoflanchers++;
                        block.name = "pow" + amountoflanchers;

                    }
                    Transform blockT = block.GetComponent<Transform>();
                    Rigidbody2D rigggg = block.GetComponent<Rigidbody2D>();

                    rigggg.simulated = true;
                    Vector3 gopoint = new Vector3(outsidemove.position.x, outsidemove.position.y, 1);
                    Vector2 go = new Vector2(gopoint.x, gopoint.y);

                    Vector2 mouseeee = new Vector2(mousepos.x, mousepos.y);


                    Vector2 facingDir = (mouseeee - go).normalized;
                    float angle = Mathf.Atan2(facingDir.y, facingDir.x) * Mathf.Rad2Deg - 90f;
                    blockT.rotation = Quaternion.Euler(0, 0, angle);
                    rigggg.rotation = angle;
                    Vector2 newpos = ((Vector2)(blockT.transform.up) * -1.2f);
                    rigggg.position = (go + newpos);



                    rigggg.AddRelativeForce(Vector2.up * 3000f, ForceMode2D.Impulse);

                }
            }
         


        }
    }

    public void Oww()
    {
        hp -= 10;
    }
    private void FixedUpdate()
    {



        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {

            if (insidemove.mass < maxmas)
            {
                if (insidemove.mass == minmas)
                {
                    if (Input.GetKey(KeyCode.D))
                    {
                        outsidemove.AddTorque(-70);

                    }
                    else
                    {
                        outsidemove.AddTorque(70);

                    }
                }
                insidemove.mass += 0.02f;
                phisics.friction += 0.02f;

            }
            else
            {
                phisics.friction = maxfriction;

                insidemove.mass = maxmas;
            }
        }
        else
        {
            if (insidemove.mass > minmas)
            {


                insidemove.mass -= 0.04f;
                phisics.friction -= 0.04f;



            }
            else
            {
                phisics.friction = minfriction;
                insidemove.mass = minmas;
            }
        }




    }


}
