using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedSlider : MonoBehaviour
{
	private 	Slider 	sliderScript;
	public 		Text	sliderText;

	// private 	float 	sliderValue;

	void 	Start()
	{
		sliderScript = GetComponent<Slider>();
	}

	public void 	ChangeSpeed()
	{
		if (sliderScript != null)
    		Time.timeScale = sliderScript.value;
	}

    void 	Update()
    {
    	if (sliderScript != null)
    	{
    		if (sliderScript.value < 0.1f)
    			sliderScript.value = 0.1f;
    		// sliderValue = (float)sliderScript.value;
        	sliderText.text = "Speed : " + sliderScript.value;
    	}
    }
}
