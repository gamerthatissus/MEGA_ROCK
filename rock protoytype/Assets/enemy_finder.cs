using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

public class enemy_finder : MonoBehaviour
{

    public Camera cam;
    public Transform rayOriginTransform;
    public Transform Target;
    public float edgebuffer = 50f;

    private bool offScreen = false;


   // public GameObject enemy;
   // private GameObject FINDER;
   // private Vector2 enemySreenPos;
   // private SpriteRenderer find_render;
   // public int invisLastframe = 0;
    // Start is called before the first frame update
    void Start()
    {
        Target = gameObject.transform.parent.transform;

        cam = Camera.main;

        //FINDER = gameObject;
        //find_render = FINDER.GetComponent<SpriteRenderer>();
        //enemySreenPos = Mmaincamera.WorldToScreenPoint( enemy.transform.position);
        //FINDER.transform.position = enemy.transform.position;
    }

    void Update()
    {

        if (cam == null) cam = Camera.main;
        if (cam == null) return;

        //
        //
        //THIS FOLLOWING CODE CHECKS IF THE ENEMY IS IN THE SCRREEN, and eneables the sprite renderer if it isnt.
        //
        // 

        Vector3 screenpos = cam.WorldToScreenPoint(Target.position);

        bool isOffScreen =                     //IF any of these condishions are true, then isoffscreen will be true. 
            screenpos.x < -50 || screenpos.x > Screen.width + 50 ||
            screenpos.y < -50 || screenpos.y > Screen.height + 50; 

        if (isOffScreen)
        {
            if (Target.name.Contains("OG"))
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false; //gets spriterender and  enables it in the same line of code.
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true; //gets spriterender and  enables it in the same line of code.
                //Debug.Log("enemy is far away");
            }
              

        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false; //gets spriterender and  disables it in the same line of code.
            //Debug.Log("you cannot sleep now, there are enemys nearby");
            return;
        }

        //
        //gets possition of the player and makes shrue its there
        //

        Vector2 origin = rayOriginTransform != null ?   //its basicly a inline if-else (officaly called a terinary operator) if the ray origin is there, the origin is rayorigin transform, otherwize its transform.position
            (Vector2)rayOriginTransform.position :
            (Vector2)transform.position;

        //
        //gets derection between player and enemy;
        //

        Vector2 raydereaction = ((Vector2)Target.position - origin).normalized;

        //
        //gets intersection point
        //
        Vector2 insersection = GetRayViewPortIntersection(origin, raydereaction);


        //clamps intersection point (just in case_
        float angle = Mathf.Atan2(raydereaction.y, raydereaction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        screenpos = cam.WorldToScreenPoint(insersection);
        screenpos.x = Mathf.Clamp(screenpos.x, edgebuffer, Screen.width - edgebuffer);
        screenpos.y = Mathf.Clamp(screenpos.y, edgebuffer, Screen.width - edgebuffer);


        //sets position to intersection point
        Vector3 worldpos = cam.ScreenToWorldPoint(screenpos);

        worldpos.z = 0;
        transform.position = worldpos;

    }

    public Vector2 GetRayViewPortIntersection(Vector2 origin, Vector2 derection)
    {
        if (cam == null) cam = Camera.main;
        if (derection == Vector2.zero)
        {
            Debug.LogWarning("derection is 0");
            return origin;

        }
        derection.Normalize();



        //
        //creates ray from player to enemy
        //
        Vector3 rayorgin = new Vector3(origin.x, origin.y, 0f);
        Vector3 rayderection = new Vector3(derection.x, derection.y, 0f);
        Ray ray = new Ray(rayorgin, rayderection);



        //
        //get 6 frustum planes (left, right, bottom,top, near,far)
        //
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);

        float closestDistance = float.PositiveInfinity;
        Vector3 instersectionpoint = origin;
        
        //
        //makes another raycast from player in the dececton of enemy but in the camaera, stoping at edge.
        //
        foreach (Plane plane in planes)
        {
            if (plane.Raycast(ray, out float distance))
            {

                //only consiter positive disanced (in ray deraction) and the closest one)

                if (distance > 0.001f && distance < closestDistance)
                {
                    closestDistance = distance;
                    instersectionpoint = ray.GetPoint(distance);
                }
            }
        }
        //return intersection point
        return new Vector2(instersectionpoint.x, instersectionpoint.y);

    }

}
