using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour {

	private BattleStage stageManager;

	public Transform[] monstersGUI_transform = new Transform[5];
	public readonly string[] arr_unitName = new string[2] {
		"Doom", "Spider",
	};
	public Dictionary<string, int> dict_unitName_id = new Dictionary<string, int> ();
	private const int MaxUnitNumber = 2;

	public GameObject gameMenuWindow;
	
	// Use this for initialization
	void Start () {
		stageManager = this.gameObject.GetComponent<BattleStage> ();

		this.InitializeMonsterGUI ();

		for (int i = 0; i < arr_unitName.Length; i++) {
			dict_unitName_id.Add(arr_unitName[i], i);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void HandleOnInput (ref string nameInput)
	{
		if (nameInput == "Menu_button") {
			this.SetActiveGameMenuWindow();
		}
		else if(nameInput == "Reset_button") {
			stageManager.GotoOtherStage(Mz_BaseScene.ScenesInstance.BattleStage);
		}
		else if(nameInput == "MainMenu_button") {
			stageManager.GotoOtherStage(Mz_BaseScene.ScenesInstance.MainMenu);
		}
	}

	private void SetActiveGameMenuWindow ()
	{
		if (stageManager._isPauseGameplay) {
			gameMenuWindow.gameObject.SetActive (false);
			stageManager.UpdateTimeScale (1);
			stageManager._isPauseGameplay = false;
		}
		else {
			gameMenuWindow.gameObject.SetActive (true);
			stageManager.UpdateTimeScale (0);
			stageManager._isPauseGameplay = true;
		}
	}

	void InitializeMonsterGUI ()
	{
		for (int i = 0; i < MaxUnitNumber; i++) {
			GameObject unit = Instantiate (Resources.Load (ResourcePathManager.PATH_OF_GUI_OBJECTS + "Monster_icon", typeof(GameObject))) as GameObject;
			unit.transform.parent = monstersGUI_transform[i];
			unit.transform.localPosition = Vector3.zero;
			tk2dSprite unitSprite = unit.GetComponent<tk2dSprite>();
			unitSprite.spriteId = unitSprite.GetSpriteIdByName(arr_unitName[i]);
			unit.name = arr_unitName[i];
		}
	}

	public void CreateMonsterIcon (string p_name)
	{		
		GameObject unit = Instantiate (Resources.Load (ResourcePathManager.PATH_OF_GUI_OBJECTS + "Monster_icon", typeof(GameObject))) as GameObject;
		unit.transform.parent = monstersGUI_transform[dict_unitName_id[p_name]];
		unit.transform.localPosition = Vector3.zero;
		tk2dSprite unitSprite = unit.GetComponent<tk2dSprite>();
		unitSprite.spriteId = unitSprite.GetSpriteIdByName(p_name);
		unit.name = p_name;
	}
}
