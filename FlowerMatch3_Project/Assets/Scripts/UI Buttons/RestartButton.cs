using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
	public GameManager 	gameScript;
	public BoardManager boardScript;
 	public PanelOpener 	panelScript;
 	public Petal_Logic 	petalScript;

	public void 	RestartEntireGame()
	{
		panelScript.OpenPanel();
		gameScript.GameInit();
		boardScript.RestartProtocol();
		petalScript.ResetPetals();
	}
}
