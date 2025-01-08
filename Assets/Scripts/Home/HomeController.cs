using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeController : MonoBehaviour
{
    public CanvasGroup mainMenuCanvasGroup;
    public GameObject settingsMenu;
    public GameObject settingsButton;
    
    void Start()
    {
        Debug.Log("Script has just started!! :3");
    }

    // Start the gameview scene (when button is pressed)
    public void StartGame()
    {
        SceneManager.LoadScene(SceneData.level1);
    }
    
    public void OpenLevelOverview()
    {
        SceneManager.LoadScene(SceneData.levelsview);
    }
    
    public void OpenStore()
    {
        SceneManager.LoadScene(SceneData.storepage);
    }
    
    // Opens settings pop up and makes main menu not interactible
    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        settingsButton.SetActive(false);
        SetMainMenuInteractable(false);
    }
    
    // Closes settings pop up and makes main menu interactible again
    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
        settingsButton.SetActive(true);
        SetMainMenuInteractable(true);
    }
    
    // Helper method of mainMenuCanvasGroup 
    private void SetMainMenuInteractable(bool isInteractable)
    {
        mainMenuCanvasGroup.interactable = isInteractable;
        mainMenuCanvasGroup.blocksRaycasts = isInteractable;
    }
}
