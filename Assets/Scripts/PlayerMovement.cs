using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private float Move;

    private bool isGrounded = false;
    
    [SerializeField]
    private float speed;
    
    [SerializeField]
    private float jump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Recieves float somewhere between -1 and 1 -> check if player wants to go left(-1) or right(1)
        Move = Input.GetAxisRaw("Horizontal");
        
        // -1 or 1 * amount of speed is x -> in Vector2(x, y) & y just stays the same
        rb.velocity = new Vector2(Move * speed, rb.velocity.y);
        
        // Check if Jump button has been pressed and player is touching ground
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump * 10)); // AddForce is velocity +=
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Check if player is touching the gameObject tagged with "Ground"
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
