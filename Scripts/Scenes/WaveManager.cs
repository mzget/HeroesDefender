using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {
	
	public int [,] wavePattern  = new int[5,5];
	
	/*
	
	1 0 1 0 1
	1 1 1 1	1
    0 0 0 0 0			
	0 0	5 0 0
	0 0 0 0 0					
										
	**/
	
	public float feedSpeed = 0.1f;
	public string barbarianPath = "Prototypes/Character/Barbarian";
	public string monsterPath = "Prototypes/Character/Char_01";
	public float delayPerWave = 2.0f;
	public float startTime;
	
	
	GameObject barbarianGroup;
	public List<GameObject> hero = new List<GameObject>();
	
	
	
	
	
	
	void Awake() {
		barbarianGroup = new GameObject("barbarianGroup");
		startTime = Time.time;
	}
	
	// Use this for initialization
	void Start () {
		zeroInit(wavePattern);
		
		wavePattern = new int[,] {
								{1,0,1,0,0},
								{1,0,0,0,0},
								{0,0,0,1,1},
								{1,0,0,0,0},
								{1,0,1,0,0},
								};
		
		
		unitInit(wavePattern,barbarianGroup);
		foreach(GameObject cloneHero in hero)
		{
			cloneHero.transform.Translate(0f,0f,0f);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		

		UpdateWalk(wavePattern);
		
	}
	void UpdateWalk(int [,] pattern){
		float elapsed = Time.time - startTime;
		int hours 	= (int)(elapsed / 3600);
		int minutes = (int)(elapsed / 60);
 		int seconds = (int)(elapsed % 60);
		
		//Debug.Log("elapsed : "+elapsed+" hour : "+hours+" minute : "+minutes+" second : "+seconds);
		
		int arrWidth = 5;
		int arrHeight = 5;
		for(int i =0; i < arrWidth ; i++){
			for(int j=0; j < arrHeight; j++){
				
					if(elapsed>(j*delayPerWave)){
					GameObject chooseHero = GameObject.Find("Barbarian"+(arrWidth*i+j));
					chooseHero.transform.Translate(0.2f,0f,0f);
					}
			}
		}
		
	}
	
	void unitInit(int [,] pattern,GameObject objGroup){
		float yPosition  = -70f;
		//int arrWidth = pattern.GetLength(0);
		//int arrHeight = pattern.Length/arrWidth;
		int arrWidth = 5;
		int arrHeight = 5;
		for(int i =0; i < arrWidth ; i++){
			for(int j=0; j < arrHeight; j++){
				switch (i){
				case 0 : yPosition = -70f; break;
				case 1 : yPosition = -55f; break;
				case 2 : yPosition = -40f; break;
				case 3 : yPosition = -25f; break;
				case 4 : yPosition = -10f; break;
				}
				if(pattern[i,j]!=0){
					GameObject clone = new GameObject();
					clone = Instantiate(Resources.Load(barbarianPath, typeof(GameObject))) as GameObject;
					clone.name = "Barbarian"+(arrWidth*i+j);
					clone.transform.parent = objGroup.transform;
					clone.transform.position = new Vector3(-33f,yPosition,-2f);
					hero.Add(clone);
				}
			}
		}
	}
	
	void zeroInit(int [,] pattern){
		int arrWidth = 5;
		int arrHeight = 5;
		
		for(int i=0; i < arrWidth; i++){
			for(int j=0; j < arrHeight; j++){
				pattern[i,j] = 0;
			}
		}
		
	}
}
