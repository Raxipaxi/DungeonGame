using System.Collections;
using UnityEngine;

public class TileBoard : MonoBehaviour {

	public TileManager tileManager;

	public Tile itemTile;

	public int rows = 8;
	public int cols = 8;

	public Tile[, ] matrix;

	// Use this for initialization
	void Start () {
		tileManager = (TileManager) (GameObject.FindObjectOfType (typeof (TileManager)));

		//itemTile = (Tile)(GameObject.FindObjectOfType(typeof(Tile)));

		matrix = new Tile[rows, cols];

		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < cols; j++) {
				if (i == 0 && j == 0) {
					matrix[0, 0] = (TileHero) (GameObject.FindObjectOfType (typeof (TileHero)));
					matrix[0, 0].pos = new Vector2 (0, 0);
				} else if (i == 7 && j == 7) {
					matrix[7, 7] = (TileExit) (GameObject.FindObjectOfType (typeof (TileExit)));
					matrix[7, 7].pos = new Vector2 (7, 7);
				} else {
					float value = Random.value * 7;

					switch ((int) value) {
						case 2:
							matrix[i, j] = tileManager.getWall (new Vector2 (i, j));
							break;
						case 3:
							matrix[i, j] = tileManager.getDoor (new Vector2 (i, j));
							break;
						case 4:
							matrix[i, j] = tileManager.getKey (new Vector2 (i, j));
							break;
						case 5:
							matrix[i, j] = tileManager.getTreasure (new Vector2 (i, j));
							break;
						case 6:
							matrix[i, j] = tileManager.getMonster (new Vector2 (i, j), (int) (Random.value * 6) + 1);
							break;
						default:
							matrix[i, j] = tileManager.getHallway (new Vector2 (i, j));
							break;
					}

				}
			}
		}
	}

	// Update is called once per frame
	void Update () {

	}

}