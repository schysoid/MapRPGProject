using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;


public class TriggerEncounter : MonoBehaviour
{
    //public string sceneName;
    //public Collider2D collider;
    private GameManager gameManager;
    private float lastTime;
    private float cooldown = 3f;
    public float minEncounterTimer = 2f;
    public float maxEncounterTimer = 10f;
    public int percentChancesOfGoinInEncounter = 20;

    // Use this for initialization
    void Start()
    {
        GameObject gameManagerObj = GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCoolDownOver() == true && gameManager.playerIsMoving == true)
        {
            System.Random pseudoRandom = new System.Random();
            int rand = pseudoRandom.Next(0, 100);
            if (rand >= (100 - percentChancesOfGoinInEncounter)) // if rand draw 
            {
                ForceTriggerEncounter();
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            print("A key was pressed");
            ForceTriggerEncounter();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            print("M key was pressed");
            ForceTriggerEncounter();
        }
    }

    private bool IsCoolDownOver()
    {
        if (Time.time >= lastTime + cooldown)
        {
            cooldown = Random.Range(minEncounterTimer, maxEncounterTimer);
            lastTime = Time.time;

            return true;
        }
        else
        {
            return false;
        }
    }

    private void RayCastForCollisions()
    {
       
            RaycastHit2D[] hits;
            Vector2 point = new Vector2(transform.position.x, transform.position.y);
            hits = Physics2D.RaycastAll(point, Vector2.zero); // check raycast

            if (hits.Length > 0) // if got something from raycast
            {
                gameManager.EncounterType = null;
                foreach (RaycastHit2D hit in hits)
                {

                    GameObject recipient = hit.transform.gameObject;
                    Tilemap tilemap = recipient.GetComponent<Tilemap>();
                    //     TileData tileData = new TileData();
                    // Tilemap tilemap2 = tilemap.iTilemap.GetComponent<Tilemap>();
                    Vector3Int vector3Int = new Vector3Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
                    //    TileData tileData2 =  tilemap.GetTile(vector3Int).GetTileData(vector3Int,tilemap,ref tileData);
                    Sprite sprite = tilemap.GetSprite(vector3Int);
                    print(sprite.name);

                    //   print(tilemap);
                    if (recipient.name == "LandTilemap" && gameManager.EncounterType == null)
                        gameManager.EncounterType = "plains";

                    if (recipient.name == "LandTilemap" && gameManager.EncounterType == "plains" && sprite.name != "Tileset32x32_19")
                    {
                        gameManager.EncounterType = "beach";
                        print("beach!! BIATCH");
                    }

                    if (recipient.name == "LandTilemap" && gameManager.EncounterType == "forest")
                        gameManager.EncounterType = "forest";

                    if (recipient.name == "LandTilemap" && gameManager.EncounterType == "mountains")
                        gameManager.EncounterType = "mountains";


                    if (recipient.name == "ForestTilemap" && gameManager.EncounterType == null)
                        gameManager.EncounterType = "forest";

                    if (recipient.name == "ForestTilemap" && gameManager.EncounterType == "plains")
                        gameManager.EncounterType = "forest";

                    if (recipient.name == "ForestTilemap" && gameManager.EncounterType == "mountains")
                        gameManager.EncounterType = "mountains";

                    if (recipient.name == "MountainsTilemap")
                        gameManager.EncounterType = "mountains";


                }
            gameManager.playerPosition = transform.position; // save player position on map
            SceneManager.LoadScene("EncounterPlace", LoadSceneMode.Single);
        }
    }
    private void ForceTriggerEncounter() {
        RayCastForCollisions();

    }
    }
