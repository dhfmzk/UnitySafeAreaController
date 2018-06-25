using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class CameraPropertyOverrider : MonoBehaviour {

    public bool isSafeAreaCamera = true;
    public bool useSelfSize = true;
    public Rect selfSize;

    private Rect _safeSize {
        get {
            float safeX = Screen.safeArea.x / Screen.width;
            float safeY = Screen.safeArea.y / Screen.height;
            float safeW = Screen.safeArea.width / Screen.width;
            float safeH = Screen.safeArea.height / Screen.height;

            Rect originalRect = useSelfSize ? selfSize : new Rect(0, 0, 1, 1);

            return new Rect(safeX + originalRect.x / safeW,
                            safeY + originalRect.y / safeH,
                            originalRect.width / safeW,
                            originalRect.height / safeH);
        }
    }

    // Update Method
    public void UpdateCameraProperty() {
        Camera myCamera = GetComponent<Camera>();

        if (isSafeAreaCamera) {
            myCamera.rect = _safeSize;
        }
        else {
            myCamera.rect = useSelfSize ? selfSize : new Rect(0, 0, 1, 1);
        } 
    }
}
