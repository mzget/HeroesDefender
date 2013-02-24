using UnityEngine;
using System.Collections;

public class SpawnPointBeh : ObjectBeh {
	
	public enum GUIState { none = 0, showMainGUI,};
	public GUIState guiState ;
	
	public Transform UI_Transform;
	public GameObject UI_group;
	private static GameObject UI_group_Instance = null;
	public tk2dSprite UIbackground;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		
		if(guiState == GUIState.showMainGUI) {
			if(UI_group_Instance == null) {
				UI_group_Instance = Instantiate(UI_group.gameObject) as GameObject;
				UI_group_Instance.transform.parent = UI_Transform;
			}	
			else if(UI_group_Instance != null && UI_group_Instance.active == false)
				UI_group_Instance.SetActiveRecursively(true);
		}
	}

    void OnMouseEnter() { }

    void OnMouseDown() {
		if(guiState != GUIState.showMainGUI) 
			guiState = GUIState.showMainGUI;
	}

    void OnMouseUp() { }

    void OnMouseExit() { }
}
