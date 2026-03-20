using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitCardUIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image icon;

    private CanvasGroup canvasGroup;


    private void Awake()
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
        descriptionText.text = unitInfo.Description;
        icon.sprite = unitInfo.Icon;
    }
}
