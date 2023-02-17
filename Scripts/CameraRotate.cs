using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private GameObject target;
    private static float wantedRotation;
    //private static float speeed = 0.1f;
    private static float speeed = 180f;
    private static bool active = false;
    private static int sign = -1; 
    public void Start()
    {
        target = GameObject.Find("cameraTarget");
        wantedRotation = target.transform.localEulerAngles.x;
    }
    // Update is called once per frame
    public void Update()
    {
        if (active && Mathf.Abs(transform.localEulerAngles.x - wantedRotation) >5)
        {
            //target.transform.Rotate(0, sign*speed * Time.deltaTime, 0, Space.World);
            transform.Rotate(0, Mathf.PingPong(Time.deltaTime*speeed, 90),0);
            //transform.eulerAngles = new Vector3(0,Mathf.PingPong(Time.time*40, 180),0);
            //speed -= 0.00012f;
        }
        if (active && Mathf.Abs(transform.localEulerAngles.x - wantedRotation) > 0.5 && Mathf.Abs(transform.localEulerAngles.x - wantedRotation) <= 5)
        {
            transform.Rotate(0, Mathf.PingPong(Time.deltaTime * 30f, 90), 0);
        }

    }
    public static void rotateCam()
    {
        if (active && GameControl.Game != "PvB")
        {
            if (wantedRotation == 270)
            {
                wantedRotation = 90; 
            }
            else
            {
                wantedRotation = 270;
            }
        }
    }
    public static void startUp()
    {
        
        active = true;
        
    }
}
