using UnityEngine;
using System.Collections;

public class UpdateHUD : MonoBehaviour 
{

	public static GUIText pWater ;
	public static GUIText pPressure ;
	public static GUIText pSpawnPoint ;

	private bool DebugGUI;

	// Use this for initialization
	void Start () 
	{

		//To do - Sort out finding the GUI elements - I'm too tired at the moment.
		pWater = new GUIText() ;
		pPressure = new GUIText() ;
		pSpawnPoint = new GUIText() ; 

		DebugGUI = false ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.L))
			DebugGUI = !DebugGUI ;
	}

	void OnGUI()
	{
		if(DebugGUI)
			GUI.Box(new Rect(0,0,100,50), "Debug info\nLast Spawn: " + GameLogic.lastSpawn);

		GUI.Box(new Rect(0, 200, 150, 100), "Water: " + LocalInput.water + 
		        "\nPressure: " + LocalInput.pressure + 
		        "\nSaturation: NOT ADDED" + 
		        "\nFire Distance: " + LocalInput.fireDistance) ;
		//pWater.text = "Water: " + LocalInput.water ;
		//pPressure.text = "Pressure: " + LocalInput.pressure ;
		//pSpawnPoint.text = "Spawnpoint: " + LocalInput.spawnpoint ;
	}
}
