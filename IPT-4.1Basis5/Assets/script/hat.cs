using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hat : MonoBehaviour
{
    public int activated_hat; // Hut 'ID'
    private Transform head; // Transform vom Kopf

    void Start()
    {
        head = GameObject.Find("hat_container").transform; // Sucht den hat_container
    }

    void FixedUpdate()
    {
        for(int i = 0; i < head.childCount; i++) // Für jeden hut...
        {
            head.GetChild(i).gameObject.SetActive(i == activated_hat-1?true:false); //Falls Hut ID übereinstimmt, aktiviert es den Hut
        }
    }

    public void setHat(string i)
    {
        Debug.Log("hat");
        int y;
        bool isNumeric = int.TryParse(i, out y);
        if(isNumeric)
            activated_hat = y;
    }
}
