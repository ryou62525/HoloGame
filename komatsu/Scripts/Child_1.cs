using UnityEngine;
using HoloToolkit.Unity.InputModule;

[RequireComponent(typeof(MysteryHandler))]
public class Child_1 : MysteryManager, IInputClickHandler
{
    public void OnInputClicked(InputClickedEventData eventData)
    {
        SetCallback(Hoge);
    }

    void Hoge()
    {
        if(_mysteryHandler != null)
        {
            Debug.Log(_mysteryHandler.name + "mysteryHandler is modify");
            _mysteryHandler.enabled = false;
        }

        _mysteryHandler = this.GetComponent<MysteryHandler>();
        _mysteryHandler.enabled = true;
    }
}