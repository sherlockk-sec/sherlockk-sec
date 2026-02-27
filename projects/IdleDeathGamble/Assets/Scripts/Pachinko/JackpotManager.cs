using System.Collections;
using UnityEngine;
using IdleDeathGamble.Combat;

namespace IdleDeathGamble.Pachinko
{
    public class JackpotManager : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip admiringYouMusic;
        public PlayerCombat player;
        
        public PostJackpotModifiers postJackpotModifiers;

        private void Awake()
        {
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            audioSource.clip = admiringYouMusic;
            audioSource.loop = false; // Must strictly be 4 min 11 sec length
        }

        public void StartJackpot(SpinResult result)
        {
            StartCoroutine(JackpotRoutine(result));
        }

        private IEnumerator JackpotRoutine(SpinResult result)
        {
            Debug.Log("JACKPOT! Playing 'Admiring You'. Invulnerability and Infinite Resources Enabled.");
            
            player.isInvincible = true;
            player.infiniteAbilities = true;
            
            audioSource.Play();

            // Strict synchronization to audio
            while (audioSource.isPlaying)
            {
                yield return null;
            }

            Debug.Log("Jackpot Audio Finished. Buffs removed. Restarting Domain.");
            player.isInvincible = false;
            player.infiniteAbilities = false;

            // Trigger Post-Jackpot modifiers based on character odds
            postJackpotModifiers.ApplyModifiers(result.Slot1);
            
            // Transition back to normal state using the PachinkoManager reference from player
            player.pachinkoManager.stateMachine.ChangeState(player.pachinkoManager.normalState);
        }
    }
}
