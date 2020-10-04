// -------------------------------------------------------------------------
// CLASS	:	DayNightCycle
// Desc		:	Definition/Behaviour of DayNightCycle
// -------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Project
{
	public class DayNightCycle : Singleton<DayNightCycle>
	{
		#region Inspector Visibile Field(s):
		[SerializeField] private Gradient m_gradient;
		[SerializeField] private float m_secondsPerDay = 10;
		#endregion
		
		#region Internal State Field(s):
		private Light2D m_light2D;
		private float dayTime;
		private float m_dayTimeSpeed;
		#endregion

		#region Properties:
		public float DaysSurvived => dayTime / 1f;
		#endregion
	
		#region MonoBehaviour Method(s):
		protected override void Awake()
		{
			base.Awake();
			m_light2D = GetComponent<Light2D>();
			m_dayTimeSpeed = 1 / m_secondsPerDay;
		}

		private void Update()
		{
			dayTime += Time.deltaTime * m_dayTimeSpeed;

			m_light2D.color = m_gradient.Evaluate(dayTime % 1f);
		}
		#endregion

		#region Public API:
		public float GetDayTime()
		{
			return dayTime;
		}

		public float GetTotalDaysPast()
		{
			return dayTime / 1f;
		}
		#endregion
	}
}