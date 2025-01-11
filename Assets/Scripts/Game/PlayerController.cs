using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference movement;
    [SerializeField] private float speed;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 moveDirection = movement.action.ReadValue<Vector2>(); // Read the movement input direction
        rb.velocity = moveDirection * speed;
        //transform.Translate( Time.deltaTime * speed * moveDirection ); // Move the player
    }
    
    /*
     * TODO
     * Create new movement with arrow buttons, design choice as it improves QOL when playing and moving through the maze
     */
}
