using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IsometricEngine : MonoBehaviour {
    GameObject buildingArea_group;
	public GameObject tile_prefab;

	public const int x = 24;
    public const int y = 24;
    private float tile_width = 64f;
    private float tile_height = 37f;
    private float ortho_height;
    private float ortho_width;


    public float map_width
    {
        get { return tile_width * x; }
    }
    public float map_height
    {
        get { return tile_height * y; }
    }
    public float crop_left
    {
        get { return ortho_width / 2; }
    }
    public float crop_right
    {
        get { return map_width - ortho_width / 2; }
    }
    public float crop_up
    {
        get { return (float)(map_height / 2) - (float)ortho_height / 2 + (float)tile_height / 2; }
    }
    public float crop_down
    {
        get { return -((float)(map_height / 2) - (float)ortho_height / 2) - (float)tile_height / 2; }
    }


	// Use this for initialization
    void Start()
    {
        ortho_height = 2 * Camera.main.orthographicSize;
        ortho_width = ortho_height * Camera.main.aspect;
        buildingArea_group = new GameObject("buildingArea_group");
    }

    internal IEnumerator CreateTilemap()
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = y; j > 0; j--)
            {  // Changed loop condition here.
                float a = (j * tile_width / 2) + (i * tile_width / 2);
                float b = (i * tile_height / 2) - (j * tile_height / 2);
                GameObject tile = Instantiate(tile_prefab) as GameObject;

                int j_order = y - j;
                tile.gameObject.name = i.ToString() + " : " + j_order.ToString();

                int z_pos = i + j_order;
                tile.transform.position = new Vector3(a, b, z_pos);
                tile.transform.parent = buildingArea_group.transform;
                Tile.Arr_BuildingAreas[i, j_order] = tile.GetComponent<Tile>();
            }
        }

        yield return 0;
    }
}
