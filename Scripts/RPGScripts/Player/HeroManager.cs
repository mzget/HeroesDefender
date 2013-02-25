using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AIPlayer))]
public class HeroManager : Base_CharacterBeh {

    public GUISkin mainInterface;
    public GUISkin heroSkin;
    public Texture2D hero_icon;

	private AIPlayer player;
	private Vector3 targetPos;
	private float speed;


    void Awake()
    {
        maxHP = 1000000;
        hp = maxHP;
    }

    // Use this for initialization
	protected override void Start ()
	{
		base.Start ();

		this.gameObject.name = name;
		
		//        nameBar = Instantiate(Resources.Load("PlayerName", typeof(GameObject))) as GameObject;
		//        textMeshName = nameBar.GetComponent<TextMesh>();
		player = this.gameObject.GetComponent<AIPlayer>();		
	}

    // Update is called once per frame
	protected override void Update ()
	{
		base.Update (); 

		// Mouse Click To Repath.
		if (Input.GetMouseButtonDown(0))
		{
			if(this.currentCharacterStatus == CharacterState.Active) {
				if (Input.touchCount >= 1)
					targetPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				else 
					targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				
				if (targetPos.x > this.transform.position.x)
					animatedSprite.scale = new Vector3(-1, 1,1);
				else
					animatedSprite.scale = new Vector3(1, 1, 1);

				
				this.animState = AnimationState.walk;
				this.animationManager.PlayAnimationByName(CharacterAnimationManager.NameAnimationsList.Walk);
			}
		}

		if (this.currentCharacterStatus == CharacterState.Active && this.animState == AnimationState.walk) {
			speed = 0.6f;
			Vector3 newPosition = new Vector3 (targetPos.x, targetPos.y, this.transform.position.z);
			this.transform.position = Vector3.Lerp (this.transform.position, newPosition, Time.deltaTime * speed);
		}
		
		float remain_distance = Vector2.Distance(this.transform.position, targetPos);
		if(remain_distance < 3) {
			if(this.animState != AnimationState.idle) {
				this.animState = AnimationState.idle;
				this.animationManager.PlayAnimationByName(CharacterAnimationManager.NameAnimationsList.Idle);
			}
		}
	}

    public void ReceiveDamage(float damage)
    {
        hp -= damage;
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
	
	void OnTriggerEnter(Collider collider)
	{
		Debug.Log (collider.name);

		if (collider.tag == "Monster")
		{
			if(this.animState != AnimationState.attack) {
				this.animState = AnimationState.attack;
				this.animationManager.PlayAnimationByName(CharacterAnimationManager.NameAnimationsList.Attack);
			}   
		}
	}
	
	void OnTriggerStay(Collider collider)
	{
//		if (collider.tag == "Monster")
//		{
//			if (monsters.Contains(collider.gameObject) == false)
//				monsters.Add(collider.gameObject);
//		}
	}
	
	void OnTriggerExit(Collider collider)
	{
//		if (collider.tag == "Monster")
//		{
//			monsters.Remove(collider.gameObject);
//	    if (monsterATK)
//	    {
//	        monsterATK.SendMessage("CloseMonsterName", SendMessageOptions.RequireReceiver);
//	        monsterATK = null;
//	    }
//		}
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
