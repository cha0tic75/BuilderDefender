// -------------------------------------------------------------------------
// CLASS	:	TooltipUI
// Desc		:	Definition/Behaviour of TooltipUI
// -------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Project
{
	public class TooltipUI : Singleton<TooltipUI>
	{
		#region Inspector Visibile Field(s):
		[SerializeField] private RectTransform m_canvasRectTransform;
		#endregion
		
		#region Internal State Field(s):
		private TextMeshProUGUI m_textMeshPro;
		private RectTransform m_backgroundRectTransform;
		private RectTransform m_rectTransform;
		private TooltipTimer m_tooltipTimer;
		#endregion
	
		#region MonoBehaviour Method(s):
		protected override void Awake()
		{
			base.Awake();

			m_rectTransform = GetComponent<RectTransform>();
			m_textMeshPro = transform.Find("Text_TMP").GetComponent<TextMeshProUGUI>();
			m_backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();

			Hide();
		}

		private void Update()
        {
            HandleFollowMouse();

            if (m_tooltipTimer != null)
            {
                m_tooltipTimer.timer -= Time.deltaTime;

                if (m_tooltipTimer.timer <= 0f)
                {
                    Hide();
                }
            }
        }
        #endregion

        #region Public API:
        public void Show(string tooltip, TooltipTimer tooltipTimer = null)
		{
			gameObject.SetActive(true);
			m_tooltipTimer = tooltipTimer;
			SetText(tooltip);
			HandleFollowMouse();
		}

		public void Hide()
		{
			m_tooltipTimer = null;
			gameObject.SetActive(false);
		}
		#endregion

		#region Internally Used Method(s):
        private void HandleFollowMouse()
        {
            float padding = 10f;
            Vector3 anchoredPosition = Input.mousePosition / m_canvasRectTransform.localScale.x;

            anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, padding, m_canvasRectTransform.rect.width - m_backgroundRectTransform.rect.width - padding);
            anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, padding, m_canvasRectTransform.rect.height - m_backgroundRectTransform.rect.height - padding);

            m_rectTransform.anchoredPosition = anchoredPosition;
        }

		private void SetText(string tooltipText)
		{
			m_textMeshPro.SetText(tooltipText);
			m_textMeshPro.ForceMeshUpdate();

			Vector2 textSize = m_textMeshPro.GetRenderedValues(false);
			Vector2 padding = new Vector2(10f, 10f);
			m_backgroundRectTransform.sizeDelta = textSize + padding;
		}
		#endregion

		public class TooltipTimer
		{
			public float timer;
		}
	}
}