﻿using System;
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

    public bool addSoftkeyArea = false;

    [EnumMask]
    public AreaUpdateTiming UpdateTimming = AreaUpdateTiming.Awake;

    private Canvas _mainCanvas;
    private Rect Offset { get { return new Rect(0, 0, 0, navigationBarHeight); } }
    private static int navigationBarHeight = 0;

    // Update Canvas Function
    private void UpdateSubCanvasProperty()
    {
        var targetCanvasArray = GetComponentsInChildren<CanvasPropertyOverrider>();

        foreach (var targetCanvas in targetCanvasArray)
        {
            targetCanvas.UpdateCanvasProperty(_mainCanvas.sortingOrder, Offset);
        }
    }

    // Update Camera Function
    private void UpdateCameraProperty()
    {
        var targetCameraArray = FindObjectsOfType<CameraPropertyOverrider>();

        foreach (var targetCamera in targetCameraArray)
        {
            targetCamera.UpdateCameraProperty(Offset);
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
#if UNITY_ANDROID && !UNITY_EDITOR
        if (addSoftkeyArea)
            RunOnAndroidUiThread(GetNavigationBarHeight);
#endif

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

// =================================================================
// 		Functions 4 Android
// =================================================================

    private static void RunOnAndroidUiThread(Action target)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
        using (var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {

            activity.Call("runOnUiThread", new AndroidJavaRunnable(target));
        }}
#elif UNITY_EDITOR
        target();
#endif
    }

    private static void GetNavigationBarHeight()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
        using (var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
        using (var window = activity.Call<AndroidJavaObject>("getWindow")) {
        using (var resources = activity.Call<AndroidJavaObject>("getResources")) {
            var resourceId = resources.Call<int>("getIdentifier", "navigation_bar_height", "dimen", "android");
            if (resourceId > 0) {
                navigationBarHeight = resources.Call<int>("getDimensionPixelSize", resourceId);
            }
        }}}}
#elif UNITY_EDITOR
        navigationBarHeight = 0;
#endif
    }
}
