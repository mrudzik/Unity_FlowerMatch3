using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundButton : MonoBehaviour
{
    
	private 	int 	skyboxNum;

    public 		SpriteRenderer 		thisSkybox;
    public 		Sprite [] 			skyboxes;



    public void 	ChangeSkybox()
    {
    	if (skyboxes.Length == 0)
    		return;

    	skyboxNum++;
    	if (skyboxNum >= skyboxes.Length)
    		skyboxNum = 0;

    	thisSkybox.sprite = skyboxes[skyboxNum];
    }

    // Start is called before the first frame update
    void Start()
    {
        skyboxNum = 0;
    }

}
