// -------------------------------------------------------------------------
// CLASS	:	BuildingRepairButton
// Desc		:	Definition/Behaviour of BuildingRepairButton
// -------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
	public class BuildingRepairButton : MonoBehaviour
	{
		#region Inspector Visibile Field(s):
		[SerializeField] private Building m_building;
		[SerializeField] private ResourceTypeSO m_resourceAmount;
		#endregion

		#region MonoBehaviour Method(s):
		private void Awake()
		{
			Button button = transform.Find("Button").GetComponent<Button>();
			button.onClick.AddListener(() => {
				HealthSystem healthSystem = m_building.GetHealthSystem();
				int healthDifference = healthSystem.GetHealthAmountDifference();
				int repairCost = Mathf.FloorToInt(healthDifference / m_building.GetBuildingType().repairCostDividend);

				ResourceAmount[] repairResourceAmount = new ResourceAmount[] { new ResourceAmount { resourceType = m_resourceAmount, amount = repairCost}};

				if (ResourceManager.Instance.CanAfford(repairResourceAmount))
				{
					ResourceManager.Instance.SpendResources(repairResourceAmount);
					healthSystem.HealFull();
				}
				else
				{
					TooltipUI.Instance.Show($"Cannot afford to repair this building! {repairCost}", new TooltipUI.TooltipTimer{ timer = 2f });
				}
			});
		}
		#endregion
	}
}