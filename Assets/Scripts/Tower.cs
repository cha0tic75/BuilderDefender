// -------------------------------------------------------------------------
// CLASS	:	DefenseTower
// Desc		:	Definition/Behaviour of DefenseTower
// -------------------------------------------------------------------------

using UnityEngine;

namespace Project
{
	public class Tower : MonoBehaviour
	{
		#region Inspector Visibile Field(s):
		[SerializeField] private float m_lookRadius = 20f;
		[SerializeField] private float m_lookForTargetTimerMax = 0.2f;
		[SerializeField] private float m_shootTimerMax = 0.25f;

		[Header("Arrow Settings")]
		[SerializeField] private int m_damageAmount = 10;
		[SerializeField] private float m_moveSpeed = 10f;
		[SerializeField] private Transform m_towerDefenseRadiusTransform;
		#endregion
		
		#region Internal State Field(s):
		private Enemy m_targetEnemy;
		private Vector3 m_projectileSpawnPosition;
		private float m_lookForTargetTimer;
		private float m_shootTimer;
		#endregion
	
		#region MonoBehaviour Method(s):
		private void Awake()
		{
			m_projectileSpawnPosition = transform.Find("ProjectileSpawnPosition").position;
			m_towerDefenseRadiusTransform.localScale =  new Vector3(m_lookRadius * 2, m_lookRadius * 2, 1f);
		}
		private void Update()
		{
			HandleTargeting();
			HandleShooting();
		}
		#endregion

		#region Internally Used Method(s):
        private void HandleTargeting()
        {
            m_lookForTargetTimer -= Time.deltaTime;
            if (m_lookForTargetTimer <= 0f)
            {
                LookForTargets();
                m_lookForTargetTimer += m_lookForTargetTimerMax;
            }
        }

		private void HandleShooting()
		{
			m_shootTimer -= Time.deltaTime;

			if (m_shootTimer <= 0f)
			{
				m_shootTimer += m_shootTimerMax;

				if (m_targetEnemy != null)
				{
					ArrowProjectile.Create(m_projectileSpawnPosition, m_targetEnemy, m_damageAmount, m_moveSpeed);
				}
			}
		}
		private void LookForTargets()
		{
			Collider2D[] colliders2DArray = Physics2D.OverlapCircleAll(m_projectileSpawnPosition, m_lookRadius);

			foreach(Collider2D collider2D in colliders2DArray)
			{
				Enemy enemy = collider2D.GetComponent<Enemy>();

				if (enemy != null)
				{
					if (m_targetEnemy == null)
					{
						m_targetEnemy = enemy;
					}
					else
					{
						if (Vector3.Distance(transform.position, enemy.transform.position) < 
							Vector3.Distance(transform.position, m_targetEnemy.transform.position))
						{
							m_targetEnemy = enemy;
						}
					}
				}
			}
		}
		#endregion
	}
}