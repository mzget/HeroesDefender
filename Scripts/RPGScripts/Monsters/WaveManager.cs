using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {

	public static List<MonsterManager> Arr_monsterManager = new List<MonsterManager>();
	public static Hashtable GC_Monster = new Hashtable();
	
	GameObject EnemyGroup;
	public float startTime;
	public int arrWidth;
	public int arrHeight;
	public const string PATH_OF_ENEMY_01 = "Prototypes/Monsters/Monster";
	public bool EnableUpdate = true;
	
	private float [] yAxis = new float[] { -16f, -32f, -48f, -64f, -80f};
	private float [] zAxis = new float[] { -3f, -4f, -5f, -6f, -7f};
	private int [,] wavePattern = new int[,] 
	{
		{1 ,0 ,0 ,0 ,0 ,0 ,0 ,0 },
		{0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 },
		{0 ,0 ,0 ,0 ,0 ,0 ,0 ,0	},	
		{0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 },
		{0 ,0 ,0 ,0 ,0 ,0 ,0 ,0	}
	};			
	public int [,] WavePattern
   	{
        get { return wavePattern; }
        set { wavePattern = value; }
    }
	private float initDelay = 1.0f;
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
		
		string monsterName = "Unit1";
		GameObject clone = Instantiate(Resources.Load(PATH_OF_ENEMY_01, typeof(GameObject))) as GameObject;
		clone.transform.position = new Vector3(-120f, yAxis[0], zAxis[0]);
		clone.name = monsterName;
		clone.tag = "Hero";
		clone.GetComponent<MonsterManager>().Flip = true;
	}
	
	// Use this for initialization
	void Start () {	
		WaveInit(wavePattern,EnemyGroup);
		
	}

	void Update () {
		WaveUpdate(wavePattern);
	}
	
	void WaveInit(int [,] pattern, GameObject objGroup) {
		for(int i =0; i < arrWidth ; i++) {
			for(int j=0; j < arrHeight; j++) {
				if(pattern[i,j]!=0) {
					int waveIndex = ((arrWidth*j)+i);
					string monsterName = "Enemy"+waveIndex.ToString();
					GC_Monster.Add(monsterName, waveIndex);
					GameObject clone = Instantiate(Resources.Load(PATH_OF_ENEMY_01, typeof(GameObject))) as GameObject;
					clone.name = monsterName;
					clone.transform.parent = objGroup.transform;
					clone.transform.position = new Vector3(130f, yAxis[i], zAxis[i]);
					
					clone.tag = "Monster";
					MonsterManager cloneBeh = clone.GetComponent<MonsterManager>();
					WaveManager.Arr_monsterManager.Add(cloneBeh);
				}
			}
		}
	}
	
	void WaveUpdate(int [,] pattern) {
		if(EnableUpdate)
		{
			int index = 0;
			float elapsed = Time.time - startTime;
			//int hours 	= (int)(elapsed / 3600);
			//int minutes = (int)(elapsed / 60);
	 		//int seconds = (int)(elapsed % 60);
			//Debug.Log("elapsed : "+elapsed+" hour : "+hours+" minute : "+minutes+" second : "+seconds);
			for(int i =0; i < arrWidth ; i++) {
				for(int j=0; j < arrHeight; j++) {
					if(elapsed>(j*delayPerWave+initDelay)) {
						if(pattern[i,j]!=0){
							GameObject updateEnemy = GameObject.Find("Enemy"+((arrWidth*j)+i));
							updateEnemy.GetComponent<MonsterManager>().StartWalking();
						}
					}
					index++;
						//if update all unit
					if(index==(arrWidth*arrHeight)-1){
						WaveDestroy(pattern);
						//EnableUpdate = false;
						GameObject updateUnit = GameObject.Find("Unit1");
						updateUnit.GetComponent<MonsterManager>().StartWalking();
					}
				}
			}
		}
	}
	
	void WaveDestroy(int [,] pattern){
		List<string> removeHash = new List<string>(); 
		foreach (DictionaryEntry Item in GC_Monster)
		{	
	       	if(GameObject.Find(Item.Key.ToString()).transform.position.x <= -80f){
				int wi = ((int)Item.Value / arrWidth);
				int wj = ((int)Item.Value % arrWidth);
				wavePattern[wj,wi] = 0;
				Destroy(GameObject.Find(Item.Key.ToString()));
				removeHash.Add(Item.Key.ToString());
			}
		}
		foreach(string key in removeHash){
			GC_Monster.Remove(key);
		}
	}
	
	void ZeroInit(int [,] pattern) {
		for(int i=0; i < arrWidth; i++) {
			for(int j=0; j < arrHeight; j++) {
				pattern[i,j] = 0;
			}
		}
	}
}
