using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneManager : MonoBehaviour
{
    private GameObject endingInfo;
    public GameObject textlocation;
    public GameObject backgroundImage;
    public GameObject extraSpriteLocation;
    public string hippyEndingText;
    public Sprite hippyEndingSprite;
    public string fakeEndingText;
    public Sprite fakeEndingSprite;
    public Sprite normalEndingSprite;
    public string normalEndingA;
    public Sprite normalEndingASprite;
    public string normalEndingB;
    public Sprite normalEndingBSprite;
    public string normalEndingC;
    public Sprite normalEndingCSprite;
    public string normalEndingD;
    public Sprite normalEndingDSprite;
    public string normalEndingF;
    public Sprite normalEndingFSprite;
    // Start is called before the first frame update

    private AudioSource audioSource;

    void Start()
    {

        endingInfo = GameObject.Find("DontDestroyOnLoad");
        EndingInfo ending = endingInfo.GetComponent<EndingInfo>();

        audioSource = this.GetComponent<AudioSource>();


        if (ending.hippyEnding)
        {
            textlocation.GetComponent<TextMeshProUGUI>().text = hippyEndingText;
            extraSpriteLocation.GetComponent<SpriteRenderer>().sprite = hippyEndingSprite;
        }

        else if (ending.fakeEnding)
        {
                audioSource.Play();
                textlocation.GetComponent<TextMeshProUGUI>().text = fakeEndingText;
                extraSpriteLocation.GetComponent<SpriteRenderer>().sprite = fakeEndingSprite;
        }
        else if (ending.endingGrade == 50)
        {
            textlocation.GetComponent<TextMeshProUGUI>().text = normalEndingA;
            extraSpriteLocation.GetComponent<SpriteRenderer>().sprite = normalEndingASprite;
        }

        else if (ending.endingGrade >= 40)
        {
            textlocation.GetComponent<TextMeshProUGUI>().text = normalEndingB;
            extraSpriteLocation.GetComponent<SpriteRenderer>().sprite = normalEndingBSprite;
        }

        else if (ending.endingGrade >= 30)
        {
            textlocation.GetComponent<TextMeshProUGUI>().text = normalEndingC;
            extraSpriteLocation.GetComponent<SpriteRenderer>().sprite = normalEndingCSprite;
        }

        else if (ending.endingGrade >= 20)
        {
            textlocation.GetComponent<TextMeshProUGUI>().text = normalEndingD;
            extraSpriteLocation.GetComponent<SpriteRenderer>().sprite = normalEndingDSprite;
        }
        else
        {
            textlocation.GetComponent<TextMeshProUGUI>().text = normalEndingF;
            extraSpriteLocation.GetComponent<SpriteRenderer>().sprite = normalEndingFSprite;

        }
    }

    // Update is called once per frame

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
