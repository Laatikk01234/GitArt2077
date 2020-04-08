using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artinformation : MonoBehaviour
{
    public Sprite picture;
    public string artName;
    public int pictureValue;
    public string traineeSays;
    public string professorSays;
    public string correct;
    public string notFake;
    public string itsFake;
    public string tooLow;
    public string wayTooLow;
    public string tooHigh;
    public string wayTooHigh;

    public Sprite Picture
    {
        get { return picture; }
        set => picture = value;
    }

    // miten vastauksia?

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
