using UnityEngine;
using System.Collections;

public class HeroStatus : MonoBehaviour {

    public int originalDamage = 10;
    public string heroName = "Barbarian";
	// Level.
	// Exp.
	// HP.
	// Mana.
	private int level = 1;
	private float experience = 0;
	
	private int hoursePower = 0;
	private int mana = 0;
	// STR.
	// AGI.
	// INT.
	private int strength = 0;
	private int agility = 0;
	private int intelligence = 0;
	
	/// <summary>
	/// Share Property.
	/// </summary>
	public int Level {
		get { return level; }
		set { level = value;}
	}
	public float Experience {
		get{ return experience; }
		set{ experience = value; }
	}
	
	public int Hoursepower {
		get { return hoursePower; }
		set { hoursePower = value; }
	}
	public int Mana {
		get{ return mana; }
		set{ mana = value; }
	}
	
	public int STR {
		get {return strength;}
		set {strength = value;}
	}
	public int AGI {
		get{return agility;}
		set{agility = value;}
	}
	public int INT {
		get {return intelligence;}
		set {intelligence = value;}
	}
	
	private int saveGameSlot;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		
	}
}
