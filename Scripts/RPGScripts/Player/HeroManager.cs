using UnityEngine;
using System.Collections;

public class HeroManager : Base_CharacterBeh {

    public GUISkin mainInterface;
    public GUISkin heroSkin;
    public Texture2D hero_icon;

	private Vector3 targetPos;
	private float speed = 20f;
	private float distanceToTarget;


	protected override void Awake ()
	{
		base.Awake ();

		iTween.Init (this.gameObject);
		iTween.Defaults.easeType = iTween.EaseType.linear;
	}

    // Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		
		maxHP = 1000;
		hp = maxHP;
		base.InitailizeHUDData ();
		this.gameObject.name = name;
	}

    // Update is called once per frame
	protected override void Update ()
	{
		base.Update (); 

		this.UpdateInput ();

		if (this.animState == AnimationState.attack) {
			if(targetEnemy) {
				if (targetEnemy.transform.position.x > this.transform.position.x)
					animatedSprite.scale = new Vector3(-1, 1,1);
				else
					animatedSprite.scale = new Vector3(1, 1, 1);
			}
		}
		else {
			if(targetEnemy) {
				targetPos = targetEnemy.transform.position;
			}
		}
	}
	
	void UpdateInput ()
	{
		if (Input.touchCount == 1) {
			Ray ray = Camera.main.ScreenPointToRay (stageManager.touch.position);
			RaycastHit rayHit;
			currentPhaseState = stageManager.touch.phase;
			
			if (Physics.Raycast (ray, out rayHit)) {
				Debug.Log (rayHit.collider.name);
			} else {
				
			}
			
			lastPhaseState = currentPhaseState;
		}
		
		// Mouse Click To Repath.
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit rayHit;
			
			if (Physics.Raycast (ray, out rayHit)) {
				if (rayHit.collider.tag == "WalkingTable" || rayHit.collider.tag == "Monster") {					
					if (this.currentCharacterStatus == CharacterState.Active) {
						//<@-- Walk to mouse position or touch position.
//						if (Input.touchCount >= 1)
//							targetPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
//						else 
//							targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

						if (rayHit.collider.tag == "Monster") {
							targetEnemy = rayHit.collider.gameObject;
							if (targetEnemy.transform.position.x > 150) {
								targetEnemy = null;
								return;
							}
						} else if (rayHit.collider.tag == "WalkingTable") {
							targetEnemy = null;
						}

						targetPos = rayHit.collider.transform.position;
						if (targetPos.x > this.transform.position.x)
							animatedSprite.scale = new Vector3 (-1, 1, 1);
						else
							animatedSprite.scale = new Vector3 (1, 1, 1);
						
						distanceToTarget = Vector2.Distance (this.transform.position, targetPos);
						
						this.animState = AnimationState.walk;
						this.animationManager.PlayAnimationByName (CharacterAnimationManager.NameAnimationsList.Walk);
					}
				}
			}
			
			if (this.currentCharacterStatus == CharacterState.Active && this.animState == AnimationState.walk)
			{
				if (targetEnemy == null)
					iTween.MoveTo(this.gameObject, targetPos, distanceToTarget / speed);
//				{
//					distanceToTarget = Vector2.Distance(this.transform.position, targetEnemy.transform.position);
//					float time = distanceToTarget / speed;
//					iTween.MoveUpdate(this.gameObject, targetPos, time);
//					print(time);
//				}
//				else {
//					iTween.MoveTo(this.gameObject, targetPos, distanceToTarget / speed);
//				}
//			    this.transform.position = Vector3.Lerp (this.transform.position, targetPos, 1f );
			}
		}
		
		if(this.currentCharacterStatus == CharacterState.Active && this.animState == AnimationState.walk) {
			if (targetEnemy)
			{
				distanceToTarget = Vector2.Distance(this.transform.position, targetEnemy.transform.position);
				float time = distanceToTarget / speed;
				iTween.MoveUpdate(this.gameObject, targetPos, time);
				print(time);
			}

			float remain_distance = Vector2.Distance (this.transform.position, targetPos);
			if (remain_distance < 1) {
				if (this.animState != AnimationState.idle) {
					this.animState = AnimationState.idle;
					this.animationManager.PlayAnimationByName (CharacterAnimationManager.NameAnimationsList.Idle);
				}
			}
		}
	}

    #region <@-- Handle Input Events.
	
	protected override void OnTouchDown ()
	{
		this.currentCharacterStatus = CharacterState.Active;
		this.hp_bar_status.SetActive (true);
		
		foreach (HeroManager item in CharacterManager.Arr_characterManager) {
			if(item != this) {
				item.currentCharacterStatus = CharacterState.Idle;
				item.hp_bar_status.SetActive(false);
			}	
		}
		
		base.OnTouchDown ();
	}

	#endregion

	#region <@-- Handle Collision Event.
	
	void OnTriggerEnter (Collider collider)
	{

	}
	
	void OnTriggerStay(Collider collider)
	{
		if (collider.tag == "Monster") {
			if (targetEnemy == null) {
				targetEnemy = collider.gameObject;
			}
			if(animState != AnimationState.attack) {
				this.animState = AnimationState.attack;
				this.animationManager.PlayAnimationByName (CharacterAnimationManager.NameAnimationsList.Attack);
				this.animatedSprite.animationCompleteDelegate += Handle_attackAnimationComplete; 
			}
		}
	}
	
	void OnTriggerExit(Collider collider)
	{
		if (collider.tag == "Monster")
        {
            targetEnemy = null;
            if (animState != AnimationState.attack || animState != AnimationState.dead)
            {
                this.animState = AnimationState.idle;
                this.animationManager.PlayAnimationByName(CharacterAnimationManager.NameAnimationsList.Idle);
            }
		}
	}
	
	#endregion

	public void Handle_attackAnimationComplete(tk2dAnimatedSprite sprite, int clipId) {
		this.animatedSprite.animationCompleteDelegate -= Handle_attackAnimationComplete;
		this.animState = AnimationState.idle;
		targetEnemy.SendMessage("ReceiveDamage", 50f, SendMessageOptions.DontRequireReceiver);
	}

	protected override void Handle_deadAnimationComplete (tk2dAnimatedSprite sprite, int clipId)
	{
		base.Handle_deadAnimationComplete (sprite, clipId);

		CharacterManager.Arr_characterManager.Remove (this);
		Destroy (this.gameObject);
	}
}
