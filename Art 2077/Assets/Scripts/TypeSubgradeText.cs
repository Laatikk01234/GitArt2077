using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class TypeSubgradeText : MonoBehaviour 
{
	private TextMeshPro textfield;
	string lastSpeech;
    string currentSpeech;

    private bool speechTyped;

    public GameObject panelObject;

    private Image panelImage;

    public float TypeDelay;

    public GameObject subgradeProfObj;
    private Animator subgrAnim;

    private AudioSource audioSource;


    void Awake (){
        
        subgrAnim = subgradeProfObj.GetComponent<Animator>();

        textfield = GetComponent<TextMeshPro> ();
        speechTyped = false;

        panelImage = panelObject.GetComponent<Image>();
        Color panelColor = panelImage.color;

        audioSource = subgradeProfObj.GetComponent<AudioSource>();
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
                audioSource.Play();
            }
            
	}
	IEnumerator PlayText()
	{
		foreach (char c in currentSpeech) 
		{
            //trainAnim.SetBool("IsTalking", true);
            subgrAnim.SetBool("IsSubgrading", true);
			textfield.text += c;
			yield return new WaitForSeconds (TypeDelay);
		}
    speechTyped = false;
    

   // trainAnim.SetBool("IsTalking", false);
    subgrAnim.SetBool("IsSubgrading", false);
	}

}