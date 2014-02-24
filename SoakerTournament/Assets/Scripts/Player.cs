using UnityEngine;
using System ;
using System.Collections;
using System.IO;
using System.Text;

public class Player : MonoBehaviour
{

	private GameObject pPrefab ;
	private GameObject pObject ;

//	Currently will just create a Penny
//	public Player()
//	{
//
//
//	}

	public void Start()
	{
		pPrefab = (GameObject)(Resources.Load("Prefabs/player/Penny")) ;
		pObject = (GameObject)(Instantiate (pPrefab, new Vector3(5, 10, 5), Quaternion.Euler(Vector3.zero))) ;
		Camera.main.transform.parent = pObject.transform ;
		Camera.main.transform.localPosition = new Vector3(0,20,-100) ;
		Spawn () ;
	}

	public void Update()
	{
	}

	//Reset the players health/position
	public void Spawn()
	{
		pObject.transform.position = new Vector3(5,10,5) ;
	}
}