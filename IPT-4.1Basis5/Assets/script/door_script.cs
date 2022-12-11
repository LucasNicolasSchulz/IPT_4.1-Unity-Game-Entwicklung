using UnityEngine;
using System.Collections;

public class door_script : MonoBehaviour
{
  private Animator anim;
  public AudioClip door_open;
  public AudioClip door_close;
  private AudioSource audiosource;
  private bool door;

  private void Start()
  {
    anim = GetComponent<Animator>();
    audiosource = GetComponent<AudioSource>();
  }
  void FixedUpdate()
  {
    if (Vector3.Distance(transform.position, GameObject.Find("Player").transform.position)<7.5f)
    {
      anim.SetBool("BTN 1", true);
    }

    else
    {
      anim.SetBool("BTN 1", false);
    }
  }

  void close_sound()
  {
    audiosource.clip = door_close;
    audiosource.Play();

  }
  void open_sound()
  {
    audiosource.clip = door_open;
    audiosource.Play();
  }

}
