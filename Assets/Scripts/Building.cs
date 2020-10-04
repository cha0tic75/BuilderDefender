// -------------------------------------------------------------------------
// CLASS	:	Building
// Desc		:	Definition/Behaviour of Building
// -------------------------------------------------------------------------

using System;
using UnityEngine;

namespace Project
{
	public class Building : MonoBehaviour
	{	
		#region Internal State Field(s):
		private BuildingTypeSO m_buildingType;
		private HealthSystem m_healthSystem;
		private Transform m_buildingDemolishButton;
		private Transform m_buildingRepairButton;
		#endregion
	
		#region MonoBehaviour Method(s):
		private void Awake()
		{
			m_buildingType = GetComponent<BuildingTypeHolder>().buildingType;
			m_healthSystem = GetComponent<HealthSystem>();

			m_buildingDemolishButton = transform.Find("pfBuildingDemolishButton");
			m_buildingRepairButton = transform.Find("pfBuildingRepairButton");

			SetDemolishButtonVisibility(false);
			SetRepairButtonVisibility(false);
		}
		private void Start()
		{
			m_healthSystem.SetHealthAmountMax(m_buildingType.healthAmountMax, true);

			m_healthSystem.OnDeath += HealthSystem_OnDeath;
			m_healthSystem.OnDamaged += HealthSystem_OnDamaged;
			m_healthSystem.OnHealed += HealthSystem_OnHealed;
		}

		private void OnMouseEnter()
		{
			SetDemolishButtonVisibility(true);
		}

		private void OnMouseExit()
		{
			SetDemolishButtonVisibility(false);
		}
		#endregion

		#region Public API:
		public HealthSystem GetHealthSystem()
		{
			return m_healthSystem;
		}

		public BuildingTypeSO GetBuildingType()
		{
			return m_buildingType;
		}
		#endregion

		#region Internally Used Method(s):
		private void HealthSystem_OnDeath(object sender, EventArgs e)
		{
			SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
			CinemachineShake.Instance.ShakeCamera(7f, 0.2f);
			ChromaticAberrationEffect.Instance.SetWeight(1f);
			Instantiate(GameAssets.Instance.pfBuildingDestroyedParticles, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}

		private void HealthSystem_OnDamaged(object sender, EventArgs e)
		{
			SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDamaged);
			CinemachineShake.Instance.ShakeCamera(4f, 0.15f);
			ChromaticAberrationEffect.Instance.SetWeight(1f);
			SetRepairButtonVisibility(true);
		}

		private void HealthSystem_OnHealed(object sender, EventArgs e)
		{
			if (m_healthSystem.IsFullHealth())
			{
				SetRepairButtonVisibility(false);
			}
		}
		private void SetDemolishButtonVisibility(bool state)
		{
			if (m_buildingDemolishButton == null) return;

			m_buildingDemolishButton.gameObject.SetActive(state);
		}

		private void SetRepairButtonVisibility(bool state)
		{
			if (m_buildingRepairButton == null) return;

			m_buildingRepairButton.gameObject.SetActive(state);
		}
		#endregion
	}
}