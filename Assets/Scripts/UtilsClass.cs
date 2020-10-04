// -------------------------------------------------------------------------
// CLASS	:	UtilsClass
// Desc		:	Definition/Behaviour of UtilsClass
// -------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
	public static class UtilsClass
	{
		#region Internal State Field(s):
		private static Camera s_mainCamera;
		#endregion
		
		#region Public API:
		public static Vector3 GetMouseWorldPosition()
		{
			if (s_mainCamera == null) s_mainCamera = Camera.main;

			Vector3 mouseWorldPosition = s_mainCamera.ScreenToWorldPoint(Input.mousePosition);
			mouseWorldPosition.z = 0f;

			return mouseWorldPosition;
		}

		public static Vector3 GetRandomDir()
		{
			return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
		}

		public static float GetAngleFromVector(Vector3 vector)
		{
			return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
		}

		public static string FirstLetterToUpper(string str)
		{
			if (str == null)
				return null;

			if (str.Length > 1)
				return char.ToUpper(str[0]) + str.Substring(1);

			return str.ToUpper();
		}
		#endregion
	}
}