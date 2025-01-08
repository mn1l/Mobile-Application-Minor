using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsViewController : MonoBehaviour
{   
    public void CloseLevelsView()
    {
        SceneManager.LoadScene(SceneData.homepage);
    }
    
    /*
     * Loads correct level based on the name of the pushed button
     */
    public void LoadLevel()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        
        if (clickedButton != null)
        {
            String buttonName = clickedButton.name;
            
            if (int.TryParse(buttonName, out int levelIndex))
            {
                string levelSceneName = $"level{levelIndex}";
                SceneManager.LoadScene(levelSceneName);
            }
        }
    }

    
    /*
     * "Unlocks" the given level number
     * by making the selection button interactible
     */
    public void UnlockLevel(String level)
    {
        GameObject button = GameObject.Find(level);
        if (button != null)
        {
            Button buttonComponent = button.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.interactable = true;
            }
        }
    }
}
