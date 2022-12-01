using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI : MonoBehaviour
{
    public int startlife_amount;
    public int current_life;
    private GameObject slider;
    public bool menu_active;
    public GameObject pausemenu;
    public GameObject healthbar;

    void Start()
    {
        slider = GameObject.Find("life");
        current_life = startlife_amount;
        slider.GetComponent<Slider>().maxValue = startlife_amount;
    }

    void FixedUpdate()
    {
        slider.GetComponent<Slider>().value = current_life;

        if (Input.GetKeyDown(KeyCode.P))
        {
            menu_active = !menu_active;
        }

        pausemenu.SetActive(menu_active ? true : false);
        healthbar.SetActive(menu_active ? false : true);

        Cursor.lockState = menu_active ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void getDamage(int damage)
    {
        current_life -= damage;
        if (current_life <= 0)
            death();
    }

    public void death()
    {
        Debug.Log("Player Daed");
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            //Get damage every second
        }
    }

    public void menu()
    {
        menu_active=false;
    }

    public void exit()
    {
        Application.Quit();
    }
}
