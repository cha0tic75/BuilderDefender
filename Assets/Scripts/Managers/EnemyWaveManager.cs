// -------------------------------------------------------------------------
// CLASS	:	EnemyWaveManager
// Desc		:	Definition/Behaviour of EnemyWaveManager
// -------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
	public class EnemyWaveManager : Singleton<EnemyWaveManager>
	{
		#region Enum(s):
		private enum State
		{
			WaitingToSpawnWave,
			SpawningWave
		}
		#endregion

		#region Event/Delegate(s):
		public event EventHandler OnWaveNumberChanged;
		#endregion

		#region Inspector Assigned Field(s):
		[SerializeField] private List<Transform> m_spawnPositionTransformList; 
		[SerializeField] private Transform m_nextWaveSpawnPositionTransform;
		[SerializeField] private float m_firstWaveStartTime = 5f;
		[SerializeField] private float m_minTimeBetweenWaves = 10f;
		[SerializeField] private float m_maxTimeBetweenWaves = 15f;
		[SerializeField] private int m_decreaseWaveTimeEveryWaveCount = 5;
		[SerializeField] private float m_dropTimeBetweenWavesFactor = 0.2f;
		[SerializeField] private int m_fixedEnemySpawnAmount = 3;
		[SerializeField] private int m_scaledEnemySpawnCount = 2;
		#endregion
		
		#region Internal State Field(s):
		private State m_state;
		private int m_waveNumber;
		private float m_nextWaveSpawnTimer;
		private float m_nextEnemySpawnTimer;
		private int m_remainingEnemySpawnAmount;
		private Vector3	m_spawnPosition;
		#endregion

		#region MonoBehaviour Method(s):
		private void Start()
		{
			m_state = State.WaitingToSpawnWave;
			m_nextWaveSpawnTimer = m_firstWaveStartTime;
			SelectNextSpawnPosition();
		}

		private void Update()
		{
			switch (m_state)
			{
                case State.WaitingToSpawnWave:
                    WaitToSpawnWaveState();
                    break;

                case State.SpawningWave:
                    SpawningWaveState();
                    break;
            }
		}
        #endregion

        #region Public API:
        public int GetWaveNumber()
		{
			return m_waveNumber;
		}

		public float GetNextWaveSpawnTimer()
		{
			return m_nextWaveSpawnTimer;
		}

		public Vector3 GetNextSpawnPosition()
		{
			return m_spawnPosition;
		}
		#endregion

		#region Internally Used Method(s):
        private void WaitToSpawnWaveState()
        {
            m_nextWaveSpawnTimer -= Time.deltaTime;

            if (m_nextWaveSpawnTimer <= 0f)
            {
                SpawnWave();
            }
        }

        private void SpawningWaveState()
        {
			//SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyWaveStarting);
            if (m_remainingEnemySpawnAmount > 0)
            {
                m_nextEnemySpawnTimer -= Time.deltaTime;

                if (m_nextEnemySpawnTimer < 0f)
                {
                    m_nextEnemySpawnTimer = UnityEngine.Random.Range(0f, 0.2f);
                    Enemy.Create(m_spawnPosition + UtilsClass.GetRandomDir() * UnityEngine.Random.Range(0f, 15f));
                    m_remainingEnemySpawnAmount--;

                    if (m_remainingEnemySpawnAmount <= 0)
                    {
                        m_state = State.WaitingToSpawnWave;
                        SelectNextSpawnPosition();

						// Here is where we will decrease the time based on wave count
						if (m_waveNumber % m_decreaseWaveTimeEveryWaveCount == 0)
						{
							m_minTimeBetweenWaves = Mathf.Clamp(m_minTimeBetweenWaves - m_dropTimeBetweenWavesFactor, m_dropTimeBetweenWavesFactor, 100f); // MaxVal just set to a high number.
							m_maxTimeBetweenWaves = Mathf.Clamp(m_maxTimeBetweenWaves - m_dropTimeBetweenWavesFactor, m_dropTimeBetweenWavesFactor * 2f, 100f); // MaxVal just set to a high number.
						}

						m_nextWaveSpawnTimer = UnityEngine.Random.Range(m_maxTimeBetweenWaves, m_maxTimeBetweenWaves);
                    }
                }
            }
        }

		private void SpawnWave()
		{	
			m_remainingEnemySpawnAmount = m_fixedEnemySpawnAmount + m_scaledEnemySpawnCount * m_waveNumber;
			m_state = State.SpawningWave;
			
			m_waveNumber++;
			OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
		}

		private void SelectNextSpawnPosition()
		{
			m_spawnPosition = m_spawnPositionTransformList[UnityEngine.Random.Range(0, m_spawnPositionTransformList.Count)].position;
			m_nextWaveSpawnPositionTransform.position = m_spawnPosition;
		}
		#endregion
	}
}