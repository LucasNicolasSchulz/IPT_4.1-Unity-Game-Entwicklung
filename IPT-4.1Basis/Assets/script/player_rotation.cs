using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_rotation : MonoBehaviour
{
    //rotation
    public Transform nacke;
    public Transform capsula;
    public Transform camera1;

    public AudioListener FPSaudio;

    public float sensitivity = 10.0f;
    public float ZoomSensitivity = 1.0f;
    public float maxYAngle = 80.0f;
    private Vector2 currentRotation;

    private bool camtrigger;
    public Camera fpscam;
    public float fov = 90;
    private float zoom = 45;
    void Start()
    {
        fpscam.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // rotation
        if (!GetComponent<UI>().menu_active)
        {
            currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
            currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
            currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
            currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
            nacke.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
            camera1.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
            capsula.transform.rotation = Quaternion.Euler(0, currentRotation.x, 0);
        }


        if(camtrigger)
        {
            fpscam.enabled = false;
            FPSaudio.enabled = false;
        }

        //fov
        fpscam.fieldOfView = fov;
    }

    public void setSens(string i)
    {
        Debug.Log("ses");
        int  y;
        bool isNumeric = int.TryParse(i, out y);
        if (isNumeric)
            sensitivity = y;
    }
}
