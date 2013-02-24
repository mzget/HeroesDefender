using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour {

	public GameObject barbarian;
	public Transform[] spawnPoints;

	#region <@-- Static data.
	
	public static List<HeroManager> Arr_characterManager = new List<HeroManager>(3);
	
	#endregion

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 3; i++) {
			GameObject newHero = (Instantiate(barbarian)) as GameObject;
			newHero.transform.position = spawnPoints[i].position;
			HeroManager hero = newHero.GetComponent<HeroManager>();
			Arr_characterManager.Add(hero);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
}
