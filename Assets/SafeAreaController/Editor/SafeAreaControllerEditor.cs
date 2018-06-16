using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (SafeAreaController))]
public class SafeAreaControllerEditor : Editor {

	public SafeAreaType testType;

	public override void OnInspectorGUI() {
		base.OnInspectorGUI ();

		SafeAreaController saController = target as SafeAreaController;

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Safe-Area Test in Editor");

		testType = (SafeAreaType)EditorGUILayout.EnumPopup("Test Device Type:", testType);
		
		if(!saController.IsSafeAreaOn) {
			if (GUILayout.Button("Show Safe-Area")) {
				if (saController) {
					saController.ShowSafeAreaFrame(testType);
					saController.SetupDeviceFrame(testType);
					saController.IsSafeAreaOn = true;
				}
			}
		}
		else {
			if (GUILayout.Button("Hide Safe-Area") ) {
				if (saController) {
					saController.HideSafeAreaFrame(testType);
					saController.DeleteDeviceFrame();
					saController.IsSafeAreaOn = false;
				}
			}
		}

		EditorGUILayout.Space();
	}
}
