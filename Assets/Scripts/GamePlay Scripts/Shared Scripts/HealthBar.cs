using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Character playerCharacter;

    void Start()
    {
        playerCharacter = PlayerController.Instance;
    }
    public float percent()
    {
        if (1 - playerCharacter.characterHealth.GetPercentage() <= 0)
        {
            return 0;
        }
        else
        {
            return 1 - playerCharacter.characterHealth.GetPercentage();
        }
    }

    void Update()
    {
        if (playerCharacter != null)
        {
            float bottomY = -1.1f;
            float topY = 0.1f;
            float newY = Mathf.Lerp(bottomY, topY, percent());
            transform.localPosition = new Vector3(0, newY, -2);
        }
    }
}
