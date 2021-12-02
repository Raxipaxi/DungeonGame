using UnityEngine;
using System.Collections;

public class TileHero : Tile {
	
	public Tile item=null;
	public int level;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public Tile pickUp (Tile tile)
	{
		Tile drop = item;
		item = tile;
		return drop;
	}

	public void levelUp (int par1)
	{
		level=level+par1;
	}
}
