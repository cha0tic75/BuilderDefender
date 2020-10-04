// -------------------------------------------------------------------------
// CLASS	:	SoundManager
// Desc		:	Definition/Behaviour of SoundManager
// -------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
	public class SoundManager : MonoBehaviour
	{
		#region Enum(s):
		public enum Sound 
		{
			BuildingPlaced, 
			BuildingDamaged,
			BuildingDestroyed,
			EmeyDie,
			EnemyHit,
			EnemyWaveStarting, 
			GameOver,
		}
		#endregion

		#region Properties:
		public static SoundManager Instance { get; private set; }
		#endregion
		
		#region Internal State Field(s):
		private AudioSource m_audioSource;
		private Dictionary<Sound, AudioClip> m_audioClipDictionary;
		private float m_volume = 0.5f;
		private string m_volumePlayerPrefsKey = "SoundVolume";
		#endregion
	
		#region MonoBehaviour Method(s):
		private void Awake()
		{
			Instance = this;

			m_volume = PlayerPrefs.GetFloat(m_volumePlayerPrefsKey, m_volume);

			m_audioSource = GetComponent<AudioSource>();
			m_audioClipDictionary = new Dictionary<Sound, AudioClip>();

			foreach(Sound sound in System.Enum.GetValues(typeof(Sound)))
			{
				m_audioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
			}
		}
		#endregion

		#region Public API:
		public void PlaySound(Sound sound)
		{
			m_audioSource.PlayOneShot(m_audioClipDictionary[sound], m_volume);
		}

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
			PlayerPrefs.SetFloat(m_volumePlayerPrefsKey, m_volume);
		}
		#endregion
	}
}