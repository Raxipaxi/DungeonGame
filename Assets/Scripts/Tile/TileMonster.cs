using UnityEngine;
using System.Collections;

public class TileMonster : Tile {

	public int level;
	
	public override void threeMatch (TileBoard tileBoard, ArrayList matchingTiles)
	{
		foreach (TileMonster monster in matchingTiles){
			float value = Random.value*3;
			
			switch ((int)value)
				{
					case 1: 
						Tile hallway = tileManager.getHallway(monster.pos);
						tileBoard.matrix[(int)monster.pos.x,(int)monster.pos.y]= hallway;
						GameObject.Destroy(monster.gameObject);
						break;
					case 2: 
						Tile reward = tileManager.getReward(monster.pos);
						tileBoard.matrix[(int)monster.pos.x,(int)monster.pos.y]= reward;
						GameObject.Destroy(monster.gameObject);
						break;
					default:
						monster.level+=(int)(Random.value*6)+1;
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
