using UnityEngine;
using System.Collections;

public class DragableGUI : ObjectsBeh {

	protected BattleStage stageManager;

	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		
		stageManager = baseScene.GetComponent<BattleStage>();
	}
	
	protected override void ImplementDraggableObject ()
	{
		base.ImplementDraggableObject ();

		stageManager._updatable = false;
		
		Ray cursorRay;
		RaycastHit hit;
		cursorRay = new Ray(this.transform.position, Vector3.forward);
		if (Physics.Raycast(cursorRay, out hit, 100f))
		{
//			TileArea temp_originalArea = constructionArea;
//			Debug.Log(constructionArea.x + ":" + constructionArea.y + ":" + constructionArea.numSlotWidth + ":" + constructionArea.numSlotDepth);
			
			if (hit.collider.tag == ObjectsTagManager.WALKABLEAREA)
			{
				string[] slotId = hit.collider.name.Split(':');
				TileArea newarea = new TileArea() { 
					x = int.Parse(slotId[0]), y = int.Parse(slotId[1]), 
					numSlotWidth = 2,
					numSlotHeight = 2,
				};
				bool canCreateBuilding = Tile.CheckingOrthographicTileStatus(newarea);

				Debug.Log(newarea.x + " : " + newarea.y);
				
				if (this._isDropObject)
				{
					if(canCreateBuilding) {
						this.transform.position = Tile.GetAreaPosition(newarea);
						this.originalPosition = this.transform.position;
//						constructionArea = newarea;
						
						stageManager.CreateUnitOnWalkableArea(this.gameObject.name, this.originalPosition);
						Destroy(this.gameObject);
						stageManager.taskManager.CreateMonsterIcon(this.gameObject.name);
					}
					else {
						this.transform.position = this.originalPosition;
//						constructionArea = temp_originalArea;
						
						Destroy(this.gameObject);
						stageManager.taskManager.CreateMonsterIcon(this.gameObject.name);
					}
					
					this._isDropObject = false;
					base._isDraggable = false;
					stageManager._updatable = true;
				}
			}
//			else if(hit.collider.tag == "Building" || hit.collider.tag == "TerrainElement") {
//				print("Tag == " + hit.collider.tag + " : Name == " + hit.collider.name);
//				
//				TilebaseObjBeh hit_obj = hit.collider.GetComponent<TilebaseObjBeh>();
//				hit_obj.ShowConstructionAreaStatus();
//				
//				if(_isDropObject) {
//					Debug.LogWarning("Building and Terrain element cannot construction");
//					
//					this.transform.position = this.originalPosition;
//					constructionArea = temp_originalArea;
//					Destroy(this.gameObject);
//					sceneController.taskManager.CreateBuildingIconInRightSidebar(this.name);
//					
//					this._isDropObject = false;
//					base._isDraggable = false;
//					TaskManager.IsShowInteruptGUI = false;
//				}
//			}
		}
		else
		{
			print("Out of ray direction");
			if (this._isDropObject) {
				this.transform.position = originalPosition;
				this._isDropObject = false;
				base._isDraggable = false;
				stageManager._updatable = true;
				
				Destroy(this.gameObject);
				stageManager.taskManager.CreateMonsterIcon(this.name);
			}
		}
		
		Debug.DrawRay(cursorRay.origin, Vector3.forward * 100f, Color.red);
	}
}
