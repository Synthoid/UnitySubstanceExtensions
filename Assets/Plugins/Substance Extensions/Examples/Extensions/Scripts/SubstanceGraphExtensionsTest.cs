using UnityEngine;
using Substance.Game;

namespace SubstanceExtensions.Examples
{
	public class SubstanceGraphExtensionsTest : MonoBehaviour
	{
        [SerializeField, Tooltip("The SubstanceGraph to randomize the random seed value for.")]
        private SubstanceGraph graph;
        [SerializeField, Tooltip("Key to press to render substances.")]
        private KeyCode renderKey = KeyCode.R;
        [SerializeField, Tooltip("Key to press to randomize the random seed value of a graph and rebuild it.")]
        private KeyCode randomizeKey = KeyCode.Space;
        [SerializeField, Tooltip("Key to press to set the output size of a graph and rebuild it.")]
        private KeyCode outputSizeKey = KeyCode.O;
        [SerializeField, Tooltip("The size to set the substance to.")]
        private SubstanceOutputSize size = SubstanceOutputSize._1024;

        private void Update()
        {
            if(Input.GetKeyDown(randomizeKey))
            {
                graph.RandomizeSeed();
                graph.QueueForRender();
            }
            if(Input.GetKeyDown(outputSizeKey))
            {
                graph.SetOutputSize(size);
                graph.QueueForRender();
            }
            if(Input.GetKeyDown(renderKey))
            {
                Substance.Game.Substance.RenderSubstancesAsync();
            }
        }
    }
}