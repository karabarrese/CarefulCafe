using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    // Start is called before the first frame update
    void Start()
    {
        closeUI();
        flourMixture.SetActive(false);
        eggMixture.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void flourClick()
    {
        flourMixture.SetActive(true);
        closeUI();
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
