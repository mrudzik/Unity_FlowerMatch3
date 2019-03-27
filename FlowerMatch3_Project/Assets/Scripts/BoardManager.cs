using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BoardManager : MonoBehaviour
{

	public GameObject		backBrick;
	public GameObject 		spawnBrick;
	public GameObject 		blockBrick;
	public GameObject 		flower;

	private Transform			brickHolder;

	public 	GameObject 			flowerGarden;
	private Transform			flowerHolder;
	
	[HideInInspector]
	public	GameObject[][] 		brickArray = new GameObject[rows][];
	public 	GameManager 		gameScript;


	public const int 		rows = 18;
	public const int 		columns = 5;
	public float 			tileStepX = 1.6f;
	public float 			tileStepY = 0.45f;

	private 	float 	SpawnTimer;
	private 	float 	stableTimer;


	// Start is called before the first frame update
	void Awake()
	{
		brickHolder = gameObject.GetComponent<Transform>();
		flowerHolder = flowerGarden.GetComponent<Transform>();
		SpawnTimer = 0f;
		stableTimer = 0f;
		SetupLevel();
		MakeConnections();

		// SpawnFlower(brickArray[rows - 1][columns - 1].transform.position);
	}
















	GameObject 	ChooseTile(int y, int x)
	{
		int rand;

		if (y == rows - 1)
		{
			rand = Random.Range(0, 10);
			if (x == 0 || x == columns - 1)
				return spawnBrick;
			if (rand > 4)
				return spawnBrick;
		}
		if (y < rows - 1 && (x < 2 || columns - 2 < x))
		{
			rand = Random.Range(0, 10);
			if (rand > 7)
				return blockBrick;
		}

		return backBrick;
	}


	void 	SetupLevel()
	{
		int x;
		int y;

		y = 0;
		while (y < rows)
		{
			brickArray[y] = new GameObject[columns];
			x = 0;
			while (x < columns)
			{
				GameObject toInstantiate;

				toInstantiate = ChooseTile(y, x);
				if (y % 2 == 1)
				{
					brickArray[y][x] = Instantiate(toInstantiate, new Vector3 (x * tileStepX + brickHolder.position.x + (tileStepX / 2), y * tileStepY + brickHolder.position.y, 0f), Quaternion.identity, brickHolder);
				}
				else
				{
					brickArray[y][x] = Instantiate(toInstantiate, new Vector3 (x * tileStepX + brickHolder.position.x, y * tileStepY + brickHolder.position.y, 0f), Quaternion.identity, brickHolder);
				}
				brickArray[y][x].GetComponent<BackTileScript>().gameScript = gameScript;
				x++;
			}
			y++;
		}
	}








//============================== Connection stuff ==============================//
//vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv//


	void 	SetBranches(int y, int x)
	{
		BackTileScript 	thisTile;

		thisTile = brickArray[y][x].GetComponent<BackTileScript>();
		//-----> Set parent
		if (y < rows - 2)
			thisTile.parent = brickArray[y + 2][x];

		//-----> Set bottom
		if (1 < y)
			thisTile.bottom = brickArray[y - 2][x];

		//-----> Set leftUp
		if (y < rows - 1)
		{
			if (y % 2 == 0)
			{
				if (x > 0)
					thisTile.leftUp = brickArray[y + 1][x - 1];
			}
			else
				thisTile.leftUp = brickArray[y + 1][x];
		}

		//		Set leftDown
		if (0 < y)
		{
			if (y % 2 == 0)
			{
				if (x > 0)
					thisTile.leftDown = brickArray[y - 1][x - 1];
			}
			else
				thisTile.leftDown = brickArray[y - 1][x];
		}

		// 		Set rightUp
		if (y < rows - 1)
		{
			if (y % 2 == 0)
				thisTile.rightUp = brickArray[y + 1][x];
			else
			{
				if (x < columns - 1)
					thisTile.rightUp = brickArray[y + 1][x + 1];
			}
		}

		// 		Set rightDown
		if (0 < y)
		{
			if (y % 2 == 0)
				thisTile.rightDown = brickArray[y - 1][x];
			else
			{
				if (x < columns - 1)
					thisTile.rightDown = brickArray[y - 1][x + 1];
			}
		}
	}

	void 	CutBlockBranches(int y, int x)
	{
		BackTileScript 	thisTile;

		thisTile = brickArray[y][x].GetComponent<BackTileScript>();
		
		if (thisTile.parent != null)
			if (thisTile.parent.tag == "BlockTile")
				thisTile.parent = null;

		if (thisTile.bottom != null)
			if (thisTile.bottom.tag == "BlockTile")
				thisTile.bottom = null;

		if (thisTile.leftDown != null)
			if (thisTile.leftDown.tag == "BlockTile")
				thisTile.leftDown = null;

		if (thisTile.leftUp != null)
			if (thisTile.leftUp.tag == "BlockTile")
				thisTile.leftUp = null;

		if (thisTile.rightDown != null)
			if (thisTile.rightDown.tag == "BlockTile")
				thisTile.rightDown = null;

		if (thisTile.rightUp != null)
			if (thisTile.rightUp.tag == "BlockTile")
				thisTile.rightUp = null;
	}


	void 	MakeConnections()
	{
		int x;
		int y;

		y = 0;
		while (y < rows)
		{
			x = 0;
			while (x < columns)
			{
				SetBranches(y, x);
				CutBlockBranches(y, x);
				x++;
			}
			y++;
		}
	}

//^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^//
//============================== Connections up there ============================== //









//vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv//
//========= Spawn Stuff ===========//

	

	void 	SpawnFlower(Vector3 toSpawnPos)
	{
		GameObject 		newFlower;
		FlowerScript 	newFlowerCode;

		newFlower = Instantiate(flower, new Vector3(toSpawnPos.x, toSpawnPos.y, -5), Quaternion.identity, flowerHolder);
		newFlowerCode = newFlower.GetComponent<FlowerScript>();

		newFlowerCode.gameScript = gameScript;		

	}

	int 	SpawnLine()
	{
		int x;
		int y;
		int spawned;

		spawned = 0;
		y = 0;
		while (y < rows)
		{
			x = 0;
			while (x < columns)
			{
				if (brickArray[y][x].tag == "SpawnTile")
					if (brickArray[y][x].GetComponent<BackTileScript>().inside == "None")
					{
						SpawnFlower(brickArray[y][x].transform.position);
						spawned++;
					}
				x++;
			}
			y++;
		}
		return spawned;
	}

	public bool		IsStableGrid()
	{
		int x;
		int y;
		int notReady;

		notReady = 0;
		y = 0;
		while (y < rows)
		{
			x = 0;
			while (x < columns)
			{
				string thisInsides;

				thisInsides = brickArray[y][x].GetComponent<BackTileScript>().inside;
				if (thisInsides == "Charging")
					notReady++;
				if (thisInsides != "Charging" && thisInsides != "None")
					if (!brickArray[y][x].GetComponent<BackTileScript>().flower.GetComponent<FlowerScript>().ready)
						notReady++;
				x++;
			}
			y++;
		}

		if (notReady > 0)
			return false;
		return true;
	}


	public bool		SpawnLogic(float spawnDelay)
	{
		if (SpawnTimer > spawnDelay)
		{
			SpawnTimer = 0;
			if (SpawnLine() == 0)
				if (IsStableGrid())
				{
					Debug.Log(stableTimer);
					stableTimer += 0.35f;
					if (stableTimer > 1)
						return false;
				}
			else
				stableTimer = 0;
		}
		else
		{
			SpawnTimer += 2 * Time.deltaTime;
		}

		return true;
	}


//========= Spawn up there ========//
//^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^//









//========= ========= Match Stuff ======== ========= //
//vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv //

	bool 	DoMatchStuff(int y, int x)
	{
		string 			thisInsides;
		GameObject 		thisTile;
		bool 			foundSomething;
		
		GameObject 	tempTile1;
		GameObject 	tempTile2;


		foundSomething = false;
		thisTile = brickArray[y][x];
		thisInsides = thisTile.GetComponent<BackTileScript>().inside;

		if (thisInsides == "None" || thisInsides == "Charging")
			return false;

		// Realization Up Line
		tempTile1 = thisTile.GetComponent<BackTileScript>().parent;
		if (tempTile1 != null)
		{
			if (tempTile1.GetComponent<BackTileScript>().inside == thisInsides)
			{
				tempTile2 = tempTile1.GetComponent<BackTileScript>().parent;
				if (tempTile2 != null)
				{
					if (tempTile2.GetComponent<BackTileScript>().inside == thisInsides)
					{
						foundSomething = true;
						thisTile.GetComponent<BackTileScript>().killProtocol = true;
						tempTile1.GetComponent<BackTileScript>().killProtocol = true;
						tempTile2.GetComponent<BackTileScript>().killProtocol = true;
						while (tempTile2.GetComponent<BackTileScript>().parent != null)
						{
							tempTile2 = tempTile2.GetComponent<BackTileScript>().parent;
							if (tempTile2.GetComponent<BackTileScript>().inside == thisInsides)
								tempTile2.GetComponent<BackTileScript>().killProtocol = true;
							else
								break;
						}
					}

				}
			}
		}
	
		// Realization Up Left line
		tempTile1 = thisTile.GetComponent<BackTileScript>().leftUp;
		if (tempTile1 != null)
		{
			if (tempTile1.GetComponent<BackTileScript>().inside == thisInsides)
			{
				tempTile2 = tempTile1.GetComponent<BackTileScript>().leftUp;
				if (tempTile2 != null)
				{
					if (tempTile2.GetComponent<BackTileScript>().inside == thisInsides)
					{
						foundSomething = true;
						thisTile.GetComponent<BackTileScript>().killProtocol = true;
						tempTile1.GetComponent<BackTileScript>().killProtocol = true;
						tempTile2.GetComponent<BackTileScript>().killProtocol = true;
						while (tempTile2.GetComponent<BackTileScript>().leftUp != null)
						{
							tempTile2 = tempTile2.GetComponent<BackTileScript>().leftUp;
							if (tempTile2.GetComponent<BackTileScript>().inside == thisInsides)
								tempTile2.GetComponent<BackTileScript>().killProtocol = true;
							else
								break;
						}
					}

				}
			}
		}

	// Realization Up Right
		tempTile1 = thisTile.GetComponent<BackTileScript>().rightUp;
		if (tempTile1 != null)
		{
			if (tempTile1.GetComponent<BackTileScript>().inside == thisInsides)
			{
				tempTile2 = tempTile1.GetComponent<BackTileScript>().rightUp;
				if (tempTile2 != null)
				{
					if (tempTile2.GetComponent<BackTileScript>().inside == thisInsides)
					{
						foundSomething = true;
						thisTile.GetComponent<BackTileScript>().killProtocol = true;
						tempTile1.GetComponent<BackTileScript>().killProtocol = true;
						tempTile2.GetComponent<BackTileScript>().killProtocol = true;
						while (tempTile2.GetComponent<BackTileScript>().rightUp != null)
						{
							tempTile2 = tempTile2.GetComponent<BackTileScript>().rightUp;
							if (tempTile2.GetComponent<BackTileScript>().inside == thisInsides)
								tempTile2.GetComponent<BackTileScript>().killProtocol = true;
							else
								break;
						}
					}

				}
			}
		}
/* - - - - - - HARD CODED? - - - - - - */
		return foundSomething;
	}

	public bool 	MatchLogic()
	{
		int 	x;
		int 	y;
		bool 	foundSomething;

		foundSomething = false;
		y = 0;
		while (y < rows)
		{
			x = 0;
			while (x < columns)
			{
				if (DoMatchStuff(y, x))
					foundSomething = true;
				x++;
			}
			y++;
		}

		return foundSomething;
	}
	

//========= ========= Match Stuff ======== ========= //
//^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ //


	public 	void 	ResetSelection()
	{
		int x;
		int y;

		y = 0;
		while (y < rows)
		{
			x = 0;
			while (x < columns)
			{
				brickArray[y][x].GetComponent<BackTileScript>().selected = false;
				// brickArray[y][x].GetComponent<BackTileScript>().gameObject.transform.Find("Circle_Particles").GetComponent<ParticleSystem>().Stop();

				x++;
			}
			y++;
		}
		gameScript.clickedTiles = 0;
	}


	bool SwapWillMatch(GameObject toSwap1, GameObject toSwap2)
	{
		bool 	willMatch;
		string 	thisInsides;

		GameObject tempTile;

		willMatch = false;
		thisInsides = toSwap1.GetComponent<BackTileScript>().inside;

		// 	Need for correct checks
		toSwap1.GetComponent<BackTileScript>().inside = toSwap2.GetComponent<BackTileScript>().inside;
		toSwap2.GetComponent<BackTileScript>().inside = thisInsides;

		//		Check Patterns

		//	xff in all six directions;
		//	fxf in three directions;

		//xff

		tempTile = toSwap2.GetComponent<BackTileScript>().parent;
		if (tempTile != null)
			// if (tempTile == toSwap1)
				// tempTile
			if (tempTile.GetComponent<BackTileScript>().inside == thisInsides)
			{
				tempTile = tempTile.GetComponent<BackTileScript>().parent;
				if (tempTile != null)
					if (tempTile.GetComponent<BackTileScript>().inside == thisInsides)
						willMatch = true;
			}

		tempTile = toSwap2.GetComponent<BackTileScript>().bottom;
		if (tempTile != null)
			if (tempTile.GetComponent<BackTileScript>().inside == thisInsides)
			{
				tempTile = tempTile.GetComponent<BackTileScript>().bottom;
				if (tempTile != null)
					if (tempTile.GetComponent<BackTileScript>().inside == thisInsides)
						willMatch = true;
			}

		tempTile = toSwap2.GetComponent<BackTileScript>().leftUp;
		if (tempTile != null)
			if (tempTile.GetComponent<BackTileScript>().inside == thisInsides)
			{
				tempTile = tempTile.GetComponent<BackTileScript>().leftUp;
				if (tempTile != null)
					if (tempTile.GetComponent<BackTileScript>().inside == thisInsides)
						willMatch = true;
			}

		tempTile = toSwap2.GetComponent<BackTileScript>().leftDown;
		if (tempTile != null)
			if (tempTile.GetComponent<BackTileScript>().inside == thisInsides)
			{
				tempTile = tempTile.GetComponent<BackTileScript>().leftDown;
				if (tempTile != null)
					if (tempTile.GetComponent<BackTileScript>().inside == thisInsides)
						willMatch = true;
			}

		tempTile = toSwap2.GetComponent<BackTileScript>().rightUp;
		if (tempTile != null)
			if (tempTile.GetComponent<BackTileScript>().inside == thisInsides)
			{
				tempTile = tempTile.GetComponent<BackTileScript>().rightUp;
				if (tempTile != null)
					if (tempTile.GetComponent<BackTileScript>().inside == thisInsides)
						willMatch = true;
			}

		tempTile = toSwap2.GetComponent<BackTileScript>().rightDown;
		if (tempTile != null)
			if (tempTile.GetComponent<BackTileScript>().inside == thisInsides)
			{
				tempTile = tempTile.GetComponent<BackTileScript>().rightDown;
				if (tempTile != null)
					if (tempTile.GetComponent<BackTileScript>().inside == thisInsides)
						willMatch = true;
			}




		//fxf

		tempTile = toSwap2.GetComponent<BackTileScript>().parent;
		if (tempTile != null)
			if (tempTile.GetComponent<BackTileScript>().inside == thisInsides)
			{
				tempTile = toSwap2.GetComponent<BackTileScript>().bottom;
				if (tempTile != null)
					if (tempTile.GetComponent<BackTileScript>().inside == thisInsides)
						willMatch = true;
			}

		tempTile = toSwap2.GetComponent<BackTileScript>().leftUp;
		if (tempTile != null)
			if (tempTile.GetComponent<BackTileScript>().inside == thisInsides)
			{
				tempTile = toSwap2.GetComponent<BackTileScript>().rightDown;
				if (tempTile != null)
					if (tempTile.GetComponent<BackTileScript>().inside == thisInsides)
						willMatch = true;
			}

		tempTile = toSwap2.GetComponent<BackTileScript>().rightUp;
		if (tempTile != null)
			if (tempTile.GetComponent<BackTileScript>().inside == thisInsides)
			{
				tempTile = toSwap2.GetComponent<BackTileScript>().leftDown;
				if (tempTile != null)
					if (tempTile.GetComponent<BackTileScript>().inside == thisInsides)
						willMatch = true;
			}

		// 		Turning back insides;
		toSwap2.GetComponent<BackTileScript>().inside = toSwap1.GetComponent<BackTileScript>().inside;
		toSwap1.GetComponent<BackTileScript>().inside = thisInsides;

		return willMatch;
	}

	void SetShake(GameObject toSwap1, GameObject toSwap2)
	{
		if (toSwap1 != null)
		{
			toSwap1.GetComponent<BackTileScript>().shakeFlower = true;
			toSwap1.GetComponent<BackTileScript>().shakeTimer = 0;
		}
		if (toSwap2 != null)
		{
			toSwap2.GetComponent<BackTileScript>().shakeFlower = true;
			toSwap2.GetComponent<BackTileScript>().shakeTimer = 0;
		}	
	}

	void SetSwapMove(GameObject toSwap1, GameObject toSwap2)
	{
		toSwap1.GetComponent<BackTileScript>().SetMove(toSwap2.transform.position);
		toSwap1.GetComponent<BackTileScript>().inside = "Charging";

		toSwap2.GetComponent<BackTileScript>().SetMove(toSwap1.transform.position);
		toSwap2.GetComponent<BackTileScript>().inside = "Charging";
	}

	bool RealSwap(GameObject toSwap1, GameObject toSwap2)
	{
		bool needChange = false;


		if (toSwap1.GetComponent<BackTileScript>().parent != null)
			if (toSwap1.GetComponent<BackTileScript>().parent.transform.position == toSwap2.transform.position)
				needChange = true;
		if (toSwap1.GetComponent<BackTileScript>().bottom != null)
			if (toSwap1.GetComponent<BackTileScript>().bottom.transform.position == toSwap2.transform.position)
				needChange = true;
		if (toSwap1.GetComponent<BackTileScript>().leftUp != null)
			if (toSwap1.GetComponent<BackTileScript>().leftUp.transform.position == toSwap2.transform.position)
				needChange = true;
		if (toSwap1.GetComponent<BackTileScript>().leftDown != null)
			if (toSwap1.GetComponent<BackTileScript>().leftDown.transform.position == toSwap2.transform.position)
				needChange = true;
		if (toSwap1.GetComponent<BackTileScript>().rightUp != null)
			if (toSwap1.GetComponent<BackTileScript>().rightUp.transform.position == toSwap2.transform.position)
				needChange = true;
		if (toSwap1.GetComponent<BackTileScript>().rightDown != null)
			if (toSwap1.GetComponent<BackTileScript>().rightDown.transform.position == toSwap2.transform.position)
				needChange = true;


		// This will be TRUE if selected neighboring Tiles
		if (needChange)
		{
			if (!SwapWillMatch(toSwap1, toSwap2))
				if (!SwapWillMatch(toSwap2, toSwap1))
				{
				//	This is a case when swap will not do match at all IT'S NOT OK	
					// This will swap flowers
					SetSwapMove(toSwap1, toSwap2);
					// This will make them return;
					toSwap1.GetComponent<BackTileScript>().SetReturnMove();
					toSwap2.GetComponent<BackTileScript>().SetReturnMove();

					return false;
				}
		// This is a case when must be changed IT'S OK
			
			SetSwapMove(toSwap1, toSwap2);

			return true;
		}

		// This is a case when selected far away tiles IT'S NOT OK
		SetShake(toSwap1, toSwap2);

		return false;
	}


	public	bool 	SwapTiles()
	{
		int x;
		int y;

		GameObject 	toSwap1 = null;
		GameObject 	toSwap2 = null;

		y = 0;
		while (y < rows)
		{
			x = 0;
			while (x < columns)
			{
				if (brickArray[y][x].GetComponent<BackTileScript>().selected)
				{
					if (toSwap1 == null)
						toSwap1 = brickArray[y][x];
					else if (toSwap2 == null)
					{
						toSwap2 = brickArray[y][x];
						break;
					}
				}
				x++;
			}
			y++;
		}

		if (toSwap2 != null)
		{
			RealSwap(toSwap1, toSwap2);
			return true;
		}
		return false;

	}


	public 	void 	SpecialAbility(string type)
	{
		int x;
		int y;

		y = 0;
		while (y < rows)
		{
			x = 0;
			while (x < columns)
			{

				if (brickArray[y][x].GetComponent<BackTileScript>().inside == type)
					brickArray[y][x].GetComponent<BackTileScript>().killProtocol = true;
				
				x++;
			}
			y++;
		}

		gameScript.turnForPlayer = false;
		gameScript.turnToFall = true;
		gameScript.timerBeforeFall = 0;
		gameScript.timerBeforePlayer = 0;
		ResetSelection();
	}








	void 	KillAllTiles()
	{
		int x;
		int y;

		y = 0;
		while (y < rows)
		{
			x = 0;
			while (x < columns)
			{
				GameObject tempFlower;

				tempFlower = brickArray[y][x].GetComponent<BackTileScript>().flower;
				if (tempFlower != null)
					Destroy(tempFlower);
				Destroy(brickArray[y][x]);

				x++;
			}
			y++;
		}
	}



	public void RestartProtocol()
	{
		
		KillAllTiles();


		SpawnTimer = 0f;
		stableTimer = 0f;
		SetupLevel();
		MakeConnections();

	}




}
