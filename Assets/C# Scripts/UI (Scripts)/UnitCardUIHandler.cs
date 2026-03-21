using TMPro;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(CanvasGroup))]
public class UnitCardUIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image icon;

    private CanvasGroup canvasGroup;


    public void Init()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetActiveState(bool isActive)
    {
        canvasGroup.alpha = isActive ? 1 : 0;
    }
    public void UpdateCardUI(UnitInfo unitInfo)
    {
        titleText.text = unitInfo.Name;
        titleText.color = unitInfo.Color;

        descriptionText.text = unitInfo.Description;
        icon.sprite = unitInfo.Icon;
    }
}
