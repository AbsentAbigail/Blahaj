using System.Collections;
using System.Linq;

namespace Blahaj
{
    internal class StatusStress : StatusEffectBonusDamageEqualToX
    {
        public override void Init()
        {
            base.PreCardPlayed += Gain;
        }

        public new IEnumerator Gain(Entity entity, Entity[] targets)
        {
            int num = FindOnBoard();
            if (!toReset || num != currentAmount)
            {
                if (toReset)
                {
                    LoseCurrentAmount();
                }

                if (num > 0)
                {
                    yield return GainAmount(num);
                }
            }
        }

        public new int FindOnBoard()
        {
            int damagedAllies = target.GetAllies().Count(e => e.hp.safeMax.Value > e.hp.safeCurrent.Value);
            return GetAmount() * damagedAllies;
        }
    }
}