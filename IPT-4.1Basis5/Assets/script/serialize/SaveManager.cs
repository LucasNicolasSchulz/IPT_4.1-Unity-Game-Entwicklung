using System.IO;
using UnityEngine;

public class SaveMa : MonoBehaviour
{
  public static string directory = "/SaveData/";
  public static string filename = "/Data.txt";

  public static void Save(SaveObject so)
  {
    /*
     * Speichert die übergebenen Daten der GameObjects (als SaveObject) in ein SaveObject (das was gespeichert wird.)
     */
    string dir = Application.persistentDataPath + directory;
    if (!Directory.Exists(dir))
      Directory.CreateDirectory(dir); // Ordner wird erstellt falls nicht vorhanden
    string json = JsonUtility.ToJson(so);
    File.WriteAllText(dir + filename, json);
    Debug.Log(dir);
  }

  public static SaveObject Load()
  {
    /*
     * Es ladet die SaveFile und gibt die Werte weiter zu den GameObjects (Falls File vorhanden), sonst erstellt es eine neue.
     */
    string fullpath = Application.persistentDataPath + directory + filename;
    SaveObject so = new SaveObject();

    if (File.Exists(fullpath)) // Falls datei exestiert
    { //Datei lesen
      string json = File.ReadAllText(fullpath);
      so = JsonUtility.FromJson<SaveObject>(json);
    }
    else //Falls nicht vorhanden
    { //Datei neu erstellen
      Debug.Log("Save file does not exist: Creating new");
      Save(new SaveObject(GameObject.Find("Spawnpoint").transform.position, "Level", 100));
      string json = File.ReadAllText(fullpath);
      so = JsonUtility.FromJson<SaveObject>(json);
    }
    return so; //SaveObject zurückgeben
  }

  public static void reset()
  {
    /*
    *Stellt ein neues SaveObject her. Es werden Werte gegeben, so dass man wieder am anfang startet.
     */
    string fullpath = Application.persistentDataPath + directory + filename;
    SaveObject so = new SaveObject();
    DirectoryInfo SaveFile = new DirectoryInfo(fullpath);
    Save(new SaveObject(GameObject.Find("Spawnpoint").transform.position, "Level", 100));
  }

  public static bool file_exist()
  {
    /*
     * Gibt einen bool zurück
     * Schaut, ob die Savefile exestiert oder nicht
     */
    string fullpath = Application.persistentDataPath + directory + filename;
    return File.Exists(fullpath) ? true : false;
  }

  public static void delete()
  {
    /*
     * Löscht das Savefile
     * Für 'new Game'
     */
    string fullpath = Application.persistentDataPath + directory + filename;
    File.Delete(fullpath);
  }
}
