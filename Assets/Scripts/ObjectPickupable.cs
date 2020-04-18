using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickupable : MonoBehaviour
{
    public Vector3 offset;
    private GameObject Pickuper;
    private bool pickedUp = false;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        //if (pickedUp) {
        //    transform.position = Pickuper.transform.position + offset;
        //}
    }
    public void pickUp(GameObject Pickupper) {
        pickedUp = true;
        transform.SetParent(Pickupper.transform);
    }
    public void dropOff() {
        pickedUp = false;
        transform.SetParent(null);
    }
}
