using UnityEngine;

public class Character : MonoBehaviour
{

    //Properties
    public int maxHP ;
    public int HP ;

    private Vector3 direction;



    public int getHP(){ return HP; }


    // Return true if killed
    public bool dealDamage( int damage ){ 
        if( (HP - damage ) <= 0 ){
            Destroy( gameObject, 0.1f );
            return true;
        }

        return false;

     }

}
