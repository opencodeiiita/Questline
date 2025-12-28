using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float sprintSpeed = 7f;
    public float jumpForce = 6f;
    public float gravity = -9.81f;
    public float mouseXSensitivity = 220f;
    public float mouseYSensitivity = 140f;
    public Transform cameraPivot;
    public float minPitch = -40f;
    public float maxPitch = 60f;

    private float pitch;
    private float yVelocity;
    private CharacterController controller;
    private PlayerInputHandler input;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInputHandler>();
    }

    void Update()
    {
        // Horizontal rotation (player)
        float yaw = input.mouseX * mouseXSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * yaw);

        // Vertical rotation (camera pivot)
        pitch -= input.mouseY * mouseYSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        float speed = input.sprintHeld ? sprintSpeed : walkSpeed;

        Vector3 move = transform.right * input.moveInput.x +
                       transform.forward * input.moveInput.y;

        if (controller.isGrounded)
        {
            if (yVelocity < 0)
                yVelocity = 0;

            if (input.jumpPressed)
                yVelocity = jumpForce;
        }

        yVelocity += gravity * Time.deltaTime;
        move.y = yVelocity;

        controller.Move(move * speed * Time.deltaTime);
    }
}
