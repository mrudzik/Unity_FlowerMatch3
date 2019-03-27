using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButton : MonoBehaviour
{
	public AudioSource 	music;
	private bool 		isPlaying = true;

    public void		TurnMusic()
    {
    	isPlaying = !isPlaying;
    	
    	if (isPlaying)
    		music.Play();
    	else
    		music.Pause();
    }
}
