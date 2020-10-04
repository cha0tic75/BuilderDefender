// -------------------------------------------------------------------------
// CLASS	:	ResourceManager
// Desc		:	Definition/Behaviour of ResourceManager
// -------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
	public class ResourceManager : MonoBehaviour
	{
		#region Event/Delegates:
		public event EventHandler OnResourceAmountChanged;
		#endregion
		
		#region Inspector Assigned Field(s):
		[SerializeField] private List<ResourceAmount> m_startingResourceAmountList;
		#endregion

		#region Internal State Field(s):
		private Dictionary<ResourceTypeSO, int> m_resourceAmountDictionary;
		#endregion

		#region Properties:
		public static ResourceManager Instance { get; private set;}
		#endregion
	
		#region MonoBehaviour Method(s):
		private void Awake()
		{
			Instance = this;

			m_resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
			ResourceTypesListSO resourceTypeList = Resources.Load<ResourceTypesListSO>(typeof(ResourceTypesListSO).Name);
		
			// Init all resources to 0:
			foreach(ResourceTypeSO resourceType in resourceTypeList.typesList)
			{
				m_resourceAmountDictionary[resourceType] = 0;
			}

			// Add starting resources:
			foreach(ResourceAmount resourceAmount in m_startingResourceAmountList)
			{
				AddResource(resourceAmount.resourceType, resourceAmount.amount);
			}
		}
		#endregion	


		#region Public API:
		public static ResourceTypeSO GetResourceByName(string name)
		{
			Dictionary<ResourceTypeSO, int> resourceDictionary = Instance.m_resourceAmountDictionary;

			foreach (ResourceTypeSO resourceType in resourceDictionary.Keys)
			{
				if (resourceType.name == name)
				{
					return resourceType;
				}
			}

			return null;
		}

		public void AddResource(ResourceTypeSO resourceType, int amount)
		{
			m_resourceAmountDictionary[resourceType] += amount;
			OnResourceAmountChanged?.Invoke (this, EventArgs.Empty);
		}

		public int GetResourceAmount(ResourceTypeSO resourceType)
		{
			return m_resourceAmountDictionary[resourceType];
		}

		public bool CanAfford(ResourceAmount[] resourceAmountArray)
		{
			foreach(ResourceAmount resourceAmount in resourceAmountArray)
			{
				if (GetResourceAmount(resourceAmount.resourceType) < resourceAmount.amount)
				{
					return false;
				}
			}

			return true;
		}

		public void SpendResources(ResourceAmount[] resourceAmountArray)
		{
			foreach(ResourceAmount resourceAmount in resourceAmountArray)
			{
				m_resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.amount;
			}
		}
		#endregion
	}
}