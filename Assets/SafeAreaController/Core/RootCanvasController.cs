using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SafeAreaMethodType {
    CutOff,
    FullScreen,
};

public enum AreaUpdateTiming {
    OnReciveMessage = (1 << 0),
    Awake           = (1 << 1),
    OnEnable        = (1 << 2),
    Start           = (1 << 3),
    Update          = (1 << 4),
    FixedUpdate     = (1 << 5),
};

public enum SafeAreaType {
    IphoneXTall,
    IphoneXWide
}

[RequireComponent(typeof(Canvas))]
public class RootCanvasController: MonoBehaviour {

    public SafeAreaMethodType ControlType = SafeAreaMethodType.FullScreen;

    [EnumMask]
    public AreaUpdateTiming UpdateTimming = AreaUpdateTiming.Awake;

    private Canvas _mainCanvas;

    // Update Function
    public void UpdateSubCanvasProperty() {
        var targetCanvasArray = GetComponentsInChildren<CanvasPropertyOverrider>();

        foreach (var targetCanvas in targetCanvasArray) {
            targetCanvas.UpdateCanvasProperty(_mainCanvas.sortingOrder);
        }
    }

    // Life cycle function
    private void Awake() {
        _mainCanvas = GetComponent<Canvas>();

        if (haveMask(AreaUpdateTiming.Awake))
           UpdateSubCanvasProperty();
    }

    private void OnEnable() {
        if (haveMask(AreaUpdateTiming.OnEnable))
            UpdateSubCanvasProperty();
    }

    private void Start() {
        if (haveMask(AreaUpdateTiming.Start))
            UpdateSubCanvasProperty();
    }

    private void Update() {
        if (haveMask(AreaUpdateTiming.Update))
            UpdateSubCanvasProperty();
    }

    private void FixedUpdate() {
        if (haveMask(AreaUpdateTiming.FixedUpdate))
            UpdateSubCanvasProperty();
    }

    // Utility
    private bool haveMask(AreaUpdateTiming mask) {
        return ((int)UpdateTimming & (int)mask) != 0;
    }

// =================================================================
// 		Functions 4 Editor
// =================================================================
	
}
