using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SETS IF ADVENTURER, MONSTER, ETC. INSIDE THE ENCOUNTER SCENE 
public class EncounterType : MonoBehaviour {
    
    public GameObject[] Encounters;
    public EncounterGenerator encounterGenerator; // known by encounterGenerator

    private void Awake()
    {
        encounterGenerator.encounterTypeScript = this;
    }


    public void SetEncounterType(int encounterIndex) {
        for (int i = 0; i < Encounters.Length; i++)
        {
            Encounters[i].SetActive(false);
        }
        Encounters[encounterIndex].SetActive(true);
    }
}
