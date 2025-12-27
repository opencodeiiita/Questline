using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 3, -6);
    public float sensitivity = 100f;

    float mouseX;

    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        player.Rotate(Vector3.up * mouseX);

        transform.position = player.position + offset;
        transform.LookAt(player);
    }
}
