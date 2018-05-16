using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyObject : MonoBehaviour
{
    public GameObject[] _obj = null;
    private void Awake()
    {
        foreach (var item in _obj)
        {
             DontDestroyOnLoad(item);
        }
        SceneManager.LoadScene(1);
    }
}
