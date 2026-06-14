using System;
using System.Collections;
using UnityEngine;

namespace FeedbacksEditor
{
    /// <summary>
    /// Waits for a random duration before calling the next instruction.
    /// </summary>
    [Serializable]
    public class EffectWaitRandom : GameEffect
    {
        public Vector2 RandomTimer;
        
        public override IEnumerator Execute(GameEvent gameEvent, GameObject target)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(RandomTimer.x, RandomTimer.y));
        }
        
        public override Color GetColor() => new Color(0.3f, .7f, 0.3f);
        
        public override string ToString()
        {
            return $"Waits between {RandomTimer.x} and {RandomTimer.y} seconds";
        }
    }
}