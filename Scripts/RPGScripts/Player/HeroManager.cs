using UnityEngine;
using System.Collections;

public class HeroManager : Base_CharacterBeh {

    public GUISkin mainInterface;
    public GUISkin heroSkin;
    public Texture2D hero_icon;

	private Vector3 targetPos;
	private float speed = 20;
	private float distanceToTarget;
	private bool _IsStayWithMonster;


	void Awake() {
		iTween.Init (this.gameObject);
		iTween.Defaults.easeType = iTween.EaseType.linear;
	}

    // Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		
		maxHP = 1000;
		hp = maxHP;
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
					if (this.currentCharacterStatus == CharacterState.Active) {							//<@-- Walk to mouse position or touch position.
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
		}
		
		if (this.currentCharacterStatus == CharacterState.Active && this.animState == AnimationState.walk) {
			iTween.MoveUpdate (this.gameObject, iTween.Hash ("position", targetPos, "time", distanceToTarget / speed));
//			Vector3 newPosition = new Vector3 (targetPos.x, targetPos.y, targetPos.z);
//			this.transform.position = Vector3.Lerp (this.transform.position, newPosition, Time.deltaTime * speed);

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
		if (collider.tag == "Monster") {
			if (targetEnemy == null) {
				targetEnemy = collider.gameObject;
				if (this.animState != AnimationState.attack) {
					this.animState = AnimationState.attack;
					this.animationManager.PlayAnimationByName (CharacterAnimationManager.NameAnimationsList.Attack);
					this.animatedSprite.animationCompleteDelegate = (sprite, clipId) => {
						targetEnemy.SendMessage("ReceiveDamage", 10f, SendMessageOptions.DontRequireReceiver);
						this.animationManager.PlayAnimationByName (CharacterAnimationManager.NameAnimationsList.Attack);
					}; 
				}   
			}
		
			Debug.Log (collider.tag);
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
			_IsStayWithMonster = false;
		}
	}
	
	#endregion

	/**
    void OnGUI()
    {
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, Screen.height/Main.FixedGameHeight, 1));

        GUI.BeginGroup(new Rect(0, 0, 300, 100));
        {
            GUI.Box(new Rect(0, 0, 60, 60), new GUIContent(hero_icon, "Icon"), GUIStyle.none);

            /// HP Management.
            hpBarScale = hp * 320f / maxHP;
            if (hp > 2 * (maxHP / 3))
            {
                GUI.Box(new Rect(60, 0, hpBarScale, 12), new GUIContent(hp.ToString() + "/" + maxHP.ToString(), "HP"), heroSkin.customStyles[0]);
            }
            else if (hp > maxHP / 3 && hp <= 2 * (maxHP / 3))
            {
                GUI.Box(new Rect(60, 0, hpBarScale, 12), new GUIContent(hp.ToString() + "/" + maxHP.ToString(), "HP"), heroSkin.customStyles[1]);
            }
            else
            {
                GUI.Box(new Rect(60, 0, hpBarScale, 12), new GUIContent(hp.ToString() + "/" + maxHP.ToString(), "HP"), heroSkin.customStyles[2]);
            }

            /// MP Management.
            GUI.Box(new Rect(60, 12.5f, 300, 12), new GUIContent("256 / 256", "MP"), heroSkin.customStyles[3]);

            //            GUI.BeginGroup(new Rect(85, 45, 125, 60), GUIContent.none, mainInterface.box);
            //            {
            //
            //            }
            //            GUI.EndGroup();

            //            GUI.BeginGroup(new Rect(215, 50, 185, 50), GUIContent.none, mainInterface.box);
            //            {
            //                GUI.Box(new Rect(5, 5, 40, 40), "Q");
            //                GUI.Box(new Rect(50, 5, 40, 40), "W");
            //                GUI.Box(new Rect(95, 5, 40, 40), "E");
            //                GUI.Box(new Rect(140, 5, 40, 40), "R");
            //            }
            //            GUI.EndGroup();
        }
        GUI.EndGroup();
    }
	**/
}
