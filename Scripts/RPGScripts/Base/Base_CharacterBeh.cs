using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterAnimationManager))]
public class Base_CharacterBeh : Base_ObjectBeh {
	
	public enum CharacterState {
		Idle = 0,
		Active,
	};
	public CharacterState currentCharacterStatus;

	public enum AnimationState : int {
		idle = 0,
		walk,
		attack,
		dead,
		skill
	};
	public AnimationState animState = AnimationState.idle;
	
	public enum CharacterType {
		Malee = 0, 
		Range = 1,	
	}
	public CharacterType characterType = CharacterType.Malee;
	
	protected BattleStage stageManager;
	protected TouchPhase currentPhaseState;
	protected TouchPhase lastPhaseState;
	
	public GameObject hp_bar_status;
	public GameObject targetEnemy;
	protected float attack;
	protected float atkSpeed;
	protected float maxHP;
	protected float hp;
	protected float hpBarScale;

//	protected GameObject nameBar;
	protected tk2dTextMesh hp_textmesh;
	protected tk2dAnimatedSprite animatedSprite;
	protected CharacterAnimationManager animationManager;


	// Use this for initialization
	protected virtual void Start ()
	{
		if (stageManager == null) {
			GameObject gameController = GameObject.FindGameObjectWithTag ("GameController");
			stageManager = gameController.GetComponent<BattleStage> ();
		}
				
		if (animatedSprite == null)
			animatedSprite = this.GetComponent<tk2dAnimatedSprite>();
			
		if(animationManager == null)
			animationManager = this.GetComponent<CharacterAnimationManager>();

		StartCoroutine_Auto (this.CreateHUD ());
	}

	IEnumerator CreateHUD ()
	{
		hp_bar_status = Instantiate (Resources.Load (ResourcePathManager.PATH_OF_HUD_OBJECTS + "Hp_bar", typeof(GameObject))) as GameObject;
		hp_bar_status.transform.parent = this.transform;
		hp_bar_status.transform.localPosition = Vector3.up * 40f;
		hp_bar_status.gameObject.SetActive (false);
		
		Transform hp_textmesh_transform = hp_bar_status.transform.Find("Hp");
		hp_textmesh = hp_textmesh_transform.gameObject.GetComponent<tk2dTextMesh>();

		yield return null;
	}

	internal void UnActiveHpbar() {
		hp_bar_status.SetActive (false);
	}
	
	internal void ReceiveDamage(float damage)
	{
		hp -= damage;
		hp_textmesh.text = hp.ToString() + "/" + maxHP.ToString();
		hp_textmesh.Commit();
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();

		if (Input.touchCount == 1) {
			Ray ray = Camera.main.ScreenPointToRay (stageManager.touch.position);
			RaycastHit rayHit;
			currentPhaseState = stageManager.touch.phase;
			
			if (Physics.Raycast (ray, out rayHit)) {
				Debug.Log (rayHit.collider.name);
			}
			else {
				if(lastPhaseState == TouchPhase.Began || lastPhaseState == TouchPhase.Stationary || lastPhaseState == TouchPhase.Moved) {
					if(currentPhaseState == TouchPhase.Ended) {
						UnActiveHpbar();
					}
				}
			}
			
			lastPhaseState = currentPhaseState;
		}

		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit rayHit;
			
			if (!Physics.Raycast (ray, out rayHit)) {
				UnActiveHpbar();
			}
		}
	}
}
