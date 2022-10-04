using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float sensivityMultipler;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float cameraSmoothnes;
    [SerializeField] private Vector3 cameraPosition;
    [SerializeField] private float jumpForce;
    [SerializeField] TextMeshProUGUI textScore;
    private Rigidbody rb;
    public Coin coinScript;
    private float keyboardX, keyboardY;
    public int coins;
    private bool canJump;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        textScore.SetText($"{coins}");
    }

    private void FixedUpdate()
    {
        Movement();
    }
    private void Update()
    {
        playerInput();
        CameraMovement();
    }

    private void Movement()
    {
        rb.AddForce(new Vector3(keyboardX, 0, keyboardY), ForceMode.VelocityChange);
        rb.velocity = new(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y, Mathf.Clamp(rb.velocity.z, -maxSpeed, maxSpeed));
        if (canJump && Input.GetKey(KeyCode.Space))
            Jump();
    }

    private void Jump()
    {
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Force);
    }
    private void playerInput()
    {
        keyboardX = Input.GetAxis("Horizontal") * sensivityMultipler;
        keyboardY = Input.GetAxis("Vertical") * sensivityMultipler;
    }

    private void CameraMovement()
    {
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, transform.position + cameraPosition, Time.deltaTime * cameraSmoothnes);
    }

    private void OnCollisionEnter(Collision collision)
    {
        canJump = true;   
    }
    private void OnCollisionExit(Collision collision)
    {
        canJump = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            coinScript = other.GetComponent<Coin>();
            coinScript.DestroyObject();
            coins++;
            textScore.SetText($"{coins}");
        }
    }
}
