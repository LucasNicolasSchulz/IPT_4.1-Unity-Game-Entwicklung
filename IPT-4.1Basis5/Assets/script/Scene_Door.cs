using UnityEngine;

public class Scene_Door : MonoBehaviour
{
  void FixedUpdate()
  {
    if (Vector3.Distance(this.transform.position, GameObject.Find("Player").transform.position) < 5) // Wenn Distanz zwischen Spieler unt Türe unter 5 ist
    {
      GameObject.Find("SceneManager").GetComponent<SceneChange>().NextScene_activate = true; // Wechselt zur nächsten Szene
    }
  }
}
