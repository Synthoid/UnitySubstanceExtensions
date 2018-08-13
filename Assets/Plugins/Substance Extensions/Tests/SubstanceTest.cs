using UnityEngine;
using Substance.Game;

namespace SleepyOwl.Common
{
	public class SubstanceTest : MonoBehaviour
	{
        [SerializeField]
        private Material mat;

        /// <summary>
        /// Find the <see cref="SubstanceGraph"/> associated with a given material.
        /// </summary>
        private void Start()
        {
            SubstanceGraph graph = SubstanceGraph.Find(mat);

            if(graph != null)
            {
                
            }
        }
    }
}