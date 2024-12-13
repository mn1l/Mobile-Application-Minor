using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour 
{
    public GameObject settingsMenuUI;
    
    public void CloseSettings()
    {
        settingsMenuUI.SetActive(true);
    }
}
