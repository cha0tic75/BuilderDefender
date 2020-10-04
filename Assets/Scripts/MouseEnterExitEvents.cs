// -------------------------------------------------------------------------
// CLASS	:	MouseEnterExitEvents
// Desc		:	Definition/Behaviour of MouseEnterExitEvents
// -------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace Project
{
    public class MouseEnterExitEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        #region Event/Delegates:
		public event EventHandler OnMouseEnter;
		public event EventHandler OnMouseExit;
        #endregion

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnMouseEnter?.Invoke(this, EventArgs.Empty);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnMouseExit?.Invoke(this, EventArgs.Empty);
        }
    }
}