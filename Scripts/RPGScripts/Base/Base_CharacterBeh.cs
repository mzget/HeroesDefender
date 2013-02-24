using UnityEngine;
using System.Collections;

public class Base_CharacterBeh : Base_ObjectBeh {
	
	public GameObject hp_bar_status;
	
	public enum CharacterStatus {
		Idle = 0,
		Active,
	};
	public CharacterStatus currentCharacterStatus;
	
	protected float maxHP;
	protected float hp;
	protected float hpBarScale;

	protected GameObject nameBar;
	protected TextMesh textMeshName;
	protected tk2dAnimatedSprite animatingSprite;


	// Use this for initialization
	protected virtual void Start () {
		StartCoroutine_Auto (this.CreateHUD ());
	}

	IEnumerator CreateHUD ()
	{
		hp_bar_status = Instantiate (Resources.Load (ResourcePathManager.PATH_OF_HUD_OBJECTS + "Hp_bar", typeof(GameObject))) as GameObject;
		hp_bar_status.transform.parent = this.transform;
		hp_bar_status.transform.localPosition = Vector3.up * 40f;
		hp_bar_status.gameObject.SetActive (false);

		yield return null;
	}
	
	// Update is called once per frame
//	void Update () {
//	
//	}
}
