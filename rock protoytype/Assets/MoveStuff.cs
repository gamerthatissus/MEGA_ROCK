using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;


public class move22 : MonoBehaviour
{
    public GameObject tnt_OBJECT;
    public TextMeshProUGUI TNT_GUI;
    public int TnT;
    private bool canpunch = true;
    private bool jumpCooldown=false;
    public RawImage stone_IMG;
    public RawImage stone_DARK;

    public Button path_rigid;
    public Button path_smooth;
    public Button path_choose;
    public LayerMask PLAYER_layermask;
    public LayerMask distructable_Layermask;

    public bool canspend = true;
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
    public int stone_MAX;

    public Object launcher;
    //public float minfriction = 0.4f;
    //public float maxfriction = 1.2f;
    public int amountoflanchers=0;


    public int stone=5;
    private float manaPOS = -17f;
    private float manaPOS2 = -17f;
    private float oldspeed = 0f;

    private float speed = 0;
    private float maxspeed = 8;

    public float blockMultiplier = 1f;

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
    private string choosenPath="none";
    private int start = 0;

    // Start is called before the first frame update
    void Start()
    {
        jumpCooldown = false;
        choosenPath = "none";
        path_choose.gameObject.SetActive(false);
        path_rigid.gameObject.SetActive(false);
        path_smooth.gameObject.SetActive(false);

        move1.text = "locked";
        move2.text = "locked";
        move3.text = "Place TNT";
        move4.text = "punch";
        mana1.text = "0";
        mana2.text = "0";
        mana3.text = "1 TNT";
        mana4.text = "1 stone";

        canspend = true;
        speed = 0;
        maxspeed = 8;
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

       
        
        hp = 100;
        
        maxmas = 1.2f;
        minmas = 0.3f;

        //minfriction = 1f;
        //maxfriction = 5f;
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

    IEnumerator waitCanPUNCH()
    {
        canpunch = false;
        yield return new WaitForSeconds(0.3f);
        canpunch = true;

    }
    IEnumerator PlaceTNT()
    {
        GameObject tntCLONE = Instantiate(tnt_OBJECT);
        Transform tntCLONT_transform = tntCLONE.GetComponent<Transform>();
        tntCLONT_transform.position = new Vector2(outsidemove.position.x, outsidemove.position.y + 1.2f);
        yield return new WaitForSeconds(1f);
        Collider2D[] distructables = Physics2D.OverlapCircleAll(tntCLONT_transform.position, 3, distructable_Layermask);
        foreach (Collider2D distructablePART in distructables)
        {
            Destroy(distructablePART.gameObject);
        }
        Destroy(tntCLONE);
    }
    // Update is called once per frame
    void Update()
    {
        TNT_GUI.text = "Amount of TnT: " + TnT;

        if (Input.GetKeyDown(KeyCode.R) || hp < 1)
        {
            Scene scenceString = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scenceString.name);

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scenceString = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scenceString.name);
        }
        hpbar.value = hp;
        if (start == 1)
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


        if (stone > stone_MAX)
        {
            stone = stone_MAX;
        }
        stoneTEXT.text = "stone: " + stone;

        mouseposstart = Input.mousePosition;
        mousepos = maincam.ScreenToWorldPoint(mouseposstart);
        mousepos.z = 0;


        if (choosenPath == "smooth")
        {
            if (Input.GetKey(KeyCode.D) && blockMultiplier == 1)
            {
                if (Mathf.Abs(outsidemove.velocity.magnitude) <= 15)
                {
                    outsidemove.AddForce(Vector2.right * 300f * outsidemove.mass * Time.deltaTime, ForceMode2D.Force);

                }

            }

            if (Input.GetKey(KeyCode.A) && blockMultiplier == 1)
            {
                if (Mathf.Abs(outsidemove.velocity.magnitude) <= 15)
                {
                    outsidemove.AddForce(Vector2.right * -300f * outsidemove.mass * Time.deltaTime, ForceMode2D.Force);

                }

            }
        }

        else if (choosenPath == "none")
        {

            if (Input.GetKey(KeyCode.D) && blockMultiplier==1)
            {
                outsidemove.AddForce(Vector2.right * 10f * outsidemove.mass * Time.deltaTime, ForceMode2D.Force);

            }
            if (Input.GetKey(KeyCode.A) && blockMultiplier == 1)
            {
                outsidemove.AddForce(Vector2.right * -10f * outsidemove.mass * Time.deltaTime, ForceMode2D.Force);

            }






        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (move1.text == "stone launch")
            {

                if (stone >= 2)
                {
                    canspend = true;
                    StartCoroutine(spendstone(2));

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
                    Vector2 newpos = ((Vector2)(blockT.transform.up) * -1.5f);
                    rigggg.position = (go + newpos);



                    rigggg.AddRelativeForce(Vector2.up * 4000f, ForceMode2D.Impulse);

                }
            } //e

            if (move1.text == "jump")
            {
                if (stone >= 1 && jumpCooldown==false)
                {
                    canspend = true;
                    stone -= 1;

                    jumpCooldown = true;
                    StartCoroutine(jumpwait());

                    outsidemove.AddForce(Vector2.up * 20f, ForceMode2D.Impulse);

                }
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (canpunch == true)
            {
                if (move3.text == "Place TNT" && TnT >= 1)
                {

                    TnT -= 1;
                    StartCoroutine(PlaceTNT());


                }

            }


        }


        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (canpunch == true)
            {
                if (move4.text == "punch" && stone >= 1)
                {
                    StartCoroutine(waitCanPUNCH());

                    stone -= 1;
                    Collider2D[] enemysUpunch = Physics2D.OverlapCircleAll(outsidemove.position, 1.5f, PLAYER_layermask);
                    foreach (Collider2D enemyObject in enemysUpunch)
                    {

                        Enemyscript enmtScript = enemyObject.gameObject.GetComponent<Enemyscript>();
                        enmtScript.HP -= 50;

                    }
                }

            }

           
        }
        if (Input.GetKey(KeyCode.F)) // block ability
        {
            blockMultiplier = 0.3f;
        }
        else // can only move if not blocking
        {
            blockMultiplier = 1f;



        }
    }


    IEnumerator spendstone(int amount)
        {

            yield return new WaitForSeconds(0.03f);

            if (canspend == true)
            {
                stone -= amount;
            }


        }
    IEnumerator jumpwait()
    {
       
        yield return new WaitForSeconds(2f);

        jumpCooldown = false;


    }

    public void choosepath()
    {
        path_choose.gameObject.SetActive(true);
        path_rigid.gameObject.SetActive(true);
        path_smooth.gameObject.SetActive(true);
        phisics.friction = 1f;

    }

    public void RIGID()
    {
        path_choose.gameObject.SetActive(false);
        path_rigid.gameObject.SetActive(false);
        path_smooth.gameObject.SetActive(false);
        choosenPath = "rigid";
        mana1.text = "2 stone";
        move1.text = "stone launch";
        phisics.friction = 1.5f;
    }
    public void SMOOTH()
    {
        path_choose.gameObject.SetActive(false);
        path_rigid.gameObject.SetActive(false);
        path_smooth.gameObject.SetActive(false);
        choosenPath = "smooth";
        mana1.text = "1 stone";
        move1.text = "jump";
        phisics.friction = 0.9f;
    }

    public void spike(float obSpeed)
    {
        hp -= 0.15f * math.abs(oldspeed*oldspeed) * blockMultiplier;
        
        
    }
    public void dmg()
    {
        hp -= 30 * blockMultiplier; // NOTE FOR FUTURE: multiply all damage by blockMultiplier if you think it should be able to be blocked


    }

    public void refundStone()
    {
        canspend = false;
    }

    public void KILL()
    {
        hp = 0;
    }

    public void HOLE()
    {
        hp -= 20; // no blockMultiplier here because you shouldn't be able to block pit damage
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


        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) && blockMultiplier == 1f) // can only move if not blocking
        {


            if (speed < maxspeed)
            {
                speed += 0.1f;

            }
            else
            {
                speed = maxspeed;

            }

            switch (choosenPath)
            {



                case "rigid":

                    if (Input.GetKey(KeyCode.D))
                    {
                        if (Mathf.Abs(outsidemove.angularVelocity) <= 2000f)
                        {
                            outsidemove.AddTorque(speed * (-1.2f));

                        }

                    }
                    else
                    {
                        if (Mathf.Abs(outsidemove.angularVelocity) <= 2000f)
                        {
                            outsidemove.AddTorque(speed * (1.2f));

                        }

                    }
                    break;

                case "none":
                    if (Input.GetKey(KeyCode.D))
                    {
                        if (Mathf.Abs(outsidemove.angularVelocity) <= 1800f)
                        {
                            outsidemove.AddTorque(speed * (-1));

                        }


                    }
                    else
                    {
                        if (Mathf.Abs(outsidemove.angularVelocity) <= 1800f)
                        {
                            outsidemove.AddTorque(speed);

                        }

                    }
                    break;



                case "smooth":
                    if (Input.GetKey(KeyCode.D))
                    {
                        if (Mathf.Abs(outsidemove.angularVelocity) <= 1500f)
                        {
                            outsidemove.AddTorque(speed * (-0.5f));

                        }

                    }
                    else
                    {
                        if (Mathf.Abs(outsidemove.angularVelocity) <= 1500f)
                        {
                            outsidemove.AddTorque(speed * (0.5f));

                        }

                    }
                    break;
            }
           

            //if (insidemove.mass < maxmas)
            //{
            //    if (insidemove.mass == minmas)
            //    {
            //        if (Input.GetKey(KeyCode.D))
            //        {
            //            outsidemove.AddTorque(-70);

            //        }
            //        else
            //        {
            //            outsidemove.AddTorque(70);

            //        }
            //    }
            //    insidemove.mass += 0.02f;
            //    phisics.friction += 0.02f;

            //}
            //else
            //{
            //    phisics.friction = maxfriction;

            //    insidemove.mass = maxmas;
            //}
        }
        else
        {

            if (speed < 0.1f)
            {
                speed = 0;
            }
            else
            {
                speed *= 0.95f;

            }
            //if (insidemove.mass > minmas)
            //{


            //    insidemove.mass -= 0.04f;
            //    phisics.friction -= 0.04f;



            //}
            //else
            //{
            //    phisics.friction = minfriction;
            //    insidemove.mass = minmas;
            //}
        }




    }


}
