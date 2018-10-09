using UnityEngine;
using UnityEngine.UI;
using Substance.Game;

namespace SubstanceExtensions.Examples
{
    public class ParameterMenu : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The Substance graph being adjusted.")]
        private SubstanceGraph graph;
        [SerializeField]
        [Tooltip("Slider to control substance luminosity parameter.")]
        private Slider luminositySlider;
        [SerializeField]
        [Tooltip("Slider to control substance hue shift parameter.")]
        private Slider hueSlider;
        [SerializeField]
        [Tooltip("Slider to control substance saturation parameter.")]
        private Slider saturationSlider;
        [SerializeField]
        [Tooltip("Label showing the value for the luminosity parameter.")]
        private Text luminosityValueLabel;
        [SerializeField]
        [Tooltip("Label showing the value for the hue shift parameter.")]
        private Text hueValueLabel;
        [SerializeField]
        [Tooltip("Label showing the value for the saturation parameter.")]
        private Text saturationValueLabel;
        [SerializeField]
        private Button refreshButton;
        [SerializeField]
        private SubstanceParameter luminosityParam = new SubstanceParameter();
        [SerializeField]
        private SubstanceParameter hueParam = new SubstanceParameter();
        [SerializeField]
        private SubstanceParameter saturationParam = new SubstanceParameter();


        private void OnLuminosityChanged(float value)
        {
            luminosityValueLabel.text = value.ToString("0.0");
            graph.SetInputFloat(luminosityParam.parameter, value);
        }


        private void OnHueChanged(float value)
        {
            hueValueLabel.text = value.ToString("0.0");
            graph.SetInputFloat(hueParam.parameter, value);
        }


        private void OnSaturationChanged(float value)
        {
            saturationValueLabel.text = value.ToString("0.0");
            graph.SetInputFloat(saturationParam.parameter, value);
        }


        private void OnRefreshClicked()
        {
            graph.QueueForRender();

            Substance.Game.Substance.RenderSubstancesSync();
        }


        private void Start()
        {
            luminositySlider.value = graph.GetInputFloat(luminosityParam.parameter);
            hueSlider.value = graph.GetInputFloat(hueParam.parameter);
            saturationSlider.value = graph.GetInputFloat(saturationParam.parameter);

            luminositySlider.onValueChanged.AddListener(OnLuminosityChanged);
            hueSlider.onValueChanged.AddListener(OnHueChanged);
            saturationSlider.onValueChanged.AddListener(OnSaturationChanged);

            refreshButton.onClick.AddListener(OnRefreshClicked);

            Debug.Log(string.Format("{0} - {1}\n{2} - {3}\n{4} - {5}", luminosityParam.parameter, luminosityParam.type, hueParam.parameter, hueParam.type, saturationParam.parameter, saturationParam.type));
        }
    }
}