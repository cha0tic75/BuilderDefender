// -------------------------------------------------------------------------
// CLASS	:	MusicManager
// Desc		:	Definition/Behaviour of MusicManager
// -------------------------------------------------------------------------

using UnityEngine;

namespace Project
{
	public class MusicManager : MonoBehaviour
	{
		#region Internal State Field(s):
		private AudioSource m_audioSource;
		private float m_volume = 0.5f;

		private string m_volumePlayerPrefsKey = "MusicVolume";
		#endregion

		#region Properties:
		public static MusicManager Instance { get; private set; }
		#endregion

		#region MonoBehaviour Method(s):
		private void Awake()
		{
			Instance = this;
			m_volume = PlayerPrefs.GetFloat(m_volumePlayerPrefsKey, m_volume);
			m_audioSource = GetComponent<AudioSource>();
			m_audioSource.volume = m_volume;
		}
		#endregion

		#region Public API:

		public void IncreaseVolume()
		{
			SetVolume(m_volume + 0.1f);
		}

		public void DecreaseVolume()
		{
			SetVolume(m_volume - 0.1f);
		}

		public float GetVolume()
		{
			return m_volume;
		}
		#endregion

		#region Internally Used Method(s):
		private void SetVolume(float volume)
		{
			m_volume = Mathf.Clamp01(volume);
			m_audioSource.volume = m_volume;

			PlayerPrefs.SetFloat(m_volumePlayerPrefsKey, m_volume);
		}
		#endregion
	}
}