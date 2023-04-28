using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ChangeButtonColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI textMesh;
    public Image buttonImage;
    public Color hoverTextColor;
    public Color hoverButtonImageColor;

    private Color originalTextColor;
    private Color originalButtonImageColor;

    private void Start()
    {
        originalTextColor = textMesh.color;
        originalButtonImageColor = buttonImage.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        buttonImage.color = hoverButtonImageColor;
        originalButtonImageColor.a = 1f;
        textMesh.color = hoverTextColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textMesh.color = originalTextColor;
        buttonImage.color = originalButtonImageColor;
    }
}