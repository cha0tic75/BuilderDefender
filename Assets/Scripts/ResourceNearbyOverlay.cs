// -------------------------------------------------------------------------
// CLASS	:	ResourceNearbyOverlay
// Desc		:	Definition/Behaviour of ResourceNearbyOverlay
// -------------------------------------------------------------------------

using UnityEngine;
using TMPro;

namespace Project
{
	public class ResourceNearbyOverlay : MonoBehaviour
	{	
		#region Internal State Field(s):
		private ResourceGeneratorData m_resourceGeneratorData;
		private TextMeshPro m_displayTMP;
		#endregion
	
		#region MonoBehaviour Method(s):
		private void Awake()
		{
			m_displayTMP = transform.Find("Text_TMP").GetComponent<TextMeshPro>();
			Hide();
		}

		private void Update()
		{
			int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(m_resourceGeneratorData, transform.position - transform.localPosition);
			
			float percent = Mathf.RoundToInt((float)nearbyResourceAmount / m_resourceGeneratorData.maxResourceAmount * 100f);
			
			m_displayTMP.SetText($"{percent}%");
			
		}
		#endregion

		#region Public API:
		public void Show(ResourceGeneratorData resourceGeneratorData)
		{
			m_resourceGeneratorData = resourceGeneratorData;
			gameObject.SetActive(true);

			transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}
		#endregion
	}
}