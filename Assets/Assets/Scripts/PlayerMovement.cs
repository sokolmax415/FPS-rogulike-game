using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    // �������� ��������
    [Range(1, 30)]
    public float lookSpeed = 5f;  // �������� �������� ������
    public Transform playerCamera; // ������ �� ������ ������

    private CharacterController characterController;
    private float rotationX = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovePlayer();
        LookAround();
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move.y = -9.8f * Time.deltaTime;
        characterController.Move(move * Time.deltaTime);

    }

    void LookAround()
    {
        // ������� ������ �� �6298�� Y
        float rotY = Input.GetAxis("Mouse X") * lookSpeed;
        transform.Rotate(0, rotY, 0);


        rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f);
        playerCamera.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
}