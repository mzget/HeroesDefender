using UnityEngine;
using System.Collections;

public class MonsterManager : Base_CharacterBeh {

	private bool _isStarting = false;

//    private AIMonster aiMonster;
//	private IDMonster idMonster;
	

	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();

		maxHP = 300;
		hp = maxHP;
		base.InitailizeHUDData ();

		this.animState = AnimationState.idle;
		this.animationManager.PlayAnimationByName (CharacterAnimationManager.NameAnimationsList.Idle);

//		aiMonster = this.gameObject.GetComponent<AIMonster>();
//		idMonster = this.gameObject.GetComponent<IDMonster>();
	}

	public void StartWalking ()
	{
		if(_isStarting == false) {
			if (this.animState != AnimationState.walk) {
				this.animState = AnimationState.walk;
				this.animationManager.PlayAnimationByName (CharacterAnimationManager.NameAnimationsList.Walk);
			}
		}
		
		_isStarting = true;
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
		
		if(_isStarting && this.animState == AnimationState.walk) {
			this.transform.Translate(Vector3.left * WaveManager.EnemySpeed, Space.World);
		}
		
		/// Check Dying Event.
//		if (idMonster.hp <= 0 && _isAlive)
//		{
//			_isAlive = false;
//			this.collider.enabled = false;
//			
//			if (aiMonster.AnimationStateProp != AIMonster.AnimationState.dead)
//			{
//				aiMonster.AnimationStateProp = AIMonster.AnimationState.dead;
//				aiMonster.PlayAnimation();
//			}
//		}  
	}
	
	#region <@-- Handle Input Events.

	protected override void OnTouchDown ()
	{
		this.currentCharacterStatus = CharacterState.Active;
		this.hp_bar_status.SetActive (true);
		
		foreach (MonsterManager item in WaveManager.Arr_monsterManager) {
			if(item != this) {
				item.currentCharacterStatus = CharacterState.Idle;
				item.hp_bar_status.SetActive(false);
			}	
		}

		base.OnTouchDown ();
	}

    #endregion
	
	#region <@-- Handle Collision Event.
	
	void OnTriggerEnter (Collider coll)
	{
//		if (coll.tag == "Hero" || coll.tag == "Unit") {
//			if(this.gameObject.tag == "Unit") {
//				// Ignore.
//			}
//			else {
//				if (targetEnemy == null) {
//					targetEnemy = coll.gameObject;
//					if (this.animState != AnimationState.dead) {
//						this.animState = AnimationState.attack;
//						this.animationManager.PlayAnimationByName (CharacterAnimationManager.NameAnimationsList.Attack);
//						this.animatedSprite.animationCompleteDelegate = (sprite, clipId) => {
//							if(targetEnemy) {
//								targetEnemy.SendMessage("ReceiveDamage", 10f, SendMessageOptions.DontRequireReceiver);
//								this.animationManager.PlayAnimationByName (CharacterAnimationManager.NameAnimationsList.Attack);
//							}
//						}; 
//					}   
//				}
//			}
//			
//			Debug.Log (coll.tag);
//		}
	}
	
	void OnTriggerStay(Collider coll)
	{
	}
	
	void OnTriggerExit (Collider collider)
	{
//		if (collider.tag == "Hero" || collider.tag == "Unit") {
//			targetEnemy = null;
//			if(animState != AnimationState.dead || animState != AnimationState.attack) {
//				this.animState = AnimationState.walk;
//				this.animationManager.PlayAnimationByName(CharacterAnimationManager.NameAnimationsList.Walk);
//			}
//		}
	}
	
	#endregion

	protected override void Handle_deadAnimationComplete (tk2dAnimatedSprite sprite, int clipId)
	{
		base.Handle_deadAnimationComplete (sprite, clipId);

		WaveManager.Arr_monsterManager.Remove (this);
		Destroy (this.gameObject);
	}
}
