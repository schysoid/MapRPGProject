using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SETS UP THE PILLAR IN THE RIGHT STATE ON START
// CHANGES THE PILLAR STATE AND DUNGONE STATE ON PLAYER INPUT ON PILLAR

public class ActivatePillar : MonoBehaviour {
    private GameManager gameManager;
    public RPGTalk rpgTalk;
    private Dungeon dungeon;
    private int dungeonID;
    public SpriteRenderer EvilSkull;

    // Use this for initialization
    void Start () {
        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
        GameObject dungeonObj = GameObject.Find("DungeonManager");
        dungeon = dungeonObj.GetComponent<Dungeon>();
        dungeonID = dungeon.dungeonID;

        SetPillarState();
    }

    //the player cant move
    public void CancelControls()
    {
        gameManager.controls = false;
    }

    //give back the controls to player
    public void GiveBackControls()
    {
        gameManager.controls = true;
    }

    public void StartConversation()
    {
      //  CancelControls();
        SwitchPillarState();
    }

    public void EndConversation()
    {
       // GiveBackControls();

    }

    public void SetPillarState() {
        int dungeonState = gameManager.dungeonsState[dungeonID];

        if (dungeonState == 0 || dungeonState == 1)
            SetEvilSkullActive(false);
        if (dungeonState == 2 || dungeonState == 3)
            SetEvilSkullActive(true);
    }

    public void  SwitchPillarState() {
        int dungeonState = gameManager.dungeonsState[dungeonID];

        if (dungeonState == 0 || dungeonState == 1)
            rpgTalk.NewTalk("1", "2", rpgTalk.txtToParse, this, "FlagDungeonWithEvilWand");
        if (dungeonState == 2 || dungeonState == 3)
            rpgTalk.NewTalk("3", "3", rpgTalk.txtToParse, this, "UnflagDungeonWithEvilWand");
    }

    public void SetEvilSkullActive(bool state) {
        EvilSkull.enabled = state;
        print("SetEvilSkullActive " +state);
    }

    public void FlagDungeonWithEvilWand()
    {
        print(gameManager.dungeonsState.Length);

        int dungeonState;
        for (int i = 0; i < gameManager.dungeonsState.Length; i++)
        {
            dungeonState = gameManager.dungeonsState[i];
            if (dungeonState == 2)
                gameManager.dungeonsState[i] = 1;
            if (dungeonState == 3)
                gameManager.dungeonsState[i] = 0;
        }
        dungeonState = gameManager.dungeonsState[dungeonID];

        if (dungeonState == 1)
            gameManager.dungeonsState[dungeonID] = 2;
        if (dungeonState == 0)
            gameManager.dungeonsState[dungeonID] = 3;
        gameManager.flaggedDungeon = dungeon.dungeonName; // remember this is the last dungeon that was loaded to send adventurers there


        SetEvilSkullActive(true);
        print("gameManager.dungeonsState[dungeonID] " + gameManager.dungeonsState[dungeonID]);
    }

    public void UnflagDungeonWithEvilWand() {
        int dungeonState = gameManager.dungeonsState[dungeonID];
        if (dungeonState == 2)
            gameManager.dungeonsState[dungeonID] = 1;
        if (dungeonState == 3)
            gameManager.dungeonsState[dungeonID] = 0;

        SetEvilSkullActive(false);
        print("gameManager.dungeonsState[dungeonID] " + gameManager.dungeonsState[dungeonID]);
    }
}
