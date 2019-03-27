using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyWallManager : MonoBehaviour
{
	public bool 			randomSkybox = true;
	public SpriteRenderer 	thisSkybox;

    public Sprite [] 		skyboxes;


	void 	SelectRandomSkybox()
	{
		int rand;

		if (skyboxes.Length == 0)
			return;
			
		rand = Random.Range(0, skyboxes.Length);
		thisSkybox.sprite = skyboxes[rand];
		return;
	}



    // Start is called before the first frame update
    void Start()
    {
        if (randomSkybox)
        {
        	if (thisSkybox != null)
        		SelectRandomSkybox();
        	else
        		Debug.Log("Troubles with skybox");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
