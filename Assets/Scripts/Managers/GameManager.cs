// -------------------------------------------------------------------------
// CLASS	:	GameManager
// Desc		:	Definition/Behaviour of GameManager
// -------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
	public class GameManager : Singleton<GameManager>
	{
		#region Inspector Visibile Field(s):
		[SerializeField] private string m_waveDisplayName;
		#endregion
		
		#region Properties:
		public string WaveDisplayName => m_waveDisplayName;
		#endregion	
	}
}