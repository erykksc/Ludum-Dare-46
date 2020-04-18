using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public Rigidbody2D rb;
    [Header("general parameters")]
    [SerializeField] private float SpeedBezPrzedmiotu = 1.0f;
    [SerializeField] private float SpeedZPrzedmiotem = 0.5f;
    [SerializeField] private float MaxSpeed = 4.0f;
    [SerializeField] private float JumpForce = 10.0f;
    [SerializeField] private float JumpForceWithBaby = 7.5f;
    [SerializeField] private float DoubleJumpForce = 8.0f;
    [SerializeField] private float DoubleJumpForceWithBaby = 5.5f;
    [SerializeField] private float default_gravity = 20.0f;
    [SerializeField] private float Masa = 0.25f;

    [Header("current parameters")]

    [SerializeField] private float Jump;
    [SerializeField] private float Speed;
    [SerializeField] private float DoubleJump;
    [SerializeField] private float Gravity; 

    [Header("current state")]
    [SerializeField] private bool Grounded;
    [SerializeField] private bool CanDoubleJump;
    [SerializeField] private bool canSwitchLevels = true;

    static bool exists = false;
    private float lastPickUpTime;
    private GameObject baby;

    // Start is called before the first frame update
    void Awake()
    {
        if(exists)
        {
            Destroy(gameObject);
            return;
        }
        exists = true;
        DontDestroyOnLoad(this);
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.mass = Masa;
        Gravity = default_gravity;
    }

    private bool BabyInHand(){
        if (baby!=null){

            return true;
        }
        else{
            return false;
        }
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

        //Gravity increase
        if(!Grounded)      
        {
            if(Gravity<6.9f) Gravity = Gravity * 1.05f;
        }

        rb.AddForce(new Vector2(0, -Gravity));

        if (Input.GetKey("e") && BabyInHand())
        {
            if(Time.time - lastPickUpTime > 0.5f)
            {
                Debug.Log("drop off");
                baby.GetComponent<Kid>().dropOff();
                baby = null;
                lastPickUpTime = Time.time;
            }
        }
    }
    void ClearState()
    {
        transform.position = new Vector3(0,0,0);
        canSwitchLevels = false;
        CanDoubleJump = false;
        Grounded = false;
        CanDoubleJump = false;
        rb.velocity = new Vector2(0,0);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Gravity = default_gravity;
        Grounded = true;
        CanDoubleJump = false;
        if(collision.gameObject.CompareTag("Trigger_NEXT"))
        {
            if(!BabyInHand())
            {
                return;
            }

            ClearState();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
        if(collision.gameObject.CompareTag("Trigger_PREVIOUS"))
        {
            if(!BabyInHand())
            {
                return;
            }
            if(SceneManager.GetActiveScene().buildIndex==1){return;}

            ClearState();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Kid")
        {
            
            if (Input.GetKey("e") && !BabyInHand()){
                if(Time.time - lastPickUpTime > 0.5f)
                {
                    Debug.Log("pick up");
                    collision.gameObject.GetComponent<Kid>().pickUp(gameObject);
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
        canSwitchLevels = true;
    }
}
