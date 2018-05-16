using UnityEngine;

public class MysteryManager : SingletonMonoBehaviour<MysteryManager>
{
    public enum MysteryNumber : int
    {
        TUTORIAL = 0,
        MYSTERY_1,
        MYSTERY_2,
        MYSTERY_3,
        MYSTERY_4,

        MYSTERY_SIZE
    }

    [HideInInspector]
    public delegate void Callback ();
    private Callback _callback;

    [HideInInspector]
    public MysteryHandler _mysteryHandler;

    private Camera player = null;

    static bool[] _isClerGames = new bool[(int)MysteryNumber.MYSTERY_SIZE];

    private void Start()
    {
        player = Camera.main;
    }

    public void SetCallback(Callback callback)
    {
        this._callback = callback;
        this._callback();
    }

    public void SetMysteryState(int num)
    {
        Debug.Log("num: " + num);

    }
}