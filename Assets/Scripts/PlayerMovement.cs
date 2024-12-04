using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Скорость движения
    public float lookSpeed = 2f;  // Скорость поворота камеры
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

    }
}