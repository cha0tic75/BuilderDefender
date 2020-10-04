// -------------------------------------------------------------------------
// CLASS	:	OptionsUI
// Desc		:	Definition/Behaviour of OptionsUI
// -------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Project
{
	public class OptionsUI : MonoBehaviour
	{	
		#region Internal State Field(s):
		private TextMeshProUGUI m_soundVolumeText;
		private TextMeshProUGUI m_musicVolumeText;
		#endregion
	
		#region MonoBehaviour Method(s):
		private void Awake()
		{
			m_soundVolumeText = transform.Find("SoundVolumeText_TMP").GetComponent<TextMeshProUGUI>();
			m_musicVolumeText = transform.Find("MusicVolumeText_TMP").GetComponent<TextMeshProUGUI>();

			// Sound Increase Button:
			transform.Find("SoundIncreaseButton").GetComponent<Button>().onClick.AddListener(() => {
				SoundManager.Instance.IncreaseVolume();
				UpdateVolumeText(m_soundVolumeText, SoundManager.Instance.GetVolume());
			});

			// Sound Decrease Button:
			transform.Find("SoundDecreaseButton").GetComponent<Button>().onClick.AddListener(() => {
				SoundManager.Instance.DecreaseVolume();
				UpdateVolumeText(m_soundVolumeText, SoundManager.Instance.GetVolume());
			});

			// Music Increase Button:
			transform.Find("MusicIncreaseButton").GetComponent<Button>().onClick.AddListener(() => {
				MusicManager.Instance.IncreaseVolume();
				UpdateVolumeText(m_musicVolumeText, MusicManager.Instance.GetVolume());
			});

			// Music Decrease Button:
			transform.Find("MusicDecreaseButton").GetComponent<Button>().onClick.AddListener(() => {
				MusicManager.Instance.DecreaseVolume();
				UpdateVolumeText(m_musicVolumeText, MusicManager.Instance.GetVolume());
			});

			// Main Menu Button:
			transform.Find("MainMenuButton").GetComponent<Button>().onClick.AddListener(() => {
				ToggleVisibility();
				GameSceneManager.LoadScene(GameSceneManager.SceneNames.MainMenuScene);
			});

			// Toggle Edge Scrolling:

			transform.Find("EdgeScrollingToggle").GetComponent<Toggle>().onValueChanged.AddListener((bool value) => {
				//UPDateToggle
				CameraHandler.Instance.SetEdgeScrollingEnabled(value);
			});
		}

		private void Start()
		{
			transform.Find("EdgeScrollingToggle").GetComponent<Toggle>().SetIsOnWithoutNotify(CameraHandler.Instance.GetEdgeScrollingEnabled());

			UpdateVolumeText(m_soundVolumeText, SoundManager.Instance.GetVolume());
			UpdateVolumeText(m_musicVolumeText, MusicManager.Instance.GetVolume());

			ToggleVisibility();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				ToggleVisibility();
			}
		}
		#endregion

		#region Public API:
		public void ToggleVisibility()
		{
			gameObject.SetActive(!gameObject.activeSelf);

			Time.timeScale = (gameObject.activeSelf) ? 0f : 1f;
		}
		#endregion

		#region Internally Used Method(s):
		private void UpdateVolumeText(TextMeshProUGUI tmp, float value)
		{
			int displayValue = Mathf.RoundToInt(value * 10);
			tmp.SetText($"{displayValue}");
		}
		#endregion
	}
}