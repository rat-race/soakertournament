using UnityEngine;
using System.Collections;



public class WaterCollection
{
	const int NUMBER_OF_WATER = 50 ;

	GameObject[] waterComponents ;

	WaterCollection()
	{
		waterComponents = new GameObject[NUMBER_OF_WATER] ;
	}

	public int Init()
	{
		for(int i = 0 ; i < NUMBER_OF_WATER; i++)
		{
			//waterComponents[i] = Instantiate(pPrefab, new Vector3(0,-5, 0), new Quaternion(0,0,0,0)) as GameObject ;
		}

		return 0 ;
	}

	public void Update()
	{
		for(int i = 0 ; i < NUMBER_OF_WATER; i++)
		{
			//waterComponents[i] = Instantiate(pPrefab, new Vector3(0,-5, 0), new Quaternion(0,0,0,0)) as GameObject ;


		}
	}

}
