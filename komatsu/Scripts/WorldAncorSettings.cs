using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.UI;

public class WorldAncorSettings : SingletonMonoBehaviour<WorldAncorSettings>, ISourceStateHandler, IInputHandler
{
    [HideInInspector] public bool IsEditingPlaced;

    [SerializeField] private GameObject SettingsCanvas = null;
    [SerializeField] private GameObject[] worldAncorObject = null;

    IInputSource currentInputSouce;
    uint id;

    private float time;
    public Text[] text;

    private bool isDraged;
    private bool isSettings;
    private bool canSettings;

    enum TextArray
    {
        ActionState = 0,
        POS_X,
        POS_Y,
        POS_Z,
        ROT_X,
        ROT_Y,
        ROT_Z,

        MAX
    }

    // Use this for initialization
    void Start()
    {
        InputManager.Instance.PushFallbackInputHandler(gameObject);
    }

    private void Update()
    {
        OnSourceDraged();

    }

    /// <summary>
    /// ドラッグした時
    /// </summary>
    private void OnSourceDraged()
    {
        if (!isDraged) return;
        text[7].text = "ゆびをおろしました: "+ canSettings;

        if (canSettings)
        {
            Vector3 pos;
            currentInputSouce.TryGetPointerPosition(id, out pos);
            time += Time.deltaTime;

            if (time < 20)text[7].text = "カウントしています: " + time.ToString();

            if (time >= 10)
            {
                text[7].text = "エディットモードを開始します";
                Debug.Log("エディットモードを開始します");
                SettingsCanvas.SetActive(true);
                IsEditingPlaced = true;
                SetSettingsEnable(true);
            }
        }

        //TODO:
        if (!canSettings) return;
    }

    /// <summary>
    /// クリックしたとき
    /// </summary>
    /// <param name="eventData"></param>
    public void OnInputDown(InputEventData eventData)
    {
        if (!eventData.InputSource.SupportsInputInfo(eventData.SourceId, SupportedInputInfo.Position)) return;

        currentInputSouce = eventData.InputSource;
        id = eventData.SourceId;
        isDraged = true;

        //TODO:
        if (!canSettings) return;
        var obj = GazeManager.Instance.HitObject;
        if (obj != null)
        {
            text[(int)TextArray.ActionState].text = obj.transform.name;

            text[(int)TextArray.POS_X].text = "x: " + obj.transform.position.x.ToString();
            text[(int)TextArray.POS_Y].text = "y: " + obj.transform.position.y.ToString();
            text[(int)TextArray.POS_Z].text = "z: " + obj.transform.position.z.ToString();

            text[(int)TextArray.ROT_X].text = "x: " + obj.transform.rotation.x.ToString();
            text[(int)TextArray.ROT_Y].text = "y: " + obj.transform.rotation.y.ToString();
            text[(int)TextArray.ROT_Z].text = "z: " + obj.transform.rotation.z.ToString();
        }
    }

    /// <summary>
    /// 指をあげたとき
    /// </summary>
    /// <param name="eventData"></param>
    public void OnInputUp(InputEventData eventData)
    {
        isDraged = false;
        time = 0;
        if (!canSettings)
        {
            canSettings = true;
        }
        //TODO:
        if (!canSettings) return;
    }

    /// <summary>
    /// 手を検出したとき
    /// </summary>
    /// <param name="eventData"></param>
    public void OnSourceDetected(SourceStateEventData eventData)
    {
        text[7].text = "手を検出しました";
        //TODO:
        if (!canSettings) return;
    }

    /// <summary>
    /// 手がセンサー外に出たとき
    /// </summary>
    /// <param name="eventData"></param>
    public void OnSourceLost(SourceStateEventData eventData)
    {
        isDraged = false;
        text[7].text = "手をロストしました";
        time = 0;
        //TODO:
        if (!canSettings) return;
    }

    public void EndSettings()
    {
        if (!canSettings) return;
        canSettings = false;
        SettingsCanvas.SetActive(false);
        time = 0;
        IsEditingPlaced = false;
        SetSettingsEnable(false);
    }

    private void SetSettingsEnable(bool flug)
    {
        for (int i = 0; i < worldAncorObject.Length; i++)
        {
            worldAncorObject[i].GetComponent<MeshRenderer>().enabled = flug;
            worldAncorObject[i].GetComponent<BoxCollider>().enabled = flug;
        }
    }
}
