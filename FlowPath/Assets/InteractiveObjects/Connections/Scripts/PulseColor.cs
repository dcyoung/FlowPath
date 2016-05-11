using UnityEngine;
using System.Collections;

public class PulseColor : MonoBehaviour
{

    //These are the colors that need you need to choose in the editor, the current color does not need to be picked.  
    public Color CurrentColor, StartingColor, EndingColor;
    //The colortime will be used to make te object change colors and does not need to be edited.  
    public float ColorTime;
    //These options will be needed to make sure the colors switch.  
    bool Swap = true, HoldColor = false;
    //will make a color stay at full color longer.  
    float holdTime;

    public float riseRate = 0.02f;
    public float fallRate = 0.02f;


    void Start()
    {
        //makes sure the starting color is the begin color.  
        CurrentColor = StartingColor;
        //The next to lines are the most import important ones in this script, the first line makes sure the object can even have emission and the second one makes sure we can use the emission map.  
        GetComponent<MeshRenderer>().material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
        GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
    }

    void FixedUpdate()
    {
  
        //This part is kind of complex maybe but what is does is the following.   
        //first it checks the current color needs to be put on hold so that is can be seen at color full longer.  
        if (HoldColor == true)
        {
            //After that we check how long the color has been active and we are ready to start the switching process  
            if (Time.time >= holdTime)
            {
                //Here we do a final check which color we currently have and to which we have to switch.  
                if (Swap == true)
                {
                    //Here we start switching colors and tell the game to stop holding this color.  
                    Swap = false;
                    ColorTime -= 0.1f;
                    HoldColor = false;
                }
                else
                {
                    Swap = true;
                    ColorTime = 0.001f;
                    HoldColor = false;
                }
            }
        }
        //Now that we are no longer switching colors we have to make sure that the color will still change.  
        else
        {
            //Again we check which color it needs to become and tell it to keep coming the other color  
            if (Swap == true)
            {
                ColorTime += riseRate;
            }
            else
            {
                ColorTime -= fallRate;
            }

            //Here we make sure that when the color reaches it's full color that it will switch to the other color after holding own to it for some time.  
            if (CurrentColor == EndingColor)
            {
                HoldColor = true;
                holdTime = Time.time + .1f;
            }
            else if (CurrentColor == StartingColor)
            {
                HoldColor = true;
                holdTime = Time.time + 0.1f;
            }
        }

        //Finaly we have the last part of the script, here we tell our shader that our current color which we are switching between the whole time, is the color we want to use for our emission map.  
        CurrentColor = Color.Lerp(StartingColor, EndingColor, ColorTime);
        GetComponent<MeshRenderer>().material.color = CurrentColor;
        GetComponent<MeshRenderer>().material.SetColor("_EMISSION", CurrentColor);
    }
}
