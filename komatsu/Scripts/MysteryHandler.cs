using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _managimentItems = null;

    private void OnEnable()
    {
        foreach (var item in _managimentItems)
        {
            item.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if (_managimentItems == null) return;

        foreach (var item in _managimentItems)
        {
            if (item == null) return;
            item.gameObject.SetActive(false);
        }
    }
}
