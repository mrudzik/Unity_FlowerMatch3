using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public BoardManager 	boardScript;
	// [HideInInspector]
	public Petal_Logic 		petalScript;

	


	[HideInInspector]
	public bool 	turnToFall;
		[HideInInspector]
		public float 	timerBeforeFall;
		public float 	delayBeforeFall = 3;
		public 	float 	spawnDelay = 1;



	[HideInInspector]
	public bool 	turnToMatch;
		[HideInInspector]
		public float 	timerBeforePlayer;

	[HideInInspector]
	public bool		turnForPlayer;
		[HideInInspector]
		public int 		clickedTiles;



	public Text 		scoreText;
		[HideInInspector]
		public int 			score = 0;
	public Text 		timeText;
		[HideInInspector]
		public float		timeSeconds = 0;
		[HideInInspector]
		public int 			timeMinutes = 0;
		// private bool 	tilesDestroyed;
		// private float 	timerBeforeMatch;
	


	// Start is called before the first frame update
	public void		GameInit()
	{
		turnToFall = true;
		timerBeforeFall = 2;

		turnToMatch = false;
		timerBeforePlayer = 0;

		turnForPlayer = false;
		clickedTiles = 0;
		score = 0;
		timeSeconds = 0;
		timeMinutes = 0;
	}

	void Start()
	{
		GameInit();

		//-----> For Testing purposes <--------//

		// thisTile = boardScript.brickArray[2][2];
		// thisSprite = thisTile.GetComponent<SpriteRenderer>();
		// thisSprite.sprite = selectedOne;

		//-------------------------------------//

	}





	void 	FallGameLogic()
	{
		if (turnToFall)
		{
			if (timerBeforeFall > delayBeforeFall)
			{
				if (!boardScript.SpawnLogic(spawnDelay))
				{
					turnToFall = false;
					turnToMatch = true;
				}
			}
			else
				timerBeforeFall += 1 * Time.deltaTime;
		}
	}


	void 	MatchGameLogic()
	{
		if (turnToMatch)
		{
			if (boardScript.IsStableGrid())
			{
				Debug.Log("Time to Match! " + timerBeforePlayer);
				
				if (boardScript.MatchLogic())
				{
					turnToMatch = false;
					turnToFall = true;
					timerBeforeFall = 0;
					timerBeforePlayer = 0;
					// tilesDestroyed = true;
				}
				else
				{
					if (timerBeforePlayer > 1)
					{
						turnToMatch = false;
						turnForPlayer = true;
					}
					else
					 	timerBeforePlayer += 2 * Time.deltaTime;
				}
			}
			
		}
	}

	void 	PlayerGameLogic()
	{
		if (turnForPlayer)
		{

			// Debug.Log("Time to Play! " + clickedTiles);

// Next Thing to Realize!!!! VVVVV

			if (clickedTiles > 1)
			{
				if (boardScript.SwapTiles())
				{
					turnForPlayer = false;
					turnToMatch = true;
					timerBeforePlayer = 0;
					// tilesDestroyed = false;
					// MatchGameLogic();
				}
				boardScript.ResetSelection();
			}
			
//// 		Here 	^^^^^^		
			
		}
	}

	public 	void 	AddPetals(string petalType, int count)
	{
		if (petalScript == null)
			return;

		petalScript.progressChanged = true;
		if (petalType == "Red")
			petalScript.redPetals += count;
		if (petalType == "Yellow")
			petalScript.yellowPetals += count;
		if (petalType == "Green")
			petalScript.greenPetals += count;
		if (petalType == "Blue")
			petalScript.bluePetals += count;
		if (petalType == "Purple")
			petalScript.purplePetals += count;
		if (petalType == "White")
			petalScript.whitePetals += count;

	}

	void 	UpdateHud()
	{
		int tempSeconds;

		// 	SCORE
		scoreText.text = "Score  " + score;
		
		// 	TIME
		if (timeSeconds >= 60)
		{
			timeSeconds = 0;
			timeMinutes++;
		}
		else
			timeSeconds += Time.deltaTime;
		tempSeconds = (int)timeSeconds;
		timeText.text = "Time  ";
		if (timeMinutes < 10)
			timeText.text += "0" + timeMinutes;
		else
			timeText.text += timeMinutes;
		timeText.text += ":";
		if (timeSeconds < 10)
			timeText.text += "0" + tempSeconds;
		else
			timeText.text += tempSeconds;


		// 	PETALS
		if (petalScript != null)
			petalScript.UpdateHudWithPetals();
	}

	// Update is called once per frame
	void 	GameLogic()
	{
		PlayerGameLogic();

		FallGameLogic();
		MatchGameLogic();
		
		UpdateHud();

	}

	void Update()
	{
		GameLogic();
		// TestGrid_Connections();
	}

















	// ---> For Testing <--- //

	// public 	Sprite 	goodOne;
	// public 	Sprite 	selectedOne;

	// private 	float 			testTimer = 0;
	// private 	GameObject 		thisTile;
	// private 	SpriteRenderer	thisSprite;

	// ----------------------//


	// private void 	TestGrid_Connections()
	// {
	// 	float horizontal;
	// 	float vertical;

	// 	if (testTimer > 1)
	// 	{
	// 		vertical = Input.GetAxisRaw("Vertical");
	// 		horizontal = Input.GetAxisRaw("Horizontal");
			
	// 		if (vertical != 0)
	// 		{
	// 			Debug.Log("Pressed horizontal " + horizontal + " and vertical " + vertical);
	// 			thisSprite = thisTile.GetComponent<SpriteRenderer>();
	// 			thisSprite.sprite = goodOne;
	// 			testTimer = 0;
	// 			if (vertical == 1 && horizontal == 0)
	// 				if (thisTile.GetComponent<BackTileScript>().parent != null)
	// 					thisTile = thisTile.GetComponent<BackTileScript>().parent;
	// 			if (vertical == -1 && horizontal == 0)
	// 				if (thisTile.GetComponent<BackTileScript>().bottom != null)
	// 					thisTile = thisTile.GetComponent<BackTileScript>().bottom;
	// 			if (vertical == 1 && horizontal == 1)
	// 				if (thisTile.GetComponent<BackTileScript>().rightUp != null)
	// 					thisTile = thisTile.GetComponent<BackTileScript>().rightUp;
	// 			if (vertical == 1 && horizontal == -1)
	// 				if (thisTile.GetComponent<BackTileScript>().leftUp != null)
	// 					thisTile = thisTile.GetComponent<BackTileScript>().leftUp;
	// 			if (vertical == -1 && horizontal == 1)
	// 				if (thisTile.GetComponent<BackTileScript>().rightDown != null)
	// 					thisTile = thisTile.GetComponent<BackTileScript>().rightDown;
	// 			if (vertical == -1 && horizontal == -1)
	// 				if (thisTile.GetComponent<BackTileScript>().leftDown != null)
	// 					thisTile = thisTile.GetComponent<BackTileScript>().leftDown;
				
	// 			thisSprite = thisTile.GetComponent<SpriteRenderer>();
	// 			thisSprite.sprite = selectedOne;
	// 		}
	// 	}
	// 	else
	// 	{
	// 		testTimer += 2 * Time.deltaTime;
	// 	}
	// 	Debug.Log("Timer " + testTimer);
	// }

}
