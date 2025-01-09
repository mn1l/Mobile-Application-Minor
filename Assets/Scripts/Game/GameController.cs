using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    
    /*
     * TODO
     * Starting up Minigames
     * When colliding with chest -> Show enter minigame button -> start correct game
     *
     * TODO
     * Finishing up Minigames
     * When game is finished -> haskey = true -> Get key -> Unactivate closed chest
     */
    
    [Header("Buttons")]
    public GameObject EnterDoorButton;
    public GameObject QuitMiniGameButton;
    public GameObject OpenMiniGameButton1;
    //public GameObject OpenMiniGameButton2;
    //public GameObject OpenMiniGameButton3;

    [Header("MiniGameViews")] 
    public GameObject MiniGame1;
    //public GameObject MiniGame2;
    //public GameObject MiniGame3;
    
    [Header("Key Icons")]
    public GameObject KeyIcon1;
    public GameObject KeyIcon2;
    public GameObject KeyIcon3;

    private bool hasKeyOne = false;
    private bool hasKeyTwo = false;
    private bool hasKeyThree = false;
    
    private bool doorOpen = false;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.collider.tag)
        {
            case "KeyOne":
                hasKeyOne = true;
                KeyIcon1.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, 1f);
                other.gameObject.SetActive(false);
                break;

            case "KeyTwo":
                hasKeyTwo = true;
                KeyIcon2.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, 1f);
                other.gameObject.SetActive(false);
                break;

            case "KeyThree":
                hasKeyThree = true;
                KeyIcon3.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, 1f);
                other.gameObject.SetActive(false);
                break;

            case "Door":
                if (hasKeyOne && hasKeyTwo && hasKeyThree)
                {
                    hasKeyOne = false;
                    hasKeyTwo = false;
                    hasKeyThree = false;
                    doorOpen = true;
                    other.gameObject.SetActive(false);
                }
                break;
        }
    }
    
    //----------
    

    private void OnCollisionStay2D(Collision2D other)
    {
        switch (other.collider.tag)
        {
            case "KeyOne":
                //
            case "KeyTwo":
                // do something
            case "KeyThree":
                // do something
            case "OpenedDoor":
                if (doorOpen)
                {
                    EnterDoorButton.SetActive(true);
                }
                break;
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


    public void QuitMiniGame()
    {
        
    }
}
