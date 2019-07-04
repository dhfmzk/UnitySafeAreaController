using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(RectTransform))]
public class CanvasPropertyOverrider : MonoBehaviour
{
    public bool isSafeCanvas = true;
    public int additionalSortingOrder = 0;

    // Update Method
    public void UpdateCanvasProperty(int rootSortingOrder, Rect offset)
    {
        // 0. Get Value
        Canvas myCanvas = GetComponent<Canvas>();
        RectTransform myTransform = GetComponent<RectTransform>();
        Rect safeArea = Screen.safeArea;
        Vector2 screen = new Vector2(Screen.width, Screen.height);

        Vector2 _saAnchorMin;
        Vector2 _saAnchorMax;

        var offset_right    = offset.x;
        var offset_left     = offset.y;
        var offset_top      = offset.width;
        var offset_bottom   = offset.height;

        // 1. Setup and apply safe area
        if (isSafeCanvas)
        {
            _saAnchorMin.x = (safeArea.x + offset_right) / screen.x;
            _saAnchorMin.y = (safeArea.y + offset_bottom) / screen.y;
            _saAnchorMax.x = (safeArea.x + safeArea.width - offset_top) / screen.x;
            _saAnchorMax.y = (safeArea.y + safeArea.height - offset_left) / screen.y;

            myTransform.anchorMin = _saAnchorMin;
            myTransform.anchorMax = _saAnchorMax;
        }

        // 2. Add aditional sorting order
        myCanvas.sortingOrder = rootSortingOrder + additionalSortingOrder;
    }
}
