using System.Collections;
using System.Collections.Generic;
using FMODUnity;
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

<<<<<<< HEAD
<<<<<<< HEAD
    [Space(20)] [Header("Sounds")] [SerializeField]
    private EventReference hoverSound;

    [SerializeField] private EventReference clickSound;
=======
=======
>>>>>>> 39852ffa05725920d14df9f071758ecd1c3d3a10
    [Space(20)] [Header("Sounds")] 
    [SerializeField] private EventReference hoverSound;
    [SerializeField] private EventReference clickSwordSound;
    [SerializeField] private EventReference clickShieldSound;
<<<<<<< HEAD
>>>>>>> 39852ffa05725920d14df9f071758ecd1c3d3a10
=======
>>>>>>> 39852ffa05725920d14df9f071758ecd1c3d3a10

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

<<<<<<< HEAD
<<<<<<< HEAD
=======
=======
>>>>>>> 39852ffa05725920d14df9f071758ecd1c3d3a10
        if (itemIndex == SpecialItemIndex.SWORD)
            PlayClickSwordSound();
        if (itemIndex == SpecialItemIndex.SHIELD)
            PlayClickShieldSound();
        
<<<<<<< HEAD
>>>>>>> 39852ffa05725920d14df9f071758ecd1c3d3a10
=======
>>>>>>> 39852ffa05725920d14df9f071758ecd1c3d3a10
        disableIcon.SetActive(true);

        interactionManager.ActivateSpecialItem(itemIndex);
    }

    public void PlayHoverSound()
    {
        RuntimeManager.PlayOneShot(hoverSound);
    }
<<<<<<< HEAD
<<<<<<< HEAD

    public void PlayClickSound()
    {
        RuntimeManager.PlayOneShot(clickSound);
=======
=======
>>>>>>> 39852ffa05725920d14df9f071758ecd1c3d3a10
    
    private void PlayClickSwordSound()
    {
        RuntimeManager.PlayOneShot(clickSwordSound);
    }
    
    private void PlayClickShieldSound()
    {
        RuntimeManager.PlayOneShot(clickShieldSound);
<<<<<<< HEAD
>>>>>>> 39852ffa05725920d14df9f071758ecd1c3d3a10
=======
>>>>>>> 39852ffa05725920d14df9f071758ecd1c3d3a10
    }
}