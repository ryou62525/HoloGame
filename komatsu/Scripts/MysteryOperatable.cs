using UnityEngine;

public class MysteryOperatable : MonoBehaviour
{
    protected MysteryManager _mysteryManager;

    private void Awake()
    {
        _mysteryManager = MysteryManager.Instance;
    }

    public void EnableHundl()
    {
        if(_mysteryManager._mysteryHandler != null)
        {
            Debug.Log(_mysteryManager._mysteryHandler.name + "mysteryHandler is modify");
            _mysteryManager._mysteryHandler.enabled = false;
        }

        _mysteryManager._mysteryHandler = this.GetComponent<MysteryHandler>();
        _mysteryManager._mysteryHandler.enabled = true;
    }
}