using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _turnSpeed = 1440;
    private Vector3 _input;
    

    void Update() 
    {
        // Gather input and change rotation once per frame
        GatherInput();
        Look();
    }

    void FixedUpdate() 
    {
        // Place Physics related functions in fixed update so they can trigger as many times per frame as necessary
        Move();
    }

    void GatherInput()
    {
        // Get input from input axis
        _input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
    }

    void Look()
    {
        if(_input != Vector3.zero)
        {
            // Create a matrix to offset input direction for isometric movement
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0,45,0));
            var skewedInput = matrix.MultiplyPoint3x4(_input);

            // Find the relative angle between input and current rotations
            var relative = (transform.position + skewedInput) - transform.position;
            // Turn relative angle into a rotation
            var rot = Quaternion.LookRotation(relative, Vector3.up);
            // Update current rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
        }
    }

    void Move()
    {
        // Update position of rigidbody
        _rb.MovePosition(transform.position + (transform.forward * _input.magnitude) *_speed * Time.deltaTime);
    }
}
