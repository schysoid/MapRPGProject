using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TradeManager : MonoBehaviour
{
    GameManager gameManager;
    private string seed;
    public GameObject tradeUI;
    public Canvas TradeUICanvas;
    public RPGTalk rpgTalk;
    public InventoryManager playerInventoryManager;
    public InventoryManager OtherInventoryManager;
  
     
    // Use this for initialization
    void Start () {
        GameObject gameManagerObj = GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
        seed = gameManager.seed;
        
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

    public void StartConversation() {
        CancelControls();
        rpgTalk.NewTalk("2", "2", rpgTalk.txtToParse, this, "InitiateTrade");
    }
    public void InitiateTrade() {
        tradeUI.SetActive(true);
        //playerInventoryManager.ClearInv();
        StackGroup stackGroup = playerInventoryManager.GetComponentInParent<StackGroup>();
        print(stackGroup);
        if (stackGroup!=null)
        {
            playerInventoryManager.SetInv();

        }

    }

    public void EndTrade() {
        GiveBackControls();
    
        playerInventoryManager.GetInv();
        tradeUI.SetActive(false);
    }

    
}
