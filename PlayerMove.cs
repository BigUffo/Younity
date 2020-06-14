using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 10;
    public Transform bus;
    public LayerMask groundMask;
    float acc = 2;
    Vector3 velocity = new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
        
    }

    void move(){
        float x = Input.GetAxis("Vertical");
        float z = Input.GetAxis("Horizontal");


        if(isGrounded()){
            rb.mass = .01f;
            bus.Rotate(new Vector3(0, z * .5f, 0));
            velocity += (bus.forward * speed * Time.deltaTime);
            velocity -= velocity/2;
            speed += acc * Time.deltaTime;

            if(Input.GetKeyDown(KeyCode.Space)){
                rb.AddForce(new Vector3(0,500,0));
            }
        }else{
            rb.mass = 1;
            rb.AddRelativeTorque(new Vector3(x * .1f,0,- z * .1f));
        }
        rb.MovePosition(bus.position + velocity);
    }

    public bool isGrounded(){
        Vector3 offset = new Vector3(0,.5f,0);
        Vector3 scale = new Vector3(.5f - .1f, .1f, 1 - .1f);
        Quaternion rot = new Quaternion(0,0,0,0);
        return Physics.CheckBox(bus.position - offset,scale,rot,groundMask);
    }
}
