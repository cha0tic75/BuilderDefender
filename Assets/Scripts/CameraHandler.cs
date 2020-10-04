// -------------------------------------------------------------------------
// CLASS	:	CameraHandler
// Desc		:	Definition/Behaviour of CameraHandler
// -------------------------------------------------------------------------

using UnityEngine;
using Cinemachine;

namespace Project
{
	public class CameraHandler : Singleton<CameraHandler>
	{
		#region Inspector Visibile Field(s):
		[Header("Settings")]
		[SerializeField] private float m_moveSpeed = 25f;
		[SerializeField] private float m_zoomAmount = 2f;
		[SerializeField] private float m_zoomSpeed = 5f;
		[SerializeField] private float m_minOrthographicSize = 10f;
		[SerializeField] private float m_maxOrthographicSize = 30f;
		[SerializeField] private float m_edgeScrollingsize = 30f;

		[Header("References")]
		[SerializeField] private CinemachineVirtualCamera m_cinemachineVirtualCamera;
		[SerializeField] private PolygonCollider2D m_cameraBounds;
		#endregion
		
		#region Internal State Field(s):
		private float m_orthographicSize;
		private float m_targetOrthographicSize;
		private bool m_edgeScrollingEnabled = false;
		private string m_edgeScrollingEnabledPlayerPrefsKey = "EdgeScrollingEnabled";
		#endregion
	
		#region MonoBehaviour Method(s):
		protected override void Awake()
		{
			base.Awake();
			m_edgeScrollingEnabled = PlayerPrefs.GetInt(m_edgeScrollingEnabledPlayerPrefsKey, m_edgeScrollingEnabled ? 1 : 0) == 1;
		}

		private void Start()
		{
			m_orthographicSize = m_cinemachineVirtualCamera.m_Lens.OrthographicSize;
			m_targetOrthographicSize = m_orthographicSize;
		}

		private void Update()
        {
            HandleMovement();

            HandleZoom();
        }
		#endregion

		#region Public API:
		public void SetEdgeScrollingEnabled(bool state)
		{
			m_edgeScrollingEnabled = state;
			PlayerPrefs.SetInt(m_edgeScrollingEnabledPlayerPrefsKey, m_edgeScrollingEnabled ? 1 : 0);
		}

		public bool GetEdgeScrollingEnabled()
		{
			return m_edgeScrollingEnabled;
		}
		#endregion

		#region Internally Used FIeld(s):
		private void HandleMovement()
		{
			float x = Input.GetAxisRaw("Horizontal");
			float y = Input.GetAxisRaw("Vertical");

			if (m_edgeScrollingEnabled || Input.GetMouseButton(2))
			{
				if (Input.mousePosition.x > Screen.width - m_edgeScrollingsize)
				{
					x = 1f;
				}
				else if(Input.mousePosition.x <  m_edgeScrollingsize)
				{
					x = -1f;
				}


				if (Input.mousePosition.y > Screen.height - m_edgeScrollingsize)
				{
					y = 1f;
				}
				else if(Input.mousePosition.y <  m_edgeScrollingsize)
				{
					y = -1f;
				}
			}

			Vector3 moveDir = new Vector3(x, y).normalized;

			Vector3 movementVector = (Input.GetKey(KeyCode.Tab)) ? 
					transform.position = Vector3.Lerp(transform.position, Vector3.zero, m_moveSpeed * Time.deltaTime)  : 
					transform.position + (moveDir * m_moveSpeed * Time.deltaTime);
			
			if (m_cameraBounds.bounds.Contains(movementVector))
			{
				transform.position = movementVector;
			}
		}

        private void HandleZoom()
        {
            m_targetOrthographicSize += -Input.mouseScrollDelta.y * m_zoomAmount;

            m_targetOrthographicSize = Mathf.Clamp(m_targetOrthographicSize, m_minOrthographicSize, m_maxOrthographicSize);

            m_orthographicSize = Mathf.Lerp(m_orthographicSize, m_targetOrthographicSize, Time.deltaTime * m_zoomSpeed);
            m_cinemachineVirtualCamera.m_Lens.OrthographicSize = m_orthographicSize;
        }
        #endregion
    }
}