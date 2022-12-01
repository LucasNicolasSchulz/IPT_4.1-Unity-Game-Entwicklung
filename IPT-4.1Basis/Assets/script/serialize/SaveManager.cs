using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveMa : MonoBehaviour
{
    public static string directory = "/SaveData/";
    public static string filename = "/Data.txt";

    public static void Save(SaveObject so)
    {
        string dir = Application.persistentDataPath + directory;
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
        string json = JsonUtility.ToJson(so);
        File.WriteAllText(dir + filename, json);
        Debug.Log(dir);
    }

    public static SaveObject Load()
    {
        string fullpath = Application.persistentDataPath + directory + filename;
        SaveObject so = new SaveObject();

        if (File.Exists(fullpath))
        {
            string json = File.ReadAllText(fullpath);
            so = JsonUtility.FromJson<SaveObject>(json);
            Debug.Log("File read'd");
        }
        else
        {
            Debug.Log("Save file does not exist");
        }

        return so;
    }
}
