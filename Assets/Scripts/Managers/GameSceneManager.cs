// -------------------------------------------------------------------------
// CLASS	:	GameSceneManager
// Desc		:	Definition/Behaviour of GameSceneManager
// -------------------------------------------------------------------------

using UnityEngine.SceneManagement;

namespace Project
{
	public static class GameSceneManager
	{
		#region Enum(s):
		public enum SceneNames 
		{
			MainMenuScene, 
			GameScene,
		}
		#endregion
	
		#region Public API:
		public static void LoadScene(SceneNames scene)
		{
			SceneManager.LoadScene((int)scene);
		}
		#endregion	
	}
}