// -------------------------------------------------------------------------
// CLASS	:	GameAssets
// Desc		:	Definition/Behaviour of GameAssets
// -------------------------------------------------------------------------

using UnityEngine;

namespace Project
{
	public class GameAssets : MonoBehaviour
	{
		#region Singleton:
		private static GameAssets m_instance;

		public static GameAssets Instance
		{
			get
			{
				if (m_instance == null)
				{
					m_instance = Resources.Load<GameAssets>("GameAssets");
				}

				return m_instance;
			}
		}
		#endregion

		#region Inspector Visibile Field(s):
		[Header("Enemy")]
		public Transform pfEnemy;
		public Transform pfEnemyDieParticles;
		[Header("ArrowPrefab")]
		public Transform pfArrowProjectile;


		[Header("Building")]
		public Transform pfBuildingConstruction;
		public Transform pfBuildingPlacedParticles;
		public Transform pfBuildingDestroyedParticles;
		#endregion	
	}
}