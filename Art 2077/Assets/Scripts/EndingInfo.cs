using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public bool fakeEnding = false;
    public bool hippyEnding = false;
    public bool normalEnding = false;
    public int endingGrade;

    //endings here as variables?
    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void SetVariables(bool allfakes, bool started, int totalGrade)
    {
        fakeEnding = allfakes;
        hippyEnding = !started;
        if (!fakeEnding && !hippyEnding)
        {
            normalEnding = true;
        }
        endingGrade = totalGrade;
    }
}
