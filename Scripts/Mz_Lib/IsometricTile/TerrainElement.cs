using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainElement : TilebaseObjBeh {

    public static List<TerrainElement> elementId = new List<TerrainElement>();

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        if (constructionArea.numSlotWidth == 0 || constructionArea.numSlotHeight == 0)
        {
            Debug.LogError("area slot is equal 0");
            return;
        }

        this.transform.position = Tile.GetAreaPosition(constructionArea);
        Tile.SetNoEmptyArea(constructionArea);
        this.originalPosition = this.transform.position;

        TerrainElement.elementId.Add(this);
        this.gameObject.name = TerrainElement.elementId.Count.ToString();
    }

    // Enables the behaviour when it is visible
    void OnBecameVisible()
    {
        enabled = true;
    }

    // Disables the behaviour when it is invisible
    void OnBecameInvisible()
    {
        enabled = false;
    }
}