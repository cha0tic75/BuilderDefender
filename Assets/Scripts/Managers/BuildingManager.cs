// -------------------------------------------------------------------------
// CLASS	:	BuildingManager
// Desc		:	Definition/Behaviour of BuildingManager
// -------------------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project
{
	public class BuildingManager : Singleton<BuildingManager>
	{
		#region Event/Delegates:
		public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

		public class OnActiveBuildingTypeChangedEventArgs : EventArgs
		{
			public BuildingTypeSO activeBuildingType;
		}
		#endregion
		
		#region Inspector Assigned Field(s):
		[SerializeField] private float m_maxConstructionRadius = 25f;
		[Header("References")]
		[SerializeField] private Building m_HqBuilding;
		#endregion

		#region Internal State Field(s):
		private Camera m_mainCamera;
		private BuildingTypeSO m_activeBuildingType;
		private BuildingTypesListSO m_buildingTypesList;	
		#endregion
	
		#region MonoBehaviour Method(s):
		protected override void Awake()
		{
			base.Awake();
			
			m_buildingTypesList = Resources.Load<BuildingTypesListSO>(typeof(BuildingTypesListSO).Name);
		}

		private void Start()
		{
			m_mainCamera = Camera.main;

			m_HqBuilding.GetHealthSystem().OnDeath += HQ_OnDeath;
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
			{
				Vector3 clickPos = UtilsClass.GetMouseWorldPosition();
				if (m_activeBuildingType != null)
				{
					if (CanSpawnBuilding(m_activeBuildingType, clickPos, out string errorMessage))
					{
						if (ResourceManager.Instance.CanAfford(m_activeBuildingType.constructionResourceCostArray))
						{
							ResourceManager.Instance.SpendResources(m_activeBuildingType.constructionResourceCostArray);
							BuildingConstruction.Create(UtilsClass.GetMouseWorldPosition(), m_activeBuildingType);

							SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
						}
						else
						{
							TooltipUI.Instance.Show($"Cannot afford {m_activeBuildingType.GetConstructionResourceCostString()}", new TooltipUI.TooltipTimer { timer = 2f });
						}
					}
					else
					{
						TooltipUI.Instance.Show(errorMessage, new TooltipUI.TooltipTimer { timer = 2f });
					}
				}
			}
		}
		#endregion

		#region Public API:
		public void SetActiveBuildingType(BuildingTypeSO buildingType)
		{
			m_activeBuildingType = buildingType;
			OnActiveBuildingTypeChanged?.Invoke(this, 
				new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = m_activeBuildingType} 
			);
		}

		public BuildingTypeSO GetActiveBuildingType()
		{
			return m_activeBuildingType;
		}

		public Building GetHqBuilding()
		{
			return m_HqBuilding;
		}
		#endregion

		#region Internally Used Field(s):
		private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string errorMessage)
		{
			BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

			Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0f);

			bool isAreaClear = collider2DArray.Length == 0;

			if (!isAreaClear)
			{
				errorMessage = "Area is not clear!";
				return false;
			}

			collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);

			foreach(Collider2D collider in collider2DArray)
			{
				// Colliders inside construction radius:
				BuildingTypeHolder buildingTypeHolder = collider.GetComponent<BuildingTypeHolder>();

				// Have a building and it is same type, then we can't build here:
				if (buildingTypeHolder != null)
				{
					if (buildingTypeHolder.buildingType == buildingType)
					{
						errorMessage = "To close to another building of the same type!";
						return false;
					}
				}
			}

			if (buildingType.hasResourceGeneratorData)
			{
				int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(buildingType.resourceGeneratorData, position);

				if (nearbyResourceAmount == 0)
				{
					errorMessage = "No nearby resources!";
					return false;
				}

			}

			// Check to see if there is a building within the maxConstructionRadius:
			collider2DArray = Physics2D.OverlapCircleAll(position, m_maxConstructionRadius);

			foreach(Collider2D collider in collider2DArray)
			{
				// Colliders inside construction radius:
				BuildingTypeHolder buildingTypeHolder = collider.GetComponent<BuildingTypeHolder>();

				// Have a building so it is okay to build:
				if (buildingTypeHolder != null)
				{
					errorMessage = "";
					return true;
				}
			}

			// To far away from another building, can't build:
			errorMessage = "Too far from from any other building!";
			return false;
		}
		#endregion

		#region Internally Used Method(s):
		private void HQ_OnDeath(object sender, System.EventArgs e)
		{
			SoundManager.Instance.PlaySound(SoundManager.Sound.GameOver);
			GameOverUI.Instance.Show();
		}
		#endregion
	}
}