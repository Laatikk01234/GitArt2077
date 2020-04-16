using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamelogic : MonoBehaviour
{
    public GameObject endingInfo;
    private bool allfakes = true;
    private bool started = false;

    public GameObject subGradeObject;
    private SubGradeList subGradeGiverList;
    public GameObject FinalGradeObject;
    private FinalGrade finalGradeGiver;

    //Andrea messing up
    public GameObject curtainsObject;
    public GameObject canvasObject;
    Animator anim;
    Animator animcanvas;

    public GameObject darknessTextObject;
    private TextMeshProUGUI darknessText;
    private int DayCount = 1;

    private AudioSource buttonAudio;

    private AudioSource audioSource;

    public AudioClip curtainsSoundStart;
    public AudioClip curtainsSound;

    public AudioSource audioSourceProf;

    //Andrea messing up end

    public List<GameObject> art;
    public GameObject artLocation;
    private SpriteRenderer currentPicture;
    private int listIndexAndArtpiecenumber = 0;

    private Artinformation currentArtinformation;

    private int ArtpieceGrade = 0;
    private List<int> DailySubGrade = new List<int>()
    {
        0,
        0,
        0,
        0,
        0
    };
    private int day = 0;
    private int totalGrade = 0;

    public GameObject feedbackBubble;
    private TextMeshPro feedbackBubbleText;


    public GameObject traineeSpeechbubble;
    private TextMeshPro traineeText;
    public GameObject professorSpeechbubble;
    private TextMeshPro professorText;

    public Slider artValueSlider;

    public GameObject sliderTextMesh;
    private TextMeshPro sliderValueText;
    public Button confirm;
    private Text buttonText;


    // art value constants
    private const int fake = 0;
    private const int cheap = 1;
    private const int mediocre = 2;
    private const int Valuable = 3;

    private const int gamelength = 20;

    //private bool waitForClick = false;

    public GameObject escMenu;

    public void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (escMenu.activeSelf)
            {
                escMenu.SetActive(false);
            }
            else
            {
                escMenu.SetActive(true);
            }
            
        }
        SliderLock();
        if (!started)
        {
            if (Time.timeSinceLevelLoad > 60)
            {
                EndGame();
            }
        }
    }

    private void SliderLock()
    {
        if (sliderValueText.text == "Fake")
        {
            artValueSlider.value = 0;
        }
        else if (sliderValueText.text == "Cheap")
        {
            artValueSlider.value = 1;
        }
        else if (sliderValueText.text == "Mediocre")
        {
            artValueSlider.value = 2;
        }
        else if (sliderValueText.text == "Valuable")
        {
            artValueSlider.value = 3;
        }
    }
    public void Start()
    {
        subGradeGiverList = subGradeObject.GetComponent<SubGradeList>();
        //subGradeList.subGrades[day].GetComponent<GradeTextValues>().gradeA;
        finalGradeGiver = FinalGradeObject.GetComponent<FinalGrade>();
        
        
        // how to introduce the game screen might go here?
        feedbackBubble.SetActive(false);
        feedbackBubbleText = feedbackBubble.GetComponentInChildren<TextMeshPro>();

        currentPicture = artLocation.GetComponent<SpriteRenderer>();


        traineeText = traineeSpeechbubble.GetComponentInChildren<TextMeshPro>();
        professorText = professorSpeechbubble.GetComponentInChildren<TextMeshPro>();
        sliderValueText = sliderTextMesh.GetComponent<TextMeshPro>();

        Button btn = confirm.GetComponent<Button>();
        btn.onClick.AddListener(StartOrSubmitOrNext);
        buttonText = confirm.GetComponentInChildren<Text>();

        // Andrea messing up 
        anim = curtainsObject.GetComponent<Animator>();
        animcanvas = canvasObject.GetComponent<Animator>();

        darknessText = darknessTextObject.GetComponent<TextMeshProUGUI>();

        animcanvas.SetTrigger("IntroRoll");

        buttonAudio = btn.GetComponent<AudioSource>();
        audioSource = this.GetComponent<AudioSource>();

        //audioSourceProf = Get
        // Andrea messing up end
    }


    public void SliderValueChange()
    {
        //Debug.Log(artValueSlider.value);
        double curValue = artValueSlider.value;
        if (curValue <= fake + 0.5)
        {
            sliderValueText.text = "Fake";
        }
        else if (curValue <= cheap + 0.5)
        {
            sliderValueText.text = "Cheap";
        }
        else if (curValue <= mediocre + 0.5)
        {
            sliderValueText.text = "Mediocre";
        }
        else if (curValue <= Valuable + 0.5)
        {
            sliderValueText.text = "Valuable";
        }

    }
    // both of these work for clicking
    void StartOrSubmitOrNext()
    {
        // minnettää? StartCoroutine(Waiter());
        if (buttonText.text == "End")
        {
            // end game
        }

        // ok clicked -> give response and then change art piece

        //initial value is start -> changed to submit when art is given -> changed to next after confirm
        // changed to submit after clicking next, changed to start at the change of week?
        //currentArtinformation = art[listIndexAndDay].GetComponent<Artinformation>();
        //NextPainting();
        //if (waitForClick == false)

        if (buttonText.text == "Start")
            {
                //currentPicture.sprite = currentArtinformation.picture;
                //traineeText.text = currentArtinformation.traineeSays;
                //buttonText.text = "Submit";

                //curtain animation opening for the first time
                animcanvas.SetTrigger("FirstDaySlideIn");
                anim.SetBool("DayStarted", true);
                audioSource.PlayOneShot(curtainsSoundStart);

                //NextPainting();
                StartCoroutine(WaitAndNextPainting());
            }
            //derbing for testing
            else if (buttonText.text == "Submit" && sliderValueText.text != "Choose art value" )
            {
                started = true;
               CurrentArtpieceGradeAndResponse();
               buttonText.text = "Next";
                
           }
            else if (buttonText.text == "Ok")
            {

                // darkness text changes - Andrea
                    DayCount = DayCount + 1;
                    darknessText.text = "DAY " + DayCount;
                
                buttonText.text = "Start";
             
                SwitchSpeechBubbleVisibility();
                NextPainting();

            }
            else if (buttonText.text == "Next")
            {
                professorText.text = "";
                // Curtains close
                listIndexAndArtpiecenumber += 1;

                if (listIndexAndArtpiecenumber == gamelength)
                {
                
                    // Game over -> ending screen
                    EndGame();
                    //GiveFinalGrade();
                }
                else if (listIndexAndArtpiecenumber % 5 == 0)
                {
                    // week change 
                    //elif ok is the next -> slider back if taken away?
                    buttonText.text = "Ok";
                    //maybe position change - definitely slider away.
                    currentPicture.sprite = null;
                    GiveSubGrade();

                }
                else
                {

                    //Andrea 
                    anim.SetTrigger("ClickNext");
                    anim.SetBool("DayStarted", false);
                    audioSource.PlayOneShot(curtainsSound);
                    //Andrea end

                    StartCoroutine(WaitAndNextPainting());
                    //curtains close
                    //NextPainting();
                    // curtains open


                }
            }


    }

    IEnumerator WaitAndNextPainting()
    {
        //waitForClick = true;
        confirm.interactable = false;
        //float halfOfAnimationMaybe = 0.5F;
        yield return new WaitForSeconds(0.5F);
        NextPainting();
        // text animation here
        //pause
        // text animation here
        // slider back up
        //pause

        //waitForClick = false;

    }


    private void NextPainting()
    {
        //slider is interactable
        artValueSlider.interactable = true;

        //not first time curtains open
        anim.SetBool("DayStarted", false);

        currentArtinformation = art[listIndexAndArtpiecenumber].GetComponent<Artinformation>();
        currentPicture.sprite = currentArtinformation.picture;

        artValueSlider.value = 1.5F;
        sliderValueText.text = "Choose art value";
        buttonText.text = "Submit";

        StartCoroutine(WaitSpeeches());
        //traineeText.text = currentArtinformation.traineeSays;
        //professorText.text = currentArtinformation.professorSays;
    }

    IEnumerator WaitSpeeches()
    {
        //waitForClick = true;
        traineeText.text = currentArtinformation.traineeSays;
        yield return new WaitForSeconds(currentArtinformation.traineeSays.Length * 0.02F + 0.10F);
        professorText.text = currentArtinformation.professorSays;
        yield return new WaitForSeconds(currentArtinformation.professorSays.Length * 0.02F + 0.10F);
        //waitForClick = false;
        confirm.interactable = true;
    }


    private void CurrentArtpieceGradeAndResponse()
    {
        // slider is not interactable
        artValueSlider.interactable = false;
        

        //Debug.Log();
        // A = 10, B = 8, C = 6, D = 4, F = 2;
        // max 2p per answer, min 0
        int picVal = currentArtinformation.pictureValue;
        if (sliderValueText.text == "Fake")
        {
            if (picVal == 0)
            {
                ArtpieceGrade = 2;
                professorText.text = currentArtinformation.correct;
            }
            else
            {
                ArtpieceGrade = 0;
                professorText.text = currentArtinformation.notFake;
            }
        }
        else if (sliderValueText.text == "Cheap")
        {
            allfakes = false;
            if (picVal == fake)
            {
                professorText.text = currentArtinformation.itsFake;
                ArtpieceGrade = 0;
            }
            else if (picVal == cheap)
            {
                professorText.text = currentArtinformation.correct;
                ArtpieceGrade = 2;
            }
            else if (picVal == mediocre) 
            {
                professorText.text = currentArtinformation.tooLow;
                ArtpieceGrade = 1;
            }
            else if (picVal == Valuable)
            {
                professorText.text = currentArtinformation.wayTooLow;
                ArtpieceGrade = 0;
            }
        }
        else if (sliderValueText.text == "Mediocre")
        {
            allfakes = false;
            if (picVal == fake)
            {
                professorText.text = currentArtinformation.itsFake;
                ArtpieceGrade = 0;
            }
            else if (picVal == cheap)
            {
                professorText.text = currentArtinformation.tooHigh;
                ArtpieceGrade = 1;
            }
            else if (picVal == mediocre)
            {
                professorText.text = currentArtinformation.correct;
                ArtpieceGrade = 2;
            }
            else if (picVal == Valuable)
            {
                professorText.text = currentArtinformation.tooLow;
                ArtpieceGrade = 1;
            }
        }
        else if (sliderValueText.text == "Valuable")
        {
            allfakes = false;
            if (picVal == fake)
            {
                professorText.text = currentArtinformation.itsFake;
                ArtpieceGrade = 0;
            }
            else if (picVal == cheap)
            {
                professorText.text = currentArtinformation.wayTooHigh;
                ArtpieceGrade = 0;
            }
            else if (picVal == mediocre)
            {
                professorText.text = currentArtinformation.tooHigh;
                ArtpieceGrade = 1;
            }
            else if (picVal == Valuable)
            {
                professorText.text = currentArtinformation.correct;
                ArtpieceGrade = 2;
            }
        }
        DailySubGrade[day] = DailySubGrade[day] + ArtpieceGrade;

        StartCoroutine(WaitForSomeRandomTime());
    }

    IEnumerator WaitForSomeRandomTime()
        {
        
        confirm.interactable = false;
        //waitForClick = true;
        //traineeText.text = currentArtinformation.traineeSays;
        //yield return new WaitForSeconds(currentArtinformation.traineeSays.Length * 0.02F + 0.10F);
        yield return new WaitForSeconds(currentArtinformation.correct.Length * 0.02F + 0.10F);
        Debug.Log(currentArtinformation.correct.Length* 0.02F + 0.10F);
        //waitForClick = false;
        confirm.interactable = true;
        }

    public void EndGame()
    {

        //update ddol
        TotalGradeCounter();
        endingInfo.GetComponent<EndingInfo>().SetVariables(allfakes, started, totalGrade);
        SceneManager.LoadScene("Final Scene");
    }

    public void TotalGradeCounter()
    {
        foreach (int x in DailySubGrade)
        {
            totalGrade += x;
        }
    }
    private void GiveFinalGrade()
    {
        //Final grade mechanics?
        SwitchSpeechBubbleVisibility();
        string grade;
        TotalGradeCounter();
        if (totalGrade == 50)
        {
            grade = finalGradeGiver.finalGrades[0];
        }
        else if (totalGrade > 40)
        {
            grade = finalGradeGiver.finalGrades[1];
        }
        else if (totalGrade > 30)
        {
            grade = finalGradeGiver.finalGrades[2];
        }
        else if (totalGrade > 20)
        {
            grade = finalGradeGiver.finalGrades[3];
        }
        else if (totalGrade > 10)
        {
            grade = finalGradeGiver.finalGrades[4];
        }
        else
        {
            grade = finalGradeGiver.finalGrades[5];
        }

        feedbackBubbleText.text = grade;
        //
        buttonText.text = "End";
    }

    private void GiveSubGrade()
    {
        GradeTextValues gradeGiver = subGradeGiverList.subGrades[day].GetComponent<GradeTextValues>();
        string grade;
            //subGradeList.subGrades[day].GetComponent<GradeTextValues>().gradeA;
        SwitchSpeechBubbleVisibility();
        if (DailySubGrade[day] == 10)
        {
            grade = gradeGiver.gradeA;
        }
        else if (DailySubGrade[day] > 8)
        {
            grade = gradeGiver.gradeB;
        }
        else if (DailySubGrade[day] > 6)
        {
            grade = gradeGiver.gradeC;
        }
        else if (DailySubGrade[day] > 4)
        {
            grade = gradeGiver.gradeD;
        }
        //else if (DailySubGrade[day] > 2)
        //{
        //    grade = gradeGiver.gradeF;
        //}
        // all answers less than 4 points total
        else
        {
            grade = gradeGiver.gradeF;
        }

        feedbackBubbleText.text = grade;
        day += 1;

    }

    private void SwitchSpeechBubbleVisibility()
    {
        if (feedbackBubble.activeSelf == true) 
        {
            feedbackBubble.SetActive(false);
            traineeSpeechbubble.SetActive(true);
            professorSpeechbubble.SetActive(true);
            //Andrea
            //text of day trigger animation
            animcanvas.SetTrigger("Text");
            //Andrea end
        }
        else
        {
            //Andrea
            //Day Ends and fade to black trigger
            animcanvas.SetTrigger("DayEndsss");
            //Andrea end
            feedbackBubble.SetActive(true);
            traineeSpeechbubble.SetActive(false);
            professorSpeechbubble.SetActive(false);
    }
    }

    //plays button sound on click - Andrea
    public void PlayButton(){
        buttonAudio.Play();
    }

}
