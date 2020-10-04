// -------------------------------------------------------------------------
// CLASS	:	ResourceTypeSO
// Desc		:	Definition/Behaviour of ResourceTypeSO
// -------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(menuName="Project/ResourceType", fileName="ResourceTypeSO")]
	public class ResourceTypeSO : ScriptableObject
	{
		#region Inspector Visibile Field(s):
		public string nameString;
		public string nameShort;
		public Sprite sprite;
		public string colorHex;
		#endregion
	}
}