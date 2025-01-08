using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelsViewController : MonoBehaviour
{   
    public GameObject[] levelButtons;
    
    public void CloseLevelsView()
    {
        SceneManager.LoadScene(SceneData.homepage);
    }
    
    // Loads correct level based on the name of the pushed button
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
}
