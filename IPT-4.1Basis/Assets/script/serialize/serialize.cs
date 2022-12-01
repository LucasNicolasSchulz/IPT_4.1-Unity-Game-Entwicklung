using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class serialize : MonoBehaviour
{
    public SaveObject so;

    //Load Data on start
    public void Start()
    {
        load_data();

    }

    private void FixedUpdate()
    {
        //Data saev
        if (Input.GetKey(KeyCode.O))
        {
            save_data();
        }

        //Date Reset
        if (Input.GetKey(KeyCode.I))
        {
            reset_data();
        }
    }

    public void reset_data()
    {
        so.lastpos = Vector3.zero;
        so.lastrot = Quaternion.identity;
        so.hat = 0;
        so.sens = 10;
        so.skincolor = Color.red;
        SaveMa.Save(so);
        string sc = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sc);
    }

    public void load_data()
    {
        so = SaveMa.Load();
        transform.position = so.lastpos;
        transform.rotation = so.lastrot;
        GetComponent<skincolor>().color = so.skincolor;
        GetComponent<hat>().activated_hat = so.hat;
        GetComponent<player_rotation>().sensitivity = so.sens;
    }

    public void save_data()
    {
        so.lastpos = this.transform.position;
        so.lastrot = this.transform.rotation;
        so.skincolor = GetComponent<skincolor>().color;
        so.hat = GetComponent<hat>().activated_hat;
        so.sens = GetComponent<player_rotation>().sensitivity;
        SaveMa.Save(so);
    }
}
