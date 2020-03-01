using UnityEngine;
using UnityEngine.UI;

namespace GrayScale
{
    [RequireComponent(typeof(Image)), ExecuteInEditMode]
    public class CanvasGrayScaleTurnable : MonoBehaviour, IGrayScaleTurnable
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

        private Image image;
        private Image ImageInstance
        {
            get
            {
                if (image == null)
                {
                    image = GetComponent<Image>();
                }

                return image;
            }
        }

        #endregion

        #region SWITCHEABLE        

        [SerializeField]
        private Material grayScaleMaterial;
        [SerializeField]
        private Material normalMaterial;

        private Material GetMaterial(bool isOn)
        {
            return isOn == true ? normalMaterial : grayScaleMaterial;
        }

        private void SwitchInternal(bool isOn)
        {
            IsOn = isOn;
            ImageInstance.material = GetMaterial(isOn);
        }        

        public void Switch(bool isOn)
        {
            SwitchInternal(isOn);
        }

        public void Switch()
        {
            SwitchInternal(!IsOn);
        }

        public bool IsOn { get; private set; } 

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
