using System.Collections;
using UnityEngine;

public class BoardViewManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public static void showBoard () {
		throw new System.NotImplementedException ();
	}

	public static void swap (Tile tile1, Tile tile2) {
		Vector3 vectAux = tile1.GetComponent<Renderer> ().transform.position;
		tile1.GetComponent<Renderer> ().transform.position = tile2.GetComponent<Renderer> ().transform.position;
		tile2.GetComponent<Renderer> ().transform.position = vectAux;
	}
}