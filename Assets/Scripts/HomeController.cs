using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeController : MonoBehaviour
{
    
    void Start()
    {
        Debug.Log("Script has just started!! :3");
    }

    // Start the gameview scene (when button is pressed)
    public void StartGame()
    {
        SceneManager.LoadScene(SceneData.gameview);
    }
}
