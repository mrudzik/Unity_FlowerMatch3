using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerScript : MonoBehaviour
{
	[HideInInspector]
	public	Vector3 	newPos;
	[HideInInspector]
	public	Vector3 	oldPos;
	[HideInInspector]
	public 	bool 		mustComeBack;


	[HideInInspector]
	public 	bool 		ready;
	[HideInInspector]
	public	bool		spawned;
	[HideInInspector]
	public	bool 		shouldDie;


	public 	GameManager 	gameScript;


	public 		Sprite 	spriteRed;
	public 		Sprite 	spriteYellow;
	public 		Sprite 	spritePurple;
	public 		Sprite 	spriteGreen;
	public 		Sprite 	spriteBlue;
	public 		Sprite 	spriteWhite;



	private 	SpriteRenderer 	spriteRend;
	private 	string 			flowerType;
	private 	Sprite 			flowerSprite;
	private 	float 			slideSpeed;



	public 		void 	SetFlowerType(string newType, Sprite flowerNewSprite, float slideNewSpeed)
	{
		flowerType = newType;
		// flowerSprite = flowerNewSprite;
		slideSpeed = slideNewSpeed;

		flowerSprite = flowerNewSprite;
		spriteRend.sprite = flowerSprite;

	}

	public 		string 	GetFlowerType()
	{
		return flowerType;
	}

	// Start is called before the first frame update
	void Start()
	{
		int rand;
		float 	newSpeed;

		newSpeed = 5f;
		rand = Random.Range(0, 6);

		spawned = true;
		ready = true;
		mustComeBack = false;
		shouldDie = false;

		spriteRend = gameObject.GetComponent<SpriteRenderer>();

		if (rand == 0)
			SetFlowerType("Red", spriteRed, newSpeed);
		if (rand == 1)
			SetFlowerType("Yellow", spriteYellow, newSpeed);
		if (rand == 2)
			SetFlowerType("Purple", spritePurple, newSpeed);
		if (rand == 3)
			SetFlowerType("Green", spriteGreen, newSpeed);
		if (rand == 4)
			SetFlowerType("Blue", spriteBlue, newSpeed);
		if (rand == 5)
			SetFlowerType("White", spriteWhite, newSpeed);

		// SetFlowerType("Red", spriteRend.sprite, 2);
	}

	// Update is called once per frame
	void Update()
	{

		if (!ready)
			DoMovement();
		if (shouldDie)
		{
			gameScript.score += 10;
			gameScript.AddPetals(flowerType, 1);
			Destroy(gameObject);
		}
			
	}


	void 	DoMovement()
	{
		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, newPos, slideSpeed * Time.deltaTime);
		if (gameObject.transform.position == newPos)
				ready = true;

		return;
	}
}
