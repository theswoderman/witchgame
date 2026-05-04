using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharacterDatabase", menuName = "Scriptable Objects/CharacterDatabase")]
public class CharacterDatabase : ScriptableObject
{
    public List<CharacterData> allCharacters;

    public Dictionary<string, CharacterData> characterLookup;

    public void Initialize()
    {
        characterLookup = new Dictionary<string, CharacterData>();
        foreach (var character in allCharacters)
        {
            if (!characterLookup.ContainsKey(character.characterID))
            {
                characterLookup.Add(character.characterID, character);
            }
        }
    }

    public CharacterData GetCharacterbById(string id)
    {
        if (characterLookup == null) Initialize();
        return characterLookup.TryGetValue(id, out CharacterData character) ? character : null;
    }
}
