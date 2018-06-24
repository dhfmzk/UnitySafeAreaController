using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(RectTransform))]
public class CanvasPropertyOverrider : MonoBehaviour {

    public bool isSafeCanvas = true;
    public int additionalSortingOrder = 0;

    // Update Method
    public void UpdateCanvasProperty(int rootSortingOrder) {

        // 0. Get Value
        Canvas myCanvas = GetComponent<Canvas>();
        RectTransform myTransform = GetComponent<RectTransform>();
        Rect safeArea = Screen.safeArea;
        Vector2 screen = new Vector2(Screen.width, Screen.height);

        Vector2 _saAnchorMin;
        Vector2 _saAnchorMax;

        // 1. Setup and apply safe area
        if (isSafeCanvas) {
            _saAnchorMin.x = safeArea.x / screen.x;
            _saAnchorMin.y = safeArea.y / screen.y;
            _saAnchorMax.x = (safeArea.x + safeArea.width) / screen.x;
            _saAnchorMax.y = (safeArea.y + safeArea.height) / screen.y;

            myTransform.anchorMin = _saAnchorMin;
            myTransform.anchorMax = _saAnchorMax;
        }

        // 2. Add aditional sorting order
        myCanvas.sortingOrder = rootSortingOrder + additionalSortingOrder;
    }
}
