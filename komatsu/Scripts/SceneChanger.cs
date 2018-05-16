using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public enum SceneName
    {
        Mystery1,
        Mystery2,
        Mystery3,
        Mystery4
    }

    [SerializeField]
    private SceneName _sceneName = 0;

    public void OnClicked()
    {
        SceneManager.LoadScene((int)_sceneName);
    }
}
