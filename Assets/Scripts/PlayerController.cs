using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private InputActionReference movement;
    [SerializeField] private float speed;

    public GameObject JumpButtonUI;

    private bool hasKey = false;
    private bool doorOpen = false;
    
    // Update is called once per frame
    void Update()
    {
        Vector2 moveDirection = movement.action.ReadValue<Vector2>(); // Read the movement input direction
        transform.Translate( Time.deltaTime * speed * moveDirection ); // Move the player
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Key"))
        {
            hasKey = true;
            Debug.Log ("key has been picked up");
            other.gameObject.SetActive(false);
        }

        if (other.collider.CompareTag("Door") && hasKey)
        {
            hasKey = false;
            doorOpen = true;
            Debug.Log("Door has been opened");
            other.gameObject.SetActive(false);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("OpenedDoor") && doorOpen)
         {
             JumpButtonUI.SetActive(true);
         }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        JumpButtonUI.SetActive(false);
    }

    public void JumpIn()
    {
        Application.Quit();
        SceneManager.LoadScene(SceneData.homepage);
    }
}
