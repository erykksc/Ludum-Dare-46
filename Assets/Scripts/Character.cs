using UnityEngine;

public class Character : MonoBehaviour
{

    [Header("health - Character class property")]
    public int maxHP ;
    public int HP ;


    public int getHP(){ return HP; }


    // Return true if killed
    public bool dealDamage( int damage ){ 
        if( (HP - damage ) <= 0 ){
            Destroy( gameObject, 0.1f );
            return true;
        }
        HP -= damage;

        return false;

     }

}
