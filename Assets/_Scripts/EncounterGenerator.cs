using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TELLS EncounterTypeSelector WHAT TYPE TO LOAD RANDOMLY (ADVENTURER, MONSTER...)
// IF ITS AN ENCOUNTER WITH NPC THEY ARE SET UP AND ALL ACCEPT TO GO TO LAST DUNGEON
// IF ITS AN ENCOUNTER WITH MONSTER IT IS SET UP AND HE GOES TO LAST DUNGEON

public class EncounterGenerator : MonoBehaviour {
    private GameManager gameManager;
    private int objSpawnSeed;
    public List<GameObject> EncounterNpcs = new List<GameObject>(); // if not a dungeon, whos there? the npcs add themselves to this array on awake
    public GameObject[] EncounterNpcArray;
    public string encounterPlace;
    public GameObject mountainsLevel;
    public GameObject plainsLevel;
    public GameObject forestLevel;
    public GameObject beachLevel; // beach not implemented in map yet
    private int npcID;
    private bool npcOnQuest;
    private string flaggedDungeon;
    public int ChancesOfTypeMonster = 50; //%
    public string encounterType;
    public EncounterType encounterTypeScript; // Referenced by the encounterTypeScript itself on Start

    // Use this for initialization
    void Start() {
        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
        gameManager.objSpawnSeed += 1;
        encounterPlace = gameManager.EncounterType;
        flaggedDungeon = gameManager.flaggedDungeon;
        EncounterNpcArray= new GameObject[1];


        if (encounterPlace == "mountains")
        {
            mountainsLevel.SetActive(true);
            SetEncounterType();
      //          AllNpcAcceptQuests();
        }
        if (encounterPlace == "forest")
        {
            forestLevel.SetActive(true);
            SetEncounterType();
           //     AllNpcAcceptQuests();
        }
        if (encounterPlace == "plains")
        {
            plainsLevel.SetActive(true);
            SetEncounterType();
            //     AllNpcAcceptQuests();
        }
        if (encounterPlace == "beach")
        {
            beachLevel.SetActive(true);
            SetEncounterType();
            //     AllNpcAcceptQuests();
        }

    }

    void SetEncounterType() {
        int rand = Random.Range(1, 100);
        if (rand>=100-ChancesOfTypeMonster)
        {
            encounterTypeScript.SetEncounterType(1);
            encounterType = "Monster";
            SetNPCMonster();
        }
        else
        {
            encounterTypeScript.SetEncounterType(0);
            encounterType = "Adventurer";
            SetNPCAdventurer();
        }
    }

    void SetNPCAdventurer() {
        for (int i = 0; i < EncounterNpcArray.Length; i++)
        {
            NPC myNpcScript = EncounterNpcArray[i].GetComponent<NPC>();
            System.Random pseudoRandom = new System.Random(GetHashCode());
            int rand = pseudoRandom.Next(1, myNpcScript.sprites.Length);
            myNpcScript.SetNPCAdventurerId(rand);
        }
    }

    void SetNPCMonster()
    {
        for (int i = 0; i < EncounterNpcArray.Length; i++)
        {
            NPC myNpcScript = EncounterNpcArray[i].GetComponent<NPC>();
            System.Random pseudoRandom = new System.Random(GetHashCode());
            int rand = pseudoRandom.Next(1, myNpcScript.sprites.Length);
            myNpcScript.SetNPCMonsterId(rand + 100);
        }
    }

    public void AllNpcAcceptQuests() {

        for (int i = 0; i < EncounterNpcArray.Length; i++)
        {
            NpcAcceptsQuest(EncounterNpcArray[i].GetComponent<NPC>().npcUniqueID);
        }
 
    }
   

   public void NpcAcceptsQuest(int npcID) {
        int dungeonToGoID  = System.Array.IndexOf(gameManager.spawnedDungeons, flaggedDungeon);

        if (dungeonToGoID >= 0)
        {
            if (encounterType == "Monster" && dungeonToGoID >= 0)// if I have a last visited dungeon
            {
                gameManager.MonstersDungeonsIds[dungeonToGoID] = npcID; // if its a monster add it to the unique npc place id corresponding to the last loaded dungeon
                if (gameManager.dungeonsState[dungeonToGoID] == 3)
                    gameManager.dungeonsState[dungeonToGoID] = 2;

                    print("Monster " + gameManager.MonstersDungeonsIds[dungeonToGoID] + " went to " + gameManager.flaggedDungeon);

            }
            if (encounterType == "Adventurer")
            {
                for (int i = 0; i < gameManager.DungeonsQueues.Length; i++)
                {
                    if (gameManager.DungeonsQueues[dungeonToGoID][i] == 0) // find the last place in queue
                    {
                        gameManager.DungeonsQueues[dungeonToGoID][i] = npcID;
                        print("NPC " + gameManager.DungeonsQueues[dungeonToGoID][i] + " at " + i + " took the quest for " + gameManager.flaggedDungeon);
                        break;
                    }
                }
            }

        }
        else
        {
            print("No dungeon to go to " + dungeonToGoID);
        }
    }

   
  

  


}
