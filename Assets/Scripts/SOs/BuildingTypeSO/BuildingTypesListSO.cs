// -------------------------------------------------------------------------
// CLASS	:	BuildingTypesListSO
// Desc		:	Definition/Behaviour of BuildingTypesListSO
// -------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project
{
	[CreateAssetMenu(menuName="Project/BuildingTypesList", fileName="New BuildingTypesList")]
	public class BuildingTypesListSO : ScriptableObject
	{
		#region Inspector Visibile Field(s):
		[FormerlySerializedAs("buildingTypeList")]
		public List<BuildingTypeSO> typesList;
		#endregion
	}
}