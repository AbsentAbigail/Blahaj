using System.Collections;

namespace Blahaj.Calm
{
    internal class StatusEffectCalm : StatusEffectApplyXWhenYAppliedToSelf
    {
        public StatusEffectCalm()
        { }

        protected override void Init()
        {
            base.PostApplyStatus += Check;
        }

        public override bool RunPostApplyStatusEvent(StatusEffectApply apply)
        {
            if (target.enabled && apply.target == target && (bool)apply.effectData && apply.count > 0)
            {
                return whenAppliedTypes.Contains(apply.effectData.type);
            }

            return false;
        }

        private IEnumerator Check(StatusEffectApply apply)
        {
            LogHelper.Log($"[{apply.effectData.type}] applied to {apply.target}");
            return Run(GetTargets(), amount: 1);
        }
    }
}