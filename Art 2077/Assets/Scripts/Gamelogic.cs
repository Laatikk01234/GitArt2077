using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gamelogic : MonoBehaviour
{
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
    private const int cheap = 200;
    private const int mediocre = 350;
    private const int expensive = 500;

    private const int gamelength = 20;

    private bool waitForClick = false;

    public void Start()
    {
        subGradeGiverList = subGradeObject.GetComponent<SubGradeList>();
        //subGradeList.subGrades[day].GetComponent<GradeTextValues>().gradeA;
        finalGradeGiver = FinalGradeObject.GetComponent<FinalGrade>();


        // Andrea messing up 
        anim = curtainsObject.GetComponent<Animator>();
        animcanvas = canvasObject.GetComponent<Animator>();

        darknessText = darknessTextObject.GetComponent<TextMeshProUGUI>();
        // Andrea messing up end
        
        
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
    }

    IEnumerator WaitAndNextPainting()
    {
        waitForClick = true;
        //float halfOfAnimationMaybe = 0.5F;
        yield return new WaitForSeconds(0.5F);
        NextPainting();
        // text animation here
        //pause
        // text animation here
        // slider back up
        //pause

        waitForClick = false;

    }

    public void SliderValueChange()
    {
        //Debug.Log(artValueSlider.value);
        int curValue = (int)artValueSlider.value;
        if (curValue == fake)
        {
            sliderValueText.text = "Fake";
        }
        else if (curValue <= cheap)
        {
            sliderValueText.text = "Cheap";
        }
        else if (curValue <= mediocre)
        {
            sliderValueText.text = "Mediocre";
        }
        else if (curValue <= expensive)
        {
            sliderValueText.text = "Expensive";
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
        if (waitForClick == false)
        {
            if (buttonText.text == "Start")
            {
                //currentPicture.sprite = currentArtinformation.picture;
                //traineeText.text = currentArtinformation.traineeSays;
                //buttonText.text = "Submit";


                // slider up?
                anim.SetBool("DayStarted", true);

                NextPainting();
                //curtains open
            }
            // derbing for testing
            else if (buttonText.text == "Submit" && sliderValueText.text != "!!Choose art value")
            {
                // animate slider to go down
                CurrentArtpieceGradeAndResponse();
                buttonText.text = "Next";
                //anim.SetBool("DayStarted", false);
            }
            else if (buttonText.text == "Ok")
            {
                // darkness text changes - Andrea
                DayCount = DayCount + 1;
                darknessText.text = "DAY " + DayCount;
                buttonText.text = "Start";
             
                SwitchSpeechBubbleVisibility();
                NextPainting();
                //anim.SetBool("DayStarted", false);
            }
            else if (buttonText.text == "Next")
            {
                // Curtains close
                listIndexAndArtpiecenumber += 1;
                if (listIndexAndArtpiecenumber == gamelength)
                {
                    // Game over -> ending screen

                    GiveFinalGrade();
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


                    //Andrea messing up
                    anim.SetTrigger("ClickNext");
                    anim.SetBool("DayStarted", false);
                    //Andrea messing up end
                    StartCoroutine(WaitAndNextPainting());
                    //curtains close
                    //NextPainting();
                    // curtains open


                }
            }
        }


    }

    private void NextPainting()
    {
        
        anim.SetBool("DayStarted", false);


        currentArtinformation = art[listIndexAndArtpiecenumber].GetComponent<Artinformation>();
        currentPicture.sprite = currentArtinformation.picture;
        traineeText.text = currentArtinformation.traineeSays;
        professorText.text = "Lets value some art!";
        artValueSlider.value = 250;
        sliderValueText.text = "Choose art value";
        buttonText.text = "Submit";
    }


    private void CurrentArtpieceGradeAndResponse()
    {
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
            if (picVal == fake)
            {
                professorText.text = currentArtinformation.itsFake;
                ArtpieceGrade = 0;
            }
            else if (picVal < cheap)
            {
                professorText.text = currentArtinformation.correct;
                ArtpieceGrade = 2;
            }
            else if (picVal < mediocre) 
            {
                professorText.text = currentArtinformation.tooLow;
                ArtpieceGrade = 1;
            }
            else if (picVal <= expensive)
            {
                professorText.text = currentArtinformation.wayTooLow;
                ArtpieceGrade = 0;
            }
        }
        else if (sliderValueText.text == "Mediocre")
        {
            if (picVal == fake)
            {
                professorText.text = currentArtinformation.itsFake;
                ArtpieceGrade = 0;
            }
            else if (picVal < cheap)
            {
                professorText.text = currentArtinformation.tooHigh;
                ArtpieceGrade = 1;
            }
            else if (picVal < mediocre)
            {
                professorText.text = currentArtinformation.correct;
                ArtpieceGrade = 2;
            }
            else if (picVal <= expensive)
            {
                professorText.text = currentArtinformation.tooLow;
                ArtpieceGrade = 1;
            }
        }
        else if (sliderValueText.text == "Expensive")
        {
            if (picVal == fake)
            {
                professorText.text = currentArtinformation.itsFake;
                ArtpieceGrade = 0;
            }
            else if (picVal < cheap)
            {
                professorText.text = currentArtinformation.wayTooHigh;
                ArtpieceGrade = 0;
            }
            else if (picVal < mediocre)
            {
                professorText.text = currentArtinformation.tooHigh;
                ArtpieceGrade = 1;
            }
            else if (picVal <= expensive)
            {
                professorText.text = currentArtinformation.correct;
                ArtpieceGrade = 2;
            }
        }

        DailySubGrade[day] = DailySubGrade[day] + ArtpieceGrade;
    }

    private void GiveFinalGrade()
    {
        //Final grade mechanics?
        SwitchSpeechBubbleVisibility();
        string grade;
        foreach (int x in DailySubGrade)
        {
            totalGrade += x;
        }
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
}
