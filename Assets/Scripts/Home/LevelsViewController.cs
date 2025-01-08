using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsViewController : MonoBehaviour
{
    public void CloseLevelsView()
    {
        SceneManager.LoadScene(SceneData.homepage);
    }
}
