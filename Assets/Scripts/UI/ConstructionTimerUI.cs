// -------------------------------------------------------------------------
// CLASS	:	ConstructionTimerUI
// Desc		:	Definition/Behaviour of ConstructionTimerUI
// -------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace Project
{
	public class ConstructionTimerUI : MonoBehaviour
	{
		#region Inspector Assigned Field(s):
		[SerializeField] private BuildingConstruction m_buildingConstruction;
		#endregion	

		#region Internal State Field(s):
		private Image m_constructionProgressImage;
		#endregion
	
		#region MonoBehaviour Method(s):
		private void Awake()
		{
			m_constructionProgressImage = transform.Find("Mask").Find("Image").GetComponent<Image>();
		}

		private void Update()
		{
			m_constructionProgressImage.fillAmount = m_buildingConstruction.GetConstructionTimerNormalized();
		}
		#endregion	
	}
}