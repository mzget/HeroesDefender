using UnityEngine;
using System.Collections;

public class OrthographicTilemapEngine : MonoBehaviour {

	public GameObject tile_prefab;

	private GameObject tilebaseArea_group;
	public const int x = 16;
	public const int y = 5;
	private float tile_width = 32f;
	private float tile_height = 32f;

	// Use this for initialization
	void Awake () {
		tilebaseArea_group = new GameObject("tilebaseArea_group");
	}
	
	internal IEnumerator CreateTilemap()
	{
		for (int i = 0; i < x; i++)
		{
			for (int j = 0; j < y; j++)
			{  // Changed loop condition here.
				float posX = -120 + (i * tile_width / 2); // (i * tile_width / 2) + (j * tile_width / 2);
				float posY = -80 + (j * tile_height / 2);

				GameObject tile = Instantiate(tile_prefab) as GameObject;
			
				tile.gameObject.name = i.ToString() + " : " + j.ToString();
		
				tile.transform.position = new Vector3(posX, posY, -7f + j);
				tile.transform.parent = tilebaseArea_group.transform;
				Tile.Arr_BuildingAreas[i, j] = tile.GetComponent<Tile>();
			}
		}
		
		yield return 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
