using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SafeAreaMethodType
{
    CanvasBased,
    CameraBased,
};

public enum AreaUpdateTiming
{
    OnReciveMessage = (1 << 0),
    Awake = (1 << 1),
    OnEnable = (1 << 2),
    Start = (1 << 3),
    Update = (1 << 4),
    FixedUpdate = (1 << 5),
};

[RequireComponent(typeof(Canvas))]
public class SafeAreaController : MonoBehaviour
{
    public SafeAreaMethodType ControlType = SafeAreaMethodType.CanvasBased;

    [EnumMask]
    public AreaUpdateTiming UpdateTimming = AreaUpdateTiming.Awake;

    private Canvas _mainCanvas;

    // Update Canvas Function
    private void UpdateSubCanvasProperty()
    {
        var targetCanvasArray = GetComponentsInChildren<CanvasPropertyOverrider>();

        foreach (var targetCanvas in targetCanvasArray)
        {
            targetCanvas.UpdateCanvasProperty(_mainCanvas.sortingOrder);
        }
    }

    // Update Camera Function
    private void UpdateCameraProperty()
    {
        var targetCameraArray = FindObjectsOfType<CameraPropertyOverrider>();

        foreach (var targetCamera in targetCameraArray)
        {
            targetCamera.UpdateCameraProperty();
        }
    }

    // Update Function
    public void UpdateSafeArea()
    {
        switch (this.ControlType)
        {
            case SafeAreaMethodType.CanvasBased:
                UpdateSubCanvasProperty();
                break;
            case SafeAreaMethodType.CameraBased:
                UpdateCameraProperty();
                break;
            default:
                break;
        }
    }

    // Life cycle function
    private void Awake()
    {
        _mainCanvas = GetComponent<Canvas>();

        if (HaveMask(AreaUpdateTiming.Awake))
            UpdateSafeArea();
    }

    private void OnEnable()
    {
        if (HaveMask(AreaUpdateTiming.OnEnable))
            UpdateSafeArea();
    }

    private void Start()
    {
        if (HaveMask(AreaUpdateTiming.Start))
            UpdateSafeArea();
    }

    private void Update()
    {
        if (HaveMask(AreaUpdateTiming.Update))
            UpdateSafeArea();
    }

    private void FixedUpdate()
    {
        if (HaveMask(AreaUpdateTiming.FixedUpdate))
            UpdateSafeArea();
    }

    // Utility
    private bool HaveMask(AreaUpdateTiming mask)
    {
        return ((int)UpdateTimming & (int)mask) != 0;
    }
}
