using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour
{
  public int damage;
  private GameObject Player;
  public float AttackRadius, ShootRadius;
  private Rigidbody rb;
  public Vector3 speed;
  private Animator anim;
  public Material Mat1, Mat2;
  private bool shoot_activate;
  public GameObject life_script;
  private LineRenderer line;
  private float laser_offset;
  private AudioSource soundeffect;
  private Vector3 startpos;
  void Start()
  {
    rb = this.GetComponent<Rigidbody>();
    anim = this.GetComponent<Animator>();
    Player = GameObject.Find("Player");
    line = this.GetComponent<LineRenderer>();
    startpos = transform.position;
    soundeffect = GetComponent<AudioSource>();
    Color();
    InvokeRepeating("laser_anim", 0, 0.01f);

    shoot_activate = false;
  }

  void FixedUpdate()
  {
    if (Vector3.Distance(this.transform.position, Player.transform.position) <= AttackRadius) // Wenn Spieler nah genug ist
    {
      if (Vector3.Distance(this.transform.position, Player.transform.position) <= ShootRadius) // Wenn Spike nah genug ist zum schiessen
      {
        this.transform.LookAt(Player.transform); // Schaut zum Spieler
        anim.SetBool("Attack", true); // Ändert die Animation
        StartCoroutine(shoot());
      }
      else // sonst bewege zu spieler
      {
        this.transform.LookAt(Player.transform);
        Move(speed);
        anim.SetBool("Attack", false);
        anim.SetBool("Move", true);
      }
    }
    else // sonst Idle
    {
      if (Vector3.Distance(transform.position, startpos) > 2.5)
      {
        Move(speed / 2);
        this.transform.LookAt(startpos);
      }
      else
      {
        rb.velocity = Vector3.zero;
      }
      anim.SetBool("Move", false);
      anim.SetBool("Attack", false);
    }
  }

  void Move(Vector3 input)
  {
    rb.AddRelativeForce(input);
  }

  void Color()
  {
    //Color//
    float r = Random.value, g = Random.value, b = Random.value;
    Mat1.color = new Color(r, g, b);
    Mat2.color = new Color(r, g, b);
  }

  IEnumerator shoot()
  {
    //Shoot
    int offset_value = 2; // Wert, damit es nicht immer trifft
    Vector3 shoot_offset = new Vector3(Random.Range(-offset_value, offset_value), Random.Range(-offset_value, offset_value), Random.Range(-offset_value, offset_value));

    if (!shoot_activate)
    {
      shoot_activate = true; // Setzt das bool auf true, damit es nicht mehrmals ausführt

      //Laser
      Vector3[] Position = { this.transform.position, GameObject.Find("Player").transform.position + shoot_offset}; // Positionen für die Linie wird ermittelt
      line.SetPositions(Position); // Positionen der Linien werden gesetzt
      line.enabled = true; // Linie wird aktiviert

      //Soundeffect
      soundeffect.Play(); // Sound wird abgespielt

      yield return new WaitForSeconds(0.25f); // Nach 0.25s wird die Linie deaktiviert
      line.enabled = false;

      yield return new WaitForSeconds(1.25f); // Wartet zusätzlich 1.25s
      shoot_activate = false;
    }
  }
  void laser_anim()
    {
      // Laser animation
      line.material.SetTextureOffset("_MainTex", new Vector2(laser_offset, -0.67f));
      laser_offset = laser_offset - 0.1f;
    }
}

