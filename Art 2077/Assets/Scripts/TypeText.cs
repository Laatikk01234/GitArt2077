using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

// attach to UI Text component (with the full text already there)

public class TypeText : MonoBehaviour 
{
	private TextMeshPro textfield;
	string lastSpeech;
    string currentSpeech;

    private bool speechTyped;

    public GameObject panelObject;

    private Image panelImage;

    public float TypeDelay;

    public GameObject traineeObj;
    private Animator trainAnim;

    public GameObject profObj;
    private Animator profAnim;


    void Awake (){
        
        trainAnim = traineeObj.GetComponent<Animator>();
        profAnim = profObj.GetComponent<Animator>();

        textfield = GetComponent<TextMeshPro> ();
        speechTyped = false;

        panelImage = panelObject.GetComponent<Image>();
        Color panelColor = panelImage.color;

    }

	void Update () 
	{

        //Debug.Log(speechTyped);
		currentSpeech = textfield.text;
        Color panelColor = panelImage.color;
        //Debug.Log(panelColor.a);

            if (speechTyped == false && currentSpeech != lastSpeech && panelColor.a == 0)
            {
                speechTyped=true;
                //saves current speech into a last speech var
                lastSpeech = currentSpeech;
                textfield.text = "";

		        StartCoroutine ("PlayText");
                trainAnim.SetBool("IsTalking", true);
                profAnim.SetBool("IsTalkingToo", true);
            }
            
	}
	IEnumerator PlayText()
	{
		foreach (char c in currentSpeech) 
		{
			textfield.text += c;
			yield return new WaitForSeconds (TypeDelay);
		}
    speechTyped = false;
    
    trainAnim.SetBool("IsTalking", false);
    profAnim.SetBool("IsTalkingToo", false);
	}

}