using System;
using UnityEngine;
using UnityEngine.UI;

public class BinocularMenu : MonoBehaviour
{

    [SerializeField] Transform Zooms;
    [SerializeField] Sprite zoomSelected;
    [SerializeField] Sprite zoomUnselecked;

    private void OnEnable()
    {
        BinocularsController.onViewFieldChoosed += SelectViewObject;
    }

    private void SelectViewObject(int value)
    {
        for (int i = 0; i < Zooms.childCount; i++)
        {
            Transform child = Zooms.GetChild(i);
            Image img = child.GetComponent<Image>();

            if (img != null)
            {
                if (i == value)
                {
                    img.sprite = zoomSelected;
                }
                else
                {
                    img.sprite = zoomUnselecked;
                }
            }
        }
    }

    private void OnDisable()
    {
        BinocularsController.onViewFieldChoosed -= SelectViewObject;
    }
}
