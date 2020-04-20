using UnityEngine;

public class Character : MonoBehaviour
{

    [Header("health - Character class property")]
    public int maxHP ;
    public int HP ;

    public bool dead = false;

    public int getHP(){ return HP; }


    // Return true if killed
    public bool dealDamage( int damage ){ 
        HP -= damage;
        if(HP <= 0 ){
            
            //Destroy( gameObject, 0.1f );
            if(dead)
            {
                return true;
            }
            if(tag == "Player"||tag=="Kid")
            {}
            else
            {
                return true;
            }
            dead = true;
            int count = Resources.FindObjectsOfTypeAll<LevelManager>().Length;
            if(count>0)
            {
                LevelManager lManager = Resources.FindObjectsOfTypeAll<LevelManager>()[0];
                lManager.Restart();
            }
            return true;
        }
        return false;

     }

}
