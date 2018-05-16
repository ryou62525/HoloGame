using UnityEngine.SceneManagement;
using UnityEngine;

public class PleLoadManager : MonoBehaviour
{
    public GameObject[] _obj = null;

    private void Awake()
    {
        SoundManager.Instance.Init();

        foreach (var item in _obj)
        {
            DontDestroyOnLoad(item);
        }
        SceneManager.LoadScene(1);
    }
}