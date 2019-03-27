using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Petal_Logic : MonoBehaviour
{

	public	GameManager 	gameScript;



	[HideInInspector]
	public 	int 		redPetals = 0;
	public	Text 		redPetal_Text;
	public 	Slider 		redSlider;

	[HideInInspector]
	public 	int 		yellowPetals = 0;
	public	Text 		yellowPetal_Text;
	public 	Slider 		yellowSlider;

	[HideInInspector]
	public 	int 		greenPetals = 0;
	public	Text 		greenPetal_Text;
	public 	Slider 		greenSlider;

	[HideInInspector]
	public 	int 		bluePetals = 0;
	public	Text 		bluePetal_Text;
	public 	Slider 		blueSlider;

	[HideInInspector]
	public 	int 		purplePetals = 0;
	public	Text 		purplePetal_Text;
	public 	Slider 		purpleSlider;

	[HideInInspector]
	public 	int 		whitePetals = 0;
	public	Text 		whitePetal_Text;
	public 	Slider 		whiteSlider;



	public 	int  	petalMax = 30;
	[HideInInspector]
	public	bool 	progressChanged = true;



	float 	GetPetalPercent(int petal)
	{
		return ((float)petal / (float)petalMax);
	}

	void 	SetPetalData(Text petalText, string text, Slider petalSlider, int petalCount)
	{
		petalSlider.value = GetPetalPercent(petalCount);
		if (petalCount >= petalMax)
			petalText.text = "Ready " + text;
		else
			petalText.text = petalCount + " / " + petalMax + " " + text;
		
	}

	public void 	UpdateHudWithPetals()
	{
		SetPetalData(redPetal_Text, "Red", redSlider, redPetals);
		SetPetalData(yellowPetal_Text, "Yellow", yellowSlider, yellowPetals);
		SetPetalData(greenPetal_Text, "Green", greenSlider, greenPetals);
		SetPetalData(bluePetal_Text, "Blue", blueSlider, bluePetals);
		SetPetalData(purplePetal_Text, "Purple", purpleSlider, purplePetals);
		SetPetalData(whitePetal_Text, "White", whiteSlider, whitePetals);
	}

	public void 	ResetPetals()
	{
		redPetals = 0;
		yellowPetals = 0;
		greenPetals = 0;
		bluePetals = 0;
		purplePetals = 0;
		whitePetals = 0;
	}

}
