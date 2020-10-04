// -------------------------------------------------------------------------
// CLASS	:	EnemyWaveUI
// Desc		:	Definition/Behaviour of EnemyWaveUI
// -------------------------------------------------------------------------

using UnityEngine;
using TMPro;

namespace Project
{
	public class EnemyWaveUI : Singleton<EnemyWaveUI>
	{
		#region Inspector Visibile Field(s):
		[SerializeField] private EnemyWaveManager m_enemyWaveManager;
		[SerializeField] private float m_nearestEnemyIndicatorOffset =  250f;
		[SerializeField] private float m_enemyWaveSpawnPositionIndicatorOffset = 350f;
		#endregion
		
		#region Internal State Field(s):
		private TextMeshProUGUI m_waveNumberText_TMP;
		private TextMeshProUGUI m_waveMessageText_TMP;
		private RectTransform m_enemyWaveSpawnPositionIndicatorRect;
		private RectTransform m_nearestEnemyPositionIndicator;

		private Camera m_mainCamera;
		#endregion
	
		#region MonoBehaviour Method(s):
		protected override void Awake()
		{
			base.Awake();

			m_waveNumberText_TMP = transform.Find("WaveNumberText_TMP").GetComponent<TextMeshProUGUI>();
			m_waveMessageText_TMP = transform.Find("WaveMessageText_TMP").GetComponent<TextMeshProUGUI>();
			m_enemyWaveSpawnPositionIndicatorRect = transform.Find("EnemyWaveSpawnPositionIndicator").GetComponent<RectTransform>();
			m_nearestEnemyPositionIndicator = transform.Find("NearestEnemyPositionIndicator").GetComponent<RectTransform>();	
		}

		private void Start()
		{
			m_mainCamera = Camera.main;
			m_enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
			SetWaveNumberText($"{UtilsClass.FirstLetterToUpper(GameManager.Instance.WaveDisplayName)} {m_enemyWaveManager.GetWaveNumber()}");
		}

		private void Update()
        {
            HandleNextWaveMessage();
            HandleEnemyWaveSpawnPositionIndicator();
			HandleNearestEnemyPositionIndicator();
        }
        #endregion

        #region Internally Used Method(s):
        private void EnemyWaveManager_OnWaveNumberChanged(object sender, System.EventArgs e)
		{
			SetWaveNumberText($"{UtilsClass.FirstLetterToUpper(GameManager.Instance.WaveDisplayName)} {m_enemyWaveManager.GetWaveNumber()}");
		}

        private void SetWaveNumberText(string text)
		{
			m_waveNumberText_TMP.SetText(text);
		}
		private void SetMessageText(string message)
		{
			m_waveMessageText_TMP.SetText(message);
		}

        private void HandleNextWaveMessage()
        {
            float nextWaveSpawnTimer = m_enemyWaveManager.GetNextWaveSpawnTimer();

            if (nextWaveSpawnTimer <= 0f)
            {
                SetMessageText("");
            }
            else
            {
                SetMessageText($"Next {GameManager.Instance.WaveDisplayName} in {nextWaveSpawnTimer:F1}s");
            }
        }

		private void HandleEnemyWaveSpawnPositionIndicator()
        {
            Vector3 directionToNextSpawnPos = (m_enemyWaveManager.GetNextSpawnPosition() - m_mainCamera.transform.position).normalized;
            m_enemyWaveSpawnPositionIndicatorRect.anchoredPosition = directionToNextSpawnPos * m_enemyWaveSpawnPositionIndicatorOffset;
            m_enemyWaveSpawnPositionIndicatorRect.eulerAngles = new Vector3(0f, 0f, UtilsClass.GetAngleFromVector(directionToNextSpawnPos));

            float distanceToNextSpawnPos = Vector3.Distance(m_enemyWaveManager.GetNextSpawnPosition(), m_mainCamera.transform.position);

            m_enemyWaveSpawnPositionIndicatorRect.gameObject.SetActive(distanceToNextSpawnPos > m_mainCamera.orthographicSize * 1.5f);
        }

		private void HandleNearestEnemyPositionIndicator()
        {

			Collider2D[] colliders2DArray = Physics2D.OverlapCircleAll(m_mainCamera.transform.position, 99999f);

			Enemy targetEnemy = null;

			foreach(Collider2D collider2D in colliders2DArray)
			{
				Enemy enemy = collider2D.GetComponent<Enemy>();

				if (enemy != null)
				{
					if (targetEnemy == null)
					{
						targetEnemy = enemy;
					}
					else
					{
						if (Vector3.Distance(m_mainCamera.transform.position, enemy.transform.position) < 
							Vector3.Distance(m_mainCamera.transform.position, targetEnemy.transform.position))
						{
							targetEnemy = enemy;
						}
					}
				}
			}

			if (targetEnemy != null)
			{
				Vector3 directionToNearestEnemyPos = (targetEnemy.transform.position - m_mainCamera.transform.position).normalized;
				m_nearestEnemyPositionIndicator.anchoredPosition = directionToNearestEnemyPos * m_nearestEnemyIndicatorOffset;
				m_nearestEnemyPositionIndicator.eulerAngles = new Vector3(0f, 0f, UtilsClass.GetAngleFromVector(directionToNearestEnemyPos));

				float distanceToNearestEnemyPos = Vector3.Distance(targetEnemy.transform.position, m_mainCamera.transform.position);

				m_nearestEnemyPositionIndicator.gameObject.SetActive(distanceToNearestEnemyPos > m_mainCamera.orthographicSize * 1.5f);
			}
			else
			{
				m_nearestEnemyPositionIndicator.gameObject.SetActive(false);
			}
        }
		#endregion
	}
}