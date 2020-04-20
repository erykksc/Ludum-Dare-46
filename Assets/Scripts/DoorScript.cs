using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : Activatable
{
    private Collider2D coll;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Awake() {
        coll = GetComponent<Collider2D>();
    }

    public override void activate() {
        coll.offset = new Vector2(9999, 0);
        // open animation
    }

    public override void de_activate() {
        coll.offset = new Vector2(9999, 0);
        // close animation
    }

}
