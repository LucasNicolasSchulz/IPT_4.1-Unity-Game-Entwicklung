using UnityEngine;
using UnityEngine.Video;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
  private VideoPlayer vp;
  public string nextscene;
  void Start()
  {
    vp = GetComponent<VideoPlayer>();
    vp.Play(); //Startet das Video direkt am anfang
    StartCoroutine(CheckVideo()); //Startet eine Couroutine
  }

  IEnumerator CheckVideo (){
    /*
     * Wenn das Video nicht mehr am abspielen ist (also fertig ist), dann wechselt es die Szene
     */
    while (vp.isPlaying) // Wartet, bis Video fertig ist
    {
      yield return new WaitForEndOfFrame();
    }
    SceneManager.LoadScene(nextscene); // wehcselt Szene
  }
}
