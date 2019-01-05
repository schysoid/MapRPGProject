using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour {
    public DadCell[] dadCells;
    private GameManager gameManager;
    public Dictionary<string,int> Inventory;
    private string[] keys;
    private int[] values;
    private StackGroup stackGroup;
    public enum InvType
    {
        chest,
        npc,
        player
    }
    public InvType InventoryType;

    private void Awake()
    {
        GameObject gameManagerObj = GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
        if (InventoryType==InvType.player)
        {
            Inventory = gameManager.invPlayerDict;
        }
 
    }
    // Use this for initialization
    void Start()
    {
        stackGroup = GetComponentInParent<StackGroup>();
        //GetInv();
        //ClearInv();
        //SetInv();
    }

    private void printInv() {
        keys = Inventory.Keys.ToArray();
        values = Inventory.Values.ToArray();
        print("Player Inventory in Game manager ============");
        for (int i = 0; i < Inventory.Count; i++)
        {
            print(keys[i] + ": " + keys[i] + " , " + values[i]);
        }
    }

    private void printDadCells()
    {
        print("Dad Cells Content =============");
        for (int i = 0; i < dadCells.Length; i++)
        {
            DadItem dadItem = dadCells[i].GetComponentInChildren<DadItem>();

            string myItem = dadItem != null ? dadItem.name : "";
            StackItem stackItem = dadCells[i].GetComponentInChildren<StackItem>();

            print(i + ": " + dadCells[i] + " , " + myItem+" , "+ stackItem);
        }
    }

    public void GetInv()
    {
        print("GetInv() ==========");

        Inventory.Clear();

        for (int i = 0; i < dadCells.Length; i++)
        {
            GameObject item = null;
            int stack = 0;
            DadItem dadItem = dadCells[i].GetComponentInChildren<DadItem>();
            StackItem stackItem = dadCells[i].GetComponentInChildren<StackItem>();
            if (dadItem != null)
            {
                item = dadItem.gameObject;
                stack = stackItem.GetStack();
                if (Inventory.Keys.Contains(item.name))
                {
                    Inventory.Add(item.name, stack++); // SEB : Adding item to player inventory
                }
                Inventory.Add(item.name, stack); // SEB : Adding item to player inventory
            }
        }
    }

    public void ClearInv() {
        print("ClearInv() ==========");
        printDadCells();
        for (int i = 0; i < dadCells.Length; i++)
        {
            int stack = 0;
            StackCell stackCell = dadCells[i].GetComponentInParent<StackCell>();
            DadItem dadItem = dadCells[i].GetComponentInChildren<DadItem>();
            StackItem stackItem = dadCells[i].GetComponentInChildren<StackItem>();
            if (dadItem != null)
            {
                stack = stackItem.GetStack();
                print(i+" : removing in ClearInv() , "+stackItem+stack);

                stackGroup.RemoveItem(stackItem, stack);
            
                print(i + " : removed in ClearInv() , " + stackItem);

            }
        }
    }

    public void SetInv() {
        print("SetInv() ==========");
        if (Inventory!=null)
        {
            ClearInv();
        keys = Inventory.Keys.ToArray();
        values = Inventory.Values.ToArray();
        for (int i = 0; i < keys.Length; i++)
        {
            int stack = 0;
            stack = values[i];
            for (int j = 0; j < stack; j++)
            {
                GameObject instance = Instantiate((GameObject)Resources.Load("Items/" + keys[i]));
                StackItem stackItem = instance.GetComponentInChildren<StackItem>();
                stackGroup.AddItem(instance.GetComponent<StackItem>(), 1);
            }
            print(i+" : "+ keys[i] + " , " + values[i]+" ,"+ stack);
        }
        }
    }
}
