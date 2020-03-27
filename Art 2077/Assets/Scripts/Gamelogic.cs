using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gamelogic : MonoBehaviour
{
    public List<GameObject> art;
    public GameObject artLocation;
    private SpriteRenderer currentPicture;
    private int listIndexAndDay = 0;
    private Artinformation currentArtinformation;

    private int dailyGrade = 0;
    private List<int> weeklyGrade = new List<int>()
    {
        0,
        0,
        0,
        0,
        0
    };
    private int week = 0;
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


    public void Start()
    {
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

    public void SliderValueChange()
    {
        //Debug.Log(artValueSlider.value);
        int curValue = (int)artValueSlider.value;
        if (curValue == fake)
        {
            sliderValueText.text = "Fake";
        }
        else if (curValue < cheap)
        {
            sliderValueText.text = "Cheap";
        }
        else if (curValue < mediocre)
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
        // ok clicked -> give response and then change art piece

        //initial value is start -> changed to submit when art is given -> changed to next after confirm
        // changed to submit after clicking next, changed to start at the change of week?
        //currentArtinformation = art[listIndexAndDay].GetComponent<Artinformation>();
        //NextPainting();
        if (buttonText.text == "Start")
        {
            //currentPicture.sprite = currentArtinformation.picture;
            //traineeText.text = currentArtinformation.traineeSays;
            //buttonText.text = "Submit";

            NextPainting();
        }
        else if (buttonText.text == "Submit" && sliderValueText.text != "Choose art value")
        {
            Debug.Log("Do we grade?");
            DailyGradeAndResponse();
            buttonText.text = "Next";
        }
        else if (buttonText.text == "Ok")
        {
            buttonText.text = "Start";
            SwitchSpeechBubbleVisibility();
            NextPainting();
        }
        else if (buttonText.text == "Next")
        {
            listIndexAndDay += 1;
            if (listIndexAndDay == 25)
            {
                // Game over -> ending screen

                GiveFinalGrade();
            }
            else if (listIndexAndDay % 5 == 0)
            {
                // week change 
                //elif ok is the next -> slider back if taken away?
                buttonText.text = "Ok";
                //maybe position change - definitely slider away.
                currentPicture.sprite = null;
                GiveGrade();
            }
            else
            {
                NextPainting();
            }
        }



    }

    private void NextPainting()
    {
        
        currentArtinformation = art[listIndexAndDay].GetComponent<Artinformation>();
        currentPicture.sprite = currentArtinformation.picture;
        traineeText.text = currentArtinformation.traineeSays;
        professorText.text = "Lets value some art!";
        artValueSlider.value = 250;
        sliderValueText.text = "Choose art value";
        buttonText.text = "Submit";
    }


    private void DailyGradeAndResponse()
    {
        //Debug.Log();
        // A = 10, B = 8, C = 6, D = 4, F = 2;
        // max 2p per answer, min 0
        int picVal = currentArtinformation.pictureValue;
        if (sliderValueText.text == "Fake")
        {
            if (picVal == 0)
            {
                dailyGrade = 2;
                professorText.text = currentArtinformation.correct;
            }
            else
            {
                dailyGrade = 0;
                professorText.text = currentArtinformation.notFake;
            }
        }
        else if (sliderValueText.text == "Cheap")
        {
            if (picVal == fake)
            {
                professorText.text = currentArtinformation.itsFake;
                dailyGrade = 0;
            }
            else if (picVal < cheap)
            {
                professorText.text = currentArtinformation.correct;
                dailyGrade = 2;
            }
            else if (picVal < mediocre) 
            {
                professorText.text = currentArtinformation.tooLow;
                dailyGrade = 1;
            }
            else if (picVal <= expensive)
            {
                professorText.text = currentArtinformation.wayTooLow;
                dailyGrade = 0;
            }
        }
        else if (sliderValueText.text == "Mediocre")
        {
            if (picVal == fake)
            {
                professorText.text = currentArtinformation.itsFake;
                dailyGrade = 0;
            }
            else if (picVal < cheap)
            {
                professorText.text = currentArtinformation.tooHigh;
                dailyGrade = 1;
            }
            else if (picVal < mediocre)
            {
                professorText.text = currentArtinformation.correct;
                dailyGrade = 2;
            }
            else if (picVal <= expensive)
            {
                professorText.text = currentArtinformation.tooLow;
                dailyGrade = 1;
            }
        }
        else if (sliderValueText.text == "Expensive")
        {
            if (picVal == fake)
            {
                professorText.text = currentArtinformation.itsFake;
                dailyGrade = 0;
            }
            else if (picVal < cheap)
            {
                professorText.text = currentArtinformation.wayTooHigh;
                dailyGrade = 0;
            }
            else if (picVal < mediocre)
            {
                professorText.text = currentArtinformation.tooHigh;
                dailyGrade = 1;
            }
            else if (picVal <= expensive)
            {
                professorText.text = currentArtinformation.correct;
                dailyGrade = 2;
            }
        }

        weeklyGrade[week] = weeklyGrade[week] + dailyGrade;
    }

    private void GiveFinalGrade()
    {
        foreach (int x in weeklyGrade)
        {
            totalGrade += x;
        }
        char grade;
        if (totalGrade == 50)
        {
            grade = 'A';
        }
        else if (totalGrade > 40)
        {
            grade = 'B';
        }
        else if (totalGrade > 30)
        {
            grade = 'C';
        }
        else if (totalGrade > 20)
        {
            grade = 'D';
        }
        else if (totalGrade > 10)
        {
            grade = 'F';
        }
        else
        {
            grade = 'F';
        }
        SwitchSpeechBubbleVisibility();
        feedbackBubbleText.text = "Your total grade is " + grade;
    }

    private void GiveGrade()
    {
        SwitchSpeechBubbleVisibility();
        char grade;
        if (weeklyGrade[week] == 10)
        {
            grade = 'A';
        }
        else if (weeklyGrade[week] > 8)
        {
            grade = 'B';
        }
        else if (weeklyGrade[week] > 6)
        {
            grade = 'C';
        }
        else if (weeklyGrade[week] > 4)
        {
            grade = 'D';
        }
        else if (weeklyGrade[week] > 2)
        {
            grade = 'F';
        }
        else
        {
            grade = 'F';
        }

        feedbackBubbleText.text = "Your weekly grade is " + grade;
        week += 1;

    }

    private void SwitchSpeechBubbleVisibility()
    {
        if (feedbackBubble.activeSelf == true) 
        {
            feedbackBubble.SetActive(false);
            traineeSpeechbubble.SetActive(true);
            professorSpeechbubble.SetActive(true);
        }
        else
        {
            feedbackBubble.SetActive(true);
            traineeSpeechbubble.SetActive(false);
            professorSpeechbubble.SetActive(false);
    }
    }
}
