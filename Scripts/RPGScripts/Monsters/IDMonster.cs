using UnityEngine;
using System.Collections;

public class IDMonster : MonoBehaviour
{
    public string fixName;
	public bool _isLeft = true;
    public bool _isMalee = true;
    public tk2dSprite bullet;

    public float damage = 120F;
	public float atkSpeed = 1F;
    public float maxHP = 400F;
    public float hp = 400F;
    public float rangeVisible = 300F;
    public float rangeFormAbode = 300f; 

    private AIMonster aiMonster;
    private tk2dAnimatedSprite animatingSprite;


	// Use this for initialization
	void Start () {
		
        this.name = fixName;
        hp = maxHP;
		
        aiMonster = this.gameObject.GetComponent<AIMonster>() as AIMonster;
        aiMonster.RangeVisible = this.rangeVisible;
        aiMonster.RangeFromAbode = this.rangeFormAbode;
        aiMonster.CalculationDamage = this.damage;
        aiMonster.AtkSpeed = this.atkSpeed;

        // Instant bullet
        if (_isMalee == false)
            animatingSprite = this.gameObject.GetComponent<tk2dAnimatedSprite>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void CreateBullet(Vector2 target) {
        tk2dSprite nBullet = (Instantiate(bullet.gameObject) as GameObject).GetComponent<tk2dSprite>();
        nBullet.transform.position = this.transform.position + (Vector3)this.animatingSprite.transform.up * (this.transform.localScale.y / 2);
        nBullet.transform.LookAt(target);
        nBullet.GetComponent<ArrowBeh>().Damage = this.damage;
    }
}

