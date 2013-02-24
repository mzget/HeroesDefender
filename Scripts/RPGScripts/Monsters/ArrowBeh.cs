using UnityEngine;
using System.Collections;

public class ArrowBeh : MonoBehaviour {

    tk2dSprite arrowSprite;
    HeroManager heroManager;

    private float damage;
    public float Damage { get { return damage; } set { damage = value; } }


	// Use this for initialization
	void Start () {
		arrowSprite = this.gameObject.GetComponent<tk2dSprite>();
        heroManager = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroManager>();
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += (Vector3)arrowSprite.transform.up * Time.deltaTime * 360;

//        if (arrowSprite.outOfView)
//            Destroy(this.gameObject);
	}

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Player") {
            heroManager.ReceiveDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
