using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
  public string NextScene; // die nächste Szene
  private Animator anim; // animator
  public bool NextScene_activate; // aktivierung für Szenenwechsel
  public bool is_dead; // Falls Spieler tod ist

  void Start()
  {
    anim = GetComponent<Animator>(); // Holt sich den Animator
  }

  void FixedUpdate()
  {
    if (NextScene_activate) // Wenn 'NextScene_activate' true ist, dann setzt es den Bool 'ChangeScene' auf true (animation)
      anim.SetBool("ChangeScene", true);
  }

  void LoadScene()
  {
    /*
     * Wechselt auf die nächste Szene oder falls spieler tot ist auf Level zurück
     * Methode wird von andern Scripts aufgerufen
     */
    if (is_dead)
    {
      SceneManager.LoadScene("Level");
    }
    else
    {
      SceneManager.LoadScene(NextScene);
      PlayerPrefs.SetFloat("Timer", GameObject.Find("Timer").GetComponent<Timer>().timeRemaining);
    }
  }
}
