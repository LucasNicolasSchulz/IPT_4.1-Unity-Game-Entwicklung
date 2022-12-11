using UnityEngine;
using UnityEngine.SceneManagement;

public class serialize : MonoBehaviour
{
  public SaveObject so;
  public bool LoadOnStart = false;
  public CameraManager x;

  //Load Data on start
  public void Start()
  {
    /*
     * Daten werden geladen wenn LoadOnStart auf true gesetzt ist.
     */
    if (LoadOnStart)
      load_data();
  }

  public void reset_data()
  {
    /*
     * Ruft die Funktion auf um Daten zu überscheiben um neu zu starten
     */
    SaveMa.reset();
    SceneManager.LoadScene("Level");
  }

  public void reset_world()
  {
    /*
     * Ladet die aktive Welt neu
     */
    string sc = SceneManager.GetActiveScene().name;
    SceneManager.LoadScene(sc);
  }

  public void load_data()
  {
    /*
     * Daten werden geladen
     */
    so = SaveMa.Load();
    Debug.Log(so.Scene);

    if (so.Scene == SceneManager.GetActiveScene().name || SceneManager.GetActiveScene().name == "Bossfight" || SceneManager.GetActiveScene().name == "Gang") //Falls die jetzige und gespeicherte Szene übereinstimmen
    {
      if (SceneManager.GetActiveScene().name == "Bossfight" || SceneManager.GetActiveScene().name == "Gang") //Falls der geladene Spielstand im Bosskampf ist. Wird der Charakter immer auf der gleichen Position starten
      {
        /*
         * Gibt die gelesenen Werte weiter
         */
        GetComponent<skincolor>().color = so.skincolor;
        transform.position = GameObject.Find("Spawnpoint").transform.position; // Man spawnt immer an einem bestimmten Punkt
        transform.rotation = so.lastrot;
        GameObject.Find("Canvas 1").GetComponent<UI>().current_life = so.life;
        GetComponent<hat>().activated_hat = so.hat;
      }
      else
      {
        /*
         * Gibt die gelesenen Werte weiter
         */
        transform.position = so.lastpos;
        transform.rotation = so.lastrot;
        GetComponent<skincolor>().color = so.skincolor;
        GetComponent<hat>().activated_hat = so.hat;
        GameObject.Find("Canvas 1").GetComponent<UI>().current_life = so.life;
      }
    }
    else
    {
      SceneManager.LoadScene(so.Scene); //Richtige szene der gespeicherten Datei wird geladen
    }
  }

  public void save_data()
  {
    /*
     * Daten werden von den einzelnen GameObjects genommen, in ein SaveObject gespeichert und weitergegeben
     */
    so.lastpos = this.transform.position;
    so.lastrot = this.transform.rotation;
    so.skincolor = GetComponent<skincolor>().color;
    so.hat = GetComponent<hat>().activated_hat;
    so.Scene = SceneManager.GetActiveScene().name;
    so.life = GameObject.Find("Canvas 1").GetComponent<UI>().current_life;
    SaveMa.Save(so);
  }

  public bool save_data_exist()
  {
    /*
     * Schaut, ob das Savefile exestiert und gibt einen Bool zurück
     */
    return SaveMa.file_exist();
  }

  public void delete_file()
  {
    /*
     * Löscht das Savefile
     */
    SaveMa.delete();
  }
}