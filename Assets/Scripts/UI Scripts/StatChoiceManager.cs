using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatChoiceManager : MonoBehaviour
{
    protected Dictionary<Statistic, StatInstance> statDictionary = new Dictionary<Statistic, StatInstance>();
    public GameObject choiceCanvas;
    public Mode1GameController gameController;
    public List<Button> choiceButtons;
    [SerializeField]
    protected List<StatInstance> availableStats;

    void Awake()
    {
        CreateDict();
    }

    void Start()
    {
        gameController = (Mode1GameController)GameController.Instance;
        //this is supposed to be creating a blank set of chosen stats on start but i'm not sure i like this implementation
        gameController.player.stats.GenerateStatDictionary(gameController.player.stats.statsChosen, availableStats); //figure out the issue here
    }

    private void OnEnable()
    {
        GameController.OnLevelUp += ShowMenu;
    }

    private void OnDisable()
    {
        GameController.OnLevelUp -= ShowMenu;
    }

    private void ShowMenu()
    {
        OpenChoicePanel();
    }

    public void OpenChoicePanel(bool toggleGameObject = true)
    {
        if (toggleGameObject)
        {
            gameController.ToggleGameObject(choiceCanvas);
        }
        GenerateStatChoices();
    }
    private void CreateDict()
    {
        foreach (var stat in availableStats)
        {
            statDictionary.Add(stat.statistic, stat);
        }
    }

    public void GenerateStatChoices()
    {
        List<Statistic> generatedStats = StatChoices();
        for (int i = 0; i < choiceButtons.Count; i++)
        {
            choiceButtons[i].GetComponent<StatChoiceButton>().Setup(
                generatedStats[i],
                gameController.GetDefinition(generatedStats[i]),
                gameController.player.GetComponent<StatHandler>());
        }
    }

    public List<Statistic> StatChoices() //TODO: longterm would like this to generate a number of choices equal to a stat that the player has so that they could increase their choices, right now it's not necessary
    {
        //create a pool of all possible stats
        List<Statistic> pool = new List<Statistic>(statDictionary.Keys);
        List<Statistic> choices = new List<Statistic>();

        //loop 3 times
        for (int i = 0; i < 3; i++)
        {
            if (pool.Count == 0) break;

            //pick a random index from the current available options
            int randomIndex = Random.Range(0, pool.Count);

            //add the choice to our choices list
            choices.Add(pool[randomIndex]);
            pool.RemoveAt(randomIndex);
        }
        return choices;
    }
}