// -------------------------------------------------------------------------
// CLASS	:	BuildingGhost
// Desc		:	Definition/Behaviour of BuildingGhost
// -------------------------------------------------------------------------


using UnityEngine;

namespace Project
{
	public class BuildingGhost : MonoBehaviour
	{	
		#region Internal State Field(s):
		private GameObject m_spriteGameObject;
		private ResourceNearbyOverlay m_resourceNearbyOverlay;
		#endregion
	
		#region MonoBehaviour Method(s):
		private void Awake()
		{
			m_spriteGameObject = transform.Find("Sprite").gameObject;
			m_resourceNearbyOverlay = transform.Find("pfResourceNearbyOverlay").GetComponent<ResourceNearbyOverlay>();
		
			Hide();
		}

        private void Start()
		{
			BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuidingTypechanged;
		}

        private void Update()
		{
			transform.position = UtilsClass.GetMouseWorldPosition();
		}
		#endregion

		#region Internally Used Method(s):
		private void BuildingManager_OnActiveBuidingTypechanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
		{
			if (e.activeBuildingType == null)
			{
				Hide();
				m_resourceNearbyOverlay.Hide();
			}
			else
			{
				Show(e.activeBuildingType.sprite);

				if (e.activeBuildingType.hasResourceGeneratorData)
				{
					m_resourceNearbyOverlay.Show(e.activeBuildingType.resourceGeneratorData);
				}
				else
				{
					m_resourceNearbyOverlay.Hide();
				}
			}
		}
		private void Show(Sprite ghostSprite)
		{
			m_spriteGameObject.SetActive(true);
			m_spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
		}

		private void Hide()
		{
			m_spriteGameObject.SetActive(false);
		}
		#endregion
	}
}