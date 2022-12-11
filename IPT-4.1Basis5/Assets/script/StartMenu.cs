using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
  public Vector3 camRotation;
  public GameObject Camera;
  private serialize serialize;
  private void Start()
  {
    serialize = GetComponent<serialize>(); // Holt sich das Serialize Skript
    if (!serialize.save_data_exist()) // Wenn Daten vorhanden sind
    {
      GameObject.Find("Resume_Button").GetComponent<Button>().interactable = false; // Aktiviert 'Resume' button
    }
  }

  private void FixedUpdate()
  {
    Camera.transform.Rotate(camRotation); // Rotiert die Kamera
  }

  public void GetHelp()
  {
    /*
     * Öffnet den Browser mit der URL
     */
    Application.OpenURL("https://spacegame.joel-erni1.bbzwinf.ch/");
  }

  public void ResumeGame()
  {
    /*
     * Wechselt es die Szene
     */
    serialize.delete_file();

    GameObject.Find("SceneManager").GetComponent<SceneChange>().NextScene_activate = true;
  }

  public void NewGame()
  {
    /*
     * Löscht den Spielstand und wechselt die Szene
     */
    serialize.delete_file();
    GameObject.Find("SceneManager").GetComponent<SceneChange>().NextScene_activate = true;
  }
}
