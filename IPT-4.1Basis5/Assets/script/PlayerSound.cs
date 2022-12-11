using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
  public AudioClip[] steps;
  public AudioClip land;
  public LayerMask groundLayer;
  private AudioSource audiosource;

  private void Start()
  {
    audiosource = GetComponentInChildren<AudioSource>();
    groundLayer = GetComponent<PlayerLocomotion>().groundLayer;
  }

  private void FixedUpdate()
  {
    /*
     * Wenn es einen bestimmten Abstand zwischen Spieler und Boden erreicht hat, spielt es den 'landing' sound ab
     */
    RaycastHit hit;
    Vector3 rayCastOrigin = transform.position;
    rayCastOrigin.y = rayCastOrigin.y + 0.5f;
    if (!Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
    {
      landing();
    }
  }

  void landing()
  {
    /*
     * ändert auf den richtigen sound um und spielt es ab
     */
    audiosource.clip = land;
    audiosource.Play();
  }

  void step()
  {
    /*
     * ändert auf einen zufälligen sound im Array um und spielt es ab
     */
    audiosource.clip = steps[Random.Range(0, steps.Length)];
    audiosource.Play();
  }
}
