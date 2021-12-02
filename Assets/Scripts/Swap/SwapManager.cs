using System.Collections;
using UnityEngine;

public class SwapManager : MonoBehaviour {

	enum Types { Hero = 0, Hallway = 1, Wall = 2, Door = 3, Key = 4, Treasure = 5, Monster = 6, Exit = 99 };

 	public TileManager tileManager;
 	public TileBoard tileBoard;
 	public Tile itemTile;
 //Matriz de posibilidad de intercambio, un 0 indica que no se pueden intercambiar, un 1 que si, un 2 o mas que es necesario un nuevo chequeo o accion,un 99 que se gana la partida.
 //Las filas y columnas reprecentan los types de tiles que hay
 	public int[, ] swapMatrix = new int[100, 100];

 // Use this for initialization
 	void Start () {
 		
		tileManager = (TileManager) (GameObject.FindObjectOfType (typeof (TileManager)));
 		tileBoard = (TileBoard) (GameObject.FindObjectOfType (typeof (TileBoard)));
 //itemTile = (Tile)(GameObject.FindObjectOfType(typeof(Tile)));

 		for (int i = 0; i < 100; i++) {
 			for (int j = 0; j < 100; j++) {
 				swapMatrix[i, j] = 0;
			}
		}
		setUpHero ();
		setUpHallway ();
		setUpWall ();
		setUpExit ();
		setUpDoor ();
		setUpKey ();
		setUpTreasure ();
		setUpMonster ();

	}

	public bool canSwap (Tile tile1, Tile tile2) {
		bool canSwapBool = false;
		Debug.Log (tile1.type + " " + tile2.type + " el valor de la matriz es: " + swapMatrix[tile1.type, tile2.type]);
		int swapValue = swapMatrix[tile1.type, tile2.type];
		if (swapValue != 0) {
			switch (swapValue) {
				case 99:
					Application.Quit ();
					break;
				case 2:
					if (!useKey (tile1, tile2)) return false;
					break;
				case 3:
					pickUpItem (tile1, tile2);
					break;
				case 4:
					pickUpTreasure (tile1, tile2);
					break;
				case 5:
					if (!fightMonster (tile1, tile2)) return false;
					break;

				default:
					Debug.Log ("Algo malio sal con el swapValue : " + swapValue);
			}
			canSwapBool = true;
		}
		return canSwapBool;
	}

	void pickUpItem (Tile tile1, Tile tile2) {
		Tile drop = null;

		TileHero tileHero;
		Tile tileItem;

		if (tile1.type == 0) {
			tileHero = (TileHero) tile1;
			tileItem = tile2;
		} else {
			tileHero = (TileHero) tile2;
			tileItem = tile1;
		}

		drop = tileHero.pickUp (tileItem);

		if (drop == null) {
			drop = tileManager.getHallway (tileHero.pos);
		}

		drop.pos = tileHero.pos; //esto tambien es innecesario

		drop.GetComponent<Renderer> ().transform.position = itemTile.GetComponent<Renderer> ().transform.position;
		BoardViewManager.swap (drop, tileHero);

		tileBoard.matrix[(int) drop.pos.x, (int) drop.pos.y] = drop;
		//permutacion necesaria porque se destruye uno de los objetos pasados como parametro;
		tileHero.pos = tileItem.pos;
		tileBoard.matrix[(int) tileHero.pos.x, (int) tileHero.pos.y] = tileHero;

		//
		itemTile.GetComponent<Renderer> ().enabled = false;
	}

	public bool useKey (Tile tile1, Tile tile2) {
		TileHero tileHero;
		TileDoor tileDoor;
		if (tile1.type == 0) {
			tileHero = (TileHero) tile1;
			tileDoor = (TileDoor) tile2;
		} else {
			tileHero = (TileHero) tile2;
			tileDoor = (TileDoor) tile1;
		}
		if (tileHero.item != null && tileHero.item.type == 4) {
			GameObject.Destroy (tileHero.item.gameObject);
			itemTile.GetComponent<Renderer> ().enabled = true;
			tileHero.item = null;
			tileBoard.matrix[(int) tileHero.pos.x, (int) tileHero.pos.y] = tileManager.getHallway (tileHero.pos);
			//permutacion necesaria porque se destruye uno de los objetos pasados como parametro;
			tileBoard.matrix[(int) tileDoor.pos.x, (int) tileDoor.pos.y] = tileHero;
			tileHero.pos = tileDoor.pos;
			//
			GameObject.Destroy (tileDoor.gameObject);

			return true;
		}
		return false;
	}

	public void pickUpTreasure (Tile tile1, Tile tile2) {
		TileHero tileHero;
		TileTreasure tileTreasure;
		if (tile1.type == 0) {
			tileHero = (TileHero) tile1;
			tileTreasure = (TileTreasure) tile2;
		} else {
			tileHero = (TileHero) tile2;
			tileTreasure = (TileTreasure) tile1;
		}

		tileBoard.matrix[(int) tileHero.pos.x, (int) tileHero.pos.y] = tileManager.getHallway (tileHero.pos);
		//permutacion necesaria porque se destruye uno de los objetos pasados como parametro;
		tileBoard.matrix[(int) tileTreasure.pos.x, (int) tileTreasure.pos.y] = tileHero;
		tileHero.pos = tileTreasure.pos;
		//
		GameObject.Destroy (tileTreasure.gameObject);
		tileHero.levelUp (1);

	}

	public bool fightMonster (Tile tile1, Tile tile2) {
		TileHero tileHero;
		TileMonster tileMonster;
		if (tile1.type == 0) {
			tileHero = (TileHero) tile1;
			tileMonster = (TileMonster) tile2;
		} else {
			tileHero = (TileHero) tile2;
			tileMonster = (TileMonster) tile1;
		}

		if (tileHero.level < tileMonster.level) {
			return false;
		} else if (tileHero.level > tileMonster.level) {
			tileBoard.matrix[(int) tileHero.pos.x, (int) tileHero.pos.y] = tileManager.getHallway (tileHero.pos);
			//permutacion necesaria porque se destruye uno de los objetos pasados como parametro;
			tileBoard.matrix[(int) tileMonster.pos.x, (int) tileMonster.pos.y] = tileHero;
			tileHero.pos = tileMonster.pos;
			//
			GameObject.Destroy (tileMonster.gameObject);
		}

		return true;

	}

	void setUpHero () {
		swapMatrix[(int) Types.Hero, (int) Types.Hallway] = 1;
		swapMatrix[(int) Types.Hero, (int) Types.Wall] = 0;
		swapMatrix[(int) Types.Hero, (int) Types.Door] = 2; //Un 2 es que necesita llave para poder invertir
		swapMatrix[(int) Types.Hero, (int) Types.Key] = 3; //Un 3 indica agarrar un objeto
		swapMatrix[(int) Types.Hero, (int) Types.Treasure] = 4; //Un 4 indica agarrar un tesoro
		swapMatrix[(int) Types.Hero, (int) Types.Monster] = 5; //Un 5 indica que debe pelear con un monstruo
		swapMatrix[(int) Types.Hero, (int) Types.Exit] = 99;
	}

	void setUpHallway () {
		swapMatrix[(int) Types.Hallway, (int) Types.Hero] = 1;
		swapMatrix[(int) Types.Hallway, (int) Types.Exit] = 0;
		swapMatrix[(int) Types.Hallway, (int) Types.Door] = 0;
		swapMatrix[(int) Types.Hallway, (int) Types.Wall] = 1;
		swapMatrix[(int) Types.Hallway, (int) Types.Key] = 1;
		swapMatrix[(int) Types.Hallway, (int) Types.Monster] = 1;
		swapMatrix[(int) Types.Hallway, (int) Types.Treasure] = 1;

	}

	void setUpWall () {
		swapMatrix[(int) Types.Wall, (int) Types.Hero] = 0;
		swapMatrix[(int) Types.Wall, (int) Types.Hallway] = 1;
		swapMatrix[(int) Types.Wall, (int) Types.Door] = 0;
		swapMatrix[(int) Types.Wall, (int) Types.Key] = 1;
		swapMatrix[(int) Types.Wall, (int) Types.Treasure] = 1;
		swapMatrix[(int) Types.Wall, (int) Types.Monster] = 0;
		swapMatrix[(int) Types.Wall, (int) Types.Exit] = 0;
	}

	void setUpExit () {
		swapMatrix[(int) Types.Exit, (int) Types.Hero] = 99;
		swapMatrix[(int) Types.Exit, (int) Types.Wall] = 0;
		swapMatrix[(int) Types.Exit, (int) Types.Hallway] = 0;
		swapMatrix[(int) Types.Exit, (int) Types.Door] = 0;
		swapMatrix[(int) Types.Exit, (int) Types.Key] = 0;
		swapMatrix[(int) Types.Exit, (int) Types.Treasure] = 0;
		swapMatrix[(int) Types.Exit, (int) Types.Monster] = 0;
	}

	void setUpDoor () {
		swapMatrix[(int) Types.Door, (int) Types.Hero] = 2;
		swapMatrix[(int) Types.Door, (int) Types.Hallway] = 0;
		swapMatrix[(int) Types.Door, (int) Types.Wall] = 0;
		swapMatrix[(int) Types.Door, (int) Types.Key] = 0;
		swapMatrix[(int) Types.Door, (int) Types.Treasure] = 0;
		swapMatrix[(int) Types.Door, (int) Types.Monster] = 0;
		swapMatrix[(int) Types.Door, (int) Types.Exit] = 0;
	}

	void setUpKey () {
		swapMatrix[(int) Types.Key, (int) Types.Hero] = 3;
		swapMatrix[(int) Types.Key, (int) Types.Hallway] = 1;
		swapMatrix[(int) Types.Key, (int) Types.Wall] = 1;
		swapMatrix[(int) Types.Key, (int) Types.Door] = 0;
		swapMatrix[(int) Types.Key, (int) Types.Treasure] = 1;
		swapMatrix[(int) Types.Key, (int) Types.Monster] = 1;
		swapMatrix[(int) Types.Key, (int) Types.Exit] = 0;
	}

	void setUpTreasure () {
		swapMatrix[(int) Types.Treasure, (int) Types.Hero] = 4;
		swapMatrix[(int) Types.Treasure, (int) Types.Hallway] = 1;
		swapMatrix[(int) Types.Treasure, (int) Types.Wall] = 1;
		swapMatrix[(int) Types.Treasure, (int) Types.Door] = 0;
		swapMatrix[(int) Types.Treasure, (int) Types.Key] = 1;
		swapMatrix[(int) Types.Treasure, (int) Types.Monster] = 0;
		swapMatrix[(int) Types.Treasure, (int) Types.Exit] = 0;
	}

	void setUpMonster () {
		swapMatrix[(int) Types.Monster, (int) Types.Hero] = 5;
		swapMatrix[(int) Types.Monster, (int) Types.Hallway] = 1;
		swapMatrix[(int) Types.Monster, (int) Types.Wall] = 0;
		swapMatrix[(int) Types.Monster, (int) Types.Door] = 0;
		swapMatrix[(int) Types.Monster, (int) Types.Key] = 1;
		swapMatrix[(int) Types.Monster, (int) Types.Treasure] = 0;
		swapMatrix[(int) Types.Monster, (int) Types.Exit] = 0;

	}
}