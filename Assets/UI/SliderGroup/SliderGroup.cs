namespace EcoTile
{
    using UnityEngine;
    using System.Collections.Generic;

    public class SliderGroup : MonoBehaviour
    {
        public delegate void CreatureSliderUpdate(CreatureType buttonType, CreatureType sliderType, int value);
        public static CreatureSliderUpdate CreatureSliderUpdateEvent;

        public float desiredTotal;

        private List<GroupedSlider> _sliders;

        private List<GroupedSlider> sliders
        {
            get
            {
                if (_sliders == null)
                {
                    _sliders = new List<GroupedSlider>();
                }
                return _sliders;
            }
        }

        public void RegisterSlider(GroupedSlider argSlider)
        {
            sliders.Add(argSlider);
            argSlider.slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(argSlider); });
            Normalize(argSlider);
        }

        private void OnSliderValueChanged(GroupedSlider argSlider)
        {
            if (!argSlider.changing)
            {
                //Normalize( argSlider );
                CreatureSliderUpdateEvent(argSlider.buttonType, argSlider.buttonType, (int)argSlider.slider.value);
            }
        }

        private void NormalizeByInt(GroupedSlider argFixed)
        {
            int total = 0;
            int nonFixedTotal = 0;

            foreach (GroupedSlider slider in sliders)
            {
                slider.changing = true;

                total += (int)slider.slider.value;
                if (slider != argFixed)
                {
                    nonFixedTotal += (int)slider.slider.value;
                }
            }

            //if(total != desiredTotal)
        }

        private void Normalize(GroupedSlider argFixed)
        {
            // get current total

            float total = 0f;
            float nonFixedTotal = 0f;

            foreach (GroupedSlider slider in sliders)
            {
                slider.changing = true;

                total += slider.slider.value;
                if (slider != argFixed)
                {
                    nonFixedTotal += slider.slider.value;
                }
            }

            if (total != desiredTotal)
            {

                float totalLeftForNonFixed = desiredTotal - argFixed.slider.value;

                foreach (GroupedSlider slider in sliders)
                {
                    if (slider != argFixed)
                    {
                        if (nonFixedTotal == 0)
                        {
                            slider.slider.value = totalLeftForNonFixed / (float)(sliders.Count - 1);
                        }
                        else
                        {
                            slider.slider.value = slider.slider.value * totalLeftForNonFixed / nonFixedTotal;
                        }
                    }
                }
            }

            foreach (GroupedSlider slider in sliders)
            {
                slider.changing = false;
            }
        }
    }
}