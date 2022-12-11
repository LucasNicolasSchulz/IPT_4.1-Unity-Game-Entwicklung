using UnityEngine;
[System.Serializable]
public class SaveObject
{
  /*
   * Klasse für die Serialisierung der Daten
   */
  public int hat;               // Hut UD
  public float sens;            // Sensitivität der Maus
  public bool GameComplete;     // Ob das Spiel durchgespielt ist
  public Vector3 lastpos;       // Letzte Position des Spielers
  public Quaternion lastrot;    // Letzte Rotation des Spielers
  public Color skincolor;       // Hautfarbe
  public int life;              // Anzahl Leben
  public string Scene;          // Welche Szene der Spieler ist
  public SaveObject(Vector3 pos, string sce, int lif)
  {
    life = lif;
    lastpos = pos;
    Scene = sce;
  }
  public SaveObject(Vector3 pos, string sce)
  {
    lastpos = pos;
    Scene = sce;
  }
  public SaveObject(Vector3 pos)
  {
    lastpos = pos;
  }
  public SaveObject()
  {
  }
}