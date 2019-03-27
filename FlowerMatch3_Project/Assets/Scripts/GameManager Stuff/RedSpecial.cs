using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedSpecial : MonoBehaviour
{
	private 	Petal_Logic 	petalScript;

    public 		Button 			redButton;
	public 		Button 			yellowButton;
	public 		Button 			greenButton;
	public 		Button 			blueButton;
	public 		Button 			purpleButton;
	public 		Button 			whiteButton;



  	// public void 	RedAbility()
  	// {
  	// 	redButton.interactable = false;

  	// 	petalScript.redPetals -= petalScript.petalMax;
  	// 	Debug.Log("Boom Red!");
  	// 	return;
  	// }

  	// public void 	YellowAbility()
  	// {
  	// 	redButton.interactable = false;

  	// 	petalScript.redPetals -= petalScript.petalMax;
  	// 	Debug.Log("Boom Red!");
  	// 	return;
  	// }

  	public void 	TempAbility(string type)
  	{
  		if (!petalScript.gameScript.turnForPlayer)
  			return;

  		if (type == "Red")
  		{
  			petalScript.redPetals = 0;
  			redButton.interactable = false;
  		}
  		if (type == "Yellow")
		{
  			petalScript.yellowPetals = 0;
  			yellowButton.interactable = false;
		}
  		if (type == "Green")
  		{
  			petalScript.greenPetals = 0;
  			greenButton.interactable = false;
  		}
  		if (type == "Blue")
  		{
  			petalScript.bluePetals = 0;
  			blueButton.interactable = false;
  		}
  		if (type == "Purple")
  		{
  			petalScript.purplePetals = 0;
  			purpleButton.interactable = false;
  		}
  		if (type == "White")
  		{
  			petalScript.whitePetals = 0;
  			whiteButton.interactable = false;
  		}
  		petalScript.gameScript.boardScript.SpecialAbility(type);

  		return;
  	}

    void Start()
    {
        petalScript = GetComponent<Petal_Logic>();
        
    }

    void 	CheckButton(Button tempButton, int tempPetals)
    {
    	if (tempPetals >= petalScript.petalMax)
        	tempButton.interactable = true;
        else
        	tempButton.interactable = false;

    }

    void Update()
    {
    	CheckButton(redButton, petalScript.redPetals);
    	CheckButton(yellowButton, petalScript.yellowPetals);
    	CheckButton(greenButton, petalScript.greenPetals);
    	CheckButton(blueButton, petalScript.bluePetals);
    	CheckButton(purpleButton, petalScript.purplePetals);
    	CheckButton(whiteButton, petalScript.whitePetals);

    }
}
