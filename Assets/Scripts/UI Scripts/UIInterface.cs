using UnityEngine;

public class UIInterface : MonoBehaviour
{
    public PlayerController character;
    public GameController gameController;

    private void Start()
    {
        gameController = GameController.Instance;
        character = PlayerController.Instance;
    }

    public void SwapActivation()
    {
        switch (gameObject.activeSelf)
        {
            case true:
                gameObject.SetActive(false);
                break;
            default:
                gameObject.SetActive(true);
                break;
        }
    }
}
