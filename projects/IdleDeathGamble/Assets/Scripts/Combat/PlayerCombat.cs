using System.Collections;
using UnityEngine;
using IdleDeathGamble.Pachinko;
using IdleDeathGamble.FSM;

namespace IdleDeathGamble.Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        public int currentHealth = 100;
        private int previousHealth = 100;
        
        [Header("Pseudo-Spins")]
        public int pseudoSpinCount = 0;
        public bool pseudoSpinsUnlocked = false; // Unlocked only during Kakuhen

        [Header("Buffs")]
        public bool isInvincible = false;
        public bool infiniteAbilities = false;

        [Header("Dependencies")]
        public PRNGManager prngManager;
        public PachinkoManager pachinkoManager;

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                PerformAttack();
            }
        }

        public void PerformAttack()
        {
            if (pachinkoManager.stateMachine.currentState == pachinkoManager.normalState ||
                pachinkoManager.stateMachine.currentState == pachinkoManager.previewState)
            {
                // Attack triggered a spin!
                pachinkoManager.TriggerAttackRoll();
                
                bool forceJackpot = (pseudoSpinCount >= 4);
                SpinResult result = prngManager.RollSpin(forceJackpot, forceJackpot);

                if (result.IndicatorBehavior == IndicatorColor.Rainbow)
                {
                    Debug.Log("Rainbow Indicator! Guaranteed Jackpot override!");
                    forceJackpot = true;
                    // Reset pseudo spins on hit
                    pseudoSpinCount = 0;
                }

                // If visual indicator fires here (ReserveBall or ShutterDoor)
                Debug.Log($"Attacking with {result.IndicatorBehavior} {result.AttackType}. Result: Jackpot={result.IsJackpot}, Reach={result.IsReach}");
                
                // Usually this is where the FSM takes over, depending on result.IsJackpot
            }
        }

        public void TakeDamage(int damage)
        {
            if (isInvincible) return;

            if (pseudoSpinsUnlocked && pseudoSpinCount < 4)
            {
                Debug.Log("Glass Shattering Effect! Pseudo-Spin avoids damage. Health rewound.");
                pseudoSpinCount++;
                currentHealth = previousHealth; // Rewind
                return;
            }

            previousHealth = currentHealth;
            currentHealth -= damage;
            pseudoSpinCount = 0; // Reset consecutive chain on actual damage
            
            if (currentHealth <= 0)
            {
                Debug.Log("Player Defeated!");
            }
        }
    }
}
