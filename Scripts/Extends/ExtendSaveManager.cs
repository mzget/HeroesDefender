using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendSaveManager : ISaveData
{	
	public const string KEY_SYSTEM_LANGUAGE = "SYSTEM_LANGUAGE";
	public static int Language_id;
	/// <summary>
	/// Storage data key.
	/// </summary>
	public const string KEY_USERNAME = "USERNAME";
	public const string KEY_MONEY = "MONEY";
	
	
	/// <summary>
	/// Standard storage game data.
	/// </summary>
	//<!-- Save Game Slot.
	public static int SaveSlot = 0;
	//<!-- User Name.
	public static string Username = "";


	#region <@-- Load secsion.

	public void Load() {
		Debug.Log("Load storage data to static variable complete.");
	}

	#endregion

	#region <@-- Save section.

	public void Save() {
		Debug.Log("SaveDataToPermanentMemory");		
	}

	#endregion
	
	public void DeleteSave() {}
}

