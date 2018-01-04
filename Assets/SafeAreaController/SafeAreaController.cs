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

	[HideInInspector]
	public bool isSafeAreaOn;
	private GameObject frameDeco;
	private Vector2 saAnchorMin;
	private Vector2 saAnchorMax;

	private void UpdateSafeArea(Rect _safeArea, Vector2 _screen) {

		// Get Safe Area and calc anchor (float)
		this.saAnchorMin.x = _safeArea.x / _screen.x;
		this.saAnchorMin.y = _safeArea.y / _screen.y;
		this.saAnchorMax.x = (_safeArea.x + _safeArea.width) / _screen.x;
		this.saAnchorMax.y = (_safeArea.y + _safeArea.height) / _screen.y;

		// Set anchors
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
		if (_type == eSafeAreaType.IPHONE_X_WIDE) { UpdateSafeArea(new Rect (132, 0, 2202, 1125), new Vector2(2436.0f, 1125.0f)); }

	}

	public void HideSafeAreaFrame(eSafeAreaType _type) {

		UpdateSafeArea (new Rect(0, 0, 1, 1), new Vector2(1, 1));

	}

	public void SetupDeviceFrame(eSafeAreaType _type) {

		// Get prefab name and path
		string prefabName = "TA Deco";
		string prefabPath = _type.ToString() + "/" + prefabName;

		// Make and setup deco game object
		GameObject prefab = Resources.Load(prefabPath) as GameObject;
		this.frameDeco = MonoBehaviour.Instantiate (prefab) as GameObject;
		frameDeco.name = prefabName;
		
		RectTransform frameDecoRectTransform = frameDeco.GetComponent<RectTransform>();
		frameDecoRectTransform.SetParent(this.transform);

		frameDecoRectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		frameDecoRectTransform.offsetMin = new Vector2(0.0f, 0.0f);
		frameDecoRectTransform.offsetMax = new Vector2(0.0f, 0.0f);

		// Setup Scale (because canvas base scale)
		Vector3 scaleFactor = this.GetComponent<RectTransform>().localScale;
		foreach(Transform tr in frameDeco.transform) {
			RectTransform temp = tr.GetComponent<RectTransform>();
			temp.localScale = new Vector3(temp.localScale.x / scaleFactor.x, temp.localScale.y / scaleFactor.y, temp.localScale.z / scaleFactor.z);
		}

	}

	public void DeleteDeviceFrame() {

		Queue<GameObject> temp = new Queue<GameObject>();

		// Find "TA Deco" objects
		foreach(Transform tr in this.transform) {
			if(tr.name == "TA Deco") {
				temp.Enqueue(tr.gameObject);
			}
		}

		// And destroy
		while(temp.Count > 0) {
			if(Application.isEditor) {
				Object.DestroyImmediate(temp.Dequeue());
			}
			else {
				Object.Destroy(temp.Dequeue());
			}
		}
	}
}
