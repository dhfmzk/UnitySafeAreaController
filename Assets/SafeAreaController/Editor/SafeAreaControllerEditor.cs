using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (SafeAreaController))]
public class SafeAreaControllerEditor : Editor {

	public eSafeAreaType testType;
	public bool isSafeAreaOn;

	public override void OnInspectorGUI() {
		base.OnInspectorGUI ();

		SafeAreaController saController = target as SafeAreaController;

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Safe-Area Test in Editor");

		testType = (eSafeAreaType)EditorGUILayout.EnumPopup("Test Device Type:", testType);
		
		if(!isSafeAreaOn) {
			if (GUILayout.Button("Show Safe-Area")) {
				if (saController) {
					saController.ShowSafeAreaFrame(testType);
					isSafeAreaOn = true;
				}
			}
		}
		else {
			if (GUILayout.Button("Hide Safe-Area") ) {
				if (saController) {
					saController.HideSafeAreaFrame(testType);
					isSafeAreaOn = false;
				}
			}
		}

		EditorGUILayout.Space();
	}
}
