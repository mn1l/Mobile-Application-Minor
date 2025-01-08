using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    
    public GameObject EnterDoorButton;

    private bool hasKey = false;
    private bool doorOpen = false;

    
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
            EnterDoorButton.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other != null)
        {
            EnterDoorButton.SetActive(false);            
        }
    }

    public void JumpIn()
    {
        Application.Quit();
        SceneManager.LoadScene(SceneData.homepage);
    }
}
