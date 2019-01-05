using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class PowerUpWordFinder : MonoBehaviour {
    public Text rpgTalkText;
    public RPGTalk rpgTalk;
    private GameManager gameManager;
    public int[] powerUpWordsCountArray;
    private int[] tempWordCountArray;
    string textString;
    string lowerTextString;

    // Use this for initialization
    void Start () {
        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
     
        rpgTalk.OnEndTalk += OnEndTalk;
        powerUpWordsCountArray = new int[gameManager.PowerUpWordsArray.Length];
        tempWordCountArray = new int[gameManager.PowerUpWordsArray.Length];
}


    void OnEndTalk()
    {
        print("text = " + rpgTalkText.text);
         textString = rpgTalkText.text;
         lowerTextString = rpgTalkText.text.ToLower();
        for (int i = 0; i < gameManager.PowerUpWordsArray.Length; i++)
        {
            string word = gameManager.PowerUpWordsArray[i];
            int wordCount = Regex.Matches(lowerTextString, (word)).Count;
            if (wordCount > 0)
            {
                tempWordCountArray[i] = wordCount;
                print("count = " + tempWordCountArray[i]);
            }
        }
      //  CheckForPowerUpWord();
       // powerUpWordsCountArray = tempWordCountArray;
    }
    public int WordCount(string s, string word)
    {
        return (s.Length - s.Replace(word, "").Length) / word.Length;
    }

    public void CheckForPowerUpWord()
    {

       
    }


}
