// -------------------------------------------------------------------------
// CLASS	:	ResourceGenerator
// Desc		:	Definition/Behaviour of ResourceGenerator
// -------------------------------------------------------------------------

using UnityEngine;

namespace Project
{
    public class ResourceGenerator : MonoBehaviour
	{	
		#region Internal State Field(s):
		private ResourceGeneratorData m_resourceGeneratorData;
		private float m_timer;
		private float m_timerMax;
		#endregion
	
		#region MonoBehaviour Method(s):
		private void Awake()
		{
			m_resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
			m_timerMax = m_resourceGeneratorData.timerMax;
		}

		private void Start()
		{
			int nearbyResourceAmount = GetNearbyResourceAmount(m_resourceGeneratorData, transform.position);

			if (nearbyResourceAmount == 0) {

				enabled = false;
			} 
			else
			{
				m_timerMax = (m_resourceGeneratorData.timerMax / 2f) +
					m_resourceGeneratorData.timerMax *
					(1 - (float)nearbyResourceAmount / m_resourceGeneratorData.maxResourceAmount);
			}

			// Debug.Log($"{transform.name}  nearbyResourceAmount: {nearbyResourceAmount}; timerMax: {m_timer}");
		}

		private void Update()
		{
			m_timer -= Time.deltaTime;

			if (m_timer <= 0f)
			{
				ResourceManager.Instance.AddResource(m_resourceGeneratorData.resourceType, 1);
				m_timer += m_timerMax;
			}
		}

		private void OnDrawGizmosSelected() 
		{
			if (m_resourceGeneratorData == null) return;
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere (transform.position, m_resourceGeneratorData.resourceDetectionRadius);
		}
		#endregion

		#region Public API:
		public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
		{
			Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionRadius);

			int nearbyResourceAmount = 0;
			foreach (Collider2D collider2D in collider2DArray) {
				ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();
				if (resourceNode != null) {
					// It's a resource node!
					if (resourceNode.resourceType == resourceGeneratorData.resourceType) {
						// Same type!
						nearbyResourceAmount++;
					}
				}
			}

			nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);

			return nearbyResourceAmount;

		}
		public ResourceGeneratorData GetResourceGeneratorData()
		{
			return m_resourceGeneratorData;
		}

		public float GetTimerNormalized()
		{
			return m_timer / m_timerMax;
		}

		public float GetAmountGeneratedPerSecond()
		{
			return 1 / m_timerMax;
		}
		#endregion
	}
}