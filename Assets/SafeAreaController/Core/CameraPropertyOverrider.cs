using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraPropertyOverrider : MonoBehaviour
{
    public bool isSafeAreaCamera = true;
    public bool useFullSize = true;

    private Camera myCamera;
    private Rect RectSize
    {
        get
        {
            if (myCamera == null)
            {
                myCamera = GetComponent<Camera>();
            }
            return myCamera.rect;
        }
    }

    private Rect SafeSize
    {
        get
        {
            float safeX = Screen.safeArea.x / Screen.width;
            float safeY = Screen.safeArea.y / Screen.height;
            float safeW = Screen.safeArea.width / Screen.width;
            float safeH = Screen.safeArea.height / Screen.height;

            Rect originalRect = useFullSize ? new Rect(0, 0, 1, 1) : RectSize;

            return new Rect(safeX + originalRect.x / safeW,
                            safeY + originalRect.y / safeH,
                            safeW / originalRect.width,
                            safeH / originalRect.height);
        }
    }

    // Update Method
    public void UpdateCameraProperty(Rect offset)
    {
        if (isSafeAreaCamera)
        {
            myCamera.rect = SafeSize;
        }
        else
        {
            myCamera.rect = useFullSize ? new Rect(0, 0, 1, 1) : RectSize;
        }
    }
}
