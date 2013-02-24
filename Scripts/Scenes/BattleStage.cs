using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class TownTutorDataStore {

};

public class BattleStage : Mz_BaseScene {

	public GameObject backgroup_group_transform;
	private bool _updatable = true;

	#region <@-- Event Handles Data section.

	public static event EventHandler newGameStartup_Event;
	private void OnnewGameStartup_Event (EventArgs e)
	{
		EventHandler handler = BattleStage.newGameStartup_Event;
		if (handler != null)
			handler (this, e);
	}

	public static void Handle_NewGameStartupEvent (object sender, EventArgs e)
	{
		BattleStage.newGameStartup_Event -= BattleStage.Handle_NewGameStartupEvent;
	}

	#endregion

	// Use this for initialization
	void Start ()
    {
		StartCoroutine(this.ReInitializeAudioClipData());
		StartCoroutine(this.InitializeAudio());
        StartCoroutine(base.InitializeIdentityGUI());
	}
	
    #region <@-- Tutor system.

	public static event EventHandler IntroduceGameUI_Event;
	private void OnIntroduceGameUI_Event (EventArgs e)
	{
		Debug.Log("OnIntroduceGameUI_Event");

		if (IntroduceGameUI_Event != null)
			IntroduceGameUI_Event (this, e);
	}	
	internal static void Handle_IntroduceGameUI_Event (object sender, EventArgs e)
	{
		IntroduceGameUI_Event -= Handle_IntroduceGameUI_Event;
	}	

    #endregion

	protected new IEnumerator InitializeAudio ()
	{
    	base.InitializeAudio();
		
        audioBackground_Obj.audio.clip = base.background_clip;
        audioBackground_Obj.audio.loop = true;
        audioBackground_Obj.audio.Play();

        yield return null;
	}
	
	private const string PATH_OF_DYNAMIC_CLIP = "AudioClips/GameIntroduce/Town/";
    private IEnumerator ReInitializeAudioClipData()
    {
        description_clips.Clear();
		if(Main.Mz_AppLanguage.appLanguage == Main.Mz_AppLanguage.SupportLanguage.TH) {
        	description_clips.Add(Resources.Load(PATH_OF_DYNAMIC_CLIP + "TH_tutor_01", typeof(AudioClip)) as AudioClip);
        	description_clips.Add(Resources.Load(PATH_OF_DYNAMIC_CLIP + "TH_Letplay", typeof(AudioClip)) as AudioClip);
        	description_clips.Add(Resources.Load(PATH_OF_DYNAMIC_CLIP + "TH_decoration", typeof(AudioClip)) as AudioClip);
			description_clips.Add(Resources.Load(PATH_OF_DYNAMIC_CLIP + "TH_trophy", typeof(AudioClip)) as AudioClip);
			description_clips.Add(Resources.Load(PATH_OF_DYNAMIC_CLIP + "TH_dress", typeof(AudioClip)) as AudioClip);
			description_clips.Add(Resources.Load(PATH_OF_DYNAMIC_CLIP + "TH_touchmove", typeof(AudioClip)) as AudioClip);
		}
		else if(Main.Mz_AppLanguage.appLanguage == Main.Mz_AppLanguage.SupportLanguage.EN) {
        	description_clips.Add(Resources.Load(PATH_OF_DYNAMIC_CLIP + "TH_tutor_01", typeof(AudioClip)) as AudioClip);
        	description_clips.Add(Resources.Load(PATH_OF_DYNAMIC_CLIP + "TH_Letplay", typeof(AudioClip)) as AudioClip);
        	description_clips.Add(Resources.Load(PATH_OF_DYNAMIC_CLIP + "TH_decoration", typeof(AudioClip)) as AudioClip);
			description_clips.Add(Resources.Load(PATH_OF_DYNAMIC_CLIP + "TH_trophy", typeof(AudioClip)) as AudioClip);
			description_clips.Add(Resources.Load(PATH_OF_DYNAMIC_CLIP + "TH_dress", typeof(AudioClip)) as AudioClip);
			description_clips.Add(Resources.Load(PATH_OF_DYNAMIC_CLIP + "TH_touchmove", typeof(AudioClip)) as AudioClip);
		}		
		
        yield return 0;
    }
	
    private void LateUpdate() {
		if (_updatable)
        {
            base.ImplementTouchPostion();
        }
    }

	protected override void MovingCameraTransform ()
	{	
		base.MovingCameraTransform();

		if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
            float speed = (500) * Time.deltaTime * Time.fixedDeltaTime;
			// Get movement of the finger since last frame   
			Vector2 touchDeltaPosition = touch.deltaPosition;
			// Move object across XY plane       
			//transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
			Camera.main.transform.Translate(-touchDeltaPosition.x * speed, 0, 0);
		}
		else if(Application.isWebPlayer || Application.isEditor)
		{
			if(_isDragMove) {
				float vector = currentPos.x - originalPos.x;
				if(vector < 0)
					Camera.main.transform.position += Vector3.right * Time.deltaTime * 200;
				else if(vector > 0) 
					Camera.main.transform.position += Vector3.left * Time.deltaTime * 200;
			}
		}

        if (Camera.main.transform.position.x > 266f)
            Camera.main.transform.position = new Vector3(266f, Camera.main.transform.position.y, Camera.main.transform.position.z); 	//Vector3.left * Time.deltaTime;
        else if (Camera.main.transform.position.x < 0)
            Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y, Camera.main.transform.position.z);	 //Vector3.right * Time.deltaTime;     
	}

    /// <summary>
    /// <!-- Gui region.
    /// </summary>
    private void OnGUI() {
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, Screen.height / Main.GAMEHEIGHT, 1));    

        if (GUI.Button(new Rect(0, Main.FixedGameHeight / 2 - 25, 150 * Mz_OnGUIManager.Extend_heightScale, 50), "Swindle"))
        {
            Mz_StorageManage.AvailableMoney = 100000;
            StartCoroutine(base.InitializeIdentityGUI());
        }
    }

	public override void OnInput (string nameInput)
	{

	}

    public override void OnDispose()
    {
        base.OnDispose();
        //<!-- Clear static NumberOfCanSellItem.
    }
}
