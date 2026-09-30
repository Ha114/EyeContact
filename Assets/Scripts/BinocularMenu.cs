using UnityEngine;
using UnityEngine.UI;

public class BinocularMenu : MonoBehaviour
{
    [SerializeField] private Transform Zooms;
    [SerializeField] private Sprite zoomSelected;
    [SerializeField] private Sprite zoomUnselecked;

    private void OnEnable()
    {
        BinocularController.onViewFieldChoosed += SelectViewObject;
    }

    private void SelectViewObject(int value)
    {
        for (int i = 0; i < Zooms.childCount; i++)
        {
            Transform child = Zooms.GetChild(i);
            Image img = child.GetComponent<Image>();

            if (img != null)
            {
                img.sprite = i == value
                    ? zoomSelected
                    : zoomUnselecked;
            }
        }
    }

    private void OnDisable()
    {
        BinocularController.onViewFieldChoosed -= SelectViewObject;
    }
}