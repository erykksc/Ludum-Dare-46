using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Rigidbody2D body;

    [SerializeField] float hp;
    [SerializeField] float hpMax;
    [SerializeField] float dmg;
    [SerializeField] float speed;
    [SerializeField] float jumpStrength;
    [SerializeField] float doubleJumpStrength;
    //distantce velocity
    [SerializeField] float dVel;
    [SerializeField] bool isOnGround;
    [SerializeField] bool canDoubleJump;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
    }

    void Move(float dv)
    {
        if(Mathf.Abs(dv)>speed)
        {
            Mathf.Clamp(dv,-speed,speed);
        }
        body.AddForce(new Vector2(dv,0));
    }
    void Jump()
    {
        if (isOnGround )
        {
            body.AddForce(new Vector2(0,JumpStrength));
        }
        else if (canDoubleJump )
        {
            body.AddForce(new Vector2(0,JumpStrength));
            canDoubleJump = false;
        }
    }
    void OnCollisonEnter(Collision collision)
    {
        Debug.Log("Info");
        jumpsAvailable = 2;
    }
}
