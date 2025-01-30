using Home;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class StoreController : MonoBehaviour, IDataPersistence
{
    public int[,] shopItems = new int[6, 6];
    public TMP_Text CoinCount;
    private int coinCount = 1000;
    
    [Header("Bought Item GameObjects")]
    public GameObject[] boughtItems;

    [Header("Item UI Texts")]
    public GameObject[] itemTextObjects; 

    private TMP_Text[] itemTexts;

    
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
        
        //Equip
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;
        shopItems[3, 5] = 0;
        
        // Initialize TMP_Text components
        itemTexts = new TMP_Text[itemTextObjects.Length];
        for (int i = 0; i < itemTextObjects.Length; i++)
        {
            itemTexts[i] = itemTextObjects[i].GetComponent<TMP_Text>();
        }
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
            var itemID = shopItems[1, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            Equipped(itemID);
        }
    }

    public void EquipItem()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        Equipped(int.Parse(EventSystem.current.currentSelectedGameObject.name));
    }

    public void Equipped(int itemID)
    {
        if (itemID < 1 || itemID > boughtItems.Length) return; // Ensure valid index

        // Disable all items and set their text to "Unequipped"
        for (int i = 0; i < boughtItems.Length; i++)
        {
            itemTexts[i].text = "Unequipped";
            itemTexts[i].color = Color.red;
        }

        // Enable the selected item and set text to "Equipped"
        int index = itemID - 1;
        boughtItems[index].SetActive(true);
        if (itemTexts[index].text == "Equipped")
        {
            itemTexts[index].text = "Unequipped";
            itemTexts[index].color = Color.red;
        }
        else
        {
            itemTexts[index].text = "Equipped";
            itemTexts[index].color = Color.green;
        }
    }
    
    //TODO make sure that bought skins can be equipped/unequipped in game (one at a time), and these are saved too
}
