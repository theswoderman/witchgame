using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatChoiceButton : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Image iconImage;
    public Animator anim;
    public TextMeshProUGUI replacedModifier;
    public float statValue;
    private StatHandler handler;
    public Statistic selectedStat;
    public Mode1GameController gameController;
    public StatChoiceManager statChoiceManager;

    void SetupButton()
    {
        anim = GetComponent<Animator>();
        gameController = (Mode1GameController)Mode1GameController.Instance;
        statChoiceManager = gameController.statChoiceManager;
    }

    public void Setup(Statistic stat, CharacterStat details, StatHandler playerHandler)
    {
        SetupButton();
        handler = playerHandler;
        if (nameText == null)
        {
            Debug.LogError($"NameText is missing on {gameObject.name}!");
            return;
        }

        if (details == null)
        {
            Debug.LogError($"The StatDefinition passed to {gameObject.name} is null!");
            return;
        }
        selectedStat = stat;
        var rawStatValue = Random.Range(details.minGrantedValue, details.maxGrantedValue);
        var roundedStatValue = System.Math.Round(rawStatValue, details.roundingFactor);
        statValue = (float)roundedStatValue;
        nameText.text = details.GetDisplayText(statValue);
        iconImage.sprite = details.icon;
        if (gameController.player.stats.statsChosen.TryGetValue(stat, out StatInstance statInstance))
        {
            if (statInstance.currentValue == 0)
            {
                replacedModifier.text = "";
            }
            replacedModifier.text = gameController.GetDefinition(stat).GetDisplayValue(statInstance.currentValue);
        }

        AnimatorOverrideController overrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = overrideController;

        overrideController["Placeholder"] = details.animationClip;
        anim.Play("StatLoop", -1, 0f);
    }
    
    public void OnClick()
    {
        Debug.Log(selectedStat + " increased by " + statValue);
        handler.UpdateStat(selectedStat, statValue);
        gameController.levelUpRewardsRemaining--;
        
        if (gameController.levelUpRewardsRemaining > 0)
        {
            statChoiceManager.OpenChoicePanel(false);
        }
        else
        {
            gameController.UnPause();
            gameController.ToggleGameObject(gameController.choiceCanvas.gameObject,false);
        }
        gameController.PrepareSave();
    }
}