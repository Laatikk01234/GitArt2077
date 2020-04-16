using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class TypeText : MonoBehaviour 
{
	private TextMeshPro textfield;
	string lastSpeech;
    string currentSpeech;

    private bool speechTyped;

    public GameObject panelObject;

    private Image panelImage;

    public float TypeDelay;

    public GameObject trainObj;
    private Animator trainAnim;

    private AudioSource audioSourceTrain;
    private MeshRenderer txtrnd;
    public GameObject trainBubbleObj;
    private SpriteRenderer spriteRenderer;

    public Button button;
    //private bool traineeTalking;

    void Awake (){
        
        trainAnim = trainObj.GetComponent<Animator>();
        //profAnim = profObj.GetComponent<Animator>();

        textfield = GetComponent<TextMeshPro> ();
        
        txtrnd = GetComponent<MeshRenderer>();

        spriteRenderer = trainBubbleObj.GetComponent<SpriteRenderer>();
        
        //duct tape so sound doesnt play at the start
        lastSpeech = textfield.text;

        speechTyped = false;

        panelImage = panelObject.GetComponent<Image>();
        Color panelColor = panelImage.color;

        audioSourceTrain = trainObj.GetComponent<AudioSource>();

        //traineeTalking = false;

    }

	void Update () 
	{
		currentSpeech = textfield.text;
        Color panelColor = panelImage.color;

            if (panelColor.a != 0){
                //disables text and bubble rendering when noone is talking
                txtrnd.enabled = false;
                spriteRenderer.enabled = false;
            }

            if (speechTyped == false && currentSpeech != lastSpeech && panelColor.a == 0)
            {
                //enables text and bubble rendering when poeple are talking
                txtrnd.enabled = true;
                spriteRenderer.enabled = true;

                speechTyped=true;

                //saves current speech into a last speech var
                lastSpeech = currentSpeech;
                
                textfield.text = "";
		        StartCoroutine ("PlayText");
                
                //plays talking soundeffect
                audioSourceTrain.Play();
            }

        //make button noninteractable when prof is talking
        //if (traineeTalking){
        //   button.interactable = false;
        //    Debug.Log("Trainee is talking");
        //}
        //else {
         //  button.interactable = true;
        //   Debug.Log("Noone is talking");
        //}
            
	}

	IEnumerator PlayText()
	{
		foreach (char c in currentSpeech) 
		{
            trainAnim.SetBool("IsTalking", true);
            //traineeTalking = true;
            //profAnim.SetBool("IsTalkingToo", true);
			textfield.text += c;
			yield return new WaitForSeconds (TypeDelay);
		}
    speechTyped = false;
    

    trainAnim.SetBool("IsTalking", false);
    //profAnim.SetBool("IsTalkingToo", false);
    ///traineeTalking = false;
	}

}