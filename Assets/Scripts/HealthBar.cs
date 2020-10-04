// -------------------------------------------------------------------------
// CLASS	:	HealthBar
// Desc		:	Definition/Behaviour of HealthBar
// -------------------------------------------------------------------------

using System;
using UnityEngine;

namespace Project
{
	public class HealthBar : MonoBehaviour
	{
		#region Inspector Visibile Field(s):
		[SerializeField] private HealthSystem m_healthSystem;
		[SerializeField] private int m_healthAmountPerSeperator = 10;
		#endregion
		
		#region Internal State Field(s):
		private Transform m_barTransform;
		private Transform m_seperatorContainer;
		#endregion
	
		#region MonoBehaviour Method(s):
		private void Awake()
		{
			m_barTransform = transform.Find("Bar");
		}

		private void Start()
        {
            m_seperatorContainer = transform.Find("SeperatorContainer");
            
			ConstructHealthBarSeperators();

            m_healthSystem.OnDamaged += HealthSystem_OnDamaged;
            m_healthSystem.OnHealed += HealthSystem_OnHealed;
            m_healthSystem.OnHealthAmountMaxChanged += HealthSystem_OnHealthAmountMaxChanged;

            UpdateHealthBar();
            UpdateHealthBarVisibility();
        }
        #endregion

        #region Internally Used Method(s):
        private void HealthSystem_OnDamaged(object sender, EventArgs e)
		{
			UpdateHealthBar();
			UpdateHealthBarVisibility();
		}

		private void HealthSystem_OnHealed(object sender, EventArgs e)
		{
			UpdateHealthBar();
			UpdateHealthBarVisibility();
		}

		private void HealthSystem_OnHealthAmountMaxChanged(object sender, EventArgs e)
		{
			ConstructHealthBarSeperators();
		}

		private void UpdateHealthBar()
		{
			m_barTransform.localScale = new Vector3( m_healthSystem.GetHealthAmountNormalized(), 1f, 1f);
		}

		private void UpdateHealthBarVisibility()
		{
			//gameObject.SetActive(!m_healthSystem.IsFullHealth());
			gameObject.SetActive(true);
		}

        private void ConstructHealthBarSeperators()
        {
            Transform seperatorTempate = m_seperatorContainer.Find("SeperatorTemplate");

			foreach(Transform seperatorTransform in m_seperatorContainer)
			{
				if (seperatorTransform == seperatorTempate) continue;
				Destroy(seperatorTransform.gameObject);
			}

            seperatorTempate.gameObject.SetActive(false);

            float barSize = 3f;
            float barOneHealthAmountSize = barSize / m_healthSystem.GetHealthAmountMax();
            int healthSeperatorCount = Mathf.FloorToInt(m_healthSystem.GetHealthAmountMax() / m_healthAmountPerSeperator);

            for (int i = 1; i < healthSeperatorCount; i++)
            {
                Transform seperatorTransform = Instantiate(seperatorTempate, m_seperatorContainer);
                seperatorTransform.gameObject.SetActive(true);
                seperatorTransform.localPosition = new Vector3(barOneHealthAmountSize * i * m_healthAmountPerSeperator, 0f, 0f);
            }
        }
		#endregion	
	}
}