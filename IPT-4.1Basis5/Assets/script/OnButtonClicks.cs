using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButtonClicks : MonoBehaviour
{
    InputManager inputManager;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
    }

    private void FixedUpdate()
    {
        if (inputManager.p_Input)
        {
            GameObject.Find("Canvas 1").GetComponent<UI>().menu_active = !GameObject.Find("Canvas 1").GetComponent<UI>().menu_active;
            inputManager.p_Input = false;
        }
    }
}
