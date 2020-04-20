using System;
using UnityEngine;

public class logicPressurePlate : logicInput
{
    //Upwenij się, że ten komponent jest jedny z logicInput na objekcie
    [Header("graphics")]
    [SerializeField] private Sprite off_sprite;
    [SerializeField] private Sprite on_sprite;


    public void UpdateSprite()
    {
        if (state) GetComponent<SpriteRenderer>().sprite = on_sprite;
        else GetComponent<SpriteRenderer>().sprite = off_sprite;
    }

    void onTouchStart(){
        if ( inCollisionKid || inCollisionPlayer)
            state = targetState;
        updateGate();
        UpdateSprite();
    }

    void onTouchEnd(){
        if(! (inCollisionKid || inCollisionPlayer))
            state = !targetState;
        updateGate();
        UpdateSprite();
    }

    private void Start() {
        targetState = true;
        state = !targetState;
    }

    override public void onPlayerTouchStart(){ onTouchStart(); }
    override public void onKidTouchStart()   { onTouchStart(); }
    override public void onPlayerTouchEnd()  { onTouchEnd();   }
    override public void onKidTouchEnd()     { onTouchEnd();   }

}