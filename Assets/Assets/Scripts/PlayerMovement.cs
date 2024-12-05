using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    // Скорость движения
    [Range(1, 30)]
    public float lookSpeed = 5f;  // Скорость поворота камеры
    public Transform playerCamera; // Ссылка на камеру игрока

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
        characterController.Move(move * Time.deltaTime);
    }

    void LookAround()
    {
        // Вращаем игрока по оси Y
        float rotY = Input.GetAxis("Mouse X") * lookSpeed;
        transform.Rotate(0, rotY, 0);


        rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f);
        playerCamera.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
}