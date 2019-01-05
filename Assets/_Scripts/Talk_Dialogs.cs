using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//We might change the language in this script, so let's use the localization
using RPGTALK.Localization;
using UnityEngine.SceneManagement;

public class Talk_Dialogs : MonoBehaviour
{

    public int lineMin;
    public int lineMax;
    private string seed;
    private EncounterGenerator encounterGenerator;
    private int npcID;

    //A few variables to move/animate this guy
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer render;
    GameManager gameManager;

    

    //We will sometimes initialize the talk by script, so let's keep a instance of the current RPGTalk
    public RPGTalk rpgTalk;

   

    // Get the right references...
    void Start()
    {
        GameObject gameManagerObj = GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
        seed = gameManager.seed;
        
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        //In the tagsDemo scene, we want to do something when we make a choice...
        rpgTalk.OnMadeChoice += OnMadeChoice;
        if (SceneManager.GetActiveScene().name == "EncounterPlace")
        {
            GameObject encounterGeneratorObj = GameObject.Find("EncounterGenerator");
            encounterGenerator = encounterGeneratorObj.GetComponent<EncounterGenerator>();
        }
        }

        // Update is called once per frame
        void Update()
    {

        //skip the Talk to the end if the player hit Return
        if (Input.GetKeyDown(KeyCode.Return))
        {
            rpgTalk.EndTalk();
        }


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


    public void PickRandomLine()
    {
        CancelControls();
        System.Random pseudoRandom = new System.Random(Time.time.ToString().GetHashCode());
        int rand = pseudoRandom.Next(lineMin, lineMax);
        string line = rand.ToString();
        rpgTalk.NewTalk(line, line, rpgTalk.txtToParse, this, "GiveBackControls");
    }

    //In the  scene, when we make a choice let's find out what we chose
    void OnMadeChoice(int questionId, int choiceID)
    {
        if (choiceID == 0)
        {
            print("Choice 0");
            rpgTalk.NewTalk("EncounterTextStart_1a", "EncounterTextEnd_1a", rpgTalk.txtToParse, this, "GiveBackControls");
            encounterGenerator.NpcAcceptsQuest(1);//

        }
        else if (choiceID == 1)
        {
            print("Choice 1");
            rpgTalk.NewTalk("EncounterTextStart_1b", "EncounterTextEnd_1b", rpgTalk.txtToParse, this, "GiveBackControls");

        }
        else
        {
            print("Choice does not exist");
        }
    }

}
