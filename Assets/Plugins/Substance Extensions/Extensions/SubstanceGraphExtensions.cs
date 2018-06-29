using UnityEngine;

namespace Substance.Game
{
    /// <summary>
    /// Class containing extension methods for the <see cref="SubstanceGraph"/> class.
    /// </summary>
	public static class SubstanceGraphExtensions
	{
        /// <summary>
        /// Name for the random seed parameter on <see cref="SubstanceGraph"/> assets.
        /// </summary>
        public const string RandomSeedParameter = "$randomseed";

        /// <summary>
        /// Randomizes and returns the random seed value for a <see cref="SubstanceGraph"/> asset.
        /// </summary>
        /// <param name="graph">The graph to randomize the seed value for.</param>
		public static int RandomizeSeed(this SubstanceGraph graph)
        {
            int newSeed = Random.Range(int.MinValue, int.MaxValue);

            graph.SetInputInt(RandomSeedParameter, newSeed);

            return newSeed;
        }

        /// <summary>
        /// Set the random seed value for a <see cref="SubstanceGraph"/> asset. Returns the new seed value.
        /// </summary>
        /// <param name="graph">The <see cref="SubstanceGraph"/> to set the random seed value for.</param>
        /// <param name="seed">Value to set the random seed to.</param>
        public static int SetRandomSeed(this SubstanceGraph graph, int seed)
        {
            graph.SetInputInt(RandomSeedParameter, seed);

            return seed;
        }
	}
}