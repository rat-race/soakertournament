using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour 
{

	private LocalPlayer lPlayer ;
	private Map pMap ;

	// Use this for initialization
	void Start () 
	{
		pMap = gameObject.AddComponent("Map") as Map ;
		lPlayer = gameObject.AddComponent("LocalPlayer") as LocalPlayer ;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
