using DG.Tweening;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using UnityEngine;

public class InsertFrame : MonoBehaviour, IInputClickHandler
{
    [SerializeField, Range(0, 10)] private float _moveTime = 1;
    [SerializeField] private float _rayLength = 10;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private int _id = 0;

    private Ray _ray;
    private RaycastHit _hit;
    private Color _color;

    private Vector3 _startPos;
    private float _startTime;

    private bool _isSelected = false;       //オブジェクトをホールドしているかどうか(ジェスチャーで切り替え)
    private bool _isInserting = false;      //オブジェクトが正解の位置に移動しているかどうか
    private bool _isCorrectAnswer = false;
    private bool _isJudge = false;          //はまっているかの判定処理を行う
    private PieaceSpace space;

    /// <summary>
    /// 3つピースがはまったらクリアフラグを出す.
    /// </summary>
    private static int correctCount;

    public float amplitude = 0.01f; // 振幅
    private int frameCnt; // フレームカウント
   
    void Start()
    {
        frameCnt = Random.Range(-100, 100);
    }

    private void Update()
    {
        if(_isSelected && !_isCorrectAnswer)
        {
            gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
        }

        //選択していない
        if (!_isSelected && !_isCorrectAnswer)
        {
            //イージング
            frameCnt += 1;
            if (10000 <= frameCnt)
            {
                frameCnt = 0;
            }
            if (0 == frameCnt % 2)
            {
                // 上下に振動させる（ふわふわを表現）
                float posYSin = Mathf.Sin(2.0f * Mathf.PI * (float)(frameCnt % 200) / (200.0f - 1.0f));
                transform.DOMove(new Vector3(transform.position.x,
                                                amplitude * posYSin + transform.position.y,
                                                transform.position.z), 0);
            }
        }

        //正解の場所でピースを離した-------------
        if (!_isCorrectAnswer) return;

        if (!_isInserting)
        {
            OnMovingStarted();
            _isInserting = true;
            CheckClear(gameObject);
        }

        InsertMoving();
    }

    private void OnTriggerEnter(Collider other)
    {
        space = other.transform.GetComponent<PieaceSpace>();
        if (space != null)
        {
            _isJudge = true; //判定処理を行う.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isJudge = false;
    }

    /// <summary>
    /// タップジェスチャーの処理（切り替え）
    /// </summary>
    /// <param name="eventData"></param>
    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (!_isInserting) _isSelected = !_isSelected;

        if (space != null && !_isSelected && _isJudge)
        {
            //選択中、ピースがはめ込む場所の周辺にある時
            _isCorrectAnswer = space._id == _id ? true : false;
        }
    }

    /// <summary>
    /// Quiz4クリアを通知する.
    /// </summary>
    /// <returns></returns>
    public IEnumerator NotifyQuiz4Clear()
    {
        yield return new WaitForSeconds(3.0f);
        TIManager.Instance.Quiz4End();
    }

    private void InsertMoving()
    {
        var diff = Time.timeSinceLevelLoad - _startTime;    //開始からの経過時間

        if (diff > _moveTime)
        {
            transform.position = _endPoint.position;
            enabled = false;
        }

        var rate = diff / _moveTime;
        transform.position = Vector3.Lerp(_startPos, _endPoint.position, rate);
      

        if (this.transform.position.Equals(_endPoint.position))
        {
            //移動し終わったときの処理
            SoundManager.Instance.PlaySe("Peace", false, transform.position);
        }
    }

    /// <summary>
    /// 正解の場所でオブジェクトを離したとき
    /// </summary>
    private void OnMovingStarted()
    {
        if (_moveTime <= 0)
        {
            transform.position = _endPoint.position;
            enabled = false;
            return;
        }

        //正解の場所でオブジェクトを離した瞬間の時間を保存
        _startTime = Time.timeSinceLevelLoad;
        _startPos = transform.position;
    }

    /// <summary>
    /// ピースがはまるたびに呼び出される.クリアしたかを確認する.
    /// </summary>
    /// <param name="peace"></param>
    private static void CheckClear(GameObject peace)
    {
        correctCount++;
        if(correctCount>=3)
        {
            peace.GetComponent<InsertFrame>().StartCoroutine(peace.GetComponent<InsertFrame>().NotifyQuiz4Clear());
            correctCount = -1; //二回目は呼ばない.
        }
    }

    
}
