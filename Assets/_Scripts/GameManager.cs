using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// GLOBAL DATA HOLDER

public class GameManager : MonoBehaviour {
    public bool isFirstLoad = true;//first time the game generates the map
    public Vector3 playerPosition;//where the player was before changing scene
    public bool isRespawnig = false;//is the player coming back from an encounter? if yes he should respawn where he left
    public string seed;//random seed to generate stuff
    public MapGenerator mapGeneratorRef;
    private string mapGeneratorName;
    private MapGenerator mapGenerator;
    public bool controls;//player has control?
    public int objSpawnSeed;
    public Dictionary<string, int> invPlayerDict = new Dictionary<string, int>();//the player inventory
    public GameObject[] PlayerStartInventory;
    public string EncounterType;
    public bool playerIsMoving;
    public string[] spawnedObjects;
    public string[] spawnedDungeons;
    public int[] dungeonsState; // 0 destroyed, 1 fixed, 2 active flagged, 3 destroyed flagged dungeon
    public string loadedDungeon;
    public string flaggedDungeon;
    public int[][] DungeonsQueues;
    public int dungeonsQty = 10;
    public int[] MonstersDungeonsIds; // where the monsters are
    public string[] PowerUpWordsArray;
    public int[] PowerUpWordsCountArray;

    private void Awake()
    {
        spawnedDungeons = new string[dungeonsQty];
        spawnedObjects = new string[10];
        dungeonsState = new int[dungeonsQty];
        MonstersDungeonsIds = new int[dungeonsQty];
        DungeonsQueues = new int[dungeonsQty][];
        for (int i = 0; i < dungeonsQty; i++)
        {
            DungeonsQueues[i] = new int[4];
        }
        PowerUpWordsArray = new string[20];
        PowerUpWordsArray[0] = "WORD";
        PowerUpWordsArray[1] = "POWER";
        PowerUpWordsArray[2] = "FOUND";
        PowerUpWordsCountArray = new int[PowerUpWordsArray.Length];
    }
    // Use this for initialization
    void Start () {
        if (mapGeneratorRef != null)
        {
            mapGeneratorName = mapGeneratorRef.gameObject.name;
        }
        else if(mapGeneratorName!=null)
        {
            GameObject mapGeneratorObj = GameObject.Find("mapGeneratorName");
            mapGenerator = mapGeneratorObj.GetComponent<MapGenerator>();
            mapGeneratorRef = mapGenerator;
        }

        
      

        
        //Fill player inv.
        for (int i = 0; i < PlayerStartInventory.Length; i++)
        {
            if (PlayerStartInventory[i] != null)
            {
                if (invPlayerDict.Keys.Contains(PlayerStartInventory[i].name))
                {
                   
                    invPlayerDict[PlayerStartInventory[i].name] = invPlayerDict[PlayerStartInventory[i].name]+=1; // SEB : Changing the item quantity 
                }
                else
                {
                    invPlayerDict.Add(PlayerStartInventory[i].name, 1); // SEB : Adding item to player inventory
                }


        
            }

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
