using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eAreaUpdateTiming {
    onAwake,
    onStart,
    onUpdate,
    onFixedUpdate
};

public enum eSafeAreaType {
	IPHONE_X_TALL,
	IPHONE_X_WIDE
}

public class SafeAreaController : MonoBehaviour {

	public eAreaUpdateTiming updateTimming = eAreaUpdateTiming.onAwake;

	public List<RectTransform> safeAreaList;

	private Rect safeArea;
	private RectTransform rootRectTransform = null;

	private Vector2 saAnchorMin;
	private Vector2 saAnchorMax;

	private void UpdateSafeArea(Rect _safeArea, Vector2 _screen) {

		if(rootRectTransform == null) {
			this.rootRectTransform = this.GetComponent<RectTransform>();
		}

		// Get Safe Area and calc anchor (float)
		this.saAnchorMin.x = _safeArea.x / _screen.x;
		this.saAnchorMin.y = _safeArea.y / _screen.y;
		this.saAnchorMax.x = (_safeArea.x + _safeArea.width) / _screen.x;
		this.saAnchorMax.y = (_safeArea.y + _safeArea.height) / _screen.y;


		// Set anchor
		this.rootRectTransform.anchorMin = saAnchorMin;
		this.rootRectTransform.anchorMax = saAnchorMax;

		// Set anchor onther roots
		foreach(RectTransform rt in safeAreaList) {
			rt.anchorMin = saAnchorMin;
			rt.anchorMax = saAnchorMax;
		}

    } 

	private void Awake() {
		if(updateTimming == eAreaUpdateTiming.onAwake) {
			UpdateSafeArea (Screen.safeArea, new Vector2(Screen.width, Screen.height));
        }
    }

	private void Start () {
		if(updateTimming == eAreaUpdateTiming.onStart) {
			UpdateSafeArea (Screen.safeArea, new Vector2(Screen.width, Screen.height));
        }
	}
	
	private void Update () {
		if(updateTimming == eAreaUpdateTiming.onUpdate) {
			UpdateSafeArea (Screen.safeArea, new Vector2(Screen.width, Screen.height));
        }
	}

	private void FixedUpdate() {
		if(updateTimming == eAreaUpdateTiming.onFixedUpdate) {
			UpdateSafeArea (Screen.safeArea, new Vector2(Screen.width, Screen.height));
        }
    }

// =================================================================
// 		Functions 4 Editor
// =================================================================
	public void ShowSafeAreaFrame(eSafeAreaType _type) {
		
		// Listing SafeArea Device
		if (_type == eSafeAreaType.IPHONE_X_TALL) { UpdateSafeArea(new Rect (0, 102, 1125, 2202), new Vector2(1125.0f, 2436.0f)); }
		if (_type == eSafeAreaType.IPHONE_X_WIDE) { UpdateSafeArea(new Rect (102, 0, 2202, 1125), new Vector2(2436.0f, 1125.0f)); }

	}

	public void HideSafeAreaFrame(eSafeAreaType _type) {

		UpdateSafeArea (new Rect(0, 0, 1, 1), new Vector2(1, 1));

	}
}
