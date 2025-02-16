using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstrumentControllerScript : MonoBehaviour
{
    public Text instruction;
    public GameObject epipen;
    // public GameObject epipen_idle;
    public GameObject leg;
    public GameObject bar;
    public GameObject arrow;
    public ArrowScript arrowScript;
    // Start is called before the first frame update
    void Start()
    {
        instruction.text = "Welcome to the epipen mini-game! Please click on the space bar to begin.";
        arrowScript.enabled = false;
        // epipen_idle.SetActive(true);
        epipen.SetActive(false);
        bar.SetActive(false);
        leg.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !arrowScript.enabled)
        {
            StartCoroutine(EpipenSequence());
        }
    }

    private IEnumerator EpipenSequence()
    {
        instruction.text = "Step 1: Form fist around EpiPen and Pull off Blue Safety Cap";
        Debug.Log("Step 1: Form fist around EpiPen and Pull off Blue Safety Cap");
        yield return new WaitForSeconds(2f);
        yield return WaitForSpaceKey();

        instruction.text = "Step 2: Position orange end about 10 cm away from outer mid-thigh";
        Debug.Log("Step 2: Position orange end about 10 cm away from outer mid-thigh");
        arrow.SetActive(true);
        // epipen_idle.SetActive(false);
        leg.SetActive(true);
        bar.SetActive(true);
        arrowScript.enabled = true;
        yield return new WaitForSeconds(2f);
        yield return WaitForSpaceKey();

        instruction.text = "Step 3: Swing and push firmly until it clicks. Hold for 10 seconds.";
        Debug.Log("Step 3: Swing and push firmly until it clicks. Hold for 10 seconds.");
        epipen.SetActive(true);
        yield return new WaitForSeconds(2f);
        yield return WaitForSpaceKey();

        instruction.text = "Step 4: Remove EpiPen and massage the area for 10 seconds.";
        Debug.Log("Step 4: Remove EpiPen and massage the area for 10 seconds.");
        epipen.SetActive(false);
        bar.SetActive(false);
        arrowScript.enabled = false;
        arrow.SetActive(false);
        yield return new WaitForSeconds(2f);
        yield return WaitForSpaceKey();
    }

    private IEnumerator WaitForSpaceKey()
    {
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }
    }
}
