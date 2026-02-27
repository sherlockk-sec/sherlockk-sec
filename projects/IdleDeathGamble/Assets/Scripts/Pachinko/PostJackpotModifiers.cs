using UnityEngine;
using IdleDeathGamble.Data;
using IdleDeathGamble.Combat;

namespace IdleDeathGamble.Pachinko
{
    public class PostJackpotModifiers : MonoBehaviour
    {
        public PRNGManager prng;
        public PlayerCombat player;
        public float defaultTimeScale = 1f;
        
        private int jitanSpinsRemaining = 0;

        public void ApplyModifiers(int winningNumber)
        {
            CharacterData winningChar = CharacterDictionary.GetCharacter(winningNumber);
            ClearStates();

            if (winningChar.IsOdd)
            {
                // Kakuhen (Probability Change Mode)
                Debug.Log($"<color=magenta>KAKUHEN ACTIVATED! (Won with Odd Number {winningNumber})</color>");
                prng.isKakuhenctive = true;
                player.pseudoSpinsUnlocked = true;
            }
            else
            {
                // Jitan (Time Reduction Mode)
                Debug.Log($"<color=blue>JITAN ACTIVATED! (Won with Even Number {winningNumber})</color>");
                jitanSpinsRemaining = (Random.value > 0.5f) ? 30 : 70;
                
                // Senpuku check (Hidden Probability)
                if (Random.value < 0.15f) // 15% hidden chance
                {
                    Debug.Log("<color=red>SENPUKU (Hidden Probability) triggered silently within Jitan!</color>");
                    prng.isSenpukuActive = true;
                    player.pseudoSpinsUnlocked = true; // Still give them the pseudo spins defensively
                }
            }
        }

        public void OnSpinComplete()
        {
            if (jitanSpinsRemaining > 0)
            {
                jitanSpinsRemaining--;
                Debug.Log($"Jitan active. Spins remaining: {jitanSpinsRemaining}");

                // 3x speed for spins within Jitan.
                Time.timeScale = 3f;

                if (jitanSpinsRemaining <= 0)
                {
                    Debug.Log("Jitan expired! Reverting to Normal Mode.");
                    Time.timeScale = defaultTimeScale;
                    prng.isSenpukuActive = false; // Expose and clear hidden probability
                    player.pseudoSpinsUnlocked = false;
                }
            }
            else
            {
                Time.timeScale = defaultTimeScale;
            }
        }

        private void ClearStates()
        {
            prng.isKakuhenctive = false;
            prng.isSenpukuActive = false;
            player.pseudoSpinsUnlocked = false;
            jitanSpinsRemaining = 0;
            Time.timeScale = defaultTimeScale;
        }
    }
}
