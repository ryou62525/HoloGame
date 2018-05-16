
using System.Collections;
using UnityEngine;

public class TransformSettings : MonoBehaviour
{
    [SerializeField] private Transform _worldAncorObject;
    [SerializeField] private Vector3 _distance = Vector3.zero;
    [SerializeField] private Vector3 _rotation = Vector3.zero;

    private void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {

            var obj = transform.GetChild(i).gameObject.GetComponent<MeshRenderer>();
            if(obj != null)
            {
                obj.enabled = false;
            }
        }
        Invoke("SetPosition", 0.5f);
    }

    private void SetPosition()
    {
        if (_worldAncorObject != null)
        {
            Debug.Log(_worldAncorObject);
            this.gameObject.transform.position = _worldAncorObject.position + _distance;
            this.gameObject.transform.Rotate(_rotation);
            this.gameObject.SetActive(true);
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            var obj = transform.GetChild(i).gameObject.GetComponent<MeshRenderer>();
            if (obj != null)
            {
                obj.enabled = true;
            }
        }
    }
}
