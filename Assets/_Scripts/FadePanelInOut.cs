//Attach this script to a GameObject
//Create an Image GameObject by going to Create>UI>Image. Attach this Image to the Image field in your GameObject’s Inspector window.
//This script creates a toggle that fades an Image in and out.
using UnityEngine;
using UnityEngine.UI;

public class FadePanelInOut : MonoBehaviour
{
    //Attach an Image you want to fade in the GameObject's Inspector
    public Image m_Image;
    //Use this to tell if the toggle returns true or false
    public bool m_Fading;
    public float duration= 2.0f;

    void Update()
    {
        //If the toggle returns true, fade in the Image
        if (m_Fading == true)
        {
            //Fully fade in Image (1) with the duration of 2
            m_Image.CrossFadeAlpha(1, duration/4, false);
        }
        //If the toggle is false, fade out to nothing (0) the Image with a duration of 2
        if (m_Fading == false)
        {
            m_Image.CrossFadeAlpha(0, duration, false);
        }
    }
}