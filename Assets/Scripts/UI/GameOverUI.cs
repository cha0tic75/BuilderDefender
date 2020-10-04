// -------------------------------------------------------------------------
// CLASS	:	GameOverUI
// Desc		:	Definition/Behaviour of GameOverUI
// -------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Project
{
	public class GameOverUI : Singleton<GameOverUI>
	{
		#region Internally Used Field(s):
		private TextMeshProUGUI m_dayStatsText_TMP;
		private TextMeshProUGUI m_waveStatsText_TMP;
		#endregion

		#region Internally Used Field(s):
		private Transform m_containerTransform;
		#endregion

		#region MonoBehaviour Method(s):
		protected override void Awake()
		{
			base.Awake();

			m_containerTransform = transform.Find("Container");

			m_dayStatsText_TMP = m_containerTransform.Find("DayStatsText_TMP").GetComponent<TextMeshProUGUI>();
			m_waveStatsText_TMP = m_containerTransform.Find("WaveStatsText_TMP").GetComponent<TextMeshProUGUI>();

			m_containerTransform.Find("RetryButton").GetComponent<Button>().onClick.AddListener(() => {
				GameSceneManager.LoadScene(GameSceneManager.SceneNames.GameScene);
			});

			m_containerTransform.Find("MainMenuButton").GetComponent<Button>().onClick.AddListener(() => {
				GameSceneManager.LoadScene(GameSceneManager.SceneNames.MainMenuScene);
			});
		}

		private void Start()
		{
			Hide();
		}
		#endregion
	
		#region Public API:
		public void Show()
		{
			m_containerTransform.gameObject.SetActive(true);

			m_dayStatsText_TMP.SetText($"{DayNightCycle.Instance.GetTotalDaysPast():F1} days!");
			m_waveStatsText_TMP.SetText($"{EnemyWaveManager.Instance.GetWaveNumber()} {GameManager.Instance.WaveDisplayName}");
		}
		#endregion

		#region Internally Used Method(s):
		private void Hide()
		{
			m_containerTransform.gameObject.SetActive(false);
		}
		#endregion
	}
}