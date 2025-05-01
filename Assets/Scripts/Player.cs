using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player : MonoBehaviour
{

    private Rigidbody rb;
    private Vector3 movement;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }
    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); 
        float verticalInput = Input.GetAxis("Vertical");  

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 10, ForceMode.Force);
        }

        rb.AddForce(movement * speed, ForceMode.VelocityChange);
    }
}
