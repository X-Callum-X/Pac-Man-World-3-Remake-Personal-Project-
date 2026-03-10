using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private Camera followCamera;

    private CharacterController playerController;

    [Header("Variables")]

    public float moveSpeed = 5f;
    public float jumpHeight = 1.0f;

    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float gravity = -9.81f;

    private Vector3 moveDirection;
    private bool isGrounded;

    private Animator anim;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerController = GetComponent<CharacterController>();

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Movement();
    }

    void Movement()
    {
        isGrounded = playerController.isGrounded;

        if (isGrounded && moveDirection.y < 0)
        {
            moveDirection.y = 0f;
        }

        bool pressingShift = Input.GetKey(KeyCode.LeftShift);
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, 0, verticalInput);
        Vector3 movementDirection = movementInput;

        playerController.Move(movementDirection.normalized * moveSpeed * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            anim.SetBool("isRunning", true);

            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetButton("Jump") && isGrounded)
        {
            moveDirection.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }

        if (!isGrounded)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }

        playerController.Move(moveDirection * Time.deltaTime);
    }
}