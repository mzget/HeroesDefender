using UnityEngine;
using System.Collections;

public class MonsterManager : Base_CharacterBeh {

	private bool _isAlive = true;
	public bool _IsAlive {
		get { return _isAlive; }
		set { _isAlive = value; }
	}
	
	private Vector2 hpBarSize;
    private AIMonster aiMonster;
	private IDMonster idMonster;
	
    private AnimationState animationSprite;
	public AnimationState AnimatingSprite{ get {return animationSprite;} set{animationSprite = value;}}
	

	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();

		this._isAlive = true;

		aiMonster = this.gameObject.GetComponent<AIMonster>();
		idMonster = this.gameObject.GetComponent<IDMonster>();
		
//		hpBarSprite = hpBar_Ins.GetComponent<tk2dSprite>() as tk2dSprite;
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
		
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
		this.currentCharacterStatus = CharacterStatus.Active;
		this.hp_bar_status.SetActive (true);
		
		foreach (HeroManager item in CharacterManager.Arr_characterManager) {
			if(item != this) {
				item.currentCharacterStatus = CharacterStatus.Idle;
				item.hp_bar_status.SetActive(false);
			}	
		}

		base.OnTouchDown ();
	}

    #endregion

    /// <summary>
    /// HookUp By Simple Event.
    /// </summary>
    public void ShowMonsterName()
    {
        textMeshName.text = idMonster.fixName;

//        textMeshName.transform.position = this.gameObject.transform.position + Vector3.up * ((this.animationSprite.size.y/2) + 40);
//        /// Set PowerBar Position.
//        hpBar_Ins.active = true;
//        powerBar_Ins.active = true;
//        powerBarSprite.transform.position = this.transform.position + new Vector3(0, (this.animationSprite.size.y/2) + 20, 0);
//        hpBarSprite.transform.position = this.transform.position + new Vector3(-50, (this.animationSprite.size.y/2) + 20, 0);
    }

    public void CloseMonsterName()
    {
        try
        {
            textMeshName.text = string.Empty;
        }
        catch { }
	}
	
    public void ReceiveDamage(int r_Damage)
    {
    	idMonster.hp -= r_Damage;
        SetHealth();
    }

    private void SetHealth()
    {
//        if (hpBarSprite)
//        {
//            float hpBarScale = idMonster.hp * 100f / idMonster.maxHP;
////            hpBarSprite.size = new Vector2(hpBarScale, hpBarSprite.size.y);
//        }
    }
}
