using UnityEngine;
using System.Collections;

public class MonsterManager : Base_CharacterBeh {

	public bool _isAlive = true;
	private bool _isStarting = false;
	
	private Vector2 hpBarSize;
//    private AIMonster aiMonster;
//	private IDMonster idMonster;
	

	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();

		this._isAlive = true;

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
		if (coll.tag == "Hero") {
			if (targetEnemy == null) {
				targetEnemy = coll.gameObject;
				if (this.animState != AnimationState.attack) {
					this.animState = AnimationState.attack;
					this.animationManager.PlayAnimationByName (CharacterAnimationManager.NameAnimationsList.Attack);
					this.animatedSprite.animationCompleteDelegate = (sprite, clipId) => {
						targetEnemy.SendMessage("ReceiveDamage", 10f, SendMessageOptions.DontRequireReceiver);
						this.animationManager.PlayAnimationByName (CharacterAnimationManager.NameAnimationsList.Attack);
					}; 
				}   
			}
			
			Debug.Log (coll.tag);
		}
		else if(coll.tag == "Monster") {

		}
	}
	
	void OnTriggerStay(Collider collider)
	{
		//		if (collider.tag == "Monster")
		//		{
		//			_IsStayWithMonster = true;
		//		}
	}
	
	void OnTriggerExit(Collider collider)
	{
		if (collider.tag == "Monster")
		{
		}
	}
	
	#endregion

    /// <summary>
    /// HookUp By Simple Event.
    /// </summary>
    public void ShowMonsterName()
    {
//        textMeshName.text = idMonster.fixName;
//        textMeshName.transform.position = this.gameObject.transform.position + Vector3.up * ((this.animationSprite.size.y/2) + 40);

//        /// Set PowerBar Position.
//        hpBar_Ins.active = true;
//        powerBar_Ins.active = true;
//        powerBarSprite.transform.position = this.transform.position + new Vector3(0, (this.animationSprite.size.y/2) + 20, 0);
//        hpBarSprite.transform.position = this.transform.position + new Vector3(-50, (this.animationSprite.size.y/2) + 20, 0);
    }
	
    public void ReceiveDamage(int r_Damage)
    {
//    	idMonster.hp -= r_Damage;
        SetHealth();
    }

    private void SetHealth()
    {
//        if (hpBarSprite)
//        {
//            float hpBarScale = idMonster.hp * 100f / idMonster.maxHP;
//            hpBarSprite.size = new Vector2(hpBarScale, hpBarSprite.size.y);
//        }
    }
}
