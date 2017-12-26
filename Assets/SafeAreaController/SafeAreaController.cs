using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AreaUpdateTiming {
    onAwake,
    onStart,
    onUpdate,
    onFixedUpdate
};

public class SafeAreaController : MonoBehaviour {

    public AreaUpdateTiming updateTimming = AreaUpdateTiming.onAwake;

    private Rect safeArea;

    void UpdateSafeArea() {
        this.safeArea = Screen.safeArea;


    } 

    void Awake() {
        if(updateTimming == AreaUpdateTiming.onAwake) {
        }
    }

    void Start () {
        if(updateTimming == AreaUpdateTiming.onStart) {
        }
	}
	
	void Update () {
        if(updateTimming == AreaUpdateTiming.onUpdate) {
        }
	}

    void FixedUpdate() {
        if(updateTimming == AreaUpdateTiming.onFixedUpdate) {
        }
    }
}
