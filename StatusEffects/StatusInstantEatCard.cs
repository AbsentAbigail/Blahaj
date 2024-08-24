using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Blahaj
{
    public class StatusInstantEatCard : StatusEffectInstantApplyEffect
    {
        [SerializeField]
        public bool gainHealth = true;

        [SerializeField]
        public bool gainAttack = true;

        [SerializeField]
        public bool gainEffects = true;

        [SerializeField]
        public TraitData[] illegalTraits;

        [SerializeField]
        public StatusEffectData[] illegalEffects;

        public override IEnumerator Process()
        {
            if ((bool)applier && applier.alive && (bool)target && (gainHealth || gainAttack || gainEffects))
            {
                Trigger trigger = new Trigger(applier, applier, "eat", new Entity[1] { target })
                {
                    countsAsTrigger = false
                };
                Hit hit = new Hit(applier, target, 0)
                {
                    canBeNullified = false,
                    canRetaliate = false,
                    damageType = "eat",
                    trigger = trigger,
                    doAnimation = false
                };
                trigger.hits = new Hit[1] { hit };
                yield return trigger.Process();
                yield return Eat();
            }

            SfxSystem.OneShot("event:/sfx/card/destroy");
            yield return base.Process();
            yield return new WaitForSeconds(0.2f);
        }

        private IEnumerator Eat()
        {
            if (gainHealth)
            {
                GainHealth();
            }

            if (gainAttack)
            {
                GainAttack();
            }

            if (gainEffects)
            {
                yield return GainEffects();
                applier.PromptUpdate();
            }
        }

        private void GainHealth()
        {
            applier.hp.current += target.hp.current;
            applier.hp.max += target.hp.max;
        }

        private void GainAttack()
        {
            applier.damage.current += target.damage.current;
            applier.damage.max += target.damage.max;
        }

        private IEnumerator GainEffects()
        {
            applier.attackEffects = CardData.StatusEffectStacks.Stack(applier.attackEffects, target.attackEffects).ToList();
            List<StatusEffectData> list = target.statusEffects.Where((StatusEffectData e) => e != this && !illegalEffects.Select((StatusEffectData e2) => e2.name).Contains(e.name)).ToList();
            foreach (Entity.TraitStacks trait in target.traits)
            {
                foreach (StatusEffectData passiveEffect in trait.passiveEffects)
                {
                    list.Remove(passiveEffect);
                }

                int num = trait.count - trait.tempCount;
                if (num > 0 && !illegalTraits.Select((TraitData t) => t.name).Contains(trait.data.name))
                {
                    applier.GainTrait(trait.data, num);
                }
            }

            foreach (StatusEffectData item in list)
            {
                yield return StatusEffectSystem.Apply(applier, target, item, item.count);
            }

            yield return applier.UpdateTraits();
            applier.display.promptUpdateDescription = true;
        }
    }
}