using UnityEngine;
using System.Collections;

public class TilebaseObjBeh : ObjectsBeh {

	// Use this for initialization
    protected override void Start()
    {
        base.Start();
    }
	
	// Update is called once per frame


    public TileArea constructionArea;
    internal virtual bool ShowConstructionAreaStatus()
    {
        bool canCreateBuilding = Tile.CheckedIsomatricTileStatus(constructionArea);

        return canCreateBuilding;
    }
}
