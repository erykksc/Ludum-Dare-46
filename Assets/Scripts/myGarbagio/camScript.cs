using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float max_speed;
    public float min_speed;
    public GameObject followObject;
    private Rigidbody parentRB;
    public float cam_rest_y;
    public float cam_speed_on_y;
    void Start()
    {
        parentRB = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var speed = (followObject.GetComponent<Rigidbody>().velocity - parentRB.velocity).magnitude;
        if (speed > max_speed){
            speed = 1f;
        }
        else if (speed < min_speed) {
            speed = 0f;
        }
        else {
            speed = speed/max_speed;
        }
        var newPos = Vector3.Lerp(transform.parent.position, followObject.transform.position, speed);
        newPos.y = transform.parent.position.y + cam_rest_y + cam_speed_on_y * parentRB.velocity.magnitude;
        transform.position = newPos;
    }
}
