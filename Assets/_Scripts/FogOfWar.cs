using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FogOfWar : MonoBehaviour {
    
    public TileBase EmptyTile;
    public Tilemap tilemap;
    public GameObject player;
    private Vector3Int playerPos;
	// Use this for initialization
	void Start () {
        tilemap.ClearAllTiles();
       // player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update () {
        playerPos = new Vector3Int(Mathf.RoundToInt(player.transform.position.x), Mathf.RoundToInt(player.transform.position.y), Mathf.RoundToInt(player.transform.position.z));

        ClearTile();

    }



    void ClearTile() {
        if (tilemap.HasTile(playerPos))
        {
            tilemap.BoxFill(playerPos, EmptyTile, playerPos.x-2, playerPos.y-2, playerPos.x + 1, playerPos.y + 1);
            //tilemap.SetTile(playerPos,EmptyTile);
         }
       
    }
}
