using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapCreator : MonoBehaviour {

    public Tilemap tilemap; 
    public Transform SpawnPosition;
    public TileBase tile;
    // Use this for initialization
    public TileBase tileA;
    public TileBase tileB;
    public Vector2Int size;

    void Start()
    {
               

        Vector3Int[] positions = new Vector3Int[size.x * size.y];
        TileBase[] tileArray = new TileBase[positions.Length];

        for (int index = 0; index < positions.Length; index++)
        {
            positions[index] = new Vector3Int(index % size.x, index / size.y, 0);
            tileArray[index] = index % 2 == 0 ? tileA : tileB;
        }

        //Tilemap tilemap = GetComponent<Tilemap>();
        tilemap.SetTiles(positions, tileArray);
        MakeTile();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void MakeTile() {
        Vector3Int tilePosition = new Vector3Int(Mathf.RoundToInt(SpawnPosition.position.x), Mathf.RoundToInt(SpawnPosition.position.y), Mathf.RoundToInt(SpawnPosition.position.z));

        tilemap.SetTile(tilePosition, tile);
    }

}
