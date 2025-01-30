using Home;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreController : MonoBehaviour, IDataPersistence
{
    public int[,] shopItems = new int[6, 6];
    public TMP_Text CoinCount;
    private int coinCount = 1000;

    public GameObject Own1;
    public GameObject Own2;
    public GameObject Own3;
    public GameObject Own4;
    public GameObject Own5;
    
    private void Start()
    {
        CoinCount.text = coinCount.ToString();
        
        //ID's
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;
        shopItems[1, 5] = 5;
        
        //Price
        shopItems[2, 1] = 10;
        shopItems[2, 2] = 14;
        shopItems[2, 3] = 24;
        shopItems[2, 4] = 38;
        shopItems[2, 5] = 80;
    }
    
    public void CloseStore()
    {
        SceneManager.LoadScene(SceneData.homepage);
    }

    public void LoadData(GameData data)
    {
        coinCount = data.coinCount;
    }

    public void SaveData(ref GameData data)
    {
        data.coinCount = coinCount;
    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        
        if (coinCount >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            coinCount -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            CoinCount.text = coinCount.ToString();
            ButtonRef.SetActive(false);
            
            //TODO make sure that the item that is now owned, is visible Own1-Own5...
            
        }
    }
    
    //TODO make sure that bought skins can be equipped/unequipped in game (one at a time), and these are saved too
}
