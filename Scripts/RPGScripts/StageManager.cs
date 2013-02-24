using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class StageManager : MonoBehaviour
{
    enum MonsterNames {
        Bodyguard = 0,
        SkeletonArcher,
        SkeletonSolder,
        CommanderSkeleton,
    }

    private List<GameObject> monstersList = new List<GameObject>();
	private List<Vector2> bodyguardPos = new List<Vector2>(5);
    private List<Vector2> skeletonArcherPos = new List<Vector2>(5);
    private List<Vector2> skeletonSolderPos = new List<Vector2>(5);
	private List<Vector2> commanderSkeletonPos = new List<Vector2>(5);


    void Awake() {
        monstersList.Add(GameObject.Find("Bodyguard"));
        monstersList.Add(GameObject.Find("SkeletonArcher"));
        monstersList.Add(GameObject.Find("SkeletonSolder"));
        monstersList.Add(GameObject.Find("CommanderSkeleton"));
    }

    // Application initialization
    void Start()
    {
		#region Bodyguard Position List.
		
		bodyguardPos.Add(new Vector2(600, -168));
		bodyguardPos.Add(new Vector2(414, -458));
		bodyguardPos.Add(new Vector2(800, -355));
		
		#endregion
		
		#region Skeleton Archer Position List.

        skeletonArcherPos.Add(new Vector2(880, -100));
        skeletonArcherPos.Add(new Vector2(1060, -160));
        skeletonArcherPos.Add(new Vector2(1150, 190));

        #endregion

        #region Skeleton Solder Position List.

        skeletonSolderPos.Add(new Vector2(100, 400));
        skeletonSolderPos.Add(new Vector2(372, 380));
        skeletonSolderPos.Add(new Vector2(540, 80));

        #endregion

        #region Commander Skeleton Possition List.

        commanderSkeletonPos.Add(new Vector2(590, 268)); 
		commanderSkeletonPos.Add(new Vector2(900, 362)); 
		commanderSkeletonPos.Add(new Vector2(960, 130));
		
		#endregion
    }

    // Update is called once per frame
    void Update() {

    }
}
