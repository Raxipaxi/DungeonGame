using System.Collections;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	static public TileBoard tileBoard;
	static public SwapManager swapManager;
	static public MatchManager matchManager;
	//private Board board = new Board();
	static public int tilesSelected = 0; //cantidad de tiles seleccionadas al mismo tiempo, si llega a 2 hay que chequear si son adjacentes e intercambiarlas
	static private Tile selectedTile1 = null;
	static private Tile selectedTile2 = null;

	// Use this for initialization
	void Start () {
		tileBoard = (TileBoard) (GameObject.FindObjectOfType (typeof (TileBoard)));
		swapManager = (SwapManager) (GameObject.FindObjectOfType (typeof (SwapManager)));
		matchManager = (MatchManager) (GameObject.FindObjectOfType (typeof (MatchManager)));
		/*createBoard(board.rows,board.cols);
		initTiles();*/
	}

	// Update is called once per frame
	void Update () {
		matchManager.updateBoard (tileBoard);
	}

	void initTiles () {
		throw new System.NotImplementedException ();
	}

	void checkforLines () {
		throw new System.NotImplementedException ();
	}

	void createBoard (int rows, int cols) {
		throw new System.NotImplementedException ();
	}

	static bool checkAdjacent (Tile tile1, Tile tile2) {
		bool isAdjacent = false;
		Vector2 dist = new Vector2 (Mathf.Abs (tile1.pos.x - tile2.pos.x), Mathf.Abs (tile1.pos.y - tile2.pos.y));
		if (dist == new Vector2 (0, 1) || dist == new Vector2 (1, 0)) {
			isAdjacent = true;
		}
		return isAdjacent;
	}

	static void swapTiles (Tile tile1, Tile tile2) {
		tileBoard.matrix[(int) tile1.pos.x, (int) tile1.pos.y] = tile2;
		tileBoard.matrix[(int) tile2.pos.x, (int) tile2.pos.y] = tile1;
		Vector2 vectAux = tile1.pos;
		tile1.pos = tile2.pos;
		tile2.pos = vectAux;
		BoardViewManager.swap (tile1, tile2);
	}

	public static void selectTile (Tile tile) {
		tilesSelected++;
		if (selectedTile1 == null) {
			selectedTile1 = tile;
		} else {
			selectedTile2 = tile;
			if (checkAdjacent (selectedTile1, selectedTile2)) {
				if (swapManager.canSwap (selectedTile1, selectedTile2)) {
					swapTiles (selectedTile1, selectedTile2);
					matchManager.updateBoard (tileBoard);

				}
			}
			tilesSelected = 0;
			selectedTile1.deselect ();
			selectedTile2.deselect ();
			selectedTile1 = null;
			selectedTile2 = null;
			//checkforLines();
			//BoardViewManager.showBoard();
		}
	}

	public static void unSelectTile () {
		tilesSelected = 0;
		selectedTile1 = null;

	}
}