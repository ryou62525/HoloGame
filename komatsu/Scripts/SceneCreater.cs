using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCreater : MonoBehaviour
{
    public string[] _scenes;
    
    void Start()
    {
        foreach (var scene in _scenes)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
    }
}