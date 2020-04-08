using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class TypeTextUI : MonoBehaviour 
{
	private TextMeshProUGUI textfield;
	string lastSpeech;
    string currentSpeech;

    private bool speechTyped;
	public float TypeDelay;

    void Awake (){
        textfield = GetComponent<TextMeshProUGUI> ();
        speechTyped = false;
    }

	void Update () 
	{

        //Debug.Log(speechTyped);
		currentSpeech = textfield.text;

        if (speechTyped == false && currentSpeech != lastSpeech){

            speechTyped=true;
            lastSpeech = currentSpeech;

            textfield.text = "";
		    StartCoroutine ("PlayText");
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
	}

}