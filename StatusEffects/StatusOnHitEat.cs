using System.Collections;

namespace Blahaj
{
    internal class StatusOnHitEat : StatusEffectApplyX
    {
        public override void Init()
        {
            this.OnHit += Check;
        }

        private IEnumerator Check(Hit hit)
        {
            yield return StatusEffectSystem.Apply(hit.target, target, effectToApply, 1);
            hit.countsAsHit = false;
        }

        public override bool RunHitEvent(Hit hit)
        {
            if (hit.attacker != this.target)
                return false;

            bool result = base.RunPreAttackEvent(hit);
            bool shouldKill = hit.target.hp.current <= hit.damage;
            return result && shouldKill;
        }
    }
}