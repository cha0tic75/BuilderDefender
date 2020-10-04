// -------------------------------------------------------------------------
// CLASS	:	BuildingConstruction
// Desc		:	Definition/Behaviour of BuildingConstruction
// -------------------------------------------------------------------------

using UnityEngine;

namespace Project
{
	public class BuildingConstruction : MonoBehaviour
	{	
		#region Internal State Field(s):
		private BuildingTypeSO m_buildingType;
		private BoxCollider2D m_boxCollider2D;
		private SpriteRenderer m_spriteRenderer;
		private BuildingTypeHolder m_buildingTypeholder;
		private Material m_constructionMaterial;
		private float m_constructionTimer;
		private float m_constructionTimerMax;
		#endregion
	
		#region MonoBehaviour Method(s):
		private void Awake()
		{
			m_boxCollider2D = GetComponent<BoxCollider2D>();
			m_spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
			m_buildingTypeholder = GetComponent<BuildingTypeHolder>();
			m_constructionMaterial = m_spriteRenderer.material;
			PlayBuildParticles(transform.position);
		}

		private void Update()
		{
			m_constructionTimer -= Time.deltaTime;

			m_constructionMaterial.SetFloat("_Progress", GetConstructionTimerNormalized());
			if (m_constructionTimer <= 0f)
			{
				Instantiate(m_buildingType.prefab, transform.position, Quaternion.identity);
				PlayBuildParticles(transform.position);
				SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
				Destroy(gameObject);
			}
		}
		#endregion

		#region Public API:
		public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingType)
		{
			Transform buildingConstructionTransform = Instantiate(GameAssets.Instance.pfBuildingConstruction, position, Quaternion.identity);

			BuildingConstruction buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstruction>();
			buildingConstruction.SetBuildingType(buildingType);

			return buildingConstruction;
		}

		public float GetConstructionTimerNormalized()
		{
			return 1 - m_constructionTimer / m_constructionTimerMax;
		}
		#endregion

		#region Internally Used Method(s):
		private void SetBuildingType(BuildingTypeSO buildingType)
		{
			m_buildingType = buildingType;

			m_buildingTypeholder.buildingType = buildingType;
			m_constructionTimerMax = buildingType.constructionTimerMax;
			m_constructionTimer = m_constructionTimerMax;

			m_spriteRenderer.sprite = buildingType.sprite;

			BoxCollider2D buildingBoxCollider = buildingType.prefab.GetComponent<BoxCollider2D>();
			m_boxCollider2D.offset = buildingBoxCollider.offset;
			m_boxCollider2D.size =  buildingBoxCollider.offset;
		}

		private static void PlayBuildParticles(Vector3 position)
		{
			Instantiate(GameAssets.Instance.pfBuildingPlacedParticles, position, Quaternion.identity);
		}
		#endregion
	}
}