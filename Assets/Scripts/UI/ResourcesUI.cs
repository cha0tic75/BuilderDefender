// -------------------------------------------------------------------------
// CLASS	:	ResourcesUI
// Desc		:	Definition/Behaviour of ResourcesUI
// -------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Project.UI
{
	public class ResourcesUI : MonoBehaviour
	{
		#region Inspector Assigned Field(s):
		[SerializeField] private float m_offsetAmount = -100f;
		#endregion

		#region Internal State Field(s):
		private Dictionary<ResourceTypeSO, Transform> m_resourceTypeTransformDictionary;
		private ResourceTypesListSO m_resourceTypeList;
		#endregion
	
		#region MonoBehaviour Method(s):
		private void Awake()
		{
			m_resourceTypeList = Resources.Load<ResourceTypesListSO>(typeof(ResourceTypesListSO).Name);
		
			Transform resourceTemplate = transform.Find("ResourceTemplate");
			float initialX = resourceTemplate.GetComponent<RectTransform>().anchoredPosition.x;
			resourceTemplate.gameObject.SetActive(false);

			m_resourceTypeTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();

			int index = 0;
			foreach(ResourceTypeSO resourceType in m_resourceTypeList.typesList)
			{
				Transform resourceTransform = Instantiate(resourceTemplate, transform);
				resourceTransform.gameObject.SetActive(true);

				float offset = initialX + index * m_offsetAmount;

				resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset, 0f);

				resourceTransform.Find("Image").GetComponent<Image>().sprite = resourceType.sprite;

				m_resourceTypeTransformDictionary[resourceType] = resourceTransform;
				index++;
			}
		}

		private void Start()
		{
			UpdateResourceAmount();
			ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
		}
		#endregion

		#region Internally Used Method(s):
		private void ResourceManager_OnResourceAmountChanged(object sender, System.EventArgs e)
		{
			UpdateResourceAmount();
		}

		private void UpdateResourceAmount()
		{
			foreach(ResourceTypeSO resourceType in m_resourceTypeList.typesList)
			{
				Transform resourceTransform = m_resourceTypeTransformDictionary[resourceType];
				int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
				resourceTransform.Find("Text_TMP").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString()); 
			}
		}
		#endregion
	}
}