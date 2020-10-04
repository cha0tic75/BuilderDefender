// -------------------------------------------------------------------------
// CLASS	:	ChromaticAberrationEffect
// Desc		:	Definition/Behaviour of ChromaticAberrationEffect
// -------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Rendering;

namespace Project
{
	public class ChromaticAberrationEffect : Singleton<ChromaticAberrationEffect>
	{
		#region Inspector Visibile Field(s):
		[SerializeField] private float m_decreaseSpeed = 1f;
		#endregion
		
		#region Internal State Field(s):
		private Volume m_volume;
		#endregion
	
		#region MonoBehaviour Method(s):
		protected override void Awake()
		{
			base.Awake();
			m_volume = GetComponent<Volume>();
		}

		private void Update()
		{
			if (m_volume.weight > 0f)
			{
				m_volume.weight -= Time.deltaTime * m_decreaseSpeed;
			}
		}
		#endregion

		#region Public API:
		public void SetWeight(float weight)
		{
			m_volume.weight = weight;
		}
		#endregion
	}
}