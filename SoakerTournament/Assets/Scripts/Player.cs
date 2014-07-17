using UnityEngine;
using System ;
using System.Collections;
using System.IO;
using System.Text;

public class Player : MonoBehaviour
{
	
	protected GameObject pPrefab ;
	protected GameObject pObject ;
	protected GameObject pWaterHitBox ;
	protected GameObject pGun ;
	public Gun gun ;
	
	// Currently will just create a Penny
	// public Player()
	// {
	//
	//
	// }

	public void Start()
	{
		pGun = (GameObject)Instantiate((GameObject)(Resources.Load("Prefabs/Guns/TestGun"))) ;
		//gun = (Gun)pGun.GetComponent("Gun") ;

		gun = pGun.GetComponent<Gun>() ;
	}

	public void Awake()
	{

		//Camera.main.transform.parent = pObject.transform ;
		//Camera.main.transform.localPosition = new Vector3(0,20,-100) ;
		//Spawn () ;
	}
	
	public void Init()
	{

	}
	
	public void Update()
	{
		//gun.Fire () ;
	}
	
	//Reset the players health/position
	// public void Spawn()
	// {
	// pObject.transform.position = new Vector3(10,10,10) ;
	// }
	
	public void Spawn(Vector3 spawnpos, Vector3 rotation)
	{
		//if(pObject == null)
		// Start () ;
		pObject.transform.position = spawnpos ;
		//pObject.transform.rotation = Quaternion.LookRotation(Vector3.forward) ;
		pObject.transform.rotation = Quaternion.AngleAxis(rotation.y, Vector3.up) ;
		//pGun.transform.parent = pPrefab.transform ;
		pGun.GetComponent<Gun>().SetParent(pPrefab) ;
		//pGun.transform.localPosition = new Vector3(0.0f, 0.0f, 5.0f) ;
		//gun.SetParent(pObject) ;
		//pObject.transform.localEulerAngles = rotation ;
	}
}