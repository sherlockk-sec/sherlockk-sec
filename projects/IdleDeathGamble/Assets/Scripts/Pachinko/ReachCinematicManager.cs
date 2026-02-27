using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using IdleDeathGamble.Pachinko;

namespace IdleDeathGamble.Cinematics
{
    public class ReachCinematicManager : MonoBehaviour
    {
        [Header("Timelines")]
        public PlayableAsset oneStarHype; // IC Card
        public PlayableAsset twoStarHype_A; // Seat Scramble
        public PlayableAsset twoStarHype_B; // Full Bladder
        public PlayableAsset threeStarHype; // Last Friday Train
        
        private PlayableDirector director;

        private void Awake()
        {
            director = gameObject.AddComponent<PlayableDirector>();
        }

        public void PlayReachCinematic(SpinResult result, System.Action<bool> onComplete)
        {
            StartCoroutine(ReachRoutine(result, onComplete));
        }

        private IEnumerator ReachRoutine(SpinResult result, System.Action<bool> onComplete)
        {
            PlayableAsset selected = oneStarHype;
            float chanceUpProb = 0.05f;

            if (result.HypeLevel == 3)
            {
                selected = threeStarHype;
                chanceUpProb = 0.3f;
            }
            else if (result.HypeLevel == 2)
            {
                selected = (Random.value > 0.5f) ? twoStarHype_A : twoStarHype_B;
                chanceUpProb = 0.15f;
            }

            director.Play(selected);

            bool isChanceUpTriggered = false;

            // Wait while timeline plays
            while (director.state == PlayState.Playing)
            {
                // Inject random visual popup chance up mid-sequence
                if (!isChanceUpTriggered && Random.value < chanceUpProb * Time.deltaTime)
                {
                    Debug.Log("CHANCE UP TRIGGERED! Dynamic pop-up injected.");
                    isChanceUpTriggered = true;
                }
                yield return null;
            }

            bool finalSuccess = result.IsJackpot;

            // Chance up overrides fail state
            if (!finalSuccess && isChanceUpTriggered)
            {
                Debug.Log("Chance Up flipped the fail state! GUARANTEED JACKPOT!");
                finalSuccess = true;
            }

            onComplete?.Invoke(finalSuccess);
        }
    }
}
