using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UpgradeManager : MonoBehaviour
{

    public List<Ability> allAbilities;
    public List<Ability> playerAbilities;
    public Button[] options;
    public Sprite[] allIcons;

    // Start is called before the first frame update
    void Start()
    {
        allAbilities = new List<Ability>();
        playerAbilities = new List<Ability>();
        allAbilities.Add(new FleetFoot());
        allAbilities.Add(new StrongBody());
        allAbilities.Add(new AdvancedWeapons());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateSelection()
    {
        Ability[] randomSelection =  new Ability[3];
        if(playerAbilities.Count < allAbilities.Count && allAbilities.Count - playerAbilities.Count >= 3)
        {
            randomSelection[0] = selectRandom(randomSelection);
            randomSelection[1] = selectRandom(randomSelection);
            randomSelection[2] = selectRandom(randomSelection);
            updateButton(options[0], randomSelection[0]);
            updateButton(options[1], randomSelection[1]);
            updateButton(options[2], randomSelection[2]);
        }
    }

    private Ability selectRandom(Ability[] currentSelected)
    {
        Ability[] availableAbilities = allAbilities.Except(playerAbilities).ToArray();
        availableAbilities = availableAbilities.Except(currentSelected).ToArray();
        int num = Random.Range(0, availableAbilities.Count() -1);
        Ability selected = availableAbilities[num];
        return selected;
    }

    void updateButton(Button button, Ability ability)
    {
        TMP_Text name = button.transform.Find("AbilityName").GetComponent<TMP_Text>();
        TMP_Text description = button.transform.Find("AbilityDescription").GetComponent<TMP_Text>();
        Image icon = button.transform.Find("Icon").GetComponent<Image>();
        name.text = ability.name;
        description.text = ability.description;
        icon.overrideSprite = allIcons[(int)ability.type];
    }
}
