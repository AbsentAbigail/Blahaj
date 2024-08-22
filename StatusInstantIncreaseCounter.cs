using System;
using System.Collections;

namespace Blahaj
{
    internal class StatusInstantIncreaseCounter : StatusEffectInstant
    {
        public override IEnumerator Process()
        {
            LogHelper.Log("Target: " + target.name + " with counter " + target.counter.current + "/" + target.counter.max);
            target.counter.current = Math.Min(target.counter.current + count, target.counter.max);
            yield return base.Process();
        }
    }
}