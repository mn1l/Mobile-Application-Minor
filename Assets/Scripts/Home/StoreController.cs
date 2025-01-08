using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreController : MonoBehaviour
{
    public void CloseStore()
    {
        SceneManager.LoadScene(SceneData.homepage);
    }
}
