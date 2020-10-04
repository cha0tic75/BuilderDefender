// -------------------------------------------------------------------------
// CLASS	:	ResourceTypeSO
// Desc		:	Definition/Behaviour of ResourceTypeSO
// -------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(menuName="Project/ResourceTypesList", fileName="ResourceTypesListSO")]
	public class ResourceTypesListSO : ScriptableObject
	{
		#region Inspector Visibile Field(s):
		public List<ResourceTypeSO> typesList;
		#endregion
	}
}