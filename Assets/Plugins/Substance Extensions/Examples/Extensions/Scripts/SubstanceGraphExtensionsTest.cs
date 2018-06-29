using UnityEngine;
using Substance.Game;

namespace Substance.Examples
{
	public class SubstanceGraphExtensionsTest : MonoBehaviour
	{
        [SerializeField]
        [Tooltip("The SubstanceGraph to randomize the random seed value for.")]
        private SubstanceGraph graph;
        [SerializeField]
        [Tooltip("Key to press to randomize the random seed value of a graph and rebuild it.")]
        private KeyCode randomizeKey = KeyCode.Space;

        private void Update()
        {
            if(Input.GetKeyDown(randomizeKey))
            {
                graph.RandomizeSeed();
                graph.QueueForRender();
            }
        }
    }
}