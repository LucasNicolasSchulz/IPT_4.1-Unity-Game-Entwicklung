using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class intro : MonoBehaviour
{
    public float time; // Zeit, bis zur Szenenwechsel
    private void Start()
    {
        StartCoroutine(skip_wait()); //Startet Coroutine
    }

    IEnumerator skip_wait()
    {
        yield return new WaitForSeconds(time); //Wartet eine bestimmte Länge (Länge des Intros)
    GameObject.Find("SceneManager").GetComponent<SceneChange>().NextScene_activate = true; // Wechselt zur nächsten Szene
    }
}
