using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPlayer : MonoBehaviour
{
    /** Target to move to */
    public Transform target;
    public float pickNextWaypointDistance = 1F;
	
    private float atkSpeed = 0;
	public float ATKSpeed { get{ return atkSpeed; } set{ atkSpeed = value; } }

    public enum AnimationState : int {
		idle = 0,
		walk,
		attack,
		dead,
		skill_1
    };
    private AnimationState animState;
    public AnimationState AnimState { get { return animState; } set { animState = value; } }

    private bool _follow = false;
    private int calculationDamage;

    private List<GameObject> monsters = new List<GameObject>();
    private GameObject monsterATK;
	private MonsterManager monsterManager;
    private tk2dAnimatedSprite animatingSprite;


 


    /// <summary>
    /// Public Property.
    /// </summary>
    //private bool _clickMonster = false;
    //public bool _ClickMonster
    //{
    //    get { return _clickMonster; }
    //    set { _clickMonster = value; }
    //}
    

    // Use this for initialization
    public void Start()
    {
        animatingSprite = this.gameObject.GetComponent<tk2dAnimatedSprite>();
		animatingSprite.animationCompleteDelegate = OnAnimationFinish;
    }	

    // Update is called once per frame.
    public void Update()
    {
        #region Calculation WayPoint.
		
		/** 
        //Change target to the next waypoint if the current one is close enough
        Vector3 currentWaypoint = path[pathIndex];
        currentWaypoint.z = tr.position.z;
        while ((currentWaypoint - tr.position).sqrMagnitude < pickNextWaypointDistance * pickNextWaypointDistance)
        {
            pathIndex++;
            if (pathIndex >= path.Length)
            {
                //Use a lower pickNextWaypointDistance for the last point. If it isn't that close, then decrement the pathIndex to the previous value and break the loop
                if ((currentWaypoint - tr.position).sqrMagnitude < (pickNextWaypointDistance * 0.2) * (pickNextWaypointDistance * 0.2))
                {
                    ReachedEndOfPath();
                    return;
                }
                else
                {
                    pathIndex--;
                    //Break the loop, otherwise it will try to check for the last point in an infinite loop
                    break;
                }
            }
            currentWaypoint = path[pathIndex];
            currentWaypoint.z = tr.position.z;
        }
		*/

		/**
                /// Rotate towards the target 
                tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion.LookRotation(new Vector3(dir.x, dir.y, 0)), rotationSpeed * Time.deltaTime);
                tr.eulerAngles = new Vector3(0, 0, tr.eulerAngles.z);
		
                Vector3 forwardDir = transform.forward;
                /// Move Forwards - forwardDir is already normalized
                forwardDir = forwardDir * speed;
                forwardDir *= Mathf.Clamp01 (Vector3.Dot (dir, tr.forward));

                controller.SimpleMove(forwardDir);
        **/
		
		#endregion
        /// Mouse Click To Repath.
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
//            _follow = true;
//
//            OTAnimatingSprite animating = this.gameObject.GetComponent<OTAnimatingSprite>();
//
//            if (OT.view.mouseWorldPosition.x > this.transform.position.x)
//            { animating.flipHorizontal = true; }
//            else
//            { animating.flipHorizontal = false; }
//
//            StartCoroutine(WaitToRepath());
        }

        /// AI Follow WayPoint.
//        Vector3 dir = currentWaypoint - tr.position;
		float speed = 0;
        if (_follow)
        {
            if (animState != AnimationState.walk) 
            {
                animState = AnimationState.walk;
                this.PlayAnimation();
            }

            speed = Time.deltaTime * 60f;
//            this.transform.position = Vector3.Lerp(tr.position, currentWaypoint, speed);
        }

        /// Keycode To Skill.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _follow = false;
//            animatingSprite.PlayOnce("Skill_1");
			animState = AnimationState.skill_1;
        }

        /// MonsterAtk && Monster manager.
        if (monsterATK == null)
        {
            if (monsters.Count != 0) {
				monsterATK = monsters[monsters.Count - 1];
			}
            else
            {
                if (animState != AnimationState.idle && animState != AnimationState.walk)
                {
                    animState = AnimationState.idle;
                    this.PlayAnimation();
                }
            }
        }
        else if (monsterATK && monsters.Count != 0)
        {
            monsterManager = monsterATK.GetComponent<MonsterManager>();

            if (monsterManager._IsAlive)
            {
                monsterManager.ShowMonsterName();

                /// Check flip this sprite.			
//                if (this.animatingSprite.position.x < monsterManager.AnimatingSprite.position.x)
//                { animatingSprite.flipHorizontal = true; }
//                else
//                { animatingSprite.flipHorizontal = false; }
				
//				if(this.animatingSprite.position.

                if (animState != AnimationState.attack && animState != AnimationState.skill_1)
                {
                    animState = AnimationState.attack;
                    this.PlayAnimation();
                }
            }
            else
            {
                monsters.Remove(monsterATK);
				monsterManager.CloseMonsterName();
                monsterManager = null;
                monsterATK = null;
            }
        }
		else if(monsterATK && monsters.Count == 0) {
			monsterManager.CloseMonsterName();
			monsterManager = null;			
			monsterATK = null;
		}
    }

    #region All Collision Event.

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Monster")
        {
            _follow = false;   
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Monster")
        {
            if (monsters.Contains(collider.gameObject) == false)
				monsters.Add(collider.gameObject);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Monster")
        {
            monsters.Remove(collider.gameObject);
//            if (monsterATK)
//            {
//                monsterATK.SendMessage("CloseMonsterName", SendMessageOptions.RequireReceiver);
//                monsterATK = null;
//            }
        }
    }

    #endregion

    public void PlayAnimation()
    {
//        if (animState == AnimationState.idle) 
//        { animatingSprite.PlayLoop("Idle"); }
//        else if (animState == AnimationState.walk) 
//        { animatingSprite.PlayLoop("Walk"); }
//        else if (animState == AnimationState.attack) 
//        { animatingSprite.PlayOnce("Attack"); }
//        else if (animState == AnimationState.dead) 
//        { animatingSprite.Play("Dead"); }
//        else if (animState == AnimationState.skill_1) { }
    }

    /** The OnAnimationFinish delegate will be called when an animation or **/
    /** animation frameset finishes playing. **/
	public void OnAnimationFinish(tk2dAnimatedSprite sprite, int clipId) {
		if(animState == AnimationState.skill_1) {
			if(monsterATK && monsterManager._IsAlive) 
            monsterManager.ReceiveDamage(320);
			
	    	animState = AnimationState.idle;
	    	this.PlayAnimation();
		}
		
		if(animState == AnimationState.attack) {
        if(monsterATK&&monsterManager)
            monsterManager.ReceiveDamage(calculationDamage);

			animState = AnimationState.idle;
	        this.PlayAnimation();
		}
    }
}
