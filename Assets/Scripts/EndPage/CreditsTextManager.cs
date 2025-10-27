
using UnityEngine;
using UnityEngine.UI; 
using System.Collections;
using System.Collections.Generic; 

public class CreditsTextManager : MonoBehaviour
{
    [Header("Text Elements")]
    [Tooltip("[Important] Drag all Text objects here in the order you want them to appear")]
    public List<Text> creditsList;

    [Header("Typing Settings")]
    [Tooltip("Typewriter effect speed")]
    public float typingSpeed = 0.05f;

    [Tooltip("The delay time between the completion of the previous text segment and the start of the next text segment")]
    public float pauseBetweenTexts = 0.5f;


    private List<string> fullTexts; 

    void Start()
    {

        fullTexts = new List<string>();


        if (creditsList == null || creditsList.Count == 0)
        {
            Debug.LogError("Credits List (制作人员名单列表) 没有被设置！", this);
            return;
        }


        foreach (Text creditText in creditsList)
        {
    
            fullTexts.Add(creditText.text);


            creditText.text = "";
        }


        StartCoroutine(ShowCreditsSequentially());
    }


    private IEnumerator ShowCreditsSequentially()
    {

        for (int i = 0; i < creditsList.Count; i++)
        {


            yield return StartCoroutine(TypeText(creditsList[i], fullTexts[i]));


            yield return new WaitForSeconds(pauseBetweenTexts);
        }


        Debug.Log("The list of production personnel has been displayed.");

    }


    private IEnumerator TypeText(Text textObject, string fullText)
    {

        foreach (char c in fullText)
        {

            textObject.text += c;


            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
