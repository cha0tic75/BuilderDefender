// -------------------------------------------------------------------------
// CLASS	:	SimpleSingleton
// Desc		:	Definition/Behaviour of SimpleSingleton
// -------------------------------------------------------------------------

using UnityEngine;

namespace Project
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
        #region Inspector Assigned Field(s):
        [SerializeField] private bool m_dontDestroyOnLoad = true;
        #endregion

        #region Properties:
        public static T Instance { get; private set; }
        #endregion

        #region MonoBehaviour Specific Method(s):
        protected virtual void Awake() 
		{
			if (Instance != null)
			{
                DestroyImmediate(this);
                return;
            }

            Instance = this as T;

            if (m_dontDestroyOnLoad)
            {
                DontDestroyOnLoad(this);
            }
        }
        #endregion
	}
}