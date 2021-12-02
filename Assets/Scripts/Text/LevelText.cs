using System.Collections;
using UnityEngine;

public class LevelText : MonoBehaviour {

	public TileHero hero;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		GetComponent<TextMesh> ().text = hero.level.ToString ();

	}
}