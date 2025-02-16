using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowl : MonoBehaviour
{
    public GameObject egg;

    public GameObject crackedEgg;
    public GameObject rawEgg;
    public uiManager ui;
    private bool eggActive = true;

    private void OnTriggerStay2D(Collider2D other)
    {
       if (other.CompareTag("Respawn"))
        {
            Debug.Log("Egg is in the bowl and has respawned!");
            eggActive = false;
        }

    }
    void Start()
    {
        crackedEgg.SetActive(false);
        rawEgg.SetActive(false);
    }

    void Update() {
        if (!eggActive)
        {
            StartCoroutine(WaitForEgg());
        }
    }
    IEnumerator WaitForEgg()
    {
        eggActive = true;
        yield return new WaitForSeconds(1);
        egg.SetActive(false);
        Debug.Log("Egg gone");
        crackedEgg.SetActive(true);
        yield return new WaitForSeconds(1);
        crackedEgg.SetActive(false);
        rawEgg.SetActive(true);
        ui.numIngredients++;
        ui.eggIn = true;
    }
}
