using UnityEngine;
using UnityEngine.SceneManagement;

public class portal : MonoBehaviour
{
    public string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponentInParent<PlayerController>();
        if (player != null && player.currentlyControlled)
        {
            SaveManager.Instance.currentSaveData.characterId = player.characterData.characterID;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
