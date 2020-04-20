using UnityEngine;

public class logicLever : logicInput
{
    /*
        Upwenij się, że ten komponent jest jedny z logicInput na objekcie
        Używaj tego componentu, dla każdej dźwigni w scenie

        Requirements:
        - rigidbody in player
        - tryingToInteract must be implemented in player
        - must be attached to an object with a spriterenderer
    */
    [Header("graphics")]
    [SerializeField] private Sprite off_sprite;
    [SerializeField] private Sprite on_sprite;

    public void UpdateSprite()
    {
        if (state) GetComponent<SpriteRenderer>().sprite = on_sprite;
        else GetComponent<SpriteRenderer>().sprite = off_sprite;
    }
    

    override public void onInteraction(){
        state = !state;
        updateGate();
        UpdateSprite();
    }
}
