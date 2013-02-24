using UnityEngine;
using System.Collections;

public class PauseGameBeh : MonoBehaviour {
	
	private bool pauseGameState = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(pauseGameState) {
			Time.timeScale = 0;
		}
		else {
			Time.timeScale = 1;
		}
	}
	
	public void SetPauseGame(bool setPause) {
		pauseGameState = setPause;
	}

    void OnGUI() {
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, Screen.height/Main.FixedGameHeight, 1));

        if(pauseGameState) {

            GUI.BeginGroup(new Rect(Main.GAMEWIDTH / 2 - 100, Main.GAMEHEIGHT / 2 - 100, 200, 200));
            {   
                if(GUI.Button(new Rect(20,10,160,40), "Resume")) {
                    pauseGameState = false;
                }
                else if(GUI.Button(new Rect(20,70,160,40), "Exit")) {
                    if(!Application.isLoadingLevel) {
                        Application.LoadLevel("MainMenuScene");
                    }
                }
            }
            GUI.EndGroup();
        }
    }
}
