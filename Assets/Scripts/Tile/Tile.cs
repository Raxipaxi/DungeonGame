using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	public bool isSelected=false;
	public int type;
	public ArrayList properties;
	public Vector2 pos;
	
	public TileManager tileManager;
	
	
	// Use this for initialization
	void Start () {
		tileManager = (TileManager)(GameObject.FindObjectOfType(typeof(TileManager)));
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown(){
        // this object was clicked - do something
    	//BoardManager.selectTile(this);
		if (this.isSelected){
			deselect();
		}else{
			selectTile();	
		}
		
    }  
	
	virtual public void selectTile()
	{
		this.isSelected=true;
		this.GetComponent<Renderer>().material.color= Color.red;
		BoardManager.selectTile(this);
	}

	virtual public void deselect ()
	{
		this.isSelected=false;
		this.GetComponent<Renderer>().material.color= Color.white;
		BoardManager.unSelectTile();
	}

	virtual public void fiveMatch (TileBoard tileBoard, ArrayList matchingTiles)
	{
		
	}

	virtual public void fourMatch (TileBoard tileBoard, ArrayList macthingTiles)
	{
		
	}

	virtual public void threeMatch (TileBoard tileBoard, ArrayList matchingTiles)
	{
		
	}


}
