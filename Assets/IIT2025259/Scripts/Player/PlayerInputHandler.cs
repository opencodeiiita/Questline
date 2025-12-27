using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 moveInput;
    public bool jumpPressed;
    public bool sprintHeld;
    public float mouseX;
    public float mouseY;

    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        jumpPressed = Input.GetButtonDown("Jump");
        sprintHeld = Input.GetKey(KeyCode.LeftShift);
    }
}