using UnityEngine;

public class Character : MonoBehaviour
{

    [Header("health - Character class property")]
    public int maxHP ;
    public int HP ;


    public int getHP(){ return HP; }


    // Return true if killed
    public bool dealDamage( int damage ){ 
        HP -= damage;
        if(HP <= 0 ){
            
            Destroy( gameObject, 0.1f );
            return true;
        }
        return false;

     }

}
