using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float doubleJumpHeight = 8f;

    private Rigidbody rb;
    private bool canJump = true;
    private bool canDoubleJump = false;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        CheckGrounded();
        HandleJumpInput();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        if (isGrounded)
        {
            canJump = true;
        }
    }

    void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump(jumpHeight);
                canJump = false;
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                Jump(doubleJumpHeight);
                canDoubleJump = false;
            }
        }
    }

    void Jump(float height)
    {
        rb.AddForce(Vector3.up * height, ForceMode.Impulse);
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        rb.AddForce(movement * speed, ForceMode.VelocityChange);
    }
}