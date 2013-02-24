using UnityEngine;
using System.Collections;

public class AIMonster : MonoBehaviour {

    public enum AnimationState {
		idle = 0,
		walk,
		attack,
		dead
	}
	
    private AnimationState animationState;
    public AnimationState AnimationStateProp { get { return animationState; } set { animationState = value; }}
	
    private float calculateDamage;
	public float CalculationDamage { get { return calculateDamage; } set { calculateDamage = value; }}
	
    private float atkSpeed = 0;
	public float AtkSpeed {	get { return atkSpeed; } set { atkSpeed = value; }}
	
    private Vector2 originalPosition;

    private bool _collision;

    /// <summary>
    /// In order to Set Mouse Icon while mouse Over. 
    /// </summary>
    private GameObject mouse;		
	/// <summary>
	/// The Share Property.
	/// </summary>
    private float rangeVisible = 150;
    public float RangeVisible { get { return rangeVisible; } set { rangeVisible = value; } }

    private float rangeFromAbode;
    public float RangeFromAbode { get { return rangeFromAbode; } set { rangeFromAbode = value; } }

    private tk2dAnimatedSprite playerSprite;
	private HeroManager  heroManager;
    private IDMonster idMonster;
    private tk2dAnimatedSprite animatingSprite;


	// Use this for initialization
	void Start ()
	{
        GameObject playerOBJ = GameObject.FindGameObjectWithTag("Player");
        mouse = GameObject.FindWithTag("Pointer");
		
		heroManager = playerOBJ.GetComponent<HeroManager>();
        playerSprite = playerOBJ.GetComponent<tk2dAnimatedSprite>();

		idMonster = this.gameObject.GetComponent<IDMonster>();

        animatingSprite = this.gameObject.GetComponent<tk2dAnimatedSprite>();		
		animatingSprite.animationCompleteDelegate = OnAnimationFinish;

        originalPosition = this.transform.position;
	}
	
	// Update is called once per frame
    void Update()
    {
		Vector2 playerPos = heroManager.transform.position;

        float distanceFromPlayer = Vector2.Distance(this.transform.position, playerPos);
		float distanceFromOriginal = Vector2.Distance(this.transform.position, originalPosition);
		float distanceXFromPlayer = Vector2.Distance(new Vector2(this.transform.position.x,0), new Vector2(playerPos.x, 0));
		float distanceYFromPlayer = Vector2.Distance(new Vector2(0, this.transform.position.y), new Vector2(0, playerPos.y));

        float speed = Time.deltaTime;

        if (distanceFromOriginal < rangeFromAbode && animationState != AnimationState.dead) 
        {
			/// See Player. 
            if (distanceFromPlayer <= rangeVisible)             
            {
                #region Flip Monster Sprite follow Player position.

//                if (playerPos.x > this.animatingSprite.position.x)
//                { 
//					if(idMonster._isLeft.Equals(true))
//						animatingSprite.flipHorizontal = true;
//					else
//						animatingSprite.flipHorizontal = false;
//				}
//                else { 
//					if(idMonster._isLeft.Equals(true))
//						animatingSprite.flipHorizontal = false;
//					else
//						animatingSprite.flipHorizontal = true;					
//                }

                #endregion

                #region Set Monster Animation.

                if (idMonster._isMalee && _collision)
                {
                    if (distanceYFromPlayer > -20f && distanceYFromPlayer < 20f) 
					{
                        if (animationState != AnimationState.attack)
                        {
                            animationState = AnimationState.attack;
                            this.PlayAnimation();
                        }
                    }
					else {	
                        if (animationState != AnimationState.walk)
                        {
                            animationState = AnimationState.walk;
                            this.PlayAnimation();
                        }
                        
						this.transform.position = Vector2.Lerp(this.transform.position, new Vector2(this.transform.position.x, playerPos.y), speed);
					}
                }
                else if (idMonster._isMalee == false) {
                    if (animationState != AnimationState.attack) {
                        animationState = AnimationState.attack;
						this.PlayAnimation();
                    }
                }
                else
                {
                    if (animationState != AnimationState.walk) {
						animationState = AnimationState.walk;
						this.PlayAnimation();
					}
                    
					this.transform.position = Vector2.Lerp(this.transform.position, playerPos, speed);
                }

                #endregion
            }
            else
            {
//				if (transform.position.x < originalPosition.x) 
//                    animatingSprite.flipHorizontal = true;
//                else 
//                    animatingSprite.flipHorizontal = false;
//
//                this.transform.position = Vector2.Lerp(this.animatingSprite.position, originalPosition, speed);
//
//                /// To Set Monster at original position.
//                if (distanceFromOriginal <= 10) 
//                {
//                    animatingSprite.position = originalPosition;
//                    animatingSprite.flipHorizontal = false;
//					
//                    if (animationState != AnimationState.idle) {
//                        animationState = AnimationState.idle;
//						this.PlayAnimation();
//                    }
//                }
            }
        }
        else {
//            if(animationState != AnimationState.dead)
//				this.animatingSprite.position = Vector2.Lerp(this.animatingSprite.position, originalPosition, speed);
        }
    }

    #region All Collision Event.

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            _collision = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            _collision = false;
        }
    }

    #endregion

    public void PlayAnimation()
    {
//        if (animationState == AnimationState.idle) 
//            this.animatingSprite.PlayLoop("Idle");
//        else if (animationState == AnimationState.walk)
//            this.animatingSprite.PlayLoop("Walk");
//        else if (animationState == AnimationState.attack)
//            this.animatingSprite.PlayOnce("Attack");
//        else if (animationState == AnimationState.dead)
//            this.animatingSprite.PlayOnce("Dead");
    }

    // The OnAnimationFinish delegate will be called when an animation or.
    // animation frameset finishes playing.
	public void OnAnimationFinish(tk2dAnimatedSprite sprite, int clipId)
    {
	    if (animationState == AnimationState.dead)
	        DestroyObject(this.gameObject);
			
	    if (animationState == AnimationState.attack)
	    {
	        if (idMonster._isMalee)
	        {
	            animationState = AnimationState.idle;
	            heroManager.ReceiveDamage(calculateDamage);
	        }
	        else {
	            animationState = AnimationState.idle;
	            idMonster.CreateBullet(heroManager.transform.position);
	        }
	    }
    }
}
