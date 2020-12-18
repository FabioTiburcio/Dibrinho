using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    public Vector2 direcao;
    public float speed;
    float rotateSpeed = 1000f;
    float rotateAmount;
    Animator playerAnim;

    PlayerController playerControllerScript;

    Touch touch;
    //public GameObject Lost;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerControllerScript = GetComponent<PlayerController>();
    }

    private void Update()
    {
        touch = Input.GetTouch(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rotateAmount = Vector3.Cross(direcao, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;
        if (Input.GetMouseButton(0) || touch.phase != TouchPhase.Ended)
        {
            direcao = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - rb.position;
            direcao.Normalize();
            playerAnim.SetBool("isRunning", true);
        }
        else
        {
            rb.velocity = Vector3.zero;
            playerAnim.SetBool("isRunning", false);
        }
    }
}
