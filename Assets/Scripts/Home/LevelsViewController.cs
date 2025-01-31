using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsViewController : MonoBehaviour
{
    public GameObject pageOne;
    public GameObject pageTwo;
    
    public Dictionary<string, bool> levelsUnlocked;

    public void Start()
    {
        pageOne.SetActive(true);
        pageTwo.SetActive(false);
    }

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


    public void nextPage()
    {
        pageOne.SetActive(false);
        pageTwo.SetActive(true);
    }

    public void previousPage()
    {
        pageOne.SetActive(true);
        pageTwo.SetActive(false);
    }
}
