using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
	public GameObject 	thePanel;

	public void 	OpenPanel()
	{
		if (thePanel != null)
		{
			Animator animOpen;

			animOpen = thePanel.GetComponent<Animator>();
			if (animOpen != null)
			{
				bool 	isOpen;

				isOpen = animOpen.GetBool("open");

				animOpen.SetBool("open", !isOpen);
			}


			// bool isActive = thePanel.activeSelf;
			// thePanel.SetActive(!isActive);
		}
	}
}
