using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private UI life_script;
    private LineRenderer line;
    private float laser_offset;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
        Player = GameObject.Find("Player");
        life_script = Player.GetComponent<UI>();
        line = this.GetComponent<LineRenderer>();
        Color();
        InvokeRepeating("shoot", 0, 1f);
        InvokeRepeating("laser_anim", 0, 0.01f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //anim.SetBool("Move", false);
        if (Vector3.Distance(this.transform.position, Player.transform.position) <= AttackRadius) // wenn Spieler nah genug ist
        {
            if (Vector3.Distance(this.transform.position, Player.transform.position) <= ShootRadius) // wenn Spike nah genug ist
            {
                this.transform.LookAt(Player.transform);
                anim.SetBool("Attack", true);
                shoot_activate = true;
            }
            else // sonst gehe zu spieler
            {
                this.transform.LookAt(Player.transform);
                Move(speed);
                anim.SetBool("Attack", false);
                anim.SetBool("Move", true);
            }
        }
        else // sonst Idle
        {
            float max = 2, min = -max;
            transform.Rotate(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
            Move(speed/2);
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

    void shoot()
    {
        if (shoot_activate) {
            //Shoot
            if(Physics.Raycast(this.transform.position, Player.transform.position))
            {
                Debug.Log("hit");
                Debug.DrawLine(this.transform.position, Player.transform.position);
                life_script.getDamage(damage);

                //Laser
                Vector3[] Position = {this.transform.position, Player.transform.position}; // get position for lines
                line.SetPositions(Position); // set line position
            }
        }
    }

    void laser_anim()
    {
        line.material.SetTextureOffset("_MainTex", new Vector2(laser_offset, -0.67f));
        laser_offset = laser_offset - 0.1f;
    }
}
