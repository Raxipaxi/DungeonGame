using System.Collections;
using UnityEngine;

public class MatchManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void updateBoard (TileBoard tileBoard) {
		int tileType;
		ArrayList matchingTiles;
		for (int i = 0; i < tileBoard.rows; i++) {
			for (int j = 0; j < tileBoard.cols; j++) {
				tileType = tileBoard.matrix[i, j].type;

				matchingTiles = getMatchingTiles (tileBoard, i, j, tileType);

				switch (matchingTiles.Count) {
					case 5:
						tileBoard.matrix[i, j].fiveMatch (tileBoard, matchingTiles);
						break;
					case 4:
						tileBoard.matrix[i, j].fourMatch (tileBoard, matchingTiles);
						break;
					case 3:
						tileBoard.matrix[i, j].threeMatch (tileBoard, matchingTiles);
						break;

				}

			}

		}

	}

	public ArrayList getMatchingTiles (TileBoard tileBoard, int i, int j, int tileType) {
		ArrayList matchingTiles = new ArrayList ();

		for (int k = 0; k < 5; k++) {
			if (i + k < 8 && tileBoard.matrix[i + k, j].type == tileType) {
				matchingTiles.Add (tileBoard.matrix[i + k, j]);
			} else {
				break;
			}
		}
		//si hay 3 o mas agrega las verticales, si no solo cuenta verticales

		if (matchingTiles.Count < 3) {
			matchingTiles = new ArrayList ();
			matchingTiles.Add (tileBoard.matrix[i, j]);
		}

		for (int k = 1; k < 5; k++) {
			if (j + k < 8 && tileBoard.matrix[i, j + k].type == tileType) {
				matchingTiles.Add (tileBoard.matrix[i, j + k]);
			} else {
				break;
			}
		}
		return matchingTiles;

	}
}