using UnityEngine;
using HoloToolkit.Unity.InputModule;

[RequireComponent(typeof(MysteryHandler))]
public class MysteryOperator : MysteryOperatable, IInputClickHandler
{
    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("Input clicked!");
        _mysteryManager.SetCallback(EnableHundl);
    }
}
