using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class TypeProfessorText : MonoBehaviour 
{
	private TextMeshPro textfield;
	string lastSpeech;
    string currentSpeech;

    private bool speechTyped;

    public GameObject panelObject;

    private Image panelImage;

    public float TypeDelay;

    //public GameObject traineeObj;
    //private Animator trainAnim;

    public GameObject profObj;
    private Animator profAnim;

    public Button button;
    private bool someoneTalking;

    private AudioSource audioSource;

    private MeshRenderer textRenderer;
    private SpriteRenderer spriteRenderer;
    public GameObject profBubbleObj;

    void Awake (){
        
        profAnim = profObj.GetComponent<Animator>();

        textfield = GetComponent<TextMeshPro> ();
        
        textRenderer = GetComponent<MeshRenderer>();

        spriteRenderer = profBubbleObj.GetComponent<SpriteRenderer>();

        //duct tape so sound doesnt play at the start
        lastSpeech = textfield.text;

        speechTyped = false;

        panelImage = panelObject.GetComponent<Image>();
        Color panelColor = panelImage.color;

        //this is here for function to switch button interactivity
        someoneTalking = false;

        audioSource = profObj.GetComponent<AudioSource>();
    }


	void Update () 
	{
		currentSpeech = textfield.text;
        Color panelColor = panelImage.color;

            if (panelColor.a != 0){
                //disables text and bubble rendering when noone is talking
                textRenderer.enabled = false;
                spriteRenderer.enabled = false;
            }

            if (speechTyped == false && currentSpeech != lastSpeech && panelColor.a == 0)
            {
                //enables text and bubble rendering when people are talking
                textRenderer.enabled = true;
                spriteRenderer.enabled = true;
                
                speechTyped=true;

                //saves current speech into a last speech var
                lastSpeech = currentSpeech;

                textfield.text = "";
		        StartCoroutine ("PlayText");

                audioSource.Play();
            }
            
            
            
        //make button noninteractable when prof is talking
        if (someoneTalking){
            button.interactable = false;
            Debug.Log("Professor is talking");
        }
        else {
            button.interactable = true;
            Debug.Log("Noone is talking");
        }

	}

	IEnumerator PlayText()
	{
		foreach (char c in currentSpeech) 
		{
            //trainAnim.SetBool("IsTalking", true);
            profAnim.SetBool("IsTalkingToo", true);
            someoneTalking = true;
			textfield.text += c;
			yield return new WaitForSeconds (TypeDelay);
		}
        
    speechTyped = false;
    
    profAnim.SetBool("IsTalkingToo", false);
    someoneTalking = false;
	}

}