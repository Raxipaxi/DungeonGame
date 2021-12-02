using UnityEngine;
using System.Collections;

public class TileTreasure : Tile {


	
	public override void threeMatch (TileBoard tileBoard, ArrayList matchingTiles)
	{
		foreach (TileTreasure treasure in matchingTiles){
			float value = Random.value*3;
			
			switch ((int)value)
				{
					case 1: 
						Tile hallway = tileManager.getHallway(treasure.pos);
						tileBoard.matrix[(int)treasure.pos.x,(int)treasure.pos.y]= hallway;
						GameObject.Destroy(treasure.gameObject);
						break;
					case 2: 
						Tile obstacle = tileManager.getObstacle(treasure.pos);
						tileBoard.matrix[(int)treasure.pos.x,(int)treasure.pos.y]= obstacle;
						GameObject.Destroy(treasure.gameObject);
						break;
					
				}
				
		}
	}
	
	public override void fourMatch (TileBoard tileBoard, ArrayList macthingTiles)
	{
		threeMatch(tileBoard,macthingTiles);
	}

	public override void fiveMatch (TileBoard tileBoard, ArrayList macthingTiles)
	{
		threeMatch(tileBoard,macthingTiles);
	}
}
