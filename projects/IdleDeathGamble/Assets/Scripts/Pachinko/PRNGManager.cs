using UnityEngine;

namespace IdleDeathGamble.Pachinko
{
    public enum IndicatorColor
    {
        Green,   // Low Probability
        Red,     // Medium Probability
        Gold,    // High Probability
        Rainbow  // Guaranteed Jackpot
    }

    public enum IndicatorType
    {
        ReserveBall,  // Blunt projectile
        ShutterDoor   // Crushing trap
    }

    public class SpinResult
    {
        public bool IsJackpot;
        public bool IsReach;
        public int Slot1;
        public int Slot2;
        public int Slot3;
        public IndicatorColor IndicatorBehavior;
        public IndicatorType AttackType;
        public int HypeLevel; // 1, 2, or 3
    }

    public class PRNGManager : MonoBehaviour
    {
        [Header("Probabilities")]
        [SerializeField] private float baseProbability = 1f / 239f;
        
        // Dynamic modifiers
        public bool isKakuhenctive = false;
        public bool isSenpukuActive = false;
        public int currentBaseOddsDenom = 239;

        public SpinResult RollSpin(bool forceJackpot = false, bool forceRainbow = false)
        {
            SpinResult result = new SpinResult();
            
            // Determine visual indicator type
            result.AttackType = (Random.value > 0.5f) ? IndicatorType.ReserveBall : IndicatorType.ShutterDoor;
            
            // Check for guaranteed jackpots
            if (forceJackpot || forceRainbow)
            {
                result.IsJackpot = true;
                result.IndicatorBehavior = IndicatorColor.Rainbow;
                GenerateJackpotSlots(result);
                return result;
            }

            // Calculate actual hit based on current probability
            float roll = Random.Range(1, currentBaseOddsDenom + 1);
            if (roll == 1 || isKakuhenctive || isSenpukuActive)
            {
                result.IsJackpot = true;
                result.IndicatorBehavior = DetermineJackpotColor();
                GenerateJackpotSlots(result);
            }
            else
            {
                // Miss. Check if we trigger a Reach (2 slots match)
                float reachChance = 0.15f; // Random placeholder for reach frequency
                if (Random.value <= reachChance && !result.IsJackpot)
                {
                    result.IsReach = true;
                    GenerateReachSlots(result);
                    // Decide hype level based on failure
                    result.HypeLevel = Random.Range(1, 4); 
                    
                    if (result.HypeLevel == 3) result.IndicatorBehavior = IndicatorColor.Gold;
                    else if (result.HypeLevel == 2) result.IndicatorBehavior = IndicatorColor.Red;
                    else result.IndicatorBehavior = IndicatorColor.Green;
                }
                else
                {
                    GenerateMissSlots(result);
                    result.IndicatorBehavior = IndicatorColor.Green;
                }
            }

            return result;
        }

        private void GenerateJackpotSlots(SpinResult result)
        {
            // 75% forced odd recurrence if Kakuhen active
            if (isKakuhenctive || isSenpukuActive)
            {
                if (Random.value <= 0.75f)
                {
                    int oddNum = Random.Range(0, 4) * 2 + 1; // 1, 3, 5, 7
                    result.Slot1 = oddNum; result.Slot2 = oddNum; result.Slot3 = oddNum;
                    return;
                }
            }
            
            int jackpotNum = Random.Range(1, 8);
            result.Slot1 = jackpotNum; result.Slot2 = jackpotNum; result.Slot3 = jackpotNum;
        }

        private void GenerateReachSlots(SpinResult result)
        {
            int reachNum = Random.Range(1, 8);
            result.Slot1 = reachNum;
            result.Slot2 = reachNum;
            
            // Ensure slot 3 doesn't match
            do
            {
                result.Slot3 = Random.Range(1, 8);
            } while (result.Slot3 == reachNum);
        }

        private void GenerateMissSlots(SpinResult result)
        {
            result.Slot1 = Random.Range(1, 8);
            result.Slot2 = Random.Range(1, 8);
            if (result.Slot1 == result.Slot2)
            {
                result.Slot2 = (result.Slot1 % 7) + 1; // force no reach
            }
            result.Slot3 = Random.Range(1, 8);
        }

        private IndicatorColor DetermineJackpotColor()
        {
            float rand = Random.value;
            if (rand < 0.1f) return IndicatorColor.Rainbow;
            if (rand < 0.6f) return IndicatorColor.Gold;
            return IndicatorColor.Red;
        }
    }
}
