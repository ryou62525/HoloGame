using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.SpatialMapping;
using HoloToolkit.Unity.InputModule;
using System;

public class MakePlanes : MonoBehaviour, IInputClickHandler
{
    [SerializeField]
    private float _time = 1;

    private bool _makeStatus = false;

    void Start()
    {
        InputManager.Instance.PushFallbackInputHandler(gameObject);
        SurfaceMeshesToPlanes.Instance.MakePlanesComplete += SurfaceMeshesToPlanes_MakePlanesComplete;
        StartCoroutine(MakePlane());
    }

    /// <summary>
    /// 指定時間後にプレーンを生成します
    /// </summary>
    /// <returns></returns>
    IEnumerator MakePlane()
    {
        yield return new WaitForSeconds(_time);
        SpatialMappingManager.Instance.StopObserver();
        SurfaceMeshesToPlanes.Instance.MakePlanes();
        _makeStatus = true;
    }

    /// <summary>
    /// この関数を有効にするとタップジェスチャーしたときに空間マッピングが行われます
    /// </summary>
    /// <param name="eventData"></param>
    public void OnInputClicked(InputClickedEventData eventData)
    {
        //SpatialMappingManager.Instance.StopObserver();
        //SurfaceMeshesToPlanes.Instance.MakePlanes();
    }

    /// <summary>
    /// プレーンが生成されたらメッシュを削除する
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    private void SurfaceMeshesToPlanes_MakePlanesComplete(object source, EventArgs args)
    {
        RemoveSurfaceVertices.Instance.RemoveSurfaceVerticesWithinBounds(SurfaceMeshesToPlanes.Instance.ActivePlanes);
        SpatialMappingManager.Instance.DrawVisualMeshes = false;
    }

    public bool getMakeStatus()
    {
        return _makeStatus;
    }
}
