using UnityEngine;
using System.Collections;

public class MonsterManager : Base_CharacterBeh {

	public bool _isAlive = true;
	private bool _isStarting = false;
	private Vector2 hpBarSize;
	private float monsterSpeed = 0.4f;
	private bool tempFlip;
	private bool enableAttack = true;
	public float MonsterSpeed
	{
		get { return monsterSpeed; }
        set { monsterSpeed = value; }
	}
	private bool flip = false;
	public bool Flip
	{
		get { return flip; }
        set { flip = value; }
	}
	
	void Awake(){
		if(flip){
			this.animatedSprite.scale = new Vector3(-1f,1f,1f);
		}
		tempFlip = Flip;
	}
	
//  private AIMonster aiMonster;
//	private IDMonster idMonster;

	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		this._isAlive = true;
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
			
			if(flip!=tempFlip){
				this.animatedSprite.scale = new Vector3(-1f,1f,1f);
			}else{
				this.animatedSprite.scale = new Vector3(1f,1f,1f);
			}
			if(this.tag!="Monster"){
				this.transform.Translate(Vector3.right * monsterSpeed, Space.World);
			}else{
				this.transform.Translate(Vector3.left * monsterSpeed, Space.World);
			}
			
		}else if(this.animState == AnimationState.attack){
			if(targetEnemy!=null){
				if(targetEnemy.transform.position.x > this.gameObject.transform.position.x){
					this.animatedSprite.scale = new Vector3(-1f,1f,1f);
				}else{
					this.animatedSprite.scale = new Vector3(1f,1f,1f);
				}
			}
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


	
	bool isRange(GameObject targetEnemy)
	{
		float mePos = this.transform.position.x;
		float targetPos = targetEnemy.transform.position.x;
		float range =  Mathf.Abs(mePos-targetPos);	
		Debug.Log(range);
		if(range<80f){
			return true;
		}
		return false;
	}
	IEnumerator atk(){
		yield return new WaitForSeconds(0.5f);
		this.animState = AnimationState.attack;
		if(enableAttack && targetEnemy){
			enableAttack = false;
			this.animationManager.PlayAnimationByName (CharacterAnimationManager.NameAnimationsList.Attack);	
			this.animatedSprite.animationCompleteDelegate = (sprite, clipId) => {
				targetEnemy.SendMessage("ReceiveDamage", 10f, SendMessageOptions.DontRequireReceiver);
				this.animationManager.PlayAnimationByName (CharacterAnimationManager.NameAnimationsList.Attack);
				enableAttack = true;	
				};
		}
	}
	IEnumerator exit(){
		enableAttack = false;
		yield return new WaitForSeconds(0.5f);
		enableAttack = true;
		this.animState = AnimationState.walk;
		this.animationManager.PlayAnimationByName (CharacterAnimationManager.NameAnimationsList.Walk);
		targetEnemy = null;
	}
	void OnTriggerEnter (Collider coll)
	{	
		if (collider.tag == "Hero" || collider.tag == "Unit") {
			Debug.Log(coll.gameObject.tag);
			if(coll.gameObject.tag=="Monster"){
				targetEnemy = coll.gameObject;
				StartCoroutine(this.atk());
			}
		}else if (collider.tag == "Monster"){
			if(coll.gameObject.tag!="Monster"){
				targetEnemy = coll.gameObject;
				StartCoroutine(this.atk());
			}
		}
	}
	
	void OnTriggerExit (Collider coll)
	{
		StartCoroutine(this.exit());
		//
		//Debug.Log(this.gameObject.name+" exit");
		//targetEnemy = coll.gameObject;
		//StartCoroutine(this.CheckTarget(this.gameObject.name,coll.gameObject.name));

		/*
		if (coll.tag == "Hero" || coll.tag == "Unit") {
			if(this.gameObject.tag == "Unit") {
				Debug.Log(this.gameObject.tag);
				// Ignore.
			}
			else  {
				if (targetEnemy == null) {
					targetEnemy = coll.gameObject;
					StartCoroutine(this.CheckTarget(this.gameObject.name,coll.gameObject.name));
				}
			}
		}*/
		/*
		this.animState = AnimationState.walk;
		if (collider.tag == "Hero") {
			targetEnemy = null;
			//if(animState != AnimationState.walk) {
				this.animState = AnimationState.walk;
				this.animationManager.PlayAnimationByName(CharacterAnimationManager.NameAnimationsList.Walk);
			//}
		}
		else if (collider.tag == "Monster")
		{
		}
		*/
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
