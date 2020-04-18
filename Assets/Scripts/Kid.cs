using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : Character
{
    public Vector3 offset;
    private GameObject Pickuper;
    private bool pickedUp = false;

    public void pickUp(GameObject Pickupper) {
        pickedUp = true;
        transform.SetParent(Pickupper.transform);
    }
    public void dropOff() {
        pickedUp = false;
        transform.SetParent(null);
    }

}
