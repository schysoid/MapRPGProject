using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//SETS THE DUNGEON STATE ON THE MAP
public class DungeonState : MonoBehaviour {
    public GameObject[] dungeonVisuals;
    public string dungeonName;
    public int dungeonId;
    private GameManager gameManager;
    public int dungeonState;
    // Use this for initialization
    void Start ()
    {
        GameObject gameManagerObj = GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();

        for (int i = 0; i < gameManager.spawnedDungeons.Length; i++)
        {
            if (gameManager.spawnedDungeons[i] == gameObject.name)
            {
                dungeonState = gameManager.dungeonsState[i];
            }
        }
       
      
        for (int i = 0; i < dungeonVisuals.Length; i++)
        {
            dungeonVisuals[i].SetActive(false);
        }
            dungeonVisuals[dungeonState].SetActive(true);
       
    }
	
	
}
