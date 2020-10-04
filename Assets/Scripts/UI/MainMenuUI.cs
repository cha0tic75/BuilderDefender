// -------------------------------------------------------------------------
// CLASS	:	MainMenuUI
// Desc		:	Definition/Behaviour of MainMenuUI
// -------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
	public class MainMenuUI : MonoBehaviour
	{
		#region MonoBehaviour Method(s):
		private void Awake()
		{
			// Play Button:
			transform.Find("PlayButton").GetComponent<Button>().onClick.AddListener(() =>{
				GameSceneManager.LoadScene(GameSceneManager.SceneNames.GameScene);
			});

			// Quit Button:
			transform.Find("QuitButton").GetComponent<Button>().onClick.AddListener(() =>{
				Application.Quit();
			});

		}
		#endregion	
	}
}