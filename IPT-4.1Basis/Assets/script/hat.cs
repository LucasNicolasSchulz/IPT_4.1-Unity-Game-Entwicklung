using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hat : MonoBehaviour
{
    public int activated_hat;
    private Transform head;
    public GameObject test;

    // Start is called before the first frame update
    void Start()
    {
        head = GameObject.Find("hat_container").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i = 0; i < head.childCount; i++)
        {
            head.GetChild(i).gameObject.SetActive(i == activated_hat-1?true:false);
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
