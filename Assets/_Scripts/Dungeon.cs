using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// KNOWS THE DUNGEON NAME & ID
// THE QUEUE IS POPULATED WITH NPCS
// THE MONSTER SIGNPOST IS POPULATED
// CHECKS IF RUINED, ACTIVE, FLAGGED ACTIVE OR RUINED ACTIVE AND TURNS ON THE RIGHT GAMEOBJECT

public class Dungeon : MonoBehaviour {
    private GameManager gameManager;
    private int objSpawnSeed;
    public GameObject[] dungeonNpcQueue; // references to NPCs in the editor
    public DungeonSingPost singPost;
    public GameObject dungeonActiveLevel;
    public GameObject dungeonInactiveLevel;
    public string dungeonName;
    private string[] dungeonsArray;
    public int dungeonID;
    private bool npcOnQuest;
    private string flaggedDungeon;
    private Component[] pillars;

    // Use this for initialization
    void Start() {
        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
        gameManager.objSpawnSeed += 1; // ???? what does that do?

        flaggedDungeon = gameManager.flaggedDungeon;
        dungeonName = gameManager.loadedDungeon; // when loading a dungeon ask gameManager which one it is
        dungeonID = System.Array.IndexOf(gameManager.spawnedDungeons, dungeonName);
        print("dungeonID " + dungeonID);

        for (int i = 0; i < gameManager.spawnedDungeons.Length; i++)
        {
            if (gameManager.spawnedDungeons[i] == dungeonName)
            {
                if (gameManager.dungeonsState[i] == 1 || gameManager.dungeonsState[i] == 2)
                {
                    dungeonInactiveLevel.SetActive(false);
                    dungeonActiveLevel.SetActive(true);// Active Dungeon
                }
                else if (gameManager.dungeonsState[i] == 0 || gameManager.dungeonsState[i] == 3)
                {
                    dungeonActiveLevel.SetActive(false);
                    dungeonInactiveLevel.SetActive(true); //Ruined Dungeon
                }
            }
        }

        PopulateDungeonQueue();
            SetSignPost();
     }

   

    void SetSignPost() {

        if (gameManager.MonstersDungeonsIds[dungeonID] > 0)
        {
            singPost.SetSignPost(gameManager.MonstersDungeonsIds[dungeonID]);
        }
        else {
            print(" Dungeon does not have a Monster ID "+gameManager.MonstersDungeonsIds[dungeonID]);
        }
    }

    // When in an active dungeon
    void PopulateDungeonQueue() {
        if (gameManager.DungeonsQueues[dungeonID][0] > 0)
        {
            for (int i = 0; i < gameManager.DungeonsQueues[dungeonID].Length; i++)
            {
                int npcID = gameManager.DungeonsQueues[dungeonID][i];
                if (npcID == 0)
                {
                    dungeonNpcQueue[i].SetActive(false);
                }
                else
                {
                    print("Adding to queue : NPC " + dungeonNpcQueue[i]);
                    dungeonNpcQueue[i].SetActive(true);
                    dungeonNpcQueue[i].GetComponent<NPC>().SetNPCAdventurerId(npcID);
                }
            }
        }
        else
        {
            for (int i = 0; i < gameManager.DungeonsQueues[dungeonID].Length; i++)
            {
                dungeonNpcQueue[i].SetActive(false);
            }
            print("Dungeon queue is empty");
        }
    }

  


}
