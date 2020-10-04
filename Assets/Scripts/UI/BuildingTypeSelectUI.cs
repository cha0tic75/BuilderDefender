// -------------------------------------------------------------------------
// CLASS	:	BuildingTypeSelectUI
// Desc		:	Definition/Behaviour of BuildingTypeSelectUI
// -------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
	public class BuildingTypeSelectUI : MonoBehaviour
	{
		#region Inspector Visibile Field(s):
		[SerializeField] private float m_buttonOffset = 130f;
		[SerializeField] private Sprite arrowSprite;

		[SerializeField] private List<BuildingTypeSO> m_ignoreBuildingTypeList;
		#endregion
		
		#region Internal State Field(s):
		private Dictionary<BuildingTypeSO, Transform> m_buttonTransformDictionary;
		private Transform m_arrowButton;
		#endregion
	
		#region MonoBehaviour Method(s):
		private void Awake()
		{
			Transform buttonTemplate = transform.Find("ButtonTemplate");
			buttonTemplate.gameObject.SetActive(false);

			BuildingTypesListSO buildingTypesList = Resources.Load<BuildingTypesListSO>(typeof(BuildingTypesListSO).Name);

			m_buttonTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();
			
			int index = 0;

			m_arrowButton = Instantiate(buttonTemplate, transform);
			m_arrowButton.gameObject.SetActive(true);

			m_arrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(m_buttonOffset * index, 0f);

			m_arrowButton.Find("Image").GetComponent<Image>().sprite = arrowSprite;
			m_arrowButton.Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -30);

			m_arrowButton.GetComponent<Button>().onClick.AddListener(() => {
				BuildingManager.Instance.SetActiveBuildingType(null);
			});

			MouseEnterExitEvents mouseEnterExitEvents = m_arrowButton.GetComponent<MouseEnterExitEvents>();
			mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) => 
			{
				TooltipUI.Instance.Show("Arrow");
			};

			mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) => 
			{
				TooltipUI.Instance.Hide();
			};

			index++;
	
			foreach(BuildingTypeSO buildingType in buildingTypesList.typesList)
			{
				if (m_ignoreBuildingTypeList.Contains(buildingType)) continue;

				Transform buttonTransform = Instantiate(buttonTemplate, transform);
				buttonTransform.gameObject.SetActive(true);

				buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(m_buttonOffset * index, 0f);

				buttonTransform.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;
				buttonTransform.GetComponent<Button>().onClick.AddListener(() => 
				{
					BuildingManager.Instance.SetActiveBuildingType(buildingType);
				});

				mouseEnterExitEvents = buttonTransform.GetComponent<MouseEnterExitEvents>();
				mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) => 
				{
					TooltipUI.Instance.Show($"{buildingType.nameString}\n{buildingType.GetConstructionResourceCostString()}");
				};

				mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) => 
				{
					TooltipUI.Instance.Hide();
				};


				m_buttonTransformDictionary[buildingType] = buttonTransform;
				index++;
			}
		}

		private void Start()
		{
			BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
			UpdateactiveBuildingTypeButton();
		}
		#endregion

		#region Internally Used Method(s):
		private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
		{
			UpdateactiveBuildingTypeButton();
		}
	
		private void UpdateactiveBuildingTypeButton()
		{
			m_arrowButton.Find("Selected").gameObject.SetActive(false);
			foreach(BuildingTypeSO buildingType in m_buttonTransformDictionary.Keys)
			{
				Transform buttonTransform = m_buttonTransformDictionary[buildingType];

				buttonTransform.Find("Selected").gameObject.SetActive(false);
			}

			BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();

			if (activeBuildingType == null)
			{
				m_arrowButton.Find("Selected").gameObject.SetActive(true);
			}
			else
			{
				m_buttonTransformDictionary[activeBuildingType].Find("Selected").gameObject.SetActive(true);
			}
		}
		#endregion
	}
}