using UnityEngine;
using System.Collections;

public class AICreep : MonoBehaviour {	
	
	void Awake(){
		
	}
		
	// Use this for initialization
	void Start () {
		//StartCoroutine_Auto(this.FindTarget());
		//StartCoroutine(this.FindTarget());
		
	}
	
	IEnumerator FindTarget()
	{
		yield return new WaitForSeconds(5.0f);
		foreach (MonsterManager item in WaveManager.Arr_monsterManager) {

			Debug.Log(item.transform.position.y);
		}
		
	}
	
	void OnTriggerEnter(Collider collider)
	{
		//Debug.Log (collider.name);
		//WaveManager.EnableUpdate = false;
		if (collider.tag == "Monster")
		{
			//Debug.Log("aaa" + collider.name);
			/*
			if(this.animState != AnimationState.attack) {
				this.animState = AnimationState.attack;
				this.animationManager.PlayAnimationByName(CharacterAnimationManager.NameAnimationsList.Attack);
			}  */
		}
	}
	
	void OnTriggerStay(Collider collider)
	{
		
		
		if(collider.tag == "Hero") {
			string target = collider.gameObject.name;
//			GameObject attackTarget = GameObject.Find(target);
			foreach(HeroManager hero in CharacterManager.Arr_characterManager)
			{
				if(hero.name == target)
				{
					//hero.renderer.material.shader = Shader.Find("Diffuse");
					
					hero.ReceiveDamage(10f);
				}
			}
			//Debug.Log(index);
//			this.transform.Translate(1f,0f,0f);
			//Debug.Log (this.gameObject.name +" vs "+target);
		}
		
		if(collider.tag == "Monster"){
			//Debug.Log("xxxx" + collider.name);
		}
		
		
//		WaveManager.EnableUpdate = false;
//		if (collider.tag == "Monster")
//		{
//			if (monsters.Contains(collider.gameObject) == false)
//				monsters.Add(collider.gameObject);
//		}
	}
	
	void OnTriggerExit(Collider collider)
	{
//		Debug.Log (collider.name);
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
	
	// Update is called once per frame
	void Update () {
		//WaveManager.EnableUpdate = false;
		//StartCoroutine(this.FindTarget());
	}
	
}
