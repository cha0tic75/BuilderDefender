// -------------------------------------------------------------------------
// CLASS	:	CinemachineShake
// Desc		:	Definition/Behaviour of CinemachineShake
// -------------------------------------------------------------------------

using UnityEngine;
using Cinemachine;

namespace Project
{
	public class CinemachineShake : Singleton<CinemachineShake>
	{
		#region Internal State Field(s):
		private CinemachineVirtualCamera m_cmVirtualCamera;
		private CinemachineBasicMultiChannelPerlin m_cmBasicMultiChannelPerlin;

		private float m_timer;
		private float m_timerMax;
		private float m_startingIntensity;
		#endregion
	
		#region MonoBehaviour Method(s):
		protected override void Awake()
		{
			base.Awake();

			m_cmVirtualCamera = GetComponent<CinemachineVirtualCamera>();
			m_cmBasicMultiChannelPerlin = m_cmVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
			m_cmBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
		}

		private void Update()
		{
			if (m_timer < m_timerMax)
			{
				m_timer += Time.deltaTime;
				float amplitude = Mathf.Lerp(m_startingIntensity, 0f, m_timer/ m_timerMax);
				m_cmBasicMultiChannelPerlin.m_AmplitudeGain = amplitude;
			
			}
		}
		#endregion

		#region Public API:
		public void ShakeCamera(float intensity, float timerMax)
		{
			m_startingIntensity = intensity;
			m_timerMax = timerMax;
			m_timer = 0f;

			m_cmBasicMultiChannelPerlin.m_AmplitudeGain = m_startingIntensity;
		}
		#endregion
	}
}