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
    OnAwake,
    OnStart,
    OnUpdate,
    OnFixedUpdate
};

public enum SafeAreaType {
	IphoneXTall,
	IphoneXWide
}

public class SafeAreaController : MonoBehaviour {

    public SafeAreaMethodType ControlType = SafeAreaMethodType.CutOff;

    private Camera _mainCamera;
    private Camera _backGroundCamera;
    public List<Camera> SubCameraList;

    // for Cut Off Type

    // for Full Screan Type
    public AreaUpdateTiming UpdateTimming = AreaUpdateTiming.OnAwake;
	public List<RectTransform> SafeAreaList;


	[HideInInspector]
	public bool IsSafeAreaOn;
	private GameObject _frameDeco;
	private Vector2 _saAnchorMin;
	private Vector2 _saAnchorMax;

    // For Cut Off Type
    private void UpdateCameraRect() {
        _mainCamera = GetComponent<Camera>();
        if (_mainCamera == null) {
            _mainCamera = gameObject.AddComponent<Camera>();
        }
        
    }

    private void UpdateSafeArea(Rect safeArea, Vector2 screen) {
		// Get Safe Area and calc anchor (float)
		this._saAnchorMin.x = safeArea.x / screen.x;
		this._saAnchorMin.y = safeArea.y / screen.y;
		this._saAnchorMax.x = (safeArea.x + safeArea.width) / screen.x;
		this._saAnchorMax.y = (safeArea.y + safeArea.height) / screen.y;
		// Set anchors
		foreach(RectTransform rt in SafeAreaList) {
			rt.anchorMin = _saAnchorMin;
			rt.anchorMax = _saAnchorMax;
		}
    } 

	private void Awake() {
		if(UpdateTimming == AreaUpdateTiming.OnAwake) {
			UpdateSafeArea(Screen.safeArea, new Vector2(Screen.width, Screen.height));
        }
	}

	private void Start () {
		if(UpdateTimming == AreaUpdateTiming.OnStart) {
			UpdateSafeArea(Screen.safeArea, new Vector2(Screen.width, Screen.height));
        }
	}
	
	private void Update () {
		if(UpdateTimming == AreaUpdateTiming.OnUpdate) {
			UpdateSafeArea(Screen.safeArea, new Vector2(Screen.width, Screen.height));
        }
	}

	private void FixedUpdate() {
		if(UpdateTimming == AreaUpdateTiming.OnFixedUpdate) {
			UpdateSafeArea(Screen.safeArea, new Vector2(Screen.width, Screen.height));
        }
    }

// =================================================================
// 		Functions 4 Editor
// =================================================================
	public void ShowSafeAreaFrame(SafeAreaType type) {
	    switch (type) {
	        // Listing SafeArea Device
	        case SafeAreaType.IphoneXTall:
	            UpdateSafeArea(new Rect (0, 102, 1125, 2202), new Vector2(1125.0f, 2436.0f));
	            break;
	        case SafeAreaType.IphoneXWide:
	            UpdateSafeArea(new Rect (132, 0, 2202, 1125), new Vector2(2436.0f, 1125.0f));
	            break;
	        default:
	            throw new ArgumentOutOfRangeException("type", type, null);
	    }
	}

	public void HideSafeAreaFrame(SafeAreaType type) {
		UpdateSafeArea (new Rect(0, 0, 1, 1), new Vector2(1, 1));
	}

	public void SetupDeviceFrame(SafeAreaType type) {

		// Get prefab name and path
		string prefabName = "TA Deco";
		string prefabPath = type.ToString() + "/" + prefabName;

		// Make and setup deco game object
		GameObject prefab = Resources.Load(prefabPath) as GameObject;
		this._frameDeco = MonoBehaviour.Instantiate (prefab) as GameObject;
		_frameDeco.name = prefabName;
		
		RectTransform frameDecoRectTransform = _frameDeco.GetComponent<RectTransform>();
		frameDecoRectTransform.SetParent(this.transform);

		frameDecoRectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		frameDecoRectTransform.offsetMin = new Vector2(0.0f, 0.0f);
		frameDecoRectTransform.offsetMax = new Vector2(0.0f, 0.0f);

		// Setup Scale (because canvas base scale)
		Vector3 scaleFactor = this.GetComponent<RectTransform>().localScale;
		foreach(Transform tr in _frameDeco.transform) {
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
			    DestroyImmediate(temp.Dequeue());
			}
			else {
			    Destroy(temp.Dequeue());
			}
		}
	}
}
