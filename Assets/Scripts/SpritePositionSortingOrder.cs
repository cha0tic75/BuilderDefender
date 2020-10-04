// -------------------------------------------------------------------------
// CLASS	:	SpritePositionSortingOrder
// Desc		:	Definition/Behaviour of SpritePositionSortingOrder
// -------------------------------------------------------------------------

using UnityEngine;

namespace Project
{
	public class SpritePositionSortingOrder : MonoBehaviour
	{
		#region Inspector Visibile Field(s):
		[SerializeField] private bool m_isStaticObject = false;
		[SerializeField] private float m_precisionMultiplier = 5f;
		[SerializeField] private float m_positionOffsetY;
		#endregion
		
		#region Internal State Field(s):
		private SpriteRenderer m_spriteRenderer;
		#endregion
	
		#region MonoBehaviour Method(s):
		private void Awake()
		{
			m_spriteRenderer = GetComponent<SpriteRenderer>();
		}

		private void LateUpdate()
		{
			m_spriteRenderer.sortingOrder = (int)(-(transform.position.y + m_positionOffsetY) * m_precisionMultiplier);

			if (m_isStaticObject)
			{
				Destroy(this);
			}
		}
		#endregion	
	}
}