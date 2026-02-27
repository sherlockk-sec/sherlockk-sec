using System.Collections.Generic;
using UnityEngine;

namespace IdleDeathGamble.Data
{
    public class CharacterData
    {
        public int Number { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsOdd => Number % 2 != 0;

        public CharacterData(int number, string name, string description)
        {
            Number = number;
            Name = name;
            Description = description;
        }
    }

    public static class CharacterDictionary
    {
        public static readonly Dictionary<int, CharacterData> Characters = new Dictionary<int, CharacterData>
        {
            { 1, new CharacterData(1, "Saitou Aya", "Childhood friend. Embezzling from regional banks.") },
            { 2, new CharacterData(2, "Amanogawa Sayuri", "Boss. Project manager by day.") },
            { 3, new CharacterData(3, "Asagiri Yume", "Main Heroine.") },
            { 4, new CharacterData(4, "Kato Sora", "University classmate. Musician.") },
            { 5, new CharacterData(5, "Ryo Shimizu", "Workmate. Highly educated.") },
            { 6, new CharacterData(6, "Yamaguchi Sayaka", "Little sister. Plays DS in class.") },
            { 7, new CharacterData(7, "Yamaguchi Yuuki", "Main character.") }
        };

        public static CharacterData GetCharacter(int number)
        {
            if (Characters.TryGetValue(number, out CharacterData data))
                return data;
            return null;
        }
    }
}
