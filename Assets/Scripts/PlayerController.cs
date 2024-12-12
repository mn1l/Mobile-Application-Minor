using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private InputActionReference movement;
    [SerializeField] private float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDirection = movement.action.ReadValue<Vector2>(); // Read the movement input direction
        transform.Translate( Time.deltaTime * speed * moveDirection ); // Move the player
    }
}
