using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private Camera followCamera;

    private CharacterController playerController;

    private Animator anim;

    public TMP_Text scoreUI;

    public AudioSource source;
    public AudioClip eat;

    [Header("Variables")]

    public float moveSpeed = 5f;
    public float jumpHeight = 1.0f;

    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float gravity = -9.81f;

    private Vector3 moveDirection;
    private bool isGrounded;

    [HideInInspector] public int score;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerController = GetComponent<CharacterController>();

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool("isGrounded", isGrounded);

        Movement();

        Debug.Log(isGrounded);
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

            anim.SetBool("hasJumped", true);
        }

        if (!isGrounded)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }
        else
        {
            anim.SetBool("hasJumped", false);
        }

        playerController.Move(moveDirection * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pellet"))
        {
            Destroy(other.gameObject);

            source.PlayOneShot(eat);

            score += 10;

            scoreUI.text = score.ToString();
        }

        if (other.gameObject.CompareTag("PowerPellet"))
        {
            Destroy(other.gameObject);

            source.PlayOneShot(eat);

            score += 50;

            scoreUI.text = score.ToString();
        }
    }
}