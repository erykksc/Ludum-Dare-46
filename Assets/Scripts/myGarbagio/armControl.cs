using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DrawArrow;

public class armControl : MonoBehaviour
{
    private Rigidbody rb;
    public float strength;
    public Camera cam;
    public float rest_distance;
    public float rest_atraction;

    public float maxForce;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate() {
        // Add force due to mouse movment
        var mouseSpeed3 = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
        if (mouseSpeed3.magnitude > maxForce) {
            mouseSpeed3 = mouseSpeed3.normalized*maxForce;
        }
        rb.AddForce(mouseSpeed3*strength);
        // Add atraction to parent
        var parent_vector = transform.position - transform.parent.position;
        if (parent_vector.magnitude < rest_distance) {
            rb.AddForce(parent_vector.normalized*rest_atraction*Mathf.Pow(rest_atraction, rest_distance - parent_vector.magnitude));
        }
        else {
            rb.AddForce(-parent_vector*rest_atraction*(parent_vector.magnitude - rest_distance));
        }
    }
    void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.DrawWireDisc(transform.parent.position, transform.up, rest_distance);
    }
}
