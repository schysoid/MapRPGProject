using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;


// CEATES THE WORLD 
// SPAWNS RAND. DUNGEONS ON LAND AND ADD THEM TO THE PLACES ARRAY
// SPAWNS RAND. ANY OBJECT GIVEN AND ADD THEM TO THE OBJECTS ARRAY
// SPAWNS RAND. THE PLAYER ON LAND
public class MapGenerator : MonoBehaviour
{
    private GameManager gameManager;
    public int width;
    public int height;
    private string seed;
    private int mapsSeedIncr; // used to ensure forest and mountains are different
    public bool useRandomSeed;
    int objSpawnSeed;//used to spawn randomly the objects
    public Tilemap WaterBackground;
    public Tilemap WaterTilemap;
    public Tilemap LandTilemap;
    public Tilemap MountainTilemap;
    public Tilemap ForestTilemap;
    public Tilemap FogOfWarTilemap;
    public TileBase EmptyTile;
    public TileBase BackgroundTile;
    public TileBase WaterTile;
    public TileBase LandTile;
    public TileBase MountainTile;
    public TileBase ForestTile;
    public TileBase FogOfWarTile;
    [Range(0, 100)]
    public int randomFillPercentLand;
    public int randomFillPercentMountain;
    public int randomFillPercentForest;
    private int[,] map1;//generated land and water
    private int[,] map2;//generated temp for overlay
    private int[,] map3;//merged for mountains
    private int[,] map4;//merged for land
    private Vector3[] tilesCoordArray;
    private List<Vector3> tilesCoordList = new List<Vector3>(); //the vector3 of each land tiles
    private GameObject[] SpawnedObjArray;
    private List<GameObject> SpawnedObjects = new List<GameObject>();
    public GameObject[] ObjectsToSpawn;
    public GameObject placesPrefab;
    public int placesQty = 10;
    private int dungeonsQty;
    public string[] dungeonsNamesArray;
    public int[] dungeonsStates;

    public GameObject PlayerToSpawn;

    void Awake()
    {
        GameObject gameManagerObj = GameObject.Find("GameManager");
        gameManager  = gameManagerObj.GetComponent<GameManager>();
        seed = gameManager.seed;
        
        mapsSeedIncr = 0;
        objSpawnSeed = seed.GetHashCode();
        map1 = new int[width, height];
        GenerateMap(map1, randomFillPercentLand);
        map2 = new int[width, height];
        GenerateMap(map2, randomFillPercentMountain);
        map3 = new int[width, height];
        map4 = new int[width, height];
        dungeonsQty = gameManager.dungeonsQty;
        
       
    }

    private void Start()
    {
       
        // Debug.Log(gameManager.isFirstLoad);
        Reinitialize();
        
    }



    private void Reinitialize() {
        WaterBackground.ClearAllTiles();
        WaterTilemap.ClearAllTiles();
        LandTilemap.ClearAllTiles();
        MountainTilemap.ClearAllTiles();
        ForestTilemap.ClearAllTiles();
        FogOfWarTilemap.ClearAllTiles();
        SpawnedObjArray = SpawnedObjects.ToArray();
        for (int i = 0; i < SpawnedObjArray.Length; i++)
        {
            Destroy(SpawnedObjArray[i]);
        }
        
        GenerateWorld();
    }

    private void GenerateWorld()
    {
        GenerateMap(map1, randomFillPercentLand);
        DrawTilemap(map1, BackgroundTile, EmptyTile, WaterBackground); //Draw Background Water
        DrawTilemap(map1, WaterTile, LandTile, LandTilemap); //Draw land
        GenerateMap(map2, randomFillPercentMountain);
        MergeMaps(map1, map2, map3);
        DrawTilemap(map3, EmptyTile, MountainTile, MountainTilemap); //Draw mountains
        GenerateMap(map2, randomFillPercentForest);
        MergeMaps(map1, map2, map4);
        DrawTilemap(map4, EmptyTile, ForestTile, ForestTilemap); //Draw forest
        DrawTilemap(map1, FogOfWarTile, FogOfWarTile, FogOfWarTilemap); //Draw Fog Of War

        //SPAWN STUFF
        for (int i = 0; i < ObjectsToSpawn.Length; i++)
        {
            if (ObjectsToSpawn != null)
            {
                string objName = "SpawnedObj" + i + "_" + ObjectsToSpawn[i].name;
                SpawnObjectsRandomely(ObjectsToSpawn[i], objName);
                gameManager.spawnedObjects[i] = objName;
            }
        }
        for (int i = 0; i < placesQty; i++)
        {
            if (placesPrefab != null)
            {
                string objName = "Dungeon" + i;
                if (gameManager.dungeonsState[i] == 2 && gameManager.spawnedDungeons[i] != gameManager.flaggedDungeon)
                {
                    gameManager.dungeonsState[i] = 1;//switch the last active dungeon to flagged
                }
                SpawnObjectsRandomely(placesPrefab, objName);
                gameManager.spawnedDungeons[i] = objName;

                
            }
            SpawnPlayer(PlayerToSpawn);
        }
    }
     void GenerateMap(int [,] map, int randomFillPercent)
    {
        RandomFillMap(map, randomFillPercent);
        for (int i = 0; i < 5; i++)
        {
            SmoothMap(map);
        }
    }

    void RandomFillMap(int [,] map, int randomFillPercent)
    {
        if (useRandomSeed )
        {
            mapsSeedIncr = Convert.ToInt32(seed) + 1; ;
            // seed = Time.time.ToString();
            seed = mapsSeedIncr.ToString();
            //gameManager.seed = seed;
        }

        System.Random pseudoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
                }
            }
        }

    }

    void SmoothMap(int[,] map)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y,map);

                if (neighbourWallTiles > 4)
                {
                map[x, y] = 1;
                }                               
                else if (neighbourWallTiles < 4)
                { 
                        map[x, y] = 0;
                }
            }
        }
    }

    int GetSurroundingWallCount(int gridX, int gridY, int[,] map)
    {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        wallCount += map[neighbourX, neighbourY];
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }

        return wallCount;
    }


    void DrawTilemap(int[,] map,TileBase tileA, TileBase tileB, Tilemap tilemap) {
       
           // int i = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    TileBase tile;
                    Vector3 pos = new Vector3(-width / 2 + x, -height / 2 + y, 0);
                    Vector3Int tilePosition = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));

                    if (map[x, y] == 1)
                    {
                        tile = tileA;
                    }
                    else
                    {
                        tile = tileB;
                        tilesCoordList.Add(pos);
                        
                    }
                      
                        tilemap.SetTile(tilePosition, tile);
                }
            }
        
    }

    void MergeMaps(int [,] mapA, int[,] mapB, int[,] mapC) {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                
                if (mapA[x, y] == 0 && mapB[x, y] == 0)
                {
            mapC[x, y] = 0;
        }else
                {
            mapC[x, y] = 1;

        }
              
               

            }
        }
    }


    void SpawnObjectsRandomely(GameObject objectToSpawn, string objName) {
        Vector3 pos = new Vector3(0, 0, 0);
        tilesCoordArray = tilesCoordList.ToArray();
        objSpawnSeed += 1;
        System.Random pseudoRandom = new System.Random(objSpawnSeed);
        int rand = pseudoRandom.Next(0, tilesCoordArray.Length - 1);
        pos.x = Mathf.RoundToInt(tilesCoordArray[rand].x) + .5f;
        pos.y = Mathf.RoundToInt(tilesCoordArray[rand].y) + .5f;
        GameObject spawnedObj;
        spawnedObj = Instantiate(objectToSpawn, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
        spawnedObj.name = objName;
        SpawnedObjects.Add(spawnedObj);

    }

    void SpawnPlayer(GameObject objectToSpawn)
    {
       // Debug.Log(gameManager.isFirstLoad);
        Vector3 pos = new Vector3(0, 0, 0);
        if (gameManager.isFirstLoad)
        {
            tilesCoordArray = tilesCoordList.ToArray();
            gameManager.isFirstLoad = false;
            System.Random pseudoRandom = new System.Random(objSpawnSeed);
            int rand = pseudoRandom.Next(0, tilesCoordArray.Length - 1);
           // Debug.Log(tilesCoordArray[rand]);
            pos.x = Mathf.RoundToInt(tilesCoordArray[rand].x) + .5f;
            pos.y = Mathf.RoundToInt(tilesCoordArray[rand].y) + .5f;
            objectToSpawn.transform.position = new Vector3(pos.x, pos.y, 0);
        }
        else
        {
            pos.x = gameManager.playerPosition.x;
            pos.y = gameManager.playerPosition.y;
            objectToSpawn.transform.position = new Vector3(pos.x, pos.y, 0);

        }



    }

}