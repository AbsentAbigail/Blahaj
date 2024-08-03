using System;
using System.Collections;

namespace Blahaj
{
    internal class StatusEffectCalm : StatusEffectApplyX
    {
        public StatusEffectData effectToApply2;
        private int currentReduction;

        public override void Init()
        {
            base.OnStack += Stack;
            base.OnHit += HitEvent;
        }

        public override bool RunHitEvent(Hit hit)
        {
            if (hit.target != target) return false;
            if (!hit.Offensive) return false;

            return hit.damage > 0;
        }

        private IEnumerator HitEvent(Hit hit)
        {
            int halved = Math.Max(count / 2, 1);
            int increase = (count / 3) - ((count - halved) / 3);
            yield return StatusEffectSystem.Apply(target, target, effectToApply2, increase);
            target.display.promptUpdateDescription = true;
            yield return RemoveStacks(halved, false);
        }

        private IEnumerator Stack(int stacks)
        {
            int oldCount = count - stacks;
            int canReduceBy = target.counter.max - 1;
            int decreaseCounterBy = (count / 3) - (oldCount / 3);

            decreaseCounterBy = Math.Min(decreaseCounterBy, canReduceBy);

            if (decreaseCounterBy > 0)
            {
                currentReduction += decreaseCounterBy;
                yield return Run(GetTargets(), amount: decreaseCounterBy);
            }

            target.display.promptUpdateDescription = true;
            target.PromptUpdate();
            yield break;
        }
    }
}