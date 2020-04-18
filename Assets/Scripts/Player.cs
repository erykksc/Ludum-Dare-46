using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Rigidbody2D rb;
    [Header("general parameters")]
    public float SpeedBezPrzedmiotu = 1.0f;
    public float SpeedZPrzedmiotem = 0.5f;
    public float MaxSpeed = 4.0f;
    public float JumpForce = 10.0f;
    public float JumpForceWithBaby = 7.5f;
    public float DoubleJumpForce = 8.0f;
    public float DoubleJumpForceWithBaby = 5.5f;
    public float default_gravity = 20.0f;
    public float Masa = 0.25f;
    public Vector2 SpawnPoint;

    [Header("current parameters")]

    public float Jump;
    public float Speed;
    public float DoubleJump;
    public float Gravity; 

    [Header("current state")]
    public bool Grounded;
    public bool BabyInHand = false;
    public bool CanDoubleJump;

    private float lastPickUpTime;
    private GameObject baby;

    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.mass = Masa;
        Gravity = default_gravity;
    }

    private void Update()
    {

        if (Grounded && Input.GetKeyDown("w"))
        {
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            CanDoubleJump = true;

        }
        else if (!Grounded && Input.GetKeyDown("w") && CanDoubleJump)
        {
            rb.AddForce(new Vector2(0, DoubleJumpForce), ForceMode2D.Impulse);
            Gravity = default_gravity;
            CanDoubleJump = false;
        }
    }

    void FixedUpdate()
    {
        if (BabyInHand == true) //sprawdzanie czy ma dziecko w lapie
        {
            Speed = SpeedZPrzedmiotem;
            Jump = JumpForceWithBaby;
            DoubleJump = DoubleJumpForceWithBaby;
        }
        else
        {
            Speed = SpeedBezPrzedmiotu;
            Jump = JumpForce;
            DoubleJump = DoubleJumpForce;
        }

        Vector2 temp = (rb.velocity);
        if (Mathf.Abs(temp.x) > MaxSpeed)
        {
            temp.x = temp.normalized.x * MaxSpeed;
            rb.velocity = temp;
        }


        if (Input.GetKey("d"))
        {
            rb.AddForce(new Vector2(Speed, 0));
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(new Vector2(-Speed, 0));
        }

        //Gravity increase
        if(!Grounded)      
        {
            if(Gravity<6.9f) Gravity = Gravity * 1.05f;
        }

        rb.AddForce(new Vector2(0, -Gravity));

        if (Input.GetKey("e") && BabyInHand)
        {
            if(Time.time - lastPickUpTime > 0.5f)
            {
                Debug.Log("drop off");
                baby.GetComponent<Kid>().dropOff();
                lastPickUpTime = Time.time;
                BabyInHand = false;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Gravity = default_gravity;
        Grounded = true;
        CanDoubleJump = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Kid")
        {
            
            if (Input.GetKey("e") && !BabyInHand){
                if(Time.time - lastPickUpTime > 0.5f)
                {
                    Debug.Log("pick up");
                    collision.gameObject.GetComponent<Kid>().pickUp(gameObject);
                    BabyInHand = true;
                    baby = collision.gameObject;
                    lastPickUpTime = Time.time;
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Grounded = false;
        CanDoubleJump = true;
    }
}
