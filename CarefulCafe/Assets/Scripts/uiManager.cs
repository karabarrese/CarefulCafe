using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class uiManager : MonoBehaviour
{
    public GameObject flourNutrition;
    public GameObject eggNutrition;
    public GameObject almondNutrition;

    public GameObject AddEgg;
    public GameObject AddFlour;

    public GameObject backToCabinet;

    public GameObject flourMixture;
    public GameObject eggMixture;

    public bool glutenAllergy;
    public bool dairyAllergy;
    public bool eggAllergy;
    public string allergy;

    [SerializeField] private Dialogue dialogue;
    [SerializeField] private Sprite managerDialogueSprite;






    // Start is called before the first frame update
    void Start()
    {
        closeUI();
        flourMixture.SetActive(false);
        eggMixture.SetActive(false);
        dairyAllergy = false;
        eggAllergy = false;
        glutenAllergy = false;
        //testing glutenAllergy
        allergy = PlayerPrefs.GetString("CurPlayerAllergy");
        //allergies: "None, Dairy, Gluten, Egg"
        if (allergy == "Dairy")
        {
            dairyAllergy = true;
        }
        else if (allergy == "Gluten")
        {
            glutenAllergy = true;
        }
        else if (allergy == "Egg")
        {
            eggAllergy = true;
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        // TODO: go back to kitchen once you have all ingredients
        if (Input.GetKeyDown(KeyCode.E)){
            PlayerPrefs.SetInt("DoneWithMinigame", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("KitchenScene");
        } 

        Debug.Log(PlayerPrefs.GetString("CurPlayerAllergy", "you failed"));
    }
    
    public void flourClick()
    {
        if (!glutenAllergy)
        {
            flourMixture.SetActive(true);
            closeUI();
        }
        else
        {
            List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  
            DialogueComponent instruction  = new DialogueComponent(CharacterEmotion.None, "Oops, we don't want to add the wheat flour to this recipe because our customer is allergic to gluten!", managerDialogueSprite);
            dialogueArray.Add(instruction);
            dialogue.UpdateFullDialogue(dialogueArray);
            closeUI();
        }
        
    }

    public void eggClick()
    {
        eggMixture.SetActive(true);
        closeUI();
    }

    public void closeUI()
    {
        flourNutrition.SetActive(false);
        eggNutrition.SetActive(false);
        almondNutrition.SetActive(false);
        AddFlour.SetActive(false);
        AddEgg.SetActive(false);
        backToCabinet.SetActive(false);
    }

    public void openFlourUI()
    {
        flourNutrition.SetActive(true);
        AddFlour.SetActive(true);
        backToCabinet.SetActive(true);
    }
    public void openEggUI()
    {
        eggNutrition.SetActive(true);
        AddEgg.SetActive(true);
        backToCabinet.SetActive(true);
    }
    public void openAlmondUI()
    {
        almondNutrition.SetActive(true);
        AddFlour.SetActive(true);
        backToCabinet.SetActive(true);
    }
}
