using UnityEngine;
using System.Collections;

public class MonsterLevelText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		TileMonster tileMonster = (TileMonster)transform.parent.gameObject.GetComponent("TileMonster");
		GetComponent<TextMesh>().text =tileMonster.level.ToString();
		
	}
}
