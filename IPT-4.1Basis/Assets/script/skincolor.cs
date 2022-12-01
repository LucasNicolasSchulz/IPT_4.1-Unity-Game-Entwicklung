using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skincolor : MonoBehaviour
{
    public Color color; //
    public string hexcolor;
    public Material skin; //Get Material

    private void Start()
    {
    }

    void FixedUpdate()
    {
        //Changes Skin
        skin.color=color;
    }

    public void Hex2RGB(string input)
    {
        ColorUtility.TryParseHtmlString(input, out color);
    }
}
