using System.Collections;

namespace Blahaj
{
    internal class StatusAbducted : StatusEffectApplyX
    {
        public override void Init()
        {
            this.OnStack += Apply;
            this.OnTurnEnd += EndTurn;
            this.OnHit += PreventDamage;
        }

        public override bool RunHitEvent(Hit hit)
        {
            return hit.target == target;
        }

        private IEnumerator PreventDamage(Hit hit)
        {
            hit.damage = 0;
            yield break;
        }

        public override bool RunTurnEndEvent(Entity entity)
        {
            return entity == target;
        }

        public IEnumerator Apply(int stacks)
        {
            ChangeAlpha(0.5f);
            target.cannotBeHitCount++;
            yield return Run(GetTargets(), amount: 1);
        }

        public IEnumerator EndTurn(Entity entity)
        {
            ChangeAlpha(1f);
            target.cannotBeHitCount--;
            yield return Remove();

            target.display.promptUpdateDescription = true;
            target.PromptUpdate();
            yield break;
        }

        private void ChangeAlpha(float alpha)
        {
            (target.display as Card).canvasGroup.alpha = alpha;
        }
    }
}