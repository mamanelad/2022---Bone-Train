using System;
using System.Collections.Generic;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LoadOption : MonoBehaviour
{
    [Serializable]
    public enum OptionIndex
    {
        GOODSOULS,
        BADSOULS,
        SOULSTONES,
        SWORD,
        SHIELD
    }
    
    [Serializable]
    public struct Option
    {
        public string title;
        
        public int goodSouls;
        public int badSouls;
        public int soulsStones;
        public bool sword;
        public bool shield;
        
        [SerializeField] public EventReference sound;
        
        [Header("Optional")]
        [TextArea] public string text;
        [Range(0, 100)] public int odds;
    }

    [SerializeField] private List<Sprite> icons;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private GameObject dealsTab;
    [SerializeField] private GameObject itemSlotPrefab;
    
    [Space(20)]
    [Header("Sounds")]
    [SerializeField] private EventReference hoverSound;
    [SerializeField] private EventReference clickSound;

    private const int GOODSOULS = 0;
    private const int BADSOULS = 1;
    private const int SOULSTONES = 2;
    private const int SWORD = 3;
    private const int SHIELD = 4;

    private List<GameObject> currentItems;
    private InteractionManager interactionManager;
    public Option option;
    private Tutorial _tutorial;

    private void Start()
    {
        interactionManager = FindObjectOfType<InteractionManager>();
    }

    public void Load(Option newOption)
    {
        ResetData();
        
        option = newOption;
        
        if (option.odds != 100)
            ChangeOptionByOdds();

        title.text = option.title != "" ? option.title : "Option";

        if (option.text != "")
        {
            textBox.text = option.text;
            return;
        }
        
        if (option.goodSouls != 0)
            LoadSingle(GOODSOULS, option.goodSouls);
        if (option.badSouls != 0)
            LoadSingle(BADSOULS, option.badSouls);
        if (option.soulsStones != 0)
            LoadSingle(SOULSTONES, option.soulsStones);
        if (option.sword)
            LoadSingle(SWORD, 1);
        if (option.shield)
            LoadSingle(SHIELD, 1);
    }

    private void ResetData()
    {
        foreach (var item in currentItems)
            Destroy(item);

        title.text = "Option";
        textBox.text = "";
        
        currentItems = new List<GameObject>();
    }

    private void LoadSingle(int iconIndex, int amount)
    {
        var newDeal = Instantiate(itemSlotPrefab, dealsTab.transform);
        var image = newDeal.GetComponentInChildren<Image>();
        var text = newDeal.GetComponentInChildren<TextMeshProUGUI>();

        image.sprite = icons[iconIndex];
        text.text = amount > 0 ? $"+ {amount}" : $"- {Math.Abs(amount)}";
        
        currentItems.Add(newDeal);
    }

    private void ChangeOptionByOdds()
    {
        if (Random.Range(0,100) < option.odds)
            return;

        option.goodSouls = 0;
        option.badSouls = 0;
        option.soulsStones = 0;
    }

    public void ChooseOption()
    {
        interactionManager.ChooseOption(option);
        
        if (GameManager.Shared.GetTutorialIsOn())
        {
            if (!_tutorial)
                _tutorial = FindObjectOfType<Tutorial>();
            _tutorial.NextButtonFromEventObject();
             
        }
    }

    public void PlayHoverSound()
    {
        RuntimeManager.PlayOneShot(hoverSound);
    }

    public void PlayClickSound()
    {
        RuntimeManager.PlayOneShot(clickSound);
    }
    
    public void SetTutorialObject(Tutorial tutorial)
    {
        _tutorial = tutorial;
    }
}
