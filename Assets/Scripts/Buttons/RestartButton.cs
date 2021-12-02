using System.Collections;
using UnityEngine;

public class RestartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer> ().enabled = false;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown () {
		Application.LoadLevel ("RandomLevel");

	}
}