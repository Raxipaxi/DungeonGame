using UnityEngine;
using System.Collections;

public class TileHallway : Tile {

	public override void threeMatch (TileBoard tileBoard, ArrayList matchingTiles)
	{
		foreach (TileHallway hall in matchingTiles){
			float value = Random.value*4;
			
			switch ((int)value)
				{
					case 1: 
						Tile reward = tileManager.getReward(hall.pos);
						tileBoard.matrix[(int)hall.pos.x,(int)hall.pos.y]= reward;
						Destroy(hall.gameObject);
						break;
					case 2: 
						
						break;
					default: 
						Tile obstacle = tileManager.getObstacle(hall.pos);
						tileBoard.matrix[(int)hall.pos.x,(int)hall.pos.y]= obstacle;
						Destroy(hall.gameObject);
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
