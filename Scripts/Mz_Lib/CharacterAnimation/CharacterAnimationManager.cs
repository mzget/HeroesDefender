using UnityEngine;
using System.Collections;

public class CharacterAnimationManager : MonoBehaviour {

	public tk2dAnimatedSprite animatedSprite;
	internal bool _isPlayingAnimation = false;

	public enum NameAnimationsList {
		Idle = 0,
		Walk,
		Attack,
		Skill,
		Dead,
	};

    double timer;

	
	// Use this for initialization
    void Start()
    {
		animatedSprite = this.gameObject.GetComponent<tk2dAnimatedSprite>();
    }
	
	// Update is called once per frame
//	void Update () {
//        timer += Time.deltaTime;
//        if (timer >= 4) {
//            timer = 0;
//
//            this.PlayAnimationByName(NameAnimationsList.Idle);
//        }
//	}

	public void PlayAnimationByName(NameAnimationsList nameAnimation) {
		animatedSprite.Play(nameAnimation.ToString());
	}
}
