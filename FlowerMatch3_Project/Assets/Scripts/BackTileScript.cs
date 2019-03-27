using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [Serializable]
public class BackTileScript : MonoBehaviour//, ICloneable
{

//=======================
	[HideInInspector]
	public GameObject 	parent = null;
	[HideInInspector]
	public GameObject 	bottom = null;

	[HideInInspector]
	public GameObject 	leftUp = null;
	[HideInInspector]
	public GameObject 	leftDown = null;

	[HideInInspector]
	public GameObject 	rightUp = null;
	[HideInInspector]
	public GameObject 	rightDown = null;
//======================


	[HideInInspector]
	public 	string 		inside;
	[HideInInspector]
	public 	GameObject 	flower;
	[HideInInspector]
	public 	bool 		killProtocol = false;

	[HideInInspector]
	public 	GameManager 	gameScript;

	private SpriteRenderer 	backRenderer;
	private bool 			colored;
	[HideInInspector]
	public 	bool 			selected;
	private bool 			selectedParticle;


	[HideInInspector]
	public 	bool 			shakeFlower;
	[HideInInspector]
	public 	float 			shakeTimer;
	private Vector3 		startShakePos;


	private Color 	Back_Color 		= new Color (185f/255f, 185f/255f, 185f/255f, 1);
	private Color 	Spawn_Color 	= new Color (185f/255f, 255f/255f, 193f/255f, 1);

	private Color 	Red_Color 		= new Color (180f/255f, 125f/255f, 125f/255f);
	private Color 	Blue_Color 		= new Color (125f/255f, 150f/255f, 180f/255f);
	private Color 	Green_Color 	= new Color (125f/255f, 180f/255f, 140f/255f);
	private Color 	Purple_Color 	= new Color (150f/255f, 125f/255f, 180f/255f);
	private Color 	Yellow_Color 	= new Color (180f/255f, 170f/255f, 125f/255f);
	private Color 	White_Color 	= new Color (220f/255f, 230f/255f, 180f/255f);

	private Color 	Select_Color 	= new Color (200f/255f, 160f/255f, 60f/255f);
// 	Back Color	B9B9B9
// 	Spawn Color B8FFC1


	// Start is called before the first frame update
	void Start()
	{
		inside = "None";
		backRenderer = gameObject.GetComponent<SpriteRenderer>();
		colored = false;
		selected = false;
		if (gameObject.tag == "SpawnTile" || gameObject.tag == "BackTile")
		{
			gameObject.transform.Find("SpawnParticles").GetComponent<ParticleSystem>().Stop();
			gameObject.transform.Find("Red_Particles").GetComponent<ParticleSystem>().Stop();
			gameObject.transform.Find("Yellow_Particles").GetComponent<ParticleSystem>().Stop();
			gameObject.transform.Find("Purple_Particles").GetComponent<ParticleSystem>().Stop();
			gameObject.transform.Find("Green_Particles").GetComponent<ParticleSystem>().Stop();
			gameObject.transform.Find("Blue_Particles").GetComponent<ParticleSystem>().Stop();
			gameObject.transform.Find("White_Particles").GetComponent<ParticleSystem>().Stop();


			// gameObject.transform.Find("Circle_Particles").GetComponent<ParticleSystem>().Stop();
			// gameObject.transform.Find("Select_Particles").GetComponent<ParticleSystem>().Stop();
			gameObject.transform.Find("Select_Confirm_Particles").GetComponent<ParticleSystem>().Stop();

			selectedParticle = false;
			shakeFlower = false;
			shakeTimer = 0;
		}
	}



	private void 	OnTriggerStay2D(Collider2D other)
	{
		inside = other.gameObject.GetComponent<FlowerScript>().GetFlowerType();
		flower = other.gameObject;

		if (flower.GetComponent<FlowerScript>().spawned)
		{
			flower.GetComponent<FlowerScript>().spawned = false;
			gameObject.transform.Find("SpawnParticles").GetComponent<ParticleSystem>().Play();
		}

	}

	private void	OnTriggerExit2D(Collider2D other)
	{
		inside = "None";
	}

	private void OnMouseDown()
	{
		if (gameScript.turnForPlayer)
		{
			if (gameObject.tag == "SpawnTile" || gameObject.tag == "BackTile")
			{
				gameScript.clickedTiles++;
				if (flower != null)
				{
					backRenderer.color = Select_Color;
					colored = true;
					// gameObject.transform.Find("Select_Particles").GetComponent<ParticleSystem>().Play();
					// gameObject.transform.Find("Circle_Particles").GetComponent<ParticleSystem>().Play();
					selected = true;
					selectedParticle = true;
				}
			}
		}
	}



/* ==========  ========== Moving Stuff ==========  ========== */
/* vvvvvvvvvv  vvvvvvvvvv 			   vvvvvvvvvv  vvvvvvvvvv */



	public void 	SetMove(Vector3 toSetPos)
	{
		FlowerScript flowerCode;

		flowerCode = flower.GetComponent<FlowerScript>();

		flowerCode.newPos = new Vector3(toSetPos.x, toSetPos.y, -5);
		flowerCode.oldPos = gameObject.transform.position;
		flowerCode.ready = false;
	}

	public void 	SetReturnMove()
	{
		FlowerScript flowerCode;

		flowerCode = flower.GetComponent<FlowerScript>();
		flowerCode.mustComeBack = true;

	}

	private bool	TryToMoveHere(GameObject newTile)
	{
		// If exist Check
		if (newTile == null)
			return false;

		string 	newTileInsides;
		newTileInsides = newTile.GetComponent<BackTileScript>().inside;
		
		// If not busy Check
		if (newTileInsides == "Charging" || newTileInsides != "None")
			return false;
		SetMove(newTile.transform.position);
		newTile.GetComponent<BackTileScript>().inside = "Charging";
		// if (newTile.GetComponent<BackTileScript>().inside == "None")
			// Debug.Log("Wierd Shiet happening");
		return true;

	}

	private void 	TryToMove()
	{
		if (!flower.GetComponent<FlowerScript>().ready)
			return;

		if (!TryToMoveHere(bottom))
			if (!TryToMoveHere(leftDown))
				if (!TryToMoveHere(rightDown))
					return;
	}



/* ==========  ========== Moving Stuff ==========  ========== */
/* ^^^^^^^^^^  ^^^^^^^^^^ 			   ^^^^^^^^^^  ^^^^^^^^^^ */



/* ==========  ==========				 ==========  ========== */
/* ==========  ==========				 ==========  ========== */



	void 	PaintToOriginal()
	{
		// if (colored)
			// Debug.Log("Must Definately be Colored Back " + colored);
		if ((gameObject.tag == "SpawnTile" || gameObject.tag == "BackTile") && colored)
		{
			// Debug.Log("Must be Colored Back");
			if (gameObject.tag == "SpawnTile")
			{
				backRenderer.color = Spawn_Color;
			}
			else if (gameObject.tag == "BackTile")
			{
				// Debug.Log("Colored Back");
				backRenderer.color = Back_Color;
			}
			colored = false;
		}
	}


	void 	KillProtocolExecute()
	{
		killProtocol = false;
		if (flower != null)
		{
			flower.GetComponent<FlowerScript>().shouldDie = true;
			/* Exploding effects */
			if (gameObject.tag == "SpawnTile" || gameObject.tag == "BackTile")
			{
				colored = true;
				if (inside == "Red")
				{
					backRenderer.color = Red_Color;
					gameObject.transform.Find("Red_Particles").GetComponent<ParticleSystem>().Play();
				}
				if (inside == "Yellow")
				{
					backRenderer.color = Yellow_Color;
					gameObject.transform.Find("Yellow_Particles").GetComponent<ParticleSystem>().Play();
				}
				if (inside == "Purple")
				{
					backRenderer.color = Purple_Color;
					gameObject.transform.Find("Purple_Particles").GetComponent<ParticleSystem>().Play();
				}
				if (inside == "Green")
				{
					backRenderer.color = Green_Color;
					gameObject.transform.Find("Green_Particles").GetComponent<ParticleSystem>().Play();
				}
				if (inside == "Blue")
				{
					backRenderer.color = Blue_Color;
					gameObject.transform.Find("Blue_Particles").GetComponent<ParticleSystem>().Play();
				}
				if (inside == "White")
				{
					backRenderer.color = White_Color;
					gameObject.transform.Find("White_Particles").GetComponent<ParticleSystem>().Play();
				}

			}
		}
	}



/* ==========  ==========				 ==========  ========== */
/* ==========  ==========				 ==========  ========== */








	// Update is called once per frame
	void Update()
	{
		if (inside != "None" && inside != "Charging")
		{
			if (gameScript.turnToFall)
				if (gameScript.timerBeforeFall > gameScript.delayBeforeFall)
				{
					TryToMove();
					PaintToOriginal();
				}
		}

		if (killProtocol)
			KillProtocolExecute();

		if (!selected)
			if (selectedParticle)
			{
				PaintToOriginal();
				// gameObject.transform.Find("Circle_Particles").GetComponent<ParticleSystem>().Stop();
				// gameObject.transform.Find("Circle_Particles").GetComponent<ParticleSystem>().Play();
				// gameObject.transform.Find("Circle_Particles").GetComponent<ParticleSystem>().Stop();
				// gameObject.transform.Find("Select_Particles").GetComponent<ParticleSystem>().Stop();

				gameObject.transform.Find("Select_Confirm_Particles").GetComponent<ParticleSystem>().Play();

				selectedParticle = false;
			}

		if (shakeFlower)
		{
			if (shakeTimer == 0)
				startShakePos = flower.transform.position;
			
			if (shakeTimer > 0.5) // Seconds
			{
				shakeFlower = false;
				flower.transform.position = startShakePos;
			}
			else
			{
				flower.transform.position = Vector3.MoveTowards(startShakePos, Random.insideUnitCircle * 3, Time.deltaTime * 5);
				shakeTimer += Time.deltaTime;
			}
		}

		if (flower != null)
			if (flower.GetComponent<FlowerScript>().mustComeBack && flower.GetComponent<FlowerScript>().ready)
			{
				inside = "Charging";
				SetMove(flower.GetComponent<FlowerScript>().oldPos);
				flower.GetComponent<FlowerScript>().mustComeBack = false;
			}
	}


	
}
