// -------------------------------------------------------------------------
// CLASS	:	BuildingDemolishButton
// Desc		:	Definition/Behaviour of BuildingDemolishButton
// -------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace Project
{
	public class BuildingDemolishButton : MonoBehaviour
	{
		#region Inspector Visibile Field(s):
		[SerializeField] private Building m_building;
		#endregion
	
		#region MonoBehaviour Method(s):
		private void Awake()
		{
			Button button = transform.Find("Button").GetComponent<Button>();
			button.onClick.AddListener(() => {
				BuildingTypeSO buildingType = m_building.GetBuildingType();
				HealthSystem healthSystem = m_building.GetHealthSystem();
				
				float recoupPercentage = (healthSystem != null) ? 
								healthSystem.GetHealthAmountNormalized() * buildingType.demolishResourceRecoupPercent : 
								buildingType.demolishResourceRecoupPercent;

				foreach(ResourceAmount resourceAmount in buildingType.constructionResourceCostArray)
				{
					int recoupAmount = Mathf.FloorToInt(resourceAmount.amount * recoupPercentage);

					ResourceManager.Instance.AddResource(resourceAmount.resourceType, recoupAmount);
				}

				Destroy(m_building.gameObject);
			});
		}
		#endregion	
	}
}