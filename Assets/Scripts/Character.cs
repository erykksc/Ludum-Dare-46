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
    [SerializeField] float traction;
    [SerializeField] float JumpStrength;
    //distantce velocity
    [SerializeField] float dVel;

    int jumpsAvailable;
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
        //if(jumpsAvailable<1){return;}
        body.AddForce(new Vector2(0,JumpStrength));
        jumpsAvailable--;
    }
    void OnCollisonEnter(Collision collision)
    {
        Debug.Log("Info");
        jumpsAvailable = 2;
    }
}
