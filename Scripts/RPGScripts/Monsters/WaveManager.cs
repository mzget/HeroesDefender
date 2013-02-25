using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {

	public static List<MonsterManager> Arr_monsterManager = new List<MonsterManager>();
	GameObject EnemyGroup;
	public float startTime;
	public int arrWidth;
	public int arrHeight;
	public static float enemySpeed = -0.3f;
	public const string PATH_OF_ENEMY_01 = "Prototypes/Monsters/Char_01";
	public bool EnableUpdate = true;
	
	private float [] yAxis = new float[]{ -35f, -45f, -55f, -65f, -80f};
	private int [,] wavePattern = new int[,] 
	{
		{1 ,0 ,1 ,0 ,1 ,1 ,1 ,1 },
		{1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 },
		{0 ,0 ,0 ,0 ,0 ,1 ,1 ,1	},	
		{0 ,0 ,5 ,0 ,0 ,1 ,1 ,1 },
		{0 ,0 ,0 ,0 ,0 ,1 ,1 ,1	}
	};			
	public int [,] WavePattern
   	{
        get { return wavePattern; }
        set { wavePattern = value; }
    }
	private float initDelay = 2.0f;
	public float InitDelay
	{
		get { return initDelay; }
		set { initDelay = value; }
	}
	private float delayPerWave = 3.0f;
	public float DelayPerWave
	{
        get { return delayPerWave; }
        set { delayPerWave = value; }
    }
	
	void Awake() {
		arrWidth  = wavePattern.GetLength(0);
		arrHeight = wavePattern.Length/arrWidth;
		//zeroInit(wavePattern);
		startTime = Time.time;
		EnemyGroup = new GameObject("EnemyGroup");
	}
	
	// Use this for initialization
	void Start () {	
		WaveInit(wavePattern,EnemyGroup);
	}

	void Update () {
		WaveUpdate(wavePattern);
	}
	
	void WaveUpdate(int [,] pattern){
		if(EnableUpdate)
		{
			float elapsed = Time.time - startTime;
			int index = 0;
			//int hours 	= (int)(elapsed / 3600);
			//int minutes = (int)(elapsed / 60);
	 		//int seconds = (int)(elapsed % 60);
			//Debug.Log("elapsed : "+elapsed+" hour : "+hours+" minute : "+minutes+" second : "+seconds);
			for(int i =0; i < arrWidth ; i++){
				for(int j=0; j < arrHeight; j++){
					if(elapsed>(j*delayPerWave+initDelay)){
						if(pattern[i,j]!=0){
						GameObject updateEnemy = GameObject.Find("Enemy"+((arrWidth*j)+i));
						updateEnemy.transform.Translate(enemySpeed,0f,0f);
						}
					}
					index++;
					// if update all unit
					if(index==(arrWidth*arrHeight)-1){
						//EnableUpdate = false;
					}
				}
			}
		}
	}
	
	void WaveInit(int [,] pattern,GameObject objGroup){
		for(int i =0; i < arrWidth ; i++){
			for(int j=0; j < arrHeight; j++){
				if(pattern[i,j]!=0){
					GameObject clone = Instantiate(Resources.Load(PATH_OF_ENEMY_01, typeof(GameObject))) as GameObject;
					clone.name = "Enemy"+((arrWidth*j)+i);
					clone.transform.parent = objGroup.transform;
					clone.transform.position = new Vector3(330f, yAxis[i], -2f);
					
					clone.tag = "Monster";
					MonsterManager cloneBeh = clone.GetComponent<MonsterManager>();
					WaveManager.Arr_monsterManager.Add(cloneBeh);
				}
			}
		}
	}
	
	void ZeroInit(int [,] pattern){
		for(int i=0; i < arrWidth; i++){
			for(int j=0; j < arrHeight; j++){
				pattern[i,j] = 0;
			}
		}
	}
}
