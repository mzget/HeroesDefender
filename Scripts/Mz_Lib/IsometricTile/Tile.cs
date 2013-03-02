using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TileArea {
    public int x;
    public int y;
    public int numSlotWidth;
    public int numSlotHeight;
};

public class Tile : MonoBehaviour {

    internal static Tile[,] Arr_BuildingAreas = new Tile[IsometricEngine.x + 1, IsometricEngine.y + 1];
	
    public enum TileStatus { Empty = 0, NoEmpty, };
    public TileStatus tileState;
	
	public enum TileAbility { None, ShowStatus,	};
    internal TileAbility currentTileAbility;
    private bool _isShowStatus = false;

    private tk2dSprite sprite;

    void Awake() {
        sprite = this.GetComponent<tk2dSprite>();
    }

	// Use this for initialization
	void Start() {
		
	}
	
	// Enables the behaviour when it is visible
	void OnBecameVisible() {
	    enabled = true;
	}
	
	// Disables the behaviour when it is invisible
	void OnBecameInvisible () {
	    enabled = false;
	}
	
	// Update is called once per frame
	void Update() {
		if(this._isShowStatus)
			DeActiveShowStatus();
	}

    private void DeActiveShowStatus()
    {
        print("DeActiveShowStatus()");
		
		_isShowStatus = false;
		currentTileAbility = TileAbility.None;
        this.sprite.color = Color.white;
    }

	public static bool CheckingOrthographicTileStatus (TileArea area)
	{
		if(area.x + area.numSlotWidth > OrthographicTilemapEngine.x || area.y < 0 || area.y + area.numSlotHeight > OrthographicTilemapEngine.y)
			return false;
		
		bool _canCreateBuilding = true;
		
		for(int i = 0; i < area.numSlotWidth;i++) {
			for (int j = 0; j < area.numSlotHeight ; j++)
			{
				int newX = area.x + i;
				int newY = area.y + j;
				Arr_BuildingAreas[newX, newY]._isShowStatus = true;
				Arr_BuildingAreas[newX, newY].currentTileAbility = TileAbility.ShowStatus;
				
				if (Arr_BuildingAreas[newX, newY].tileState == TileStatus.Empty)
				{
					Arr_BuildingAreas[newX, newY].sprite.color = Color.green;
				}
				else if (Arr_BuildingAreas[newX, newY].tileState == TileStatus.NoEmpty)
				{
					Arr_BuildingAreas[newX, newY].sprite.color = Color.red;
					_canCreateBuilding = false;
				}
			}
		}
		
		return _canCreateBuilding;
	}

    internal static bool CheckedIsomatricTileStatus(TileArea area) {
		if(area.x + area.numSlotWidth > IsometricEngine.x || area.y < 0)
			return false;

        bool _canCreateBuilding = true;
 
        for(int i = 0; i < area.numSlotWidth;i++) {
			for (int j = 0; j < area.numSlotHeight ; j++)
			{
                int newX = area.x + i;
                int newY = area.y + j;
                Arr_BuildingAreas[newX, newY]._isShowStatus = true;
                Arr_BuildingAreas[newX, newY].currentTileAbility = TileAbility.ShowStatus;

                if (Arr_BuildingAreas[newX, newY].tileState == TileStatus.Empty)
                {
                    Arr_BuildingAreas[newX, newY].sprite.color = Color.green;
                }
                else if (Arr_BuildingAreas[newX, newY].tileState == TileStatus.NoEmpty)
                {
                    Arr_BuildingAreas[newX, newY].sprite.color = Color.red;
                    _canCreateBuilding = false;
                }
			}
        }
        
        return _canCreateBuilding;
    }

    internal static Vector3 GetAreaPosition(TileArea area) {
        List<Vector3> positions = new List<Vector3>();
        Vector3 areaPositions = Vector3.zero;
        for (int i = 0; i < area.numSlotWidth; i++)
        {
            for (int j = 0 ; j < area.numSlotHeight; j++)
            {
                int newX = area.x + i;
                int newY = area.y + j;
                positions.Add(Arr_BuildingAreas[newX, newY].transform.position);
            }
        }

        foreach (Vector3 item in positions)
            areaPositions += item;
		
		areaPositions = new Vector3(areaPositions.x / positions.Count, areaPositions.y / positions.Count, ((areaPositions.z / positions.Count) - (area.numSlotHeight + 0.5f)));

        return areaPositions;
    }

    internal static void SetNoEmptyArea(TileArea area) {
        for (int i = 0; i < area.numSlotWidth; i++) {
            for (int j = 0; j < area.numSlotHeight; j++) {
                int newX = area.x + i;
                int newY = area.y + j;				
				
                Arr_BuildingAreas[newX, newY].tileState = TileStatus.NoEmpty;
            }
        }
    }

    internal static void SetEmptyArea(TileArea area) {
        for (int i = 0; i < area.numSlotWidth; i++) {
            for (int j = 0; j < area.numSlotHeight; j++) {
                int newX = area.x + i;
                int newY = area.y + j;
				
                Arr_BuildingAreas[newX, newY].tileState = TileStatus.Empty;
            }
        }
    }
}
