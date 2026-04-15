using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;

using static UnityEngine.RuleTile.TilingRuleOutput;


public class move22 : MonoBehaviour
{
    
    private enum Derection
    {
     Left,
     Right
    }
public bool MUDDY=false;

    public bool JustRespawned=true;
    public bool showedCOMbat = false;
    public bool showedLAUNCH = false;
    public bool showedTnt = false;
    public bool BURNT = false;
    public bool starteeed=false;
    public bool boss = false;
    private Derection lastDerection;
    public GameObject OG_earth_spike;
    public AudioClip DIE_sound;
    public AudioClip DIE_sound_lava;
    public AudioClip DIE_sound_quicksand;
    public AudioClip spike_enter;
    public AudioClip spike_leave;
    public boss_FIGHT_SCRIPT BOSS;

    public AudioClip ATTACKsound;
    public AudioClip BLOCKsound;
    
    public UnityEngine.Transform eye1;
    public UnityEngine.Transform eye2;

    public AudioClip Roll_rock;
    public AudioClip Roll_sand;
    public AudioClip Roll_Grass;


    public AudioClip LAUNCH;
    public AudioClip tntSOUND;
    public AudioClip KABOOM;
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
    public LayerMask floor;

    public LayerMask distructable_Layermask;
    public bool setnewspawn=false;
    public Vector2 spawn = new Vector2(0, 0);
    public bool canspend = true;
    public TextMeshProUGUI mana1;
    public TextMeshProUGUI mana2;
    public TextMeshProUGUI mana3;
    public TextMeshProUGUI mana4;
    public TextMeshProUGUI mana5;

    public TextMeshProUGUI move1;
    public TextMeshProUGUI move2;
    public TextMeshProUGUI move3;
    public TextMeshProUGUI move4;
    public TextMeshProUGUI move5;


    public float hp;
    public Slider hpbar;

    public GameObject pathRememberer;
    public GameObject PauseMenu;

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

    private bool Died=false;
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
    public bool unlockedLAUNCH = false;
public bool stalagtiteing=false;
    private float moveDirection;
    private Vector2 aimDirection;
    public RaycastHit2D hit;
    private bool launched;
    private bool attacked;
    private bool blocking;
    private bool surged;
    private bool placed;
    private bool restarted;
    public bool dropped;
    public bool paused;
    public GameObject AimRay;
    public bool ranout=false;
    public bool discovedSecret = false;
    public bool discovedSecret2 = false;

    public GameObject controls_BASIC;

    public GameObject controls_COMBAT;
    public GameObject controls_ROCK_LAUNCH;

    public event Action<int?, int?, int?, int?, int?> stalagsReset;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (Time.timeScale > 0)
        {
            moveDirection = math.sign(context.ReadValue<Vector2>().x);
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (Time.timeScale > 0)
        {
            aimDirection = context.ReadValue<Vector2>();
            if (aimDirection.x > .1 || aimDirection.x < -.1 || aimDirection.y > .1 || aimDirection.y < -.1)
                aimDirection.Normalize();
            else
                aimDirection = Vector2.zero;
            hit = Physics2D.Raycast(new Vector2(outsidemove.position.x, outsidemove.position.y), aimDirection, 20f, 1 << 10);
            AimRay.transform.rotation = Quaternion.Euler(0, 0, math.atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg);
        }
    }

    public void LaunchTrigger(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() && Time.timeScale > 0)
        {
            launched = true;
        }
    }

    public void AttackPressed(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() && Time.timeScale > 0 && (BOSS == null || BOSS.FIGHT_started))
        {
            attacked = true;
        }
    }

    public void BlockHeld(InputAction.CallbackContext context)
    {
        if (Time.timeScale > 0)
        {
            blocking = context.ReadValueAsButton();
        }
    }

    public void EarthSurged(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() && Time.timeScale > 0)
        {
            surged = true;
        }
    }

    public void TntPlaced(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() && Time.timeScale > 0)
        {
            placed = true;
        }
    }

    public void Restarted(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() && Time.timeScale > 0)
        {
            restarted = true;
        }
    }

    public void StalagtiteDropped(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() && hit && Time.timeScale > 0)
        {
            dropped = true;
        }
    }

    public void ResetStalag(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() && Time.timeScale > 0)
        {
            stalagsReset.Invoke(null, null, null, null, null);
        }
    }

    public void PauseGame(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() && Time.timeScale > 0)
        {
            paused = true;
        }
    }

    // Start is called before the first frame update

    IEnumerator waitstart()
    {
        starteeed = false;
        yield return new WaitForSeconds(2f);
        starteeed = true;
    }

    IEnumerator stone_SURGE_LOCK(Rigidbody2D launchhher)
    {
        if (launchhher != null)
        {
            float intitalrotation = launchhher.rotation;


            for (float i = 0; i < 20; i += Time.deltaTime)
            {
                if (launchhher != null)
                {
                    Collider2D[] floors = Physics2D.OverlapCircleAll(launchhher.position, 1f, floor);
                    bool conected = false;
                    foreach (Collider2D thingy in floors)
                    {
                        if (thingy.gameObject.CompareTag("floor"))
                        {
                            conected = true;

                        }
                    }
                    if (conected == false && launchhher.simulated == true)
                    {
                        launchhher.velocity = new Vector2(0, 0);
                        launchhher.angularVelocity = 0;
                    }


                    if (launchhher.rotation != intitalrotation)
                    {
                        launchhher.rotation = intitalrotation;
                    }
                    yield return null;
                }

            }
        }
    }
    IEnumerator ShowControls(SpriteRenderer thing_that_will_disaapere)
    {
        thing_that_will_disaapere.color = new Color(thing_that_will_disaapere.color.r, thing_that_will_disaapere.color.g, thing_that_will_disaapere.color.b, 255);
        
        yield return new WaitForSeconds(6);

        for (float i = 0; i<=(255/60); i += Time.deltaTime)
        {

            thing_that_will_disaapere.color = new Color32(255,255,255, (byte) Mathf.Clamp( 255 - (i * 65) ,0,255)  );
            
            yield return null;
        }

    }
    public void SHOW_COMBAT()
    {
        showedCOMbat = true;
        SpriteRenderer SR_combat = controls_COMBAT.GetComponent<SpriteRenderer>();
        StartCoroutine(ShowControls(SR_combat));


        move4.gameObject.transform.parent.gameObject.SetActive(true);
        mana4.gameObject.transform.parent.gameObject.SetActive(true);
        move5.gameObject.transform.parent.gameObject.SetActive(true);
        mana5.gameObject.transform.parent.gameObject.SetActive(true);
    }

    public void SHOW_TNT()
    {

        showedTnt = true;


        move3.gameObject.transform.parent.gameObject.SetActive(true);
        mana3.gameObject.transform.parent.gameObject.SetActive(true);


    }

    public void SHOW_totoreal_respawn()
    {

        showedTnt = true;
        showedCOMbat = true;
        showedLAUNCH = true;

        mana1.gameObject.transform.parent.gameObject.SetActive(true);
        move1.gameObject.transform.parent.gameObject.SetActive(true);

        move3.gameObject.transform.parent.gameObject.SetActive(true);
        mana3.gameObject.transform.parent.gameObject.SetActive(true);

        move3.gameObject.transform.parent.gameObject.SetActive(true);
        mana3.gameObject.transform.parent.gameObject.SetActive(true);

        move4.gameObject.transform.parent.gameObject.SetActive(true);
        mana4.gameObject.transform.parent.gameObject.SetActive(true);

        move5.gameObject.transform.parent.gameObject.SetActive(true);
        mana5.gameObject.transform.parent.gameObject.SetActive(true);
    }
    public void SHOW_LAUNCH()
    {

        move1.gameObject.transform.parent.gameObject.SetActive(true);
        mana1.gameObject.transform.parent.gameObject.SetActive(true);
        showedLAUNCH = true;
        SpriteRenderer SR_LAUNCH = controls_ROCK_LAUNCH.GetComponent<SpriteRenderer>();
        StartCoroutine(ShowControls(SR_LAUNCH));

    }
    void Start()
    {
        showedCOMbat = false;
        showedLAUNCH = false;
        showedTnt = false;
        if (controls_BASIC != null)
        {
            SpriteRenderer SR_basic = controls_BASIC.GetComponent<SpriteRenderer>();
            StartCoroutine(ShowControls(SR_basic));
        }
        if (controls_COMBAT != null)
        {
            SpriteRenderer SR_combat = controls_COMBAT.GetComponent<SpriteRenderer>();
            SR_combat.color = new Color32(255,255,255, (byte)0);

        }
        if (controls_ROCK_LAUNCH != null)
        {
            SpriteRenderer SR_launch = controls_ROCK_LAUNCH.GetComponent<SpriteRenderer>();
            SR_launch.color = new Color32(255, 255, 255, (byte)0);

        }
        starteeed = false;
        StartCoroutine(waitstart());
        BURNT = false;
        hpbar.maxValue = 120;
        Died = false;
        jumpCooldown = false;
        choosenPath = "none";
        path_choose.gameObject.SetActive(false);
        path_rigid.gameObject.SetActive(false);
        path_smooth.gameObject.SetActive(false);

        move1.text = "locked";
        move2.text = "locked";
        move3.text = "Place TNT";
        move4.text = "punch";
        move5.text = "block";

        mana1.text = "0";
        mana2.text = "0";
        mana3.text = "1 TNT";
        mana4.text = "1 stone";
        mana5.text = "free";

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

       
        
        hp = 120;
        
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

        //if (GameObject.Find("chosenPathRememberer").transform.position == Vector3.one)
        //{
            //RIGID();
        //}
        //else if (GameObject.Find("chosenPathRememberer").transform.position == Vector3.one * 2)
        //{
            //SMOOTH();
        //}

        start = 1;

        if (SceneManager.GetActiveScene().name == "game" || SceneManager.GetActiveScene().name == "MainMenu"|| SceneManager.GetActiveScene().name == "Titlescreen")
        {
            //mmmmmmmmmmmmmmmmmmmm
            move1.gameObject.transform.parent.gameObject.SetActive(false);
            move2.gameObject.transform.parent.gameObject.SetActive(false);
            move3.gameObject.transform.parent.gameObject.SetActive(false);
            move4.gameObject.transform.parent.gameObject.SetActive(false);
            move5.gameObject.transform.parent.gameObject.SetActive(false);
            mana1.gameObject.transform.parent.gameObject.SetActive(false);
            mana2.gameObject.transform.parent.gameObject.SetActive(false);
            mana3.gameObject.transform.parent.gameObject.SetActive(false);
            mana4.gameObject.transform.parent.gameObject.SetActive(false);
            mana5.gameObject.transform.parent.gameObject.SetActive(false);

        }
        else
        {
            showedCOMbat = true;
            showedLAUNCH = true;
            showedTnt = true;
            
            move1.gameObject.transform.parent.gameObject.SetActive(true);
            move2.gameObject.transform.parent.gameObject.SetActive(true);
            move3.gameObject.transform.parent.gameObject.SetActive(true);
            move4.gameObject.transform.parent.gameObject.SetActive(true);
            move5.gameObject.transform.parent.gameObject.SetActive(true);

            mana1.gameObject.transform.parent.gameObject.SetActive(true);
            mana2.gameObject.transform.parent.gameObject.SetActive(true);
            mana3.gameObject.transform.parent.gameObject.SetActive(true);
            mana4.gameObject.transform.parent.gameObject.SetActive(true);
            mana5.gameObject.transform.parent.gameObject.SetActive(true);

            path_choose.gameObject.SetActive(false);
            path_rigid.gameObject.SetActive(false);
            path_smooth.gameObject.SetActive(false);
            choosenPath = "rigid";
            mana1.text = "1 stone";
            move1.text = "stone launch";
            move5.text = "block";

            

            mana2.text = "3 stone";
            move2.text = "earth surge";
            phisics.friction = 1.5f;
        }

    }

    IEnumerator waitCanPUNCH()
    {
        canpunch = false;
        yield return new WaitForSeconds(0.3f);
        canpunch = true;

    }

    IEnumerator rocky_DIE()
    {
       
        AudioSource audeo = maincam.GetComponent<AudioSource>();
        audeo.loop = false;
        audeo.Stop();
        audeo.clip = DIE_sound;
        audeo.Play();
        
            yield return new WaitForSeconds(1f);

        

        Scene scenceString = SceneManager.GetActiveScene();
        if (scenceString.name == "THE BOSS FIGHT")
        {
            SceneManager.LoadScene("2 THE BOSS FIGHT");

        }
        else
        {
            SceneManager.LoadScene(scenceString.name);
        }


    }

    IEnumerator stoneSINK(Rigidbody2D RIG2d)
    {

       
        yield return new WaitForSeconds(2f);
       for (float i = 1; i < 6; i+=Time.deltaTime)
        {
            yield return null;
            if (RIG2d != null)
            {
                RIG2d.position = new Vector2(RIG2d.position.x, RIG2d.position.y - (1.5f * Time.deltaTime));

            }
            yield return null;

        }


    }
    IEnumerator spikeattack()
    {
        AudioSource rocky_sound=outsidemove.GetComponent<AudioSource>();
        rocky_sound.Stop();
        rocky_sound.loop = false;
        rocky_sound.clip = spike_enter;
        rocky_sound.Play();

        GameObject spike1 = Instantiate(OG_earth_spike);
        GameObject spike2 = Instantiate(OG_earth_spike);
        GameObject spike3 = Instantiate(OG_earth_spike);
                GameObject spike4 = Instantiate(OG_earth_spike);

        float oldPlayerx = outsidemove.position.x;
        if (lastDerection == Derection.Left)
        {
            spike1.transform.position = new Vector2(outsidemove.position.x - 0.5f, outsidemove.position.y - 0.8f);
            spike1.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

            spike2.transform.position = new Vector2(outsidemove.position.x - 1.2f, outsidemove.position.y - 1f);
            spike2.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

            spike3.transform.position = new Vector2(outsidemove.position.x - 2.1f, outsidemove.position.y - 1.2f);
            spike3.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);

            spike4.transform.position = new Vector2(outsidemove.position.x - 3.8f, outsidemove.position.y - 1.2f);
            spike4.transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
        }
        else
        {
            spike1.transform.position = new Vector2(outsidemove.position.x + 0.8f, outsidemove.position.y - 0.8f);
            spike1.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

            spike2.transform.position = new Vector2(outsidemove.position.x + 1.7f, outsidemove.position.y - 1f);
            spike2.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

            spike3.transform.position = new Vector2(outsidemove.position.x + 2.8f, outsidemove.position.y - 1.2f);
            spike3.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);

            spike4.transform.position = new Vector2(outsidemove.position.x + 3.8f, outsidemove.position.y - 1.2f);
            spike4.transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
        }
        float s1_pos = spike1.transform.position.y + 0.6f;
        float s2_pos = spike1.transform.position.y + 0.8f;
        float s3_pos = spike1.transform.position.y + 1f;
        float s4_pos = spike1.transform.position.y + 1.2f;

        for (int i = 0; i < 60; i++)
        {
            if (spike1.transform.position.y < s1_pos)
            {
                spike1.transform.position = new Vector2(spike1.transform.position.x, spike1.transform.position.y + 0.05f);
            }

            if (spike2.transform.position.y < s2_pos)
            {
                spike2.transform.position = new Vector2(spike2.transform.position.x, spike2.transform.position.y + 0.05f);

            }

            if (spike3.transform.position.y < s3_pos)
            {
                spike3.transform.position = new Vector2(spike3.transform.position.x, spike3.transform.position.y + 0.05f);

            }

            if (spike4.transform.position.y < s4_pos)
            {
                spike4.transform.position = new Vector2(spike4.transform.position.x, spike4.transform.position.y + 0.05f);

            }



            yield return null;
        }
        yield return new WaitForSeconds(2f);
        rocky_sound.Stop();
        rocky_sound.loop = false;
        rocky_sound.clip = spike_leave;
        rocky_sound.Play();
        for (int i = 0; i < 60; i++)
        {
          
                spike1.transform.position = new Vector2(spike1.transform.position.x, spike1.transform.position.y - 0.05f);
                spike2.transform.position = new Vector2(spike2.transform.position.x, spike2.transform.position.y - 0.05f);
                spike3.transform.position = new Vector2(spike3.transform.position.x, spike3.transform.position.y - 0.05f);

                spike4.transform.position = new Vector2(spike4.transform.position.x, spike4.transform.position.y - 0.05f);

            
            yield return null;
        }
        Destroy(spike1);
        Destroy(spike4);

        Destroy(spike2);
        Destroy(spike3);

    }
    IEnumerator PlaceTNT()
    {
        
        GameObject tntCLONE = Instantiate(tnt_OBJECT);
        UnityEngine.Transform tntCLONT_transform = tntCLONE.GetComponent<UnityEngine.Transform>();
        AudioSource tntCLONT_sound = tntCLONE.GetComponent<AudioSource>();
        GameObject OBJECT_exploshion= tntCLONE.transform.Find("EXPLOSHION").gameObject;
        UnityEngine.Transform EXPLOSHION = OBJECT_exploshion.GetComponent<UnityEngine.Transform>();
        SpriteRenderer spriteEXPLOSHION = OBJECT_exploshion.GetComponent<SpriteRenderer>();
        spriteEXPLOSHION.enabled = false;
        EXPLOSHION.localScale = new Vector2(0.1f, 0.1f);
        spriteEXPLOSHION.color = new Color(200, 0, 0, 0.2f);
        tntCLONT_sound.clip = tntSOUND;
        tntCLONT_sound.Play();
        tntCLONT_transform.position = new Vector2(outsidemove.position.x, outsidemove.position.y + 1.2f);

        //before exploshion
        yield return new WaitForSeconds(1f);
        //after exploshion

        spriteEXPLOSHION.enabled = true;
        
        tntCLONT_sound.clip = KABOOM;
        tntCLONT_sound.Play();
        Collider2D[] distructables = Physics2D.OverlapCircleAll(tntCLONT_transform.position, 3, distructable_Layermask);
    
        foreach (Collider2D distructablePART in distructables)
        {
            Destroy(distructablePART.gameObject);
            stone += 1;
        }

        Destroy(tntCLONE.GetComponent<SpriteRenderer>(), 0);

        Destroy(tntCLONE,2);

        for (float grow = 0; grow < 4; grow += 0.1f)
        {
            EXPLOSHION.localScale = new Vector2(0.1f+grow, 0.1f+grow);
            spriteEXPLOSHION.color = new Color(200, 0, 0, 0.8f-(grow/5));

            yield return null;
        }

    }
    // Update is called once per frame
    void Update()
    {

if (Input.GetKeyDown(KeyCode.K))
    {
stone_MAX=999999;
stone=999999;
hp=9999999;
    }
    
        if (Time.timeScale > 0)
        {
            if (hit)
            {
                AimRay.transform.position = new Vector3((hit.point.x + outsidemove.position.x) / 2, (hit.point.y + outsidemove.position.y) / 2, 0);
                AimRay.transform.localScale = new Vector3(hit.distance, .1f, 1);
            }
            else if (aimDirection != Vector2.zero)
            {
                AimRay.transform.position = new Vector3((aimDirection.x * 20 + outsidemove.position.x * 2) / 2, (aimDirection.y * 20 + outsidemove.position.y * 2) / 2, 0);
                AimRay.transform.localScale = new Vector3(20, .1f, 1);
            }
            else
            {
                AimRay.transform.localScale = Vector3.zero;
            }

            TNT_GUI.text = "Amount of TnT: " + TnT;

            if (Input.GetKeyDown(KeyCode.R) || restarted)
            {
                restarted = false;

                Scene scenceString = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scenceString.name);

            }
            if (Input.GetKeyDown(KeyCode.Escape) || paused)
            {
                paused = false;

                PauseMenu.transform.SetAsLastSibling();
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            if (hp < 1 && Died==false)
            {
                Died = true;
                SpriteRenderer REEEND = outsidemove.gameObject.GetComponent<SpriteRenderer>();
                REEEND.enabled = false;
                outsidemove.simulated = false;
                StartCoroutine(rocky_DIE());
            }
            //if (Input.GetKeyDown(KeyCode.R))
            //{
            //    Scene scenceString = SceneManager.GetActiveScene();
            //    SceneManager.LoadScene(scenceString.name);
            //}
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
                if ((Input.GetKey(KeyCode.D) || moveDirection == 1f) && blockMultiplier == 1)
                {
                    if (Mathf.Abs(outsidemove.velocity.magnitude) <= 15)
                    {
                        outsidemove.AddForce(Vector2.right * 350f * outsidemove.mass * Time.deltaTime, ForceMode2D.Force);

                    }

                }

                if ((Input.GetKey(KeyCode.A) || moveDirection == -1f) && blockMultiplier == 1)
                {
                    if (Mathf.Abs(outsidemove.velocity.magnitude) <= 15)
                    {
                        outsidemove.AddForce(Vector2.right * -300f * outsidemove.mass * Time.deltaTime, ForceMode2D.Force);

                    }

                }
            }



            else if (choosenPath == "none" || choosenPath== "rigid")
            {

                if ((Input.GetKey(KeyCode.D) || moveDirection == 1f) && blockMultiplier==1)
                {
                    lastDerection = Derection.Right;
                    bool onFLoor = false;

                    Collider2D[] floordetect = Physics2D.OverlapCircleAll(new Vector2(outsidemove.position.x, outsidemove.position.y), 1.2f);

                    onFLoor = false;
                    foreach (Collider2D enemyObjectfggsrwws in floordetect)
                    {

                        if (enemyObjectfggsrwws.gameObject.CompareTag("floor"))
                        {
                            onFLoor = true;

                        }

                    }

                    AudioSource audeo = insidemove.GetComponent<AudioSource>();
                    audeo.loop = false;

                    if (audeo.isPlaying == true && onFLoor == false)
                    {
                        audeo.Stop();
                    }

                    if (audeo.isPlaying != true && onFLoor == true)
                    {
                        Scene current_sceane = SceneManager.GetActiveScene();
                        switch (current_sceane.name)
                        {
                            case "game":
                                audeo.clip = Roll_rock;

                                break;

                            case "LevelOne":
                                audeo.clip = Roll_rock;
                                break;

                            case "LevelTwo":
                                audeo.clip = Roll_sand;

                                break;

                            case "LevelThree":
                                audeo.clip = Roll_Grass;
                                break;

                        }
                        audeo.volume = 0.05f + Mathf.Abs((outsidemove.angularVelocity / 200f) + (outsidemove.velocity.magnitude / 100f));
                       if (audeo.volume > 0.4f)
                        {
                            audeo.volume = 0.4f;
                        }
                        audeo.Play();
                    }
                    if (choosenPath == "none")
                    {


                        outsidemove.AddForce(Vector2.right * 10f * outsidemove.mass * Time.deltaTime, ForceMode2D.Force);
                    }
                }
                if ((Input.GetKey(KeyCode.A) || moveDirection == -1f) && blockMultiplier == 1)
                {
                    lastDerection = Derection.Left;
                    bool onFLoor = false;

                    Collider2D[] floordetect = Physics2D.OverlapCircleAll(new Vector2(outsidemove.position.x, outsidemove.position.y), 1.2f);

                    onFLoor = false;
                    foreach (Collider2D enemyObjecttttt in floordetect)
                    {

                        if (enemyObjecttttt.gameObject.CompareTag("floor"))
                        {
                            onFLoor = true;

                        }
                   
                    }

                    AudioSource audeo = insidemove.GetComponent<AudioSource>();
                    audeo.loop = false;

                    if (audeo.isPlaying == true && onFLoor == false)
                    {
                        audeo.Stop();
                    }

                    if (audeo.isPlaying != true && onFLoor==true)
                    {
                        Scene current_sceane = SceneManager.GetActiveScene();
                        switch (current_sceane.name)
                        {
                            case "game":
                                audeo.clip = Roll_rock;

                                break;

                            case "LevelOne":
                                audeo.clip = Roll_rock;
                                break;

                            case "LevelTwo":
                                audeo.clip = Roll_sand;

                                break;

                            case "LevelThree":
                                audeo.clip =Roll_Grass;
                                break;

                        }
                        audeo.volume = 0.01f + Mathf.Abs((outsidemove.angularVelocity / 200f) + (outsidemove.velocity.magnitude / 100f));
                        if (audeo.volume > 0.4f)
                        {
                            audeo.volume = 0.4f;
                        }
                        audeo.Play();
                    }

                    if (choosenPath == "none")
                    {
                        outsidemove.AddForce(Vector2.right * -10f * outsidemove.mass * Time.deltaTime, ForceMode2D.Force);

                    }

                }






            }
            if (Input.GetKeyDown(KeyCode.Space) || launched)
            {
                launched = false;

                // abcdefghi
                if (move1.text == "stone launch")
                {
                
                    if (stone >= 1)
                    {
                        AudioSource audeo = outsidemove.GetComponent<AudioSource>();
                        audeo.loop = false;
                        audeo.Stop();
                        audeo.clip = LAUNCH;
                        audeo.Play();

                        canspend = true;
                        StartCoroutine(spendstone(1));

                        Object block = Instantiate(launcher);
                        Destroy(block, 6f);

                        Object thingy = GameObject.Find("pow6");
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
                        UnityEngine.Transform blockT = block.GetComponent<UnityEngine.Transform>();
                        Rigidbody2D rigggg = block.GetComponent<Rigidbody2D>();
                        StartCoroutine(stoneSINK(rigggg));

                        rigggg.simulated = true;
                        Vector3 gopoint = new Vector3(outsidemove.position.x, outsidemove.position.y, 1);
                        Vector2 go = new Vector2(gopoint.x, gopoint.y);

                        Vector2 mouseeee;
                        if (aimDirection == Vector2.zero)
                            mouseeee = new Vector2(mousepos.x, mousepos.y);
                        else
                            mouseeee = aimDirection + go;

                        Vector2 facingDir = (mouseeee - go).normalized;
                        float angle = Mathf.Atan2(facingDir.y, facingDir.x) * Mathf.Rad2Deg - 90f;
                        blockT.rotation = Quaternion.Euler(0, 0, angle);
                        rigggg.rotation = angle;
                        //spaceeeee
                        Vector2 newpos = ((Vector2)(blockT.transform.up) * -1.5f);
                        rigggg.position = (go + newpos);



                        rigggg.AddRelativeForce(Vector2.up * 4150f, ForceMode2D.Impulse);
                        StartCoroutine(stone_SURGE_LOCK(rigggg));

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
            if ((Input.GetKeyDown(KeyCode.Alpha2) || surged) && blockMultiplier == 1f && stone>=3 && move2.text=="earth surge")
            {
                surged = false;

                //12345678987654321
                if (canpunch == true)
                {
                    StartCoroutine(waitCanPUNCH());

                     canpunch = false;
                    bool on_floor = false;
                    Collider2D[] floorparts = Physics2D.OverlapCircleAll(outsidemove.position, 1f, floor);
                    foreach (Collider2D floooor in floorparts)
                    {
                        on_floor = true;

                    }

                    if (on_floor == true)
                    {
                        StartCoroutine(spikeattack());

                        stone -= 3;

                    }



                }


            }

            if (Input.GetKeyDown(KeyCode.Alpha3) || placed)
            {
                placed = false;

                if (canpunch == true)
                {
                    if (move3.text == "Place TNT" && TnT >= 1)
                    {

                        TnT -= 1;
                        StartCoroutine(PlaceTNT());


                    }

                }


            }


            if ( ( Input.GetMouseButtonDown(0)  || attacked) && blockMultiplier==1f && stalagtiteing==false && showedCOMbat == true)
            {
                attacked = false;

                if (canpunch == true)
                {

                    AudioSource audeo = outsidemove.GetComponent<AudioSource>();
                    audeo.loop = false;
                    audeo.Stop();
                    audeo.clip = ATTACKsound;
                    audeo.Play();

                    if (move4.text == "punch" && stone >= 1)
                    {
                        StartCoroutine(waitCanPUNCH());

                        outsidemove.position = new Vector2(outsidemove.position.x, outsidemove.position.y + 0.3f);
                        eye1.position = new Vector2(eye1.position.x, eye1.position.y + 0.3f);
                        eye2.position = new Vector2(eye2.position.x, eye2.position.y + 0.3f);

                        if (outsidemove.angularVelocity > 0)
                        {
                            outsidemove.AddTorque(120);
                        }
                  else 
                        {
                            outsidemove.AddTorque(-120);
                        }
                        outsidemove.totalTorque = outsidemove.totalTorque * 2;


                        StartCoroutine(waitStopAttack());


                        stone -= 1;
                        Collider2D[] enemysUpunch = Physics2D.OverlapCircleAll(outsidemove.position, 1.5f, PLAYER_layermask);
                        foreach (Collider2D enemyObject in enemysUpunch)
                        {

                            Enemyscript enmtScript = enemyObject.gameObject.GetComponent<Enemyscript>();
                            enmtScript.HP -= 50;
                            if (enemyObject.gameObject.name == "MINION")
                            {
                                BOSS.Boss_Hp -= 50;
                            }
                        }
                    }

                }

           
            }
            if ( (Input.GetKey(KeyCode.F) || blocking) && showedCOMbat==true ) // block ability
            {
                if (blockMultiplier == 1)
                {
                    AudioSource audeo = outsidemove.GetComponent<AudioSource>();
                    audeo.loop = false;
                    audeo.Stop();
                    audeo.clip = BLOCKsound;
                    audeo.Play();
                }
                if (blockMultiplier == 1)
                {
                    blockMultiplier = 0.99f;

                }
                StartCoroutine(waitBLOCK());

       
            }
            else // can only move if not blocking
            {
                blockMultiplier = 1f;
                SpriteRenderer PSR = outsidemove.gameObject.GetComponent<SpriteRenderer>();
                PSR.color = Color.white;


            }
        }
    }
    IEnumerator waitBLOCK()
    {
 
        yield return new WaitForSeconds(0.1f);
        if (blockMultiplier == 0.99f)
        {
            SpriteRenderer PSR = outsidemove.gameObject.GetComponent<SpriteRenderer>();
            PSR.color = Color.gray;
            blockMultiplier = 0.2f;
        }
        

    }


    IEnumerator waitStopAttack()
    {
        outsidemove.velocity = new Vector2(outsidemove.velocity.x / 3, outsidemove.velocity.y / 3);

        yield return new WaitForSeconds(0.1f);
        outsidemove.totalTorque = outsidemove.totalTorque / 2;

        if (outsidemove.angularVelocity > 0)
        {
            outsidemove.AddTorque(-120);
        }
        else
        {
            outsidemove.AddTorque(120);
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
        //if (pathRememberer != null)

        //pathRememberer.gameObject.transform.position = Vector3.one;
        unlockedLAUNCH = true;

        path_choose.gameObject.SetActive(false);
        path_rigid.gameObject.SetActive(false);
        path_smooth.gameObject.SetActive(false);
        choosenPath = "rigid";
        mana1.text = "1 stone";
        move1.text = "stone launch";
        phisics.friction = 1.5f;
    }
    //public void SMOOTH()
    //{
        //if (pathRememberer != null)
            //pathRememberer.gameObject.transform.position = Vector3.one * 2;

        //path_choose.gameObject.SetActive(false);
        //path_rigid.gameObject.SetActive(false);
        //path_smooth.gameObject.SetActive(false);
        //choosenPath = "smooth";
        //mana1.text = "1 stone";
        //move1.text = "jump";
        //phisics.friction = 0.9f;
    //}

    public void spike(float obSpeed)
    {
        hp -= 0.15f * math.abs(oldspeed*oldspeed);
        //spikes should not be blocked becase it dosnt make sence, blocking should be primaraly for enemys and other stuff
        
    }
    public void dmg()
    {
        hp -= 20 ; // NOTE FOR FUTURE: multiply all damage by blockMultiplier if you think it should be able to be blocked


    }

    public void MILD_OW()
    {
        hp -= 2 ; // NOTE FOR FUTURE: multiply all damage by blockMultiplier if you think it should be able to be blocked


    }
    public void refundStone()
    {
        canspend = false;
    }

    public void KILL()
    {
        hp = 0;
        BURNT = true;
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
        if (boss == true)
        {
        if (hp < 120)
            {
                hp += 0.01f;

            }


        }
        StartCoroutine(waitforspeed());
        UnityEngine.Transform eyeoneCOlider = outsidemove.gameObject.transform.Find("coll1");
        UnityEngine.Transform eyeoneCOlider2 = outsidemove.gameObject.transform.Find("coll2");

        float distanceY_1 = Mathf.Abs(eyeoneCOlider.position.y - eye1.position.y);
        float distanceX_1 = Mathf.Abs(eyeoneCOlider.position.x - eye1.position.x);

        float distance_1 = Mathf.Sqrt((distanceX_1 * distanceX_1) + (distanceY_1 * distanceY_1));

        if (distance_1 > 0.25f)
        {

            eye1.position = eyeoneCOlider.position;
        }

        float distanceY_2 = Mathf.Abs(eyeoneCOlider2.position.y - eye2.position.y);
        float distanceX_2 = Mathf.Abs(eyeoneCOlider2.position.x - eye2.position.x);

        float distance_2 = Mathf.Sqrt((distanceX_2 * distanceX_2) + (distanceY_2 * distanceY_2));

        if (distance_2 > 0.25f)
        {

            eye2.position = eyeoneCOlider2.position;
        }

        if ((Input.GetKey(KeyCode.D) || moveDirection == 1f || Input.GetKey(KeyCode.A) || moveDirection == -1f) && blockMultiplier == 1f) // can only move if not blocking
        {
          
            if (speed < 2f)
            {
                speed = 2f;
            }
            if (speed < maxspeed && speed !=50)
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

                    if (Input.GetKey(KeyCode.D) || moveDirection == 1f)
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
                    if (Input.GetKey(KeyCode.D) || moveDirection == 1f)
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
                    if (Input.GetKey(KeyCode.D) || moveDirection == 1f)
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
            //        if (Input.GetKey(KeyCode.D) || moveDirection == 1f)
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
