using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// CHECKS FOR COLLISION WITH PLAYER 
// LOADS MAP OR ENCOUNTER BASED ON CURRENT SCENE
public class LoadScene : MonoBehaviour {
    public string sceneName;
    private GameObject player;
    //public Collider2D collider;
    private GameManager gameManager;
    private string currentScene;
    public string typeIfEncounter = "dungeon";
    public float objSize = 1f;


    // Use this for initialization
    void Start () {
        GameObject gameManagerObj = GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
        player = GameObject.FindWithTag("Player");
        currentScene = SceneManager.GetActiveScene().name;
    }
    void LoadNewScene() {
        if (currentScene != "Map") // we are returning to the map
        {
                gameManager.isRespawnig = true;
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        if (currentScene == "Map") //we are entering a dungeon
        {
            if (gameManager.isRespawnig == false)// if we did not just respawn on the dungeon
            {
                    gameManager.playerPosition = player.transform.position;
                    gameManager.EncounterType = typeIfEncounter;
                    if (typeIfEncounter == "dungeon")
                    {
                        gameManager.loadedDungeon = transform.parent.gameObject.name; //tell Game manager which dungeon this is
                    }
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
        }
    }

    
      private void OnTriggerStay2D(Collider2D collision)
    {
        LoadNewScene();

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameManager.isRespawnig = false;
    }
  

}
