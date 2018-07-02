using UnityEngine;
using Substance.Game;

namespace Substance.Examples
{
	public class SubstanceOutputExample : MonoBehaviour
	{
        [SerializeField]
        private SubstanceOutput output = new SubstanceOutput();

        private void Start()
        {
            Debug.Log(output.outputName);
        }
    }
}