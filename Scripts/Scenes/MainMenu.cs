using UnityEngine;
using System;
using System.Collections;

public class MainMenu : Mz_BaseScene
{
	// Set Build Version to Unlock All Item has locked with score.
	public enum BuildVersion : int { debugMode = 0, releaseMode = 1, };
	public BuildVersion buildVersion;

    public enum SceneState { none = 0, showOption, showNewGame, showLoadGame, };
    private SceneState sceneState;
    public enum NewGameUIState { none = 0, showTextField, showDuplicateName, showSaveGameSlot, };
    public NewGameUIState newgameUIState;

	public GUISkin standard_Skin;
    public string username = string.Empty;
    private bool _isNullUsernameNotification = false;
    private bool _isDuplicateUsername = false;
    private bool _isFullSaveGameSlot;
    private string player_1;
    private string player_2;
    private string player_3;

    Rect viewport_rect = new Rect(0, 0, Main.FixedGameWidth, Main.FixedGameHeight);
    Rect mainMenuGroup_rect = new Rect(Main.FixedGameWidth - 300, 0, 300, Main.FixedGameHeight);
    Rect newPlayer_rect = new Rect(75, 50, 150, 60);
    private Rect loadGameButton_rect = new Rect(75, 140, 150, 60);
    private Rect thirdButton_rect = new Rect(75, 240, 150, 60);
    private Rect fourthButton_rect = new Rect(75, 340, 150, 60);
    private Rect fifthButton_rect;
	
    //<@-- New game data fields.
    float newGameGroup_width = 400f , newGameGroupHeight = 300f;
    Rect midCenterWindowGroup_rect;
    Rect usernameTextInput_rect;
    Rect startButton_rect;
    Rect cancelButton_rect;
	
    //<@-- Savegame slot data fields.
    float group_width = 400f;
    float slot_width = 200f;
    Rect saveSlot_1Rect;
    Rect saveSlot_2Rect;
    Rect saveSlot_3Rect;
    Rect textbox_header_rect;

    #region <@-- Events handle.

    public event EventHandler NewPlayer_Event;
    public void OnNewPlayer_Event(EventArgs e)
    {
        if (NewPlayer_Event != null)
            NewPlayer_Event(this, e);
    }

    #endregion

    // Use this for initialization
    void Start()
    {
        this.Initializing();
    }

	protected void Initializing ()
	{
        StartCoroutine(this.InitializeAudio());
		StartCoroutine(this.CreateSaveData_Obj());
//        NewPlayer_Event += MessageManager.Handle_MainMenu_NewPlayer_Event;

        player_1 = PlayerPrefs.GetString(1 + ":" + "username");
        player_2 = PlayerPrefs.GetString(2 + ":" + "username");
        player_3 = PlayerPrefs.GetString(3 + ":" + "username");

        saveSlot_1Rect = new Rect(group_width / 2 - (slot_width / 2), 100, slot_width, 50);
        saveSlot_2Rect = new Rect(group_width / 2 - (slot_width / 2), 160, slot_width, 50);
        saveSlot_3Rect = new Rect(group_width / 2 - (slot_width / 2), 220, slot_width, 50);
        textbox_header_rect = new Rect((group_width / 2) - 150, 40, 300, 40);

        midCenterWindowGroup_rect = new Rect(Main.FixedGameWidth / 2 - newGameGroup_width / 2, Main.FixedGameHeight / 2 - newGameGroupHeight / 2, newGameGroup_width, newGameGroupHeight);
        usernameTextInput_rect = new Rect((midCenterWindowGroup_rect.width / 2) - 150, 100, 300, 60);
        startButton_rect = new Rect(30, 200, 150, 60);
        cancelButton_rect = new Rect(newGameGroup_width - 180, 200, 150, 60);

        fifthButton_rect = fourthButton_rect;
        fifthButton_rect.y += 100;


        //mainMenuGroup_rect.width = mainMenuGroup_rect.width * Mz_OnGUIManager.Extend_heightScale;
        //mainMenuGroup_rect.x = Screen.width - mainMenuGroup_rect.width;
		
		//if(PlayerPrefs.HasKey(Mz_SaveData.usernameKey)) {
		//    username = PlayerPrefs.GetString(Mz_SaveData.usernameKey);				
		//    guiState = GUIState.showSaveGame; 
		//}
		//else {			
		//    guiState = GUIState.showNewGame;			
		//    if (newgameUIState != NewGameUIState.showTextField) {
		//        newgameUIState = NewGameUIState.showTextField;
		//    }
		//}
	}

    private void MessageManager_NewPlayer_Event(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private new IEnumerator InitializeAudio() {
        base.InitializeAudio();

        audioBackground_Obj.audio.clip = base.background_clip;
        audioBackground_Obj.audio.loop = true;
        audioBackground_Obj.audio.Play();

        yield return null;
    }

	IEnumerator CreateSaveData_Obj ()
	{
		if(extendsStorageManager == null)
			extendsStorageManager = new ExtendSaveManager();

		yield return null;
	}

    private void OnGUI()
    {
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width/Main.FixedGameWidth, Screen.height / Main.FixedGameHeight, 1));

        //<!--- Draw menu outside viewport.
        if (sceneState == SceneState.none) {
            _isDuplicateUsername = false;
            _isNullUsernameNotification = false;
            _isFullSaveGameSlot = false;
            username = "";

            this.DrawMainMenuWindow();
        }
        else {
            //@-- Viewport Screen rect.
            GUI.BeginGroup(viewport_rect, GUIContent.none, GUIStyle.none);
            {
                if (sceneState == SceneState.showNewGame)
                {
                    this.DrawNewGameWindow();
                }
                else if (sceneState == SceneState.showLoadGame)
                {
                    // Call ShowSaveGameSlot Method.
                    this.DrawSaveGameSlot(_isFullSaveGameSlot);
                }

                #region Show Notification When Username have a problem.

                string notificationText = "";
                string dublicateNoticeText = "";

                if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
                {
                    notificationText = "Please Fill Your Username. \n กรุณาใส่ชื่อผู้เล่น";
                    dublicateNoticeText = "This name already exists. \n ซื่อนี้มีอยู่แล้ว";
                }
                else if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
                {
                    notificationText = "Please Fill Your Username.";
                    dublicateNoticeText = "This name already exists.";
                }

                Rect notification_Rect = new Rect(Mz_OnGUIManager.viewPort_rect.width / 2 - 200, 0, 400, 64);
                if (_isNullUsernameNotification)
                    GUI.Box(notification_Rect, notificationText);
                if (_isDuplicateUsername)
                    GUI.Box(notification_Rect, dublicateNoticeText);

                #endregion
            }
            GUI.EndGroup();
        }
    }

    private void DrawMainMenuWindow()
    {
        //<@-- Main menu.
        GUI.BeginGroup(mainMenuGroup_rect, GUIContent.none , GUI.skin.box);
        {
			if (GUI.Button(newPlayer_rect, "New Player", standard_Skin.button)) {
                sceneState = SceneState.showNewGame;
				audioEffect.PlayOnecSound(audioEffect.buttonDown_Clip);
            }
            else if (GUI.Button(loadGameButton_rect, "Load game", standard_Skin.button)) {
                sceneState = SceneState.showLoadGame;
                audioEffect.PlayOnecSound(audioEffect.buttonDown_Clip);
            }
            else if (GUI.Button(fourthButton_rect, "About", standard_Skin.button)) {
                audioEffect.PlayOnecSound(audioEffect.buttonDown_Clip);
            }
            else if (GUI.Button(fifthButton_rect, "Exit", standard_Skin.button)) {
                Application.Quit();
                audioEffect.PlayOnecSound(audioEffect.buttonDown_Clip);
            }
        }
        GUI.EndGroup();
    }

    private void DrawNewGameWindow()
    {		
        GUI.BeginGroup(midCenterWindowGroup_rect, "New Player", standard_Skin.window);
        {
            if (Event.current.isKey && Event.current.keyCode == KeyCode.Return)
            {
                audioEffect.PlayOnecWithOutStop(audioEffect.buttonDown_Clip);
                this.CheckUserNameFormInput();
            }

            if (GUI.Button(startButton_rect, "Start", standard_Skin.button)) {
                this.CheckUserNameFormInput();
                audioEffect.PlayOnecWithOutStop(audioEffect.buttonDown_Clip);
            }
            else if (GUI.Button(cancelButton_rect, "Cancel", standard_Skin.button)) {
                sceneState = SceneState.none;
                audioEffect.PlayOnecWithOutStop(audioEffect.buttonDown_Clip);
            }

            //<!-- "Please Insert Username !".
            GUI.SetNextControlName("Username");
            username = GUI.TextField(usernameTextInput_rect, username, 24, standard_Skin.textField);

            if (GUI.GetNameOfFocusedControl() == string.Empty || GUI.GetNameOfFocusedControl() == "")
            {
                GUI.FocusControl("Username");
            }
        } 
        GUI.EndGroup();
    }
    private void CheckUserNameFormInput()
    {
        username.Trim('\n');

        if (username == "" || username == string.Empty || username == null)
        {
            Debug.LogWarning("Username == null");
            _isNullUsernameNotification = true;
            _isDuplicateUsername = false;
        }
        else if (username == player_1 || username == player_2 || username == player_3)
        {
            Debug.LogWarning("Duplicate Username");
            _isDuplicateUsername = true;
            _isNullUsernameNotification = false;
            username = string.Empty;
        }
        else
        {
            _isDuplicateUsername = false;
            _isNullUsernameNotification = false;

            this.EnterUsername();
        }
    }
    //<!-- Enter Username from User. 
    void EnterUsername()
    {
        Debug.Log("EnterUsername");

        //<!-- Autosave Mechanicism. When have empty game slot.  
        if (player_1 == string.Empty)
        {
			ExtendSaveManager.SaveSlot = 1;
            this.SaveNewPlayer();
            this.OnNewPlayer_Event(EventArgs.Empty);
        }
        else if (player_2 == string.Empty)
        {
			ExtendSaveManager.SaveSlot = 2;
            this.SaveNewPlayer();
            this.OnNewPlayer_Event(EventArgs.Empty);
        }
        else if (player_3 == string.Empty)
        {
			ExtendSaveManager.SaveSlot = 3;
            this.SaveNewPlayer();
            this.OnNewPlayer_Event(EventArgs.Empty);
        }
        else
        {
			ExtendSaveManager.SaveSlot = 0;
            _isFullSaveGameSlot = true;
			sceneState = SceneState.showLoadGame;

            Debug.LogWarning("<!-- Full Slot Call Showsavegameslot method.");
        }
    }

    //<!-- Show save game slot. If slot is full.
    void DrawSaveGameSlot(bool _toSaveGame)
    {
        if (_toSaveGame)
        {
            //<!-- Full save game slot. Show notice message.
            string message = string.Empty;
            //message = "เลือกช่องที่ต้องการ เพื่อลบข้อมูลเก่า และทับด้วยข้อมูลใหม่";
            message = "Select Data Slot To Replace New Data";

            GUI.Box(new Rect(Main.FixedGameWidth / 2 - 200, 0, 400, 64), message, standard_Skin.box);
        }

        GUI.BeginGroup(midCenterWindowGroup_rect, "Load game", standard_Skin.window);
        {
            if (GUI.Button(new Rect(midCenterWindowGroup_rect.width - 40 , 0, 40, 40), "X"))
            {
                sceneState = SceneState.none;
                audioEffect.PlayOnecWithOutStop(audioEffect.buttonDown_Clip);
            }

            if (_toSaveGame) {
                #region <!-- To Save game.

                // Display To Save Username.
                GUI.Box(textbox_header_rect, username);
                /// Choose SaveGame Slot for replace new data.
                if (GUI.Button(saveSlot_1Rect, new GUIContent(player_1, "button"), standard_Skin.button))
                {
                    audioEffect.PlayOnecWithOutStop(audioEffect.buttonDown_Clip);

					ExtendSaveManager.SaveSlot = 1;
                    this.SaveNewPlayer();
            		this.OnNewPlayer_Event(EventArgs.Empty);
                }
                else if (GUI.Button(saveSlot_2Rect, new GUIContent(player_2, "button"), standard_Skin.button))
                {
                    audioEffect.PlayOnecWithOutStop(audioEffect.buttonDown_Clip);

					ExtendSaveManager.SaveSlot = 2;
                    this.SaveNewPlayer();
            		this.OnNewPlayer_Event(EventArgs.Empty);
                }
                else if (GUI.Button(saveSlot_3Rect, new GUIContent(player_3, "button"), standard_Skin.button))
                {
                    audioEffect.PlayOnecWithOutStop(audioEffect.buttonDown_Clip);

					ExtendSaveManager.SaveSlot = 3;
                    this.SaveNewPlayer();
            		this.OnNewPlayer_Event(EventArgs.Empty);
                }

                #endregion
            }
            else {
                #region <!-- To Load Game.

                string headerText = "";
                //headerText = "เลือกช่องที่ต้องการใส่ข้อมูลได้เลยครับ";
                headerText = "Select Data Slot";
                // Header.
                GUI.Box(textbox_header_rect, headerText, standard_Skin.box);
                /// Choose SaveGame Slot for Load Save Data.
                string slot_1 = string.Empty;
                string slot_2 = string.Empty;
                string slot_3 = string.Empty;

                if (player_1 == string.Empty) slot_1 = "Empty";
                else slot_1 = player_1;
                if (player_2 == string.Empty) slot_2 = "Empty";
                else slot_2 = player_2;
                if (player_3 == string.Empty) slot_3 = "Empty";
                else slot_3 = player_3;

                //<!-- Draw data slot button.
                if (GUI.Button(saveSlot_1Rect, new GUIContent(slot_1, "button"), standard_Skin.button))
                {
                    audioEffect.PlayOnecWithOutStop(audioEffect.buttonDown_Clip);

                    if (player_1 != string.Empty)
                    {
						ExtendSaveManager.SaveSlot = 1;
                        base.extendsStorageManager.Load();
						this.LoadSceneTarget();
                    }
                }
                else if (GUI.Button(saveSlot_2Rect, new GUIContent(slot_2, "button"), standard_Skin.button))
                {
                    audioEffect.PlayOnecWithOutStop(audioEffect.buttonDown_Clip);

                    if (player_2 != string.Empty)
                    {
						ExtendSaveManager.SaveSlot = 2;
						base.extendsStorageManager.Load();
						this.LoadSceneTarget();
                    }
                }
                else if (GUI.Button(saveSlot_3Rect, new GUIContent(slot_3, "button"), standard_Skin.button))
                {
                    audioEffect.PlayOnecWithOutStop(audioEffect.buttonDown_Clip);

                    if (player_3 != string.Empty)
                    {
						ExtendSaveManager.SaveSlot = 3;
						base.extendsStorageManager.Load();
						this.LoadSceneTarget();
                    }
                }

                #endregion
            }
        }
        GUI.EndGroup();
    }

    private void SaveNewPlayer()
    {
		PlayerPrefs.SetString(ExtendSaveManager.SaveSlot + ExtendSaveManager.KEY_USERNAME, username);
		
		//<@-- Mission data.
//		PlayerPrefs.SetInt(Mz_SaveData.SaveSlot + Mz_SaveData.KEY_CURRENT_MISSION_ID, 0);
//		PlayerPrefsX.SetBoolArray(Mz_SaveData.SaveSlot + Mz_SaveData.KEY_ARRAY_MISSION_COMPLETE, new bool[8]);

		base.extendsStorageManager.Load();
		this.LoadSceneTarget();
    }
    private void LoadSceneTarget()
    {
        if (Application.isLoadingLevel == false)
        {
            Mz_LoadingScreen.TargetSceneName = Mz_BaseScene.ScenesInstance.BattleStage.ToString();
            Application.LoadLevel(Mz_BaseScene.ScenesInstance.LoadingScene.ToString());
        }
    }
}