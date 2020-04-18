using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public Rigidbody rb;
    public float SpeedBezPrzedmiotu = 1.0f;
    public float SpeedZPrzedmiotem = 0.5f;
    public float Speed;
    public float JumpForce = 2.0f;
    public float JumpForceWithBaby = 1.5f;
    public float DoubleJumpForce = 1.0f;
    public float DoubleJumpForceWithBaby = 0.5f;
    public float Jump;
    public float DoubleJump;
    public float Masa = 80.0f;
    public float Gravity = 20.0f;
    public bool Grounded;
    public bool ReachedApex = false;
    public bool BabyInHand = false;
    public bool CanDoubleJump;
    public Vector3 SpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(BabyInHand == true) //sprawdzanie czy ma dziecko w lapie
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
            rb.AddForce(Speed, 0, 0);
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(-Speed, 0, 0);
        }
        
        if (Input.GetKey("w") && Grounded == true)
        {
            rb.AddForce(0, Jump, 0);
        }
        if(Input.GetKey("w") && CanDoubleJump == true)
        {
            rb.AddForce(0, DoubleJump, 0);
        }
    }
}
