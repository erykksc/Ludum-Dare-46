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

    public bool tryingToInteract = false;


    [Header("current parameters")]

    [SerializeField] private float Jump;
    [SerializeField] private float Speed;
    [SerializeField] private float DoubleJump;
    [SerializeField] private float Gravity;

    [Header("current state")]
    [SerializeField] private bool Grounded;
    [SerializeField] private bool CanDoubleJump;

    //Level Manager
    static bool exists = false;
    private LevelManager lManager;
    private AudioManager aManager;

    private float lastPickUpTime;
    private GameObject baby;

    void Awake()
    {
        if(exists)
        {
            Destroy(gameObject);
            return;
        }
        exists = true;
        DontDestroyOnLoad(this);

        //Searching for levelManager
        int count = Resources.FindObjectsOfTypeAll<LevelManager>().Length;
        if(count>0)
        {
            lManager = Resources.FindObjectsOfTypeAll<LevelManager>()[0];
        }
        aManager = FindObjectOfType<AudioManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        rb.freezeRotation = true;
        rb.mass = Masa;
        Gravity = default_gravity;
    }

    public bool BabyInHand()
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
            rb.AddForce(new Vector2(0, Jump), ForceMode2D.Impulse);
            CanDoubleJump = true;
            aManager.PlayOneShot("player_jump");
        }

        //When in air and W pressed - double jump
        else if (!Grounded && Input.GetKeyDown("w") && CanDoubleJump)
        {
            Vector2 temp = new Vector2(rb.velocity.x, 0);
            rb.velocity = temp;
            rb.AddForce(new Vector2(0, DoubleJump), ForceMode2D.Impulse);
            Gravity = default_gravity;
            CanDoubleJump = false;
            aManager.PlayOneShot("player_jump");
        }

        animator.SetFloat("vertical_velocity",rb.velocity.y);

        if(Input.GetKeyDown("i") && ! BabyInHand()) tryingToInteract = true;
        if(Input.GetKeyUp("i")) tryingToInteract = false;
    }

    void DropBaby()
    {
        Debug.Log("drop off");
        lastPickUpTime = Time.time;
        StartCoroutine(SlightlyDelayedBabyDrop(0.8f));
        animator.SetTrigger("drop_baby");
        return;
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

        //try to compare velocity with 
       // try { }
       // else { }
        animator.SetFloat("horizontal_velocity", rb.velocity.x);   


        //Gravity increase
        if (!Grounded)
        {
            if (Gravity < 10.50f) Gravity = Gravity * 1.1f;
        }

        rb.AddForce(new Vector2(0, -Gravity));


        //drop baby
        if (Input.GetKey("e") && BabyInHand())
        {
            if (Time.time - lastPickUpTime > 0.5f && Grounded )
            {
                DropBaby();
            }
            else if (Time.time - lastPickUpTime > 0.5f && !Grounded)
            {
                InstantBabyDrop();
            }
        }

       
        
    }


    //baby becomes a seprate object -enable colliders and sprite showing
    public IEnumerator SlightlyDelayedBabyDrop(float delay)
    {
        yield return new WaitForSeconds(delay);
        InstantBabyDrop();
        // If baby dropped -> can interact with levers
        //tryingToInteract = true;
    }
    public void InstantBabyDrop()
    {
        foreach (Collider2D col in baby.GetComponentsInChildren<Collider2D>())
        {
            col.enabled = true;
        }
        baby.GetComponent<SpriteRenderer>().enabled = true;
        baby.GetComponent<Kid>().dropOff();
        baby = null;
    }


    public void ClearState()
    {
        CanDoubleJump = false;
        Grounded = false;
        CanDoubleJump = false;
        rb.velocity = new Vector2(0,0);
        dead = false;
    }

    public override bool dealDamage(int damage)
    {
        aManager.PlayOneShot("player_hit");
        return base.dealDamage(damage);
    }

    //Enable jumping when player contacts ground
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Level") || collision.gameObject.CompareTag("Enemy"))
        {
            Gravity = default_gravity;
            Grounded = true;
            CanDoubleJump = false;
        }
        if(collision.gameObject.CompareTag("Enemy")&&BabyInHand()&&Time.time - lastPickUpTime > 0.5f)
        {
            Debug.Log("drop off");
            lastPickUpTime = Time.time;
            InstantBabyDrop();
        }
    }

    //Pick up baby when colliding and e-press
    private void OnCollisionStay2D(Collision2D collision)
    {

        //pick-up baby
        if (collision.gameObject.tag == "Kid")
        {
            if (Input.GetKey("e") && !BabyInHand())
            {
                if (Time.time - lastPickUpTime > 0.5f)
                {
                    //tryingToInteract = false;
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

    //Consider relative velocity if player is on a moving platform
    private void OnTriggerStay2D(Collider2D collision)
    {
        try
        {
            //if triggering with a moving platform
            collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 relative_velocity = rb.velocity - collision.gameObject.GetComponent<Rigidbody2D>().velocity;

            animator.SetFloat("horizontal_velocity", relative_velocity.x);
            animator.SetFloat("vertical_velocity", relative_velocity.y);

        }
        catch { animator.SetFloat("horizontal_velocity", rb.velocity.x); }
        
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Level") || collision.gameObject.CompareTag("Enemy"))
        {
            Grounded = false;
            CanDoubleJump = true;
        }
    }
}