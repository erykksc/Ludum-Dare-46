using UnityEngine;

public class Character : MonoBehaviour
{

    //Properties
    public int maxHP ;
    public int HP ;

    private Vector3 direction;



    int getHP(){ return HP; }


    // Return true if killed
    bool damage( int damage ){ 
        if( (HP - damage ) <= 0 ){
            Destroy( gameObject );
            return true;
        }

        return false;

     }

}
