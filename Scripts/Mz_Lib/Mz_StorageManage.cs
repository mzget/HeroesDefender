using UnityEngine;
using System.Collections;

public class Mz_StorageManage : MonoBehaviour
{
    public const string KEY_SYSTEM_LANGUAGE = "SYSTEM_LANGUAGE";
    public static int Language_id;


    /// <summary>
    /// Standard storage game data.
    /// </summary>
    //<!-- Save Game Slot.
    public static int SaveSlot = 0;
    //<!-- User Name.
    public static string Username = "";
    
    public static string ShopName;
    public static int AvailableMoney = 100000;
    public static int AccountBalance = 100000;
	public static int ShopLogo = 0;
	public static string ShopLogoColor = "Blue";

	public static int Roof_id = 255;
	public static int Awning_id = 255;
	public static int Table_id = 0;
	public static int Accessory_id = 0;

    public static int TK_clothe_id = 255;
    public static int TK_hat_id = 255;
    public static int Pet_id = 0;
	
	public static bool _IsNoticeUser = false;
	public static string KEY_NOTICE_USER_TO_UPGRADE = "NOTICE_USER_TO_UPGRADE";

	/// <summary>
	/// Storage data key.
	/// </summary>
	public const string KEY_USERNAME = "USERNAME";
	public const string KEY_SHOP_NAME = "SHOP_NAME";
	public const string KEY_MONEY = "MONEY";

    //<@-- Costume Storage Key Data.

	public virtual void LoadSaveDataToGameStorage()
	{		
		Debug.Log("Load storage data to static variable complete.");
	}

    public virtual void SaveDataToPermanentMemory() {
        Debug.Log("SaveDataToPermanentMemory");
    }
}
