using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
  private int life = 4;
  private ParticleSystem explosion;
  private Animator animator;
  private Quaternion lookRotation;
  private Vector3 direction;
  private Rigidbody rb;
  private Transform eye0;
  private Transform eye1;
  private AudioSource explosion_sound;

  [Header("General")]
  public GameObject Player;
  public Camera camPos;
  public bool start;
  public int Touchdamage;
  public GameObject bossbar;

  [Header("Movement")]
  public float RotationSpeed;
  public float speed;
  public float animationspeed;

  [Header("Wheels")]
  public GameObject[] wheels;

  [Header("Explosion")]
  public float power;
  public float radius;
  public float lift;

  private void Start()
  {
    rb = GetComponent<Rigidbody>();
    animator = GetComponent<Animator>();
    explosion = GameObject.Find("Explosion").GetComponent<ParticleSystem>();
    eye0 = GameObject.Find("EyeL").transform;
    eye1 = GameObject.Find("EyeR").transform;
    animator.speed = 0;
    explosion_sound = GameObject.Find("Explosion_sound").GetComponent<AudioSource>();
  }
  void FixedUpdate()
  {
    if (start) //Boss Start
    {
      //Animations geschwindigkeit
      animator.speed = animationspeed;

      //Rotation zum Player
      direction = (Player.transform.position - transform.position).normalized;
      lookRotation = Quaternion.LookRotation(direction);
      transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
      transform.rotation = Quaternion.Euler(new Vector3(0f, transform.rotation.eulerAngles.y, 0f));

      rb.velocity = transform.forward * speed; //Bewegung zum Spieler

      for (int i = 0; i < 4; i++) //Schaden (Räder verschwinden)
      {
        wheels[i].SetActive(!(life <= (Mathf.Abs(i - 3)))); // Je nach leben, hat es weniger Räder
      }

      if (life <= 0) // Wenn es kein Leben hat
      {
        GameObject.Find("SceneManager").GetComponent<SceneChange>().NextScene_activate = true; //lädt die nächste Szene
      }

      bossbar.GetComponentInChildren<Slider>().value = life; // Value vom Slider (Lebensanzeige)wird auf das leben gesetzt
    }
    //Augen rotation (Zur Kamera)
    eye0.LookAt(camPos.transform.position);
    eye1.LookAt(camPos.transform.position);
  }

  void OnCollisionEnter(Collision collision)
  {
    if (start) // Wenn Bosskampf startet
    {
      if (collision.gameObject.tag == "Bomb") //Wenn Kollision mit einer Bombe
      {
        life--; //Zieht Leben ab
        collision.gameObject.SetActive(false); //Deaktiviert die Bombe
        explosion.Play(); //Startet Partikel-Animation
        explosion_sound.Play(); //Startet Explision-Sound
      }
      else if (collision.gameObject.tag == "Player") // Wenn Kollision mit dem Spieler
      {
        GameObject.Find("Canvas 1").GetComponent<UI>().getDamage(Touchdamage); //Ruft die Methode für Schaden auf
        Debug.Log(Touchdamage);
      }
    }
  }
}