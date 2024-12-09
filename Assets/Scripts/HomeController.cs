using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeController : MonoBehaviour
{
    
    void Start()
    {
        Debug.Log("Script has just started!! :3");
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Start the gameview scene (when button is pressed)
    public void gotoGame()
    {
        SceneManager.LoadScene(SceneData.gameview);
    }
}
