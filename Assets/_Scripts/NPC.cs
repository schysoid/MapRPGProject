using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// RECEIVES THE NPC ID AND SET THE ASSOCIATED VISUAL WITHIN THE ENCOUNTER
// ADDS ITSELF TO ENCOUNTER GENERATOR'S LIST OF ACTIVE NPCs
public class NPC : MonoBehaviour {
    public string npcType;  // "Adventurer" , "Monster"
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    public int npcUniqueID = 0;
    private EncounterGenerator encounterGenerator;


    private void Awake()
    {
        if (SceneManager.GetActiveScene().name=="EncounterPlace")
        {
            GameObject encounterGeneratorObj = GameObject.Find("EncounterGenerator");
            encounterGenerator = encounterGeneratorObj.GetComponent<EncounterGenerator>();
            encounterGenerator.EncounterNpcs.Add(gameObject);
            encounterGenerator.EncounterNpcArray[0] = gameObject;

        }
    }
    public void GetNpcId(int id) {
        id = npcUniqueID;
    }

    public void SetNPCAdventurerId(int id) {
        npcUniqueID = id;
        spriteRenderer.sprite = sprites[npcUniqueID];
        print("NPC UNIQUE ID : " + npcUniqueID);

    }

    public void SetNPCMonsterId(int id)
    {
        npcUniqueID = id;
        print("NPC UNIQUE ID : "+ npcUniqueID);
        spriteRenderer.sprite = sprites[npcUniqueID-100];
    }
}
