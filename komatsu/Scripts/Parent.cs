using UnityEngine;

public class Parent : MonoBehaviour
{
    [HideInInspector]
    public delegate void Callback ();
    private Callback _callback;

    [HideInInspector]
    public static MysteryHandler _mysteryHandler;

    public void SetCallback(Callback callback)
    {
        this._callback = callback;
        this._callback();
    }
}