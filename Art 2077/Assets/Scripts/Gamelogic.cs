using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gamelogic : MonoBehaviour
{
    public List<GameObject> art;

    public GameObject trainee;
    private TextMeshPro traineeText;

    public GameObject professor;
    private TextMeshPro professorText;

    public Slider artValueSlider;

    public GameObject sliderValue;
    private TextMeshPro sliderValueText;
    // artValueSlider.value -> pitääkö vielä ajaa slideristä changevalueta?
    public Button confirm; 

    private void Start()
    {
        traineeText = trainee.GetComponent<TextMeshPro>();

        professorText = professor.GetComponent<TextMeshPro>();

        sliderValueText = sliderValue.GetComponent<TextMeshPro>();

        Button btn = confirm.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    public void sliderValueChange()
    {
        string message = artValueSlider.value + " Dollars";
        sliderValueText.text = message;
        
    }
    // both of these work for clicking
    void TaskOnClick()
    {
        // ok clicked -> give response and then change art piece

    }




}
