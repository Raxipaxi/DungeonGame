using UnityEngine;
using System.Collections;


//This class manages the tiles, can create new instances of each tyle.
public class TileManager : MonoBehaviour {

	public GameObject tileHallways;
	public GameObject tileWalls;
	public GameObject tileDoors;
	public GameObject tileKeys;
	public GameObject tileTreasures;
	public GameObject tileMonsters;
	
	// Use this for initialization
	void Start () {
		//tileHallways = (GameObject)Resources.Load("TileHallways", typeof(GameObject));
		tileWalls = (GameObject)Resources.Load("TileWalls", typeof(GameObject));
		tileDoors = (GameObject)Resources.Load("TileDoors", typeof(GameObject));
		tileKeys = (GameObject)Resources.Load("TileKeys", typeof(GameObject));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Tile getHallway (Vector2 par1)
	{
		Tile tileHallway=(Tile)Instantiate(tileHallways.gameObject.GetComponent<TileHallway>(),new Vector3(par1.x*22,-par1.y*23,10),Quaternion.identity);
		tileHallway.pos=par1;
		return tileHallway;
	}
	
	public Tile getWall (Vector2 par1)
	{
		Tile tileWall=(Tile)Instantiate(tileWalls.gameObject.GetComponent<TileWall>(),new Vector3(par1.x*22,-par1.y*23,10),Quaternion.identity);
		tileWall.pos=par1;
		return tileWall;
	}
	
	public Tile getDoor (Vector2 par1)
	{
		Tile tileDoor=(Tile)Instantiate(tileDoors.gameObject.GetComponent<TileDoor>(),new Vector3(par1.x*22,-par1.y*23,10),Quaternion.identity);
		tileDoor.pos=par1;
		return tileDoor;
	}
	
	public Tile getKey (Vector2 par1)
	{
		Tile tileKey=(Tile)Instantiate(tileKeys.gameObject.GetComponent<TileKey>(),new Vector3(par1.x*22,-par1.y*23,10),Quaternion.identity);
		tileKey.pos=par1;
		return tileKey;
	}
	
	public Tile getTreasure (Vector2 par1)
	{
		Tile tileTreasure=(Tile)Instantiate(tileTreasures.gameObject.GetComponent<TileTreasure>(),new Vector3(par1.x*22,-par1.y*23,10),Quaternion.identity);
		tileTreasure.pos=par1;
		return tileTreasure;
	}
	
	public Tile getMonster (Vector2 par1,int level)
	{
		TileMonster tileMonster=(TileMonster)Instantiate(tileMonsters.gameObject.GetComponent<TileMonster>(),new Vector3(par1.x*22,-par1.y*23,10),Quaternion.identity);
		tileMonster.pos=par1;
		tileMonster.level=level;
		return tileMonster;
	}
	
	public Tile getObstacle (Vector2 pos)
	{
		//rehacer para que busque obstaculos en vez de usar los que se le dice
		float value = Random.value*2;
			
			switch ((int)value)
				{
					case 0: 
						return getWall(pos);
						break;
					default: 
						return getDoor(pos);
						break;
				}
	}

	public Tile getReward (Vector2 pos)
	{
		float value = Random.value*2;
			
			switch ((int)value)
				{
					case 0: 
						return getTreasure(pos);
						break;
					default: 
						return getKey(pos);
						break;
				}
	}
}


