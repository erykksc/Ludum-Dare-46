using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Rigidbody2D rb;
    public float SpeedBezPrzedmiotu = 1.0f;
    public float SpeedZPrzedmiotem = 0.5f;
    public float Speed;
    public float JumpForce = 10.0f;
    public float JumpForceWithBaby = 7.5f;
    public float DoubleJumpForce = 8.0f;
    public float DoubleJumpForceWithBaby = 5.5f;

    public float Jump;
    public float DoubleJump;
    public float Masa = 0.25f;
    public float Gravity = 20.0f;
    public bool Grounded;
    public bool ReachedApex = false;
    public bool BabyInHand = false;
    public bool CanDoubleJump;
    public Vector2 SpawnPoint;
    private float lastPickUpTime;
    private GameObject baby;


    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.mass = Masa;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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

        if (Input.GetKey("d"))
        {
            rb.AddForce(new Vector2(Speed, 0));
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(new Vector2(-Speed, 0));
        }

        if (Input.GetKey("w") && Grounded == true)
        {
            rb.AddForce(new Vector2(0, Jump));
        }
        if (Input.GetKey("w") && CanDoubleJump == true)
        {
            rb.AddForce(new Vector2(0, DoubleJump));
            CanDoubleJump = false;
        }
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
