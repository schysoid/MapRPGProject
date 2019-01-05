using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SETS THE VISUAL OF THE SIGN POST ACCORDING TO THE ID. GETS SET BY EncounterGenerator
public class DungeonSingPost : MonoBehaviour {
    public EncounterGenerator encounterGenerator;
    private GameManager gameManager;
    public int monsterID;
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    // Use this for initialization
    void Start() {
        GameObject gameManagerObj = GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
           }

    public void SetSignPost(int monsterId) {
        monsterID = monsterId;
        spriteRenderer.sprite = sprites[monsterID - 100];// we added 100 to monsters to dinstinguish them from adventurer ids

    }
}
