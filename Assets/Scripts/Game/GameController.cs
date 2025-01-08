using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    
    public GameObject EnterDoorButton;
    public GameObject KeyOneIcon;
    public GameObject KeyTwoIcon;
    public GameObject KeyThreeIcon;

    private bool hasKeyOne = false;
    private bool hasKeyTwo = false;
    private bool hasKeyThree = false;
    
    private bool doorOpen = false;

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("KeyOne"))
        {
            hasKeyOne = true;
            KeyOneIcon.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, 1f);
            other.gameObject.SetActive(false);
        }
        
        if (other.collider.CompareTag("KeyTwo"))
        {
            hasKeyTwo = true;
            KeyTwoIcon.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, 1f);
            other.gameObject.SetActive(false);
        }
        
        if (other.collider.CompareTag("KeyThree"))
        {
            hasKeyThree = true;
            KeyThreeIcon.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, 1f);
            other.gameObject.SetActive(false);
        }

        if (other.collider.CompareTag("Door") && hasKeyOne && hasKeyTwo && hasKeyThree)
        {
            hasKeyOne = false;
            hasKeyTwo = false;
            hasKeyThree = false;
            doorOpen = true;
            other.gameObject.SetActive(false);

        }
    }
    
    //----------
    
    // Checks if door can be opened by player
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("OpenedDoor") && doorOpen)
        {
            EnterDoorButton.SetActive(true);
        }
    }
    
    //-------------
    
    // Checks whether collision is true, if not then door button isn't visible
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other != null)
        {
            EnterDoorButton.SetActive(false);            
        }
    }
    
    // Enter door and finish level
    public void EnterDoor()
    {
        Application.Quit();
        SceneManager.LoadScene(SceneData.homepage);
        
        // Unlock next level in level overview
        // Jump to next level scene
    }
}
