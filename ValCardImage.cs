using UnityEngine;
using UnityEngine.UI;

namespace Blahaj
{
    internal class ValCardImage : ScriptableCardImage
    {
        public Image Image => GetComponent<Image>();

        // gets called when the card is created (eg Leaders having one consistent avatar)
        public override void AssignEvent()
        {
            // we use the CardData's main sprite for a backup here
            // otherwise it won't have any sprite
            Image.sprite = this.entity.data.mainSprite;
        }

        public override void UpdateEvent()
        {
            float scale = ScaleFunction(entity.damage.current);

            (entity.display as Card).scriptableImage.transform.localScale = new Vector3(scale, scale, 1f);
        }

        private float ScaleFunction(int damage)
        {
            BlahajMod mod = BlahajMod.instance;
            float max = mod.valMaxSize;
            float min = mod.valMinSize;

            float m = (max - min) / (float)mod.valMaxSizeAt;
            float b = min;

            float scale = m * damage + b;
            return Mathf.Clamp(scale, min, max);
        }
    }
}