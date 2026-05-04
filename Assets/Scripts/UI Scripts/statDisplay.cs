using TMPro;
using UnityEngine;

public class statDisplay : MonoBehaviour
{
    public UIInterface panel;
    public TextMeshProUGUI displayText;
    public TextMeshProUGUI displayValue;
    public Statistic statistic;
    public float refreshTimer = 0.01f;
    public float timer = 0f;

    private void Start()
    {
        panel = GetComponentInParent<UIInterface>();
        DisplayStatistic(statistic, panel.character);
    }

    public void DisplayStatistic(Statistic statistic, Character character)
    {
        displayText.text = panel.gameController.GetDefinition(statistic).name;
        displayValue.text = panel.gameController.GetDefinition(statistic).GetDisplayValue(character.stats.GetStat(character.stats.statLookup,statistic));
    }

    private void Update()
    {
        if (refreshTimer > timer)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            DisplayStatistic(statistic, panel.character);
        }
    }
}
