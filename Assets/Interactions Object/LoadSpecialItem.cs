using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSpecialItem : MonoBehaviour
{
    public enum SpecialItemIndex
    {
        SWORD,
        SHIELD,
        NONE
    }

    [SerializeField] private List<Sprite> iconSprites;

    [SerializeField] private Image icon;

    [SerializeField] private GameObject disableIcon;

    private InteractionManager interactionManager;

    private SpecialItemIndex itemIndex;

    private void Start()
    {
        interactionManager = FindObjectOfType<InteractionManager>();
    }

    public void Load(SpecialItemIndex index)
    {
        itemIndex = index;
        LoadIcon(index);
        DisableItem(index);
    }

    private void LoadIcon(SpecialItemIndex index)
    {
        if (index == SpecialItemIndex.SWORD)
            icon.sprite = iconSprites[0];

        if (index == SpecialItemIndex.SHIELD)
            icon.sprite = iconSprites[1];

        if (index == SpecialItemIndex.NONE)
            icon.sprite = null;
    }

    private void DisableItem(SpecialItemIndex index)
    {
        disableIcon.SetActive(false);

        if (index == SpecialItemIndex.SWORD && GameManager.Shared.Swords <= 0)
            disableIcon.SetActive(true);

        if (index == SpecialItemIndex.SHIELD && GameManager.Shared.Shields <= 0)
            disableIcon.SetActive(true);
    }

    public void ClickItem()
    {
        if (itemIndex == SpecialItemIndex.SWORD && GameManager.Shared.Swords <= 0)
            return;

        if (itemIndex == SpecialItemIndex.SHIELD && GameManager.Shared.Shields <= 0)
            return;

        disableIcon.SetActive(true);

        interactionManager.ActivateSpecialItem(itemIndex);
    }
}