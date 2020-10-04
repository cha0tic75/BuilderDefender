// -------------------------------------------------------------------------
// CLASS	:	Enemy
// Desc		:	Definition/Behaviour of Enemy
// -------------------------------------------------------------------------

using UnityEngine;

namespace Project
{
	public class Enemy : MonoBehaviour
	{
		#region Inspector Visibile Field(s):
		[SerializeField] private float m_moveSpeed = 5f;
		[SerializeField] private float m_lookRadius = 10f;
		[SerializeField] private float m_lookForTargetTimerMax = 0.2f;
		#endregion
		
		#region Internal State Field(s):
		private HealthSystem m_healthSystem;
		private Transform m_targetTransform;
		private Rigidbody2D m_rigidbody2D;
		private float m_lookForTargetTimer;
		#endregion
	
		#region MonoBehaviour Method(s):
		private void Start()
		{
			m_rigidbody2D = GetComponent<Rigidbody2D>();
			
			if (BuildingManager.Instance.GetHqBuilding() != null)
			{
				m_targetTransform = BuildingManager.Instance.GetHqBuilding().transform;
			}

			m_lookForTargetTimer = Random.Range(0f, m_lookForTargetTimerMax);
			m_healthSystem = GetComponent<HealthSystem>();
			m_healthSystem.OnDeath += HealthSystem_OnDeath;
			m_healthSystem.OnDamaged += HealthSystem_OnDamaged;
		}

		private void Update()
        {
            HandleMovement();
            HandleTargeting();
        }

        private void OnCollisionEnter2D(Collision2D collision)
		{
			Building building = collision.gameObject.GetComponent<Building>();

			if (building != null)
			{
				HealthSystem healthSystem = building.GetComponent<HealthSystem>();
				healthSystem.Damage(10);
				m_healthSystem.Damage(9999);
			}
		}
		#endregion

		#region Public API:
		public static Enemy Create(Vector3 position)
		{
			Transform enemyTransform = Instantiate(GameAssets.Instance.pfEnemy, position, Quaternion.identity);

			Enemy enemy = enemyTransform.GetComponent<Enemy>();

			return enemy;
		}

		public HealthSystem GetHealthSystem()
		{
			return m_healthSystem;
		}
		#endregion

		#region Internally Used Method(s):
        private void HandleMovement()
        {
            Vector3 moveDir = (m_targetTransform != null) ? 
							  (m_targetTransform.position - transform.position).normalized :
							  Vector3.zero;

            m_rigidbody2D.velocity = moveDir * m_moveSpeed;
        }

        private void HandleTargeting()
        {
            m_lookForTargetTimer -= Time.deltaTime;
            if (m_lookForTargetTimer <= 0f)
            {
                LookForTargets();
                m_lookForTargetTimer += m_lookForTargetTimerMax;
            }
        }

		private void LookForTargets()
		{
			Collider2D[] colliders2DArray = Physics2D.OverlapCircleAll(transform.position, m_lookRadius);

			foreach(Collider2D collider2D in colliders2DArray)
			{
				Building building = collider2D.GetComponent<Building>();

				if (building != null)
				{
					if (m_targetTransform == null)
					{
						m_targetTransform = building.transform;
					}
					else
					{
						if (Vector3.Distance(transform.position, building.transform.position) < 
							Vector3.Distance(transform.position, m_targetTransform.position))
						{
							m_targetTransform = building.transform;
						}
					}
				}
			}

			if (m_targetTransform == null)
			{
				if (BuildingManager.Instance.GetHqBuilding() != null)
				{
					m_targetTransform = BuildingManager.Instance.GetHqBuilding().transform;
				}
			}
		}

		private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
		{
			SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyHit);
			CinemachineShake.Instance.ShakeCamera(2f, 0.1f);
			ChromaticAberrationEffect.Instance.SetWeight(0.5f);
		}

		private void HealthSystem_OnDeath(object sender, System.EventArgs e)
		{
			SoundManager.Instance.PlaySound(SoundManager.Sound.EmeyDie);
			CinemachineShake.Instance.ShakeCamera(4f, 0.15f);
			ChromaticAberrationEffect.Instance.SetWeight(0.5f);

			Instantiate(GameAssets.Instance.pfEnemyDieParticles, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
		#endregion
	}
}