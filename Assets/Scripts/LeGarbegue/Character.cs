using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Collider2D collider;

    [SerializeField] float[] hp = {100,100};
    [SerializeField] float[] def = {100,100};
    [SerializeField] float[] dmg = {100,100};
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        
    }
    void Move()
    {
        
    }
    void Damage(float delta)
    {

    }
    float GetHp(){return hp[0];}
}
