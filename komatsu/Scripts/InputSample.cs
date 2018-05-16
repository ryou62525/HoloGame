using System.Collections;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class InputSample : MonoBehaviour, IInputClickHandler, ISourceStateHandler, IInputHandler
{
    private bool isHold = false;
    private bool isSettingMode = false;
    private bool isFocused = false;

    private float ChangingTime = 10;
    private float holdTime = 0;
   
    void Start()
    {
        InputManager.Instance.AddGlobalListener(gameObject);

        StartCoroutine(CoroutineUpdate());
    }

    IEnumerator CoroutineUpdate()
    {
        while (true)
        {
            var obj = GazeManager.Instance.HitObject;
            var obj2 = GazeManager.Instance.HitInfo;
            
            if (obj != null)
            {
                Debug.Log("あたっています");
            }
            else
            {
                Debug.Log("あたっていません");
            }

            if (isHold)
            {
                Debug.Log("Update");

                holdTime += Time.deltaTime;

                
                //指定秒数以上指をおろし続けたら設定モードに変更
                if (!isFocused && holdTime >= ChangingTime)
                {
                    isSettingMode = true;
                }
            }

            yield return null;
        }
    }

    //クリック操作
    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("クリックしました");
    }

    public void OnInputDown(InputEventData eventData)
    {
        isHold = true;
        Debug.Log("指をさげました" + isHold);
    }

    public void OnInputUp(InputEventData eventData)
    {
        isHold = false;
        Debug.Log("指をさげました" + isHold);
    }

    //手の検出
    public void OnSourceDetected(SourceStateEventData eventData)
    {
        Debug.Log("");
    }

    public void OnSourceLost(SourceStateEventData eventData)
    {
        Debug.Log("");
    }


   
}
