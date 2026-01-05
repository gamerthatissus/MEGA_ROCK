using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class move22 : MonoBehaviour
{
    public RawImage stone_IMG;
    public RawImage stone_DARK;
    


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


    public Canvas gameScreen;
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
    public int stone_MAX=5;
    public int stone=5;
    private float manaPOS = -17f;
    private float manaPOS2 = -17f;
    private float oldspeed = 0f;

    public Vector3 mouseposstart;
    public Vector3 mousepos;
    private RawImage stone2;
    private RawImage stone3;
    private RawImage stone4;
    private RawImage stone5;
    private RawImage stone6;
    private RawImage stone7;
    private RawImage stone8;
    private RawImage stone9;
    private RawImage stone10;
    private RawImage stone11;
    private RawImage stone12;
    private RawImage stone13;
    private RawImage stone14;
    private RawImage stone15;
    private RawImage stone16;

    private int start = 0;

    // Start is called before the first frame update
    void Start()
    {
        oldspeed = 0f;
        if (stone_MAX<=0)
        {
            stone_MAX = 5;
        }
        start = 0;
        RectTransform rockrect = stone_IMG.GetComponent<RectTransform>();
        RectTransform rockrect2= stone_DARK.GetComponent<RectTransform>();

        manaPOS = rockrect.localPosition.x;
        manaPOS = rockrect2.localPosition.x;

        move1.text = "stone launch";
        mana1.text = "space (2 stone)";
        
        hp = 100;
        
        maxmas = 1.2f;
        minmas = 0.3f;

        minfriction = 1f;
        maxfriction = 5f;
        stoneTEXT.text = "stone: " + stone;

       

        manaPOS2 -= ((70 * stone_MAX) / 2);
        rockrect2.localPosition = new Vector3(manaPOS2, -272, 0);
        for (int i = 1; i < 16; i++)
        {
            Object NEWmana = Instantiate(stone_DARK.gameObject);
            RectTransform CLONEpos = NEWmana.GetComponent<RectTransform>();
            manaPOS2 += 70;
            CLONEpos.SetParent(gameScreen.gameObject.transform);

            CLONEpos.localScale = new Vector3(0.7f, 0.7f, 0);
            CLONEpos.localPosition = new Vector3(manaPOS2, -272, 0);
            NEWmana.name = "DARKstone" + (i + 1);
            if (i >= stone_MAX)
            {
                CLONEpos.localPosition = new Vector3(manaPOS2, -1272, 0);

            }

            stone = stone_MAX;
        }

        manaPOS -= ((70 * stone_MAX) / 2);
        rockrect.localPosition = new Vector3(manaPOS, -272, 0);

        for (int i = 1; i < 16; i++)
        {
            Object NEWmana = Instantiate(stone_IMG.gameObject);
            RectTransform CLONEpos = NEWmana.GetComponent<RectTransform>();
            manaPOS += 70;
            CLONEpos.SetParent(gameScreen.gameObject.transform);

            CLONEpos.localScale = new Vector3(0.7f, 0.7f, 0);
            CLONEpos.localPosition = new Vector3(manaPOS, -272, 0);
            NEWmana.name = "stone" + (i+1);
            if (i >= stone_MAX)
            {
                CLONEpos.localPosition = new Vector3(manaPOS, -1272, 0);

            }


        }
    
        stone2 = GameObject.Find("stone2").GetComponent<RawImage>();
        stone3 = GameObject.Find("stone3").GetComponent<RawImage>();
        stone4 = GameObject.Find("stone4").GetComponent<RawImage>();
        stone5 = GameObject.Find("stone5").GetComponent<RawImage>();
        stone6 = GameObject.Find("stone6").GetComponent<RawImage>();
        stone7 = GameObject.Find("stone7").GetComponent<RawImage>();
        stone8 = GameObject.Find("stone8").GetComponent<RawImage>();
        stone9 = GameObject.Find("stone9").GetComponent<RawImage>();
        stone10 = GameObject.Find("stone10").GetComponent<RawImage>();
        stone11 = GameObject.Find("stone11").GetComponent<RawImage>();
        stone12 = GameObject.Find("stone12").GetComponent<RawImage>();
        stone13 = GameObject.Find("stone13").GetComponent<RawImage>();
        stone14 = GameObject.Find("stone14").GetComponent<RawImage>();
        stone15 = GameObject.Find("stone15").GetComponent<RawImage>();
        stone16 = GameObject.Find("stone16").GetComponent<RawImage>();
        start = 1;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || hp<1)
        {
            SceneManager.LoadScene("game");

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("game");

        }
        hpbar.value = hp;
if (start==1)
        {
            switch (stone)
            {
                case 0:
                    stone_IMG.enabled = false;
                    stone2.enabled = false;
                    stone3.enabled = false;
                    stone4.enabled = false;
                    stone5.enabled = false;
                    stone6.enabled = false;
                    stone7.enabled = false;
                    stone8.enabled = false;
                    stone9.enabled = false;
                    stone10.enabled = false;
                   
                    stone11.enabled = false;
                    stone12.enabled = false;
                    stone13.enabled = false;
                    stone14.enabled = false;

                    stone15.enabled = false;
                    stone16.enabled = false;
                    break;
                case 1:
                    stone_IMG.enabled = true;
                    stone2.enabled = false;
                    stone3.enabled = false;
                    stone4.enabled = false;
                    stone5.enabled = false;
                    stone6.enabled = false;
                    stone7.enabled = false;
                    stone8.enabled = false;
                    stone9.enabled = false;
                    stone10.enabled = false;

                    stone11.enabled = false;
                    stone12.enabled = false;
                    stone13.enabled = false;
                    stone14.enabled = false;


                    stone15.enabled = false;
                    stone16.enabled = false;

                    break;

                case 2:
                    stone_IMG.enabled = true;
                    stone2.enabled = true;
                    stone3.enabled = false;
                    stone4.enabled = false;
                    stone5.enabled = false;
                    stone6.enabled = false;
                    stone7.enabled = false;
                    stone8.enabled = false;
                    stone9.enabled = false;
                    stone10.enabled = false;

                    stone11.enabled = false;
                    stone12.enabled = false;
                    stone13.enabled = false;
                    stone14.enabled = false;

                    stone15.enabled = false;
                    stone16.enabled = false;
                    break;

                case 3:
                    stone_IMG.enabled = true;
                    stone2.enabled = true;
                    stone3.enabled = true;
                    stone4.enabled = false;
                    stone5.enabled = false;
                    stone6.enabled = false;
                    stone7.enabled = false;
                    stone8.enabled = false;
                    stone9.enabled = false;
                    stone10.enabled = false;

                    stone11.enabled = false;
                    stone12.enabled = false;
                    stone13.enabled = false;
                    stone14.enabled = false;

                    stone15.enabled = false;
                    stone16.enabled = false;
                    break;

                case 4:
                    stone_IMG.enabled = true;
                    stone2.enabled = true;
                    stone3.enabled = true;
                    stone4.enabled = true;
                    stone5.enabled = false;
                    stone6.enabled = false;
                    stone7.enabled = false;
                    stone8.enabled = false;
                    stone9.enabled = false;
                    stone10.enabled = false;

                    stone11.enabled = false;
                    stone12.enabled = false;
                    stone13.enabled = false;
                    stone14.enabled = false;

                    stone15.enabled = false;
                    stone16.enabled = false;
                    break;

                case 5:
                    stone_IMG.enabled = true;
                    stone2.enabled = true;
                    stone3.enabled = true;
                    stone4.enabled = true;
                    stone5.enabled = true;
                    stone6.enabled = false;
                    stone7.enabled = false;
                    stone8.enabled = false;
                    stone9.enabled = false;
                    stone10.enabled = false;

                    stone11.enabled = false;
                    stone12.enabled = false;
                    stone13.enabled = false;
                    stone14.enabled = false;

                    stone15.enabled = false;
                    stone16.enabled = false;
                    break;

                case 6:
                    stone_IMG.enabled = true;
                    stone2.enabled = true;
                    stone3.enabled = true;
                    stone4.enabled = true;
                    stone5.enabled = true;
                    stone6.enabled = true;
                    stone7.enabled = false;
                    stone8.enabled = false;
                    stone9.enabled = false;
                    stone10.enabled = false;

                    stone11.enabled = false;
                    stone12.enabled = false;
                    stone13.enabled = false;
                    stone14.enabled = false;

                    stone15.enabled = false;
                    stone16.enabled = false;
                    break;

                case 7:
                    stone_IMG.enabled = true;
                    stone2.enabled = true;
                    stone3.enabled = true;
                    stone4.enabled = true;
                    stone5.enabled = true;
                    stone6.enabled = true;
                    stone7.enabled = true;
                    stone8.enabled = false;
                    stone9.enabled = false;
                    stone10.enabled = false;

                    stone11.enabled = false;
                    stone12.enabled = false;
                    stone13.enabled = false;
                    stone14.enabled = false;

                    stone15.enabled = false;
                    stone16.enabled = false;
                    break;

                case 8:
                    stone_IMG.enabled = true;
                    stone2.enabled = true;
                    stone3.enabled = true;
                    stone4.enabled = true;
                    stone5.enabled = true;
                    stone6.enabled = true;
                    stone7.enabled = true;
                    stone8.enabled = true;
                    stone9.enabled = false;
                    stone10.enabled = false;

                    stone11.enabled = false;
                    stone12.enabled = false;
                    stone13.enabled = false;
                    stone14.enabled = false;

                    stone15.enabled = false;
                    stone16.enabled = false;
                    break;

                case 9:
                    stone_IMG.enabled = true;
                    stone2.enabled = true;
                    stone3.enabled = true;
                    stone4.enabled = true;
                    stone5.enabled = true;
                    stone6.enabled = true;
                    stone7.enabled = true;
                    stone8.enabled = true;
                    stone9.enabled = true;
                    stone10.enabled = false;

                    stone11.enabled = false;
                    stone12.enabled = false;
                    stone13.enabled = false;
                    stone14.enabled = false;

                    stone15.enabled = false;
                    stone16.enabled = false;
                    break;

                case 10:
                    stone_IMG.enabled = true;
                    stone2.enabled = true;
                    stone3.enabled = true;
                    stone4.enabled = true;
                    stone5.enabled = true;
                    stone6.enabled = true;
                    stone7.enabled = true;
                    stone8.enabled = true;
                    stone9.enabled = true;
                    stone10.enabled = true;

                    stone11.enabled = false;
                    stone12.enabled = false;
                    stone13.enabled = false;
                    stone14.enabled = false;

                    stone15.enabled = false;
                    stone16.enabled = false;
                    break;

                case 11:
                    stone_IMG.enabled = true;
                    stone2.enabled = true;
                    stone3.enabled = true;
                    stone4.enabled = true;
                    stone5.enabled = true;
                    stone6.enabled = true;
                    stone7.enabled = true;
                    stone8.enabled = true;
                    stone9.enabled = true;
                    stone10.enabled = true;

                    stone11.enabled = true;
                    stone12.enabled = false;
                    stone13.enabled = false;
                    stone14.enabled = false;

                    stone15.enabled = false;
                    stone16.enabled = false;
                    break;

                case 12:
                    stone_IMG.enabled = true;
                    stone2.enabled = true;
                    stone3.enabled = true;
                    stone4.enabled = true;
                    stone5.enabled = true;
                    stone6.enabled = true;
                    stone7.enabled = true;
                    stone8.enabled = true;
                    stone9.enabled = true;
                    stone10.enabled = true;

                    stone11.enabled = true;
                    stone12.enabled = true;
                    stone13.enabled = false;
                    stone14.enabled = false;

                    stone15.enabled = false;
                    stone16.enabled = false;
                    break;

                case 13:
                    stone_IMG.enabled = true;
                    stone2.enabled = true;
                    stone3.enabled = true;
                    stone4.enabled = true;
                    stone5.enabled = true;
                    stone6.enabled = true;
                    stone7.enabled = true;
                    stone8.enabled = true;
                    stone9.enabled = true;
                    stone10.enabled = true;

                    stone11.enabled = true;
                    stone12.enabled = true;
                    stone13.enabled = true;
                    stone14.enabled = false;

                    stone15.enabled = false;
                    stone16.enabled = false;
                    break;

                case 14:
                    stone_IMG.enabled = true;
                    stone2.enabled = true;
                    stone3.enabled = true;
                    stone4.enabled = true;
                    stone5.enabled = true;
                    stone6.enabled = true;
                    stone7.enabled = true;
                    stone8.enabled = true;
                    stone9.enabled = true;
                    stone10.enabled = true;

                    stone11.enabled = true;
                    stone12.enabled = true;
                    stone13.enabled = true;
                    stone14.enabled = true;

                    stone15.enabled = false;
                    stone16.enabled = false;
                    break;

                case 15:
                    stone_IMG.enabled = true;
                    stone2.enabled = true;
                    stone3.enabled = true;
                    stone4.enabled = true;
                    stone5.enabled = true;
                    stone6.enabled = true;
                    stone7.enabled = true;
                    stone8.enabled = true;
                    stone9.enabled = true;
                    stone10.enabled = true;

                    stone11.enabled = true;
                    stone12.enabled = true;
                    stone13.enabled = true;
                    stone14.enabled = true;

                    stone15.enabled = true;
                    stone16.enabled = false;
                    break;

                case 16:
                    stone_IMG.enabled = true;
                    stone2.enabled = true;
                    stone3.enabled = true;
                    stone4.enabled = true;
                    stone5.enabled = true;
                    stone6.enabled = true;
                    stone7.enabled = true;
                    stone8.enabled = true;
                    stone9.enabled = true;
                    stone10.enabled = true;

                    stone11.enabled = true;
                    stone12.enabled = true;
                    stone13.enabled = true;
                    stone14.enabled = true;

                    stone15.enabled = true;
                    stone16.enabled = true;
                    break;
                default:

                    break;

            }
        }
       

        if (stone>stone_MAX)
        {
            stone = stone_MAX;
        }
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

                    Object thingy = GameObject.Find("pow100");
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
 
public void spike()
    {
        hp -= 0.15f * math.abs(oldspeed*oldspeed);
        
        
    }
    public void dmg()
    {
        hp -= 30;


    }

    public void KILL()
    {
        hp = 0;
    }

    public void HOLE()
    {
        hp -= 20;
        StartCoroutine(holeeee());
        
    }

    IEnumerator holeeee()
    {
        yield return new WaitForSeconds(0.7f);
       
     
        outsidemove.AddForce(Vector2.up * 100f/outsidemove.mass, ForceMode2D.Impulse);
        
    }

    IEnumerator waitforspeed()
    {
        yield return new WaitForSeconds(0.02f);
        oldspeed = outsidemove.velocity.magnitude;


    }

    private void FixedUpdate()
    {
        StartCoroutine(waitforspeed());


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
