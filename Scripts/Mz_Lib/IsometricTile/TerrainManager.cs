using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainManager : MonoBehaviour {

    public const string PATH_OF_GOLDMINE_PREFAB = "Prototypes/Environtment/GoldMine";
    public const string PATH_OF_STONEMINE_PREFAB = "Prototypes/Environtment/StoneMine";
    public const string PATH_OF_TREE_PREFAB = "Prototypes/Environtment/Tree";
	
    public TerrainElement goldMine;
    public TerrainElement stoneMine;
	public TerrainElement tree;
//	public TerrainElement forest;
//	public TerrainElement bush;
//	public TerrainElement flower;

	public List<TileArea> goldMine_areas = new List<TileArea>();
    public List<TileArea> stoneMine_areas = new List<TileArea>();
	public List<TileArea> tree_areas = new List<TileArea>();
	
	GameObject goldMine_group_obj;
	GameObject stoneMine_group_obj;
	GameObject tree_group_obj;
	
	
	void Awake() {
		goldMine_group_obj = new GameObject("goldMine_group_obj");
		stoneMine_group_obj = new GameObject("stoneMine_group_obj");
		tree_group_obj = new GameObject("tree_group_obj");
	}
	
	// Use this for initialization
	void Start () {
        CreateArea(tree_areas, PATH_OF_TREE_PREFAB, tree_group_obj, 14, 14, 1, 1);
        CreateArea(stoneMine_areas, PATH_OF_STONEMINE_PREFAB, stoneMine_group_obj, 14, 13, 1, 1);
        CreateArea(goldMine_areas, PATH_OF_GOLDMINE_PREFAB, goldMine_group_obj, 13, 14, 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void CreateArea(List<TileArea> listarea, string ResourcePath, GameObject objGroup, int pointx, int pointy, int width, int height)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                listarea.Add(new TileArea() { x = i + pointx, y = j + pointy, numSlotWidth = 1, numSlotHeight = 1 });
            }
        }
        for (int i = 0; i < listarea.Count; i++)
        {
            GameObject temp = Instantiate(Resources.Load(ResourcePath, typeof(GameObject))) as GameObject;
            temp.transform.parent = objGroup.transform;
            temp.GetComponent<TerrainElement>().constructionArea = listarea[i];
        }
    }
}
