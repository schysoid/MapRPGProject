using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PowerUpProphecy : MonoBehaviour {
    public Text word1_burnt;
    public Text word2_burnt;
    public Text word3_burnt;
    public Dropdown dropdown1;
    public Dropdown dropdown2;
    public Dropdown dropdown3;

    public Button SignatureButton;


    public GameObject glove1;
    public GameObject glove2;
    public GameObject glove3;
    public GameObject glove4;

    public Image pentagram;
    public Image Skull;
    public Image Cross;

    public Canvas OutroTextCanvas;
    public Canvas FadeToBalckCanvas;
    public FadePanelInOut fadetoBlackScript;

    public Text powerText;
    public Image outroPentagram;


    private int dropdownValue;
    private string dropdownText;
    private float prevAmount;
    private float t = 0f;
    private float fillAmount;
    private float duration;

    int usedWords = 0;
    private float powerUp = 0;
    private Image penta;

    private GameManager gameManager;

    // Use this for initialization
    void Start() {
        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();


        ScrollReset();

        dropdown1.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdown1);
        });
        dropdown2.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdown2);
        });
        dropdown3.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdown3);
        });
    }


    public void GetDropdownValue(Dropdown dropdown) {
        dropdownValue = dropdown.value;
        dropdownText = dropdown.captionText.text;
        print(dropdown.gameObject.name + " " + dropdownValue + " " + dropdownText);

    }

    //Ouput the new value of the Dropdown into Text
    private void DropdownValueChanged(Dropdown dropdown)
    {
        GetDropdownValue(dropdown);
        if (dropdown == dropdown1 && dropdown.captionText.text !="") { 
            DropDown1Complete();
        }
        if (dropdown == dropdown2 && dropdown.captionText.text != "")
        {
            DropDown2Complete();
        }
        if (dropdown == dropdown3 && dropdown.captionText.text != "")
        {
            DropDown3Complete();
        }

        FillDropdownItems(dropdown);


    }

    public void FillDropdownItems(Dropdown dropdown) {
        dropdown.ClearOptions();
        string[] wordsOfPowerArray = gameManager.PowerUpWordsArray;

        List<string> m_DropOptions = new List<string> ();
        m_DropOptions = wordsOfPowerArray.ToList();
        dropdown.AddOptions(m_DropOptions);
        dropdown1.captionText.text = "PowerUp";
        dropdown2.captionText.text = "Your";
        dropdown3.captionText.text = "Prophecy";

    }

    public void ScrollReset() {
        FadeToBalckCanvas.gameObject.SetActive(true);
        OutroTextCanvas.gameObject.SetActive(false);
        dropdown1.gameObject.SetActive(true);
        dropdown2.gameObject.SetActive(true);
        dropdown3.gameObject.SetActive(true);

        word1_burnt.gameObject.SetActive(false);
        word2_burnt.gameObject.SetActive(false);
        word3_burnt.gameObject.SetActive(false);

        glove1.SetActive(true);
        glove4.SetActive(false);
        Cross.gameObject.SetActive(false);
        SignatureButton.gameObject.SetActive(false);

        usedWords = 0;
        powerUp = 0;
        SetPentagramFill(pentagram,0, 1);
        SetPentagramFill(outroPentagram, 0, 1);

        FillDropdownItems(dropdown1);
        FillDropdownItems(dropdown2);
        FillDropdownItems(dropdown3);
    }

    public void Word1DropdownOn() {
     //   glove1.SetActive(false);
        dropdown1.gameObject.SetActive(true);
    }
    public void DropDown1Complete() {
        dropdown1.gameObject.SetActive(false);
        word1_burnt.gameObject.SetActive(true);
        word1_burnt.text = dropdownText;
        // glove2.SetActive(true);
        usedWords += 1;
        SetPentagramFill(pentagram,CalculatePowerUp(), 1);
        SignatureButton.gameObject.SetActive(true);
        glove1.SetActive(false);

    }

    private float CalculatePowerUp()
    {

        if (usedWords == 1)
            powerUp = (1f / 6f);
        if (usedWords == 2)
            powerUp = (3f / 6f);
        if (usedWords == 3)
        {
            glove4.SetActive(true);
            powerUp = (6f / 6f);
        }
        print("UsedWords = "+ usedWords+" , PowerUp = "+powerUp);
        return powerUp;
    }

    public void Word2DropdownOn()
    {
       // glove2.SetActive(false);
        dropdown2.gameObject.SetActive(true);
    }
    public void DropDown2Complete()
    {
        dropdown2.gameObject.SetActive(false);
        word2_burnt.gameObject.SetActive(true);
        word2_burnt.text = dropdownText;
        // glove3.SetActive(true);
        usedWords += 1;
        SetPentagramFill(pentagram,CalculatePowerUp(), 1);
        SignatureButton.gameObject.SetActive(true);

    }
    public void Word3DropdownOn()
    {
       // glove3.SetActive(false);
        dropdown3.gameObject.SetActive(true);
    }
    public void DropDown3Complete()
    {
        dropdown3.gameObject.SetActive(false);
        word3_burnt.gameObject.SetActive(true);
        word3_burnt.text = dropdownText;
        //  glove4.SetActive(true);
        usedWords += 1;
        SetPentagramFill(pentagram,CalculatePowerUp(), 1);
        SignatureButton.gameObject.SetActive(true);

    }

    public void SignatureBtnPressed() {
        SignatureButton.gameObject.SetActive(false);
        glove4.SetActive(false);
        Cross.gameObject.SetActive(true);
        dropdown1.gameObject.SetActive(false);
        dropdown2.gameObject.SetActive(false);
        dropdown3.gameObject.SetActive(false);
        Invoke("FadeToBlack", 2);
        Invoke("FadeToTransparent", 4);
    }

    public void FadeToBlack() {
        fadetoBlackScript.m_Fading = true;
        
    }
    public void FadeToTransparent()
    {
        OutroTextCanvas.gameObject.SetActive(true);
        float powr = powerUp * 6;
        powerText.text = powr.ToString()+"/6";
        fadetoBlackScript.m_Fading = false;
        SetPentagramFill(outroPentagram, CalculatePowerUp(), 1);

    }
    public void SetPentagramFill(Image myPenta, float myFillAmount, float myDuration) {

        penta = myPenta;
        prevAmount = penta.fillAmount;
        t = 0f;
        duration = myFillAmount;
        fillAmount = myFillAmount;
        InvokeRepeating("IncrementLerp", 0,0.08f);
    
    }
    private void IncrementLerp() {
        penta.fillAmount = Mathf.Lerp(prevAmount, fillAmount, t);

        if (t < 1)
        { // while t below the end limit...
            
            // increment it at the desired rate every update:
            t += Time.deltaTime / duration;
        }
        else
        {
            CancelInvoke("IncrementLerp");
                }
      
    }

}
