using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Unity.VisualScripting;

public class UpgradeManager : MonoBehaviour
{

    public static UpgradeManager instance;

    public List<Ability> allAbilities;
    public List<Ability> playerAbilities;
    public Button[] options;
    public Sprite[] allIcons;
    Ability[] randomSelection;

    public GameObject menu;

    void Awake()
    {
        if(instance == null) instance = this;
        menu.layer = LayerMask.NameToLayer("Non-Block UI");
    }

    // Start is called before the first frame update
    void Start()
    {
        allAbilities = new List<Ability>();
        playerAbilities = new List<Ability>();
        allAbilities.Add(new FleetFoot());
        allAbilities.Add(new StrongBody());
        allAbilities.Add(new AdvancedWeapons());
        allAbilities.Add(new Ghost());
        allAbilities.Add(new GrenadeLob());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateSelection()
    {
        randomSelection =  new Ability[3];
        if(playerAbilities.Count < allAbilities.Count && allAbilities.Count - playerAbilities.Count >= 3)
        {
            randomSelection[0] = selectRandom(randomSelection);
            randomSelection[1] = selectRandom(randomSelection);
            randomSelection[2] = selectRandom(randomSelection);
            updateButton(options[0], randomSelection[0]);
            updateButton(options[1], randomSelection[1]);
            updateButton(options[2], randomSelection[2]);
            return;
        }
        menu.layer = LayerMask.NameToLayer("Non-Block UI");
        menu.SetActive(false);
        options[0].gameObject.SetActive(true);
        options[1].gameObject.SetActive(true);
        options[2].gameObject.SetActive(true);
        PauseBehaviour.instance.unpauseGame();
    }

    private Ability selectRandom(Ability[] currentSelected)
    {
        Ability[] availableAbilities = allAbilities.Except(playerAbilities).ToArray();
        availableAbilities = availableAbilities.Except(currentSelected).ToArray();
        int num = Random.Range(0, availableAbilities.Count());
        Ability selected = availableAbilities[num];
        return selected;
    }

    void updateButton(Button button, Ability ability)
    {
        if (ability == null) button.gameObject.SetActive(false);
        TMP_Text name = button.transform.Find("AbilityName").GetComponent<TMP_Text>();
        TMP_Text description = button.transform.Find("AbilityDescription").GetComponent<TMP_Text>();
        Image icon = button.transform.Find("Icon").GetComponent<Image>();
        name.text = ability.name;
        description.text = ability.description;
        icon.overrideSprite = allIcons[(int)ability.type];
    }

    public void setAbility(Button button)
    {
        if(button.name == "Option1")
        {
            playerAbilities.Add(randomSelection[0]);
        }
        else if (button.name == "Option2")
        {
            playerAbilities.Add(randomSelection[1]);
        }
        else
        {
            playerAbilities.Add(randomSelection[2]);
        }
        menu.layer = LayerMask.NameToLayer("Non-Block UI");
        menu.SetActive(false);
        options[0].gameObject.SetActive(true);
        options[1].gameObject.SetActive(true);
        options[2].gameObject.SetActive(true);
        PauseBehaviour.instance.unpauseGame();
        PlayerUpgrades.ResumeGame();
    }

    public void setMenu()
    {
        PauseBehaviour.instance.pauseGame();
        menu.SetActive(true);
        menu.layer = LayerMask.NameToLayer("UI");
        generateSelection();
    }

}
