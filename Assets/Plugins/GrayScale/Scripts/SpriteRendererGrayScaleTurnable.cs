using UnityEngine;

namespace GrayScale
{
    [RequireComponent(typeof(SpriteRenderer)), ExecuteInEditMode]
    public class SpriteRendererGrayScaleTurnable : MonoBehaviour, IGrayScaleTurnable
    {
        #region RENDERER

        private MaterialPropertyBlock spriteMpb;
        private MaterialPropertyBlock SpriteMpb
        {
            get
            {
                if (spriteMpb == null)
                {
                    spriteMpb = new MaterialPropertyBlock();
                }

                return spriteMpb;
            }

            set
            {
                spriteMpb = value;
            }
        }

        private SpriteRenderer sRenderer;
        private SpriteRenderer SRenderer
        {
            get
            {
                if (sRenderer == null)
                {
                    sRenderer = GetComponent<SpriteRenderer>();
                }

                return sRenderer;
            }
        }

        #endregion

        #region SWITCHEABLE

        private float ConvertIsOn(bool isOn)
        {
            return isOn == true ? 1f : 0f;
        }

        private void SwitchInternal(bool isOn)
        {
            this.isOn = isOn;
            SRenderer.GetPropertyBlock(SpriteMpb);
            SpriteMpb.SetFloat("_IsColorized", ConvertIsOn(isOn));
            SRenderer.SetPropertyBlock(SpriteMpb);
        }

        public void Switch(bool isOn)
        {
            SwitchInternal(isOn);
        }
        
        public void Switch()
        {
            SwitchInternal(!IsOn);
        }

        private bool isOn = false;
        public bool IsOn { get { return isOn; } }

        #endregion

        #region TURNABLE

        public void TurnOn()
        {
            SwitchInternal(true);
        }
        public void TurnOff()
        {
            SwitchInternal(false);
        }

        #endregion
    }
}
