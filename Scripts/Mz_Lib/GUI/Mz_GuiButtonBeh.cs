using UnityEngine;
using System.Collections;

[AddComponentMenu("Mz_ScriptLib/GUI/Mz_GuiButtonBeh")]
public class Mz_GuiButtonBeh : Base_ObjectBeh {
	
	public bool enablePlayAudio = true;
	
	private Mz_BaseScene gameController;
    private Vector3 originalScale;
		
	
	// Use this for initialization
    void Start ()
	{
		GameObject main = GameObject.FindGameObjectWithTag ("GameController");
		gameController = main.GetComponent<Mz_BaseScene> ();
		
        originalScale = this.transform.localScale;
	}

	protected override void OnTouchBegan ()
	{
		base.OnTouchBegan ();
		
		if(this.enablePlayAudio)
			gameController.audioEffect.PlayOnecSound(gameController.audioEffect.buttonDown_Clip);

		this.transform.localScale = originalScale * 0.8f;
	}
	protected override void OnTouchDown ()
	{
        gameController.OnInput(this.gameObject.name);
		
		base.OnTouchDown ();
	}
	protected override void OnTouchEnded ()
	{
		this.transform.localScale = originalScale;

		base.OnTouchEnded ();		
	}
}
