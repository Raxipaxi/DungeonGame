using UnityEngine;
using System.Collections;

public class TileWall : Tile {

	bool locked;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void selectTile ()
	{
		if(!locked){
			base.selectTile ();
		}
	}
	
	override public void deselect ()
	{
		if(locked){
			this.isSelected=false;
		this.GetComponent<Renderer>().material.color= Color.gray;
		BoardManager.unSelectTile();
		}else{
			base.deselect ();
		}
	}
	
	public override void fiveMatch (TileBoard tileBoard, ArrayList matchingTiles)
	{
		//Tiene que aparecer una puerta!!!!!!
		foreach(TileWall tile in matchingTiles){
			tile.locked=true;
			tile.GetComponent<Renderer>().material.color=Color.gray;
		}
	}
	
	
	public override void fourMatch (TileBoard tileBoard, ArrayList macthingTiles)
	{
		foreach(TileWall tile in macthingTiles){
			tile.locked=true;
			tile.GetComponent<Renderer>().material.color=Color.gray;
		}
	}
	
	
	public override void threeMatch (TileBoard tileBoard, ArrayList matchingTiles)
	{
		foreach(TileWall tile in matchingTiles){
			tile.locked=true;
			tile.GetComponent<Renderer>().material.color=Color.gray;
		}
	}
}
