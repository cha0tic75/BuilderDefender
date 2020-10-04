// -------------------------------------------------------------------------
// CLASS	:	BuildingTypeSO
// Desc		:	Definition/Behaviour of BuildingTypeSO
// -------------------------------------------------------------------------

using System.Text;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project
{
    [CreateAssetMenu(menuName="Project/BuildingType", fileName="New Building Type")]
	public class BuildingTypeSO : ScriptableObject
	{
		#region Inspector Visibile Field(s):
		public string nameString;
		public Sprite sprite;
		public Transform prefab;
		public bool hasResourceGeneratorData = true;
		public ResourceGeneratorData resourceGeneratorData;
		public float minConstructionRadius;
		public ResourceAmount[] constructionResourceCostArray;
		public int healthAmountMax;
		public float constructionTimerMax;
		[Range(0f, 1f)] public float demolishResourceRecoupPercent = 0.6f;
		public float repairCostDividend = 2f;
		public ResourceRepairFactor[] repairFactors;
        #endregion

		#region Internally Used Field(s):
		private static StringBuilder stringbuilder; 
		#endregion

        public string GetConstructionResourceCostString()
        {
			string returnString = string.Empty;

			foreach(ResourceAmount resourceAmount in constructionResourceCostArray)
			{
				returnString += $"<size=80%><color=#{resourceAmount.resourceType.colorHex}>{resourceAmount.resourceType.nameShort}{resourceAmount.amount}</color></size> ";
			}

			return returnString;
        }
	}
}