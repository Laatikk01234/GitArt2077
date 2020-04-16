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

    public bool Graded = false;

    //endings here as variables?
    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void SetVariables(int fakes, bool started, int totalGrade)
    {
        hippyEnding = !started;
        if (fakes >= 8)
        {
            fakeEnding = true;
        }
        if (!fakeEnding && !hippyEnding)
        {
            normalEnding = true;
        }
        endingGrade = totalGrade;
    }

    //void OnLevelWasLoaded(){
     //   Debug.Log("onlevelwasloaded");
     //   fakeEnding = false;
     //   hippyEnding = false;
    //    normalEnding = false;
     //   endingGrade = 0;

   // }

    
}
