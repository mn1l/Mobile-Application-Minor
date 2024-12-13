using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : Menu 
{
    public void CloseSettings()
    {
        settingsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}
