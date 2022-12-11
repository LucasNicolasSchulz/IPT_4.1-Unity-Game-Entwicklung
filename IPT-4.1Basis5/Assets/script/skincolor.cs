using UnityEngine;

public class skincolor : MonoBehaviour
{
  public Color color; // Farbe
  public Material skin; // Hautfarbe Material

  void FixedUpdate()
  {
    skin.color = color; //Ändert die Farbe vom Material
  }

  public void Hex2RGB(string input)
  {
    /*
     * Gibt einen String und convertet es zu color
     */
    ColorUtility.TryParseHtmlString(input, out color);
  }
}
