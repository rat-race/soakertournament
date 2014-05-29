using UnityEngine;
using System ;
using System.Collections;
using System.IO;
using System.Text;

public class AssetDatabase : MonoBehaviour
{
	
	private int assetTotal ;
	private GameObject[] objTiles ;
	
	public GameObject GetAsset(int ID)
	{
		if(ID < assetTotal)
		{
			return objTiles[ID] ;
		}
		return null ;
	}
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	void Load(string location)
	{
		//First pass of file - Find the initialisation
		
		TextReader tr = new StreamReader(location) ;
		
		String str = null ;
		
		assetTotal = 0 ;
		
		while ((str = tr.ReadLine())!= null)
		{
			string[] strings = str.Split (' ') ;
			
			if(strings[0] == "L")
				assetTotal++ ;
		}
		
		objTiles = new GameObject[assetTotal] ;
		
		//Third pass of file - Load the assets
		
		tr = new StreamReader(location) ;
		
		int assetCount = 0 ;
		
		while ((str = tr.ReadLine())!= null)
		{
			string[] strings = str.Split (' ') ;
			
			if(strings[0] == "L")
			{
				try
				{
					objTiles[assetCount] = (GameObject)(Resources.Load(strings[1])) ;
					assetCount++ ;
				}
				catch(FormatException e)
				{
					break ;
				}
			}
		}	
	}
	
}