using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Vector3 _moveDir;
    public float _lifeTime = 0;

    void Start()
    {
        Debug.Log("Start");
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (_lifeTime > 0)
        {
            Debug.Log("Start");
           
            this.transform.position += _moveDir;
           

            yield return null;
        }
    }
}
