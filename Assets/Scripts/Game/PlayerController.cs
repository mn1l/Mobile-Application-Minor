using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IDataPersistence
{
    [SerializeField] private InputActionReference movement;
    [SerializeField] private float speed;
        
    [Header("Buttons")]
    public GameObject EnterDoorButton;
    
    [Header("Key Icons")]
    public GameObject KeyIcon1;
    public GameObject KeyIcon2;
    public GameObject KeyIcon3;

    public TMP_Text CoinCount;

    private bool hasKeyOne = false;
    private bool hasKeyTwo = false;
    private bool hasKeyThree = false;
    
    private int coinCount = 0;
    
    private bool doorOpen = false;

    private Rigidbody2D rb;

    private void Start()
    {
        CoinCount.text = coinCount.ToString();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 moveDirection = movement.action.ReadValue<Vector2>(); // Read the movement input direction
        rb.velocity = moveDirection * speed;
        //transform.Translate( Time.deltaTime * speed * moveDirection ); // Move the player
    }

    public void LoadData(GameData data)
    {
        this.coinCount = data.coinCount;
    }

    public void SaveData(ref GameData data)
    {
        data.coinCount = this.coinCount;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.collider.tag)
        {
            case "Coin":
                coinCount++;
                CoinCount.text = coinCount.ToString();
                other.gameObject.SetActive(false);
                break;
            case "KeyOne":
                hasKeyOne = true;
                KeyIcon1.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, 1f);
                other.gameObject.SetActive(false);
                break;

            case "KeyTwo":
                hasKeyTwo = true;
                KeyIcon2.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, 1f);
                other.gameObject.SetActive(false);
                break;

            case "KeyThree":
                hasKeyThree = true;
                KeyIcon3.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, 1f);
                other.gameObject.SetActive(false);
                break;

            case "Door":
                if (hasKeyOne && hasKeyTwo && hasKeyThree)
                {
                    hasKeyOne = false;
                    hasKeyTwo = false;
                    hasKeyThree = false;
                    doorOpen = true;
                    other.gameObject.SetActive(false);
                }
                break;
        }
    }
    
    //----------
    

    private void OnCollisionStay2D(Collision2D other)
    {
        switch (other.collider.tag)
        {
            case "KeyOne":
                //
            case "KeyTwo":
                // do something
            case "KeyThree":
                // do something
            case "OpenedDoor":
                if (doorOpen)
                {
                    EnterDoorButton.SetActive(true);
                }
                break;
        }
    }
    
    //-------------
    
    // Checks whether collision is true, if not then door button isn't visible
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other != null)
        {
            EnterDoorButton.SetActive(false);            
        }
    }
    
    // Enter door and finish level
    public void EnterDoor()
    {
        Application.Quit();
        SceneManager.LoadScene(SceneData.homepage);
        
        // Unlock next level in level overview
        // Jump to next level scene
    }
}
