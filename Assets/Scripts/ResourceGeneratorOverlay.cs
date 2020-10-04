// -------------------------------------------------------------------------
// CLASS	:	ResourceGeneratorOverlay
// Desc		:	Definition/Behaviour of ResourceGeneratorOverlay
// -------------------------------------------------------------------------

using UnityEngine;
using TMPro;

namespace Project
{
	public class ResourceGeneratorOverlay : MonoBehaviour
	{
		#region Inspector Visibile Field(s):
		[SerializeField] private ResourceGenerator m_resourceGenerator;
		#endregion
		
		#region Internal State Field(s):
		private Transform m_barTransform;
		#endregion
	
		#region MonoBehaviour Method(s):
		private void Start()
		{
			ResourceGeneratorData resourceGeneratorData = m_resourceGenerator.GetResourceGeneratorData();
			
			m_barTransform = transform.Find("Bar");
			
			transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
			float amount = m_resourceGenerator.GetAmountGeneratedPerSecond();

//			Debug.Log($"{transform.parent.name} : amount: {amount}");
			transform.Find("Text_TMP").GetComponent<TextMeshPro>().SetText(amount.ToString("F1"));
		}

		private void Update()
		{
			m_barTransform.localScale = new Vector3(1 - m_resourceGenerator.GetTimerNormalized(), 1f, 1f);
		}
		#endregion	
	}
}