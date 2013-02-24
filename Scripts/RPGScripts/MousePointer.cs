using UnityEngine;
using System.Collections;

public class MousePointer : MonoBehaviour
{
    public Texture2D normalCursor;
    public Texture2D attackCursor;

    private Texture2D mouseCursor;

    private int cursorWidth = 32;
    private int cursorHeight = 32;

    private bool _attack = false;
	
	
	void Awake() {
		Screen.showCursor = false;
	}
	
	// Use this for initialization
	void Start ()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	// Update is called once per frame
	void Update ()
	{
//        this.transform.position = OT.view.mouseWorldPosition;

        if (_attack)
        {
            mouseCursor = attackCursor;
        }
        else { mouseCursor = normalCursor; }
	}

    public void SetAttack(bool b) {
        _attack = b;
    }
	
	void OnGUI()
	{
        //GUI.depth = 0;
        GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, cursorWidth, cursorHeight), mouseCursor);
	}
}

