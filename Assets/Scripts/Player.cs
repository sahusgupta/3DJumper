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

    [SerializeField] float speed = 5f; // Movement speed
    [SerializeField] float jumpVelocity = 7f; // Jump height velocity
    [SerializeField] float rotationSpeed = 100f; // Rotation speed in degrees per second

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null) // Good practice to check if GetComponent succeeded
        {
            rb.freezeRotation = true; // Keep rotation frozen on X and Z axes
        }
        else
        {
            Debug.LogError("Player GameObject does not have a Rigidbody component.");
        }

        // *** Camera Setup Note ***
        // If your camera should follow the player's rotation:
        // Option 1 (Simplest): Make the Camera GameObject a child of the Player GameObject in the Unity Hierarchy.
        //                     Position it relative to the player (e.g., slightly behind and above).
        //                     It will automatically inherit the player's position and rotation.
        // Option 2 (Scripting): If the camera is separate, you would write a camera follow script.
        //                     This script could, for instance, update the camera's position to follow
        //                     the player and potentially match its Y rotation.
    }

    private void FixedUpdate()
    {
        // --- Input ---
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D keys
        float verticalInput = Input.GetAxis("Vertical");   // W/S keys

        // --- Rotation (Controlled by Horizontal Input) ---
        // Calculate the rotation amount based on horizontal input, speed, and time
        float rotationAmount = horizontalInput * rotationSpeed * Time.fixedDeltaTime;

        // Apply rotation around the Y-axis
        transform.Rotate(0, rotationAmount, 0);

        // --- Movement (Controlled by Vertical Input) ---
        // Determine the movement direction based on vertical input and the player's forward direction
        // transform.forward is a vector pointing in the direction the player is currently facing.
        Vector3 moveDirection = transform.forward * verticalInput;

        // Calculate the target horizontal velocity based on the move direction and speed
        // We only want to affect the horizontal plane (X and Z)
        Vector3 targetHorizontalVelocity = new Vector3(moveDirection.x, 0f, moveDirection.z) * speed;

        // Preserve current vertical velocity (for gravity, jumping, etc.)
        Vector3 currentVerticalVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);

        // Set the rigidbody's velocity directly for linear movement in the facing direction
        // Combine the new horizontal velocity with the existing vertical velocity
        rb.linearVelocity = targetHorizontalVelocity + currentVerticalVelocity;

        // --- Jump (Still uses vertical velocity) ---
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Set the vertical velocity to the jump velocity, preserving horizontal velocity
            // Check if grounded before jumping might be necessary in a full game,
            // but based on your current script, this just adds upward velocity.
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpVelocity, rb.linearVelocity.z);
        }
    }
}