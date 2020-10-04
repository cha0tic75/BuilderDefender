// -------------------------------------------------------------------------
// CLASS	:	HealthSystem
// Desc		:	Definition/Behaviour of HealthSystem
// -------------------------------------------------------------------------

using System;
using UnityEngine;

namespace Project
{
	public class HealthSystem : MonoBehaviour
	{
		#region Event/Delegate(s):
		public event EventHandler OnHealthAmountMaxChanged;
		public event EventHandler OnDamaged;
		public event EventHandler OnHealed;
		public event EventHandler OnDeath;
		#endregion

		#region Inspector Assigned Field(s):
		[SerializeField] private int m_healthAmountMax;
		#endregion
		
		#region Internal State Field(s):
		private int m_healthAmount;
		#endregion
	
		#region MonoBehaviour Method(s):
		private void Awake()
		{
			m_healthAmount = m_healthAmountMax;
		}
		#endregion

		#region Public API:
		public void Damage(int damageAmount)
		{
			m_healthAmount -= damageAmount;
			m_healthAmount = Mathf.Clamp(m_healthAmount, 0, m_healthAmountMax);
			
			OnDamaged?.Invoke(this, EventArgs.Empty);

			if (IsDead())
			{
				OnDeath?.Invoke(this, EventArgs.Empty);
			}
		}
		public void Heal(int damageAmount)
		{
			m_healthAmount += damageAmount;
			m_healthAmount = Mathf.Clamp(m_healthAmount, 0, m_healthAmountMax);
			OnHealed?.Invoke(this, EventArgs.Empty);
		}

		public void HealFull()
		{
			Heal(m_healthAmountMax);
		}

		public bool IsDead()
		{
			return m_healthAmount == 0;
		}

		public bool IsFullHealth()
		{
			return m_healthAmount == m_healthAmountMax;
		}
		
		public int GetHealthAmount()
		{
			return m_healthAmount;
		}

		public int GetHealthAmountMax()
		{
			return m_healthAmountMax;
		}

		public int GetHealthAmountDifference()
		{
			return m_healthAmountMax - m_healthAmount;
		}

		public void SetHealthAmountMax(int healthAmountMax, bool updateHealthAmount)
		{
			m_healthAmountMax = healthAmountMax;

			if (updateHealthAmount)
			{
				m_healthAmount = m_healthAmountMax;
			}

			OnHealthAmountMaxChanged?.Invoke(this, EventArgs.Empty);
		}

		public float GetHealthAmountNormalized()
		{
			return (float)m_healthAmount / m_healthAmountMax;
		}
		#endregion
	}
}