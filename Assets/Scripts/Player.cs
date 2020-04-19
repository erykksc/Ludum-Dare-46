using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Rigidbody2D rb;
    public Animator animator;
    [Header("general parameters")]
    [SerializeField] private float SpeedBezPrzedmiotu = 1.0f;
    [SerializeField] private float SpeedZPrzedmiotem = 0.5f;
    [SerializeField] private float MaxSpeed = 4.0f;
    [SerializeField] private float JumpForce = 1.5f;
    [SerializeField] private float JumpForceWithBaby = 1.3f;
    [SerializeField] private float DoubleJumpForce = 1.0f;
    [SerializeField] private float DoubleJumpForceWithBaby = 0.7f;
    [SerializeField] private float default_gravity = 0.4f;
    [SerializeField] private float Masa = 0.25f;

    [Header("current parameters")]

    [SerializeField] private float Jump;
    [SerializeField] private float Speed;
    [SerializeField] private float DoubleJump;
    [SerializeField] private float Gravity;

    [Header("current state")]
    [SerializeField] private bool Grounded;
    [SerializeField] private bool CanDoubleJump;

    private float lastPickUpTime;
    private GameObject baby;

    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        rb.freezeRotation = true;
        rb.mass = Masa;
        Gravity = default_gravity;
    }

    private bool BabyInHand()
    {
        if (baby != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        animator.SetBool("Grounded", Grounded);
        animator.SetBool("BabyInHand", BabyInHand());

        //When on ground and W pressed - jump
        if (Grounded && Input.GetKeyDown("w"))
        {
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            CanDoubleJump = true;
        }

        //When in air and W pressed - double jump
        else if (!Grounded && Input.GetKeyDown("w") && CanDoubleJump)
        {
            Vector2 temp = new Vector2(rb.velocity.x, 0);
            rb.velocity = temp;
            rb.AddForce(new Vector2(0, DoubleJumpForce), ForceMode2D.Impulse);
            Gravity = default_gravity;
            CanDoubleJump = false;
        }

        animator.SetFloat("vertical_velocity",rb.velocity.y);
    }

    void FixedUpdate()
    {

        //change parameters if baby is held
        if (BabyInHand() == true) //sprawdzanie czy ma dziecko w lapie
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

        animator.SetFloat("horizontal_velocity", rb.velocity.x); 


        //Gravity increase
        if (!Grounded)
        {
            if (Gravity < 6.9f) Gravity = Gravity * 1.05f;
        }

        rb.AddForce(new Vector2(0, -Gravity));


        //drop baby
        if (Input.GetKey("e") && BabyInHand())
        {
            if (Time.time - lastPickUpTime > 0.5f)
            {
                Debug.Log("drop off");

                //baby becomes a seprate object -enable colliders and sprite showing
                foreach (Collider2D col in baby.GetComponentsInChildren<Collider2D>())
                {
                    col.enabled = true;
                }
                baby.GetComponent<SpriteRenderer>().enabled = true;
                animator.SetTrigger("drop_baby");
                baby.GetComponent<Kid>().dropOff();
                baby = null;
                lastPickUpTime = Time.time;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Level"))
        {
            Gravity = default_gravity;
            Grounded = true;
            CanDoubleJump = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {

        //pick-up baby
        if (collision.gameObject.tag == "Kid")
        {
            if (Input.GetKey("e") && !BabyInHand())
            {
                if (Time.time - lastPickUpTime > 0.5f)
                {
                    Debug.Log("pick up");
                    animator.SetTrigger("pickup_baby");
                    collision.gameObject.GetComponent<Kid>().pickUp(gameObject);
                    baby = collision.gameObject;

                    //disable colliders to remove collisions with big mama and hide sprite renderer
                    foreach (Collider2D col in baby.GetComponentsInChildren<Collider2D>())
                    {
                        col.enabled = false;
                    }
                    baby.GetComponent<SpriteRenderer>().enabled = false;
                    lastPickUpTime = Time.time;
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Level"))
        {
            Grounded = false;
            CanDoubleJump = true;
        }
    }
}