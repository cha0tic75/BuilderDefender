// -------------------------------------------------------------------------
// CLASS	:	ArrowProjectile
// Desc		:	Definition/Behaviour of ArrowProjectile
// -------------------------------------------------------------------------

using UnityEngine;

namespace Project
{
	public class ArrowProjectile : MonoBehaviour
	{
		#region Inspector Visibile Field(s):
		private int m_damageAmount = 10;
		private float m_moveSpeed = 10f;
		#endregion
		
		#region Internal State Field(s):
		private Enemy m_targetEnemy;
		private Vector3 m_lastMoveDirection;
		private float m_timeToDie = 2f;
		#endregion
	
		#region MonoBehaviour Method(s):
		private void Update()
		{
			Vector3 moveDir;

			if (m_targetEnemy != null)
			{
				moveDir = (m_targetEnemy.transform.position - transform.position).normalized;
				m_lastMoveDirection = moveDir;
			}
			else
			{
				moveDir = m_lastMoveDirection;
			}

			transform.position += moveDir * m_moveSpeed * Time.deltaTime;
			transform.eulerAngles = new Vector3(0f, 0f, UtilsClass.GetAngleFromVector(moveDir));

			m_timeToDie -= Time.deltaTime;

			if (m_timeToDie <= 0f)
			{
				Destroy(gameObject);
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			Enemy enemy = other.GetComponent<Enemy>();

			if (enemy != null)
			{
				// We hit an enemy:
				enemy.GetHealthSystem().Damage(m_damageAmount);
				Destroy(gameObject);
			}
		}
		#endregion

		#region Public API:
		public static ArrowProjectile Create(Vector3 position, Enemy enemyTarget, int damageAmount, float moveSpeed)
		{
			Transform arrowProjectileTransform = Instantiate(GameAssets.Instance.pfArrowProjectile, position, Quaternion.identity);

			ArrowProjectile arrowProjectile = arrowProjectileTransform.GetComponent<ArrowProjectile>();
			arrowProjectile.Initialize(enemyTarget, damageAmount, moveSpeed);

			return arrowProjectile;
		}

		#endregion

		#region Internally Used Method(s):
		private void Initialize(Enemy targetEnemy, int damageAmount, float moveSpeed)
		{
			m_damageAmount = damageAmount;
			m_moveSpeed = moveSpeed;
			m_targetEnemy = targetEnemy;
		}
		#endregion
	}
}