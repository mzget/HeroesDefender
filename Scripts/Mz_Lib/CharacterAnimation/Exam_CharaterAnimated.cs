using UnityEngine;
using System.Collections;

public class Exam_CharaterAnimated : MonoBehaviour {

	public CharacterAnimationManager animationManager;

	CharacterAnimationManager.NameAnimationsList nameList_0;
	CharacterAnimationManager.NameAnimationsList nameList_1;
	CharacterAnimationManager.NameAnimationsList nameList_2;
	CharacterAnimationManager.NameAnimationsList nameList_3;
	CharacterAnimationManager.NameAnimationsList nameList_4;

	// Use this for initialization
	void Start () {
		nameList_0 = (CharacterAnimationManager.NameAnimationsList)0;
		nameList_1 = (CharacterAnimationManager.NameAnimationsList)1;
		nameList_2 = (CharacterAnimationManager.NameAnimationsList)2;
		nameList_3 = (CharacterAnimationManager.NameAnimationsList)3;
		nameList_4 = (CharacterAnimationManager.NameAnimationsList)4;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity,  new Vector3(Screen.width/ Main.FixedGameWidth, Screen.height/Main.FixedGameHeight, 1));

		if(GUI.Button(new Rect(0,0, 150,50), nameList_0.ToString())) {
			animationManager.PlayAnimationByName(nameList_0);
		}
		else if(GUI.Button(new Rect(0,50, 150,50), nameList_1.ToString())) {
			animationManager.PlayAnimationByName(nameList_1);
		}
		else if(GUI.Button(new Rect(0,100, 150,50), nameList_2.ToString())) {
			animationManager.PlayAnimationByName(nameList_2);
		}
		else if(GUI.Button(new Rect(0, 150, 150,50), nameList_3.ToString())) {
			animationManager.PlayAnimationByName(nameList_3);
		}
		else if(GUI.Button(new Rect(0, 200, 150,50), nameList_4.ToString())) {
			animationManager.PlayAnimationByName(nameList_4);
		}
	}
}
