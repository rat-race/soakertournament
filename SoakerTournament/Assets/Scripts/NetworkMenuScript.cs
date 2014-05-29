using UnityEngine;
using System.Collections;

public class NetworkMenuScript : MonoBehaviour {

	public GUIStyle customButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("escape"))
						Application.LoadLevel ("Testbed");
	}

	void OnGUI () 
	{
		if (GUI.Button (new Rect (20, 20, 180, 50), "Quick Join", customButton)) {
			Application.LoadLevel("Testbed");
		}
	}
}
