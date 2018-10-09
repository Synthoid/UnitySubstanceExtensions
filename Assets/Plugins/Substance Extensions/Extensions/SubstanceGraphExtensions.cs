using UnityEngine;
using Substance.Game;

namespace SubstanceExtensions
{
    /// <summary>
    /// Class containing extension methods for the <see cref="SubstanceGraph"/> class.
    /// </summary>
	public static class SubstanceGraphExtensions
    {
        /// <summary>
        /// Name for the output size parameter on <see cref="SubstanceGraph"/> assets.
        /// </summary>
        public const string OutputSizeParameter = "$outputsize";
        /// <summary>
        /// Name for the random seed parameter on <see cref="SubstanceGraph"/> assets.
        /// </summary>
        public const string RandomSeedParameter = "$randomseed";

        /// <summary>
        /// Returns the output size of a graph, in proper pixel width and height. ie (1024, 1024)
        /// </summary>
        /// <param name="graph">The graph to get the output size of.</param>
        public static Vector2Int GetOutputSize(this SubstanceGraph graph)
        {
            int[] ints = graph.GetInputVector2Int(OutputSizeParameter);
            int x = 1024;
            int y = 1024;

            switch(ints[0])
            {
                case 5:
                    x = 32;
                    break;
                case 6:
                    x = 64;
                    break;
                case 7:
                    x = 128;
                    break;
                case 8:
                    x = 256;
                    break;
                case 9:
                    x = 512;
                    break;
                case 10:
                    x = 1024;
                    break;
                case 11:
                    x = 2046;
                    break;
                case 12:
                    x = 4096;
                    break;
            }

            switch (ints[1])
            {
                case 5:
                    y = 32;
                    break;
                case 6:
                    y = 64;
                    break;
                case 7:
                    y = 128;
                    break;
                case 8:
                    y = 256;
                    break;
                case 9:
                    y = 512;
                    break;
                case 10:
                    y = 1024;
                    break;
                case 11:
                    y = 2046;
                    break;
                case 12:
                    y = 4096;
                    break;
            }

            return new Vector2Int(x, y);
        }

        /// <summary>
        /// Returns the output size of a graph, in internal substance int values. ie (10, 10)
        /// </summary>
        /// <param name="graph">The graph to get the output size of.</param>
        public static Vector2Int GetOutputSizeInt(this SubstanceGraph graph)
        {
            int[] ints = graph.GetInputVector2Int(OutputSizeParameter);

            return new Vector2Int(ints[0], ints[1]);
        }

        /// <summary>
        /// Sets and returns the output size of a <see cref="SubstanceGraph"/>.
        /// </summary>
        /// <param name="graph">The graph to set the output size for.</param>
        /// <param name="size">The size to set the output size value to. (10 = 1024 x 1024)</param>
        public static Vector2Int SetOutputSize(this SubstanceGraph graph, SubstanceOutputSize size)
        {
            return graph.SetOutputSize((int)size);
        }

        /// <summary>
        /// Sets and returns the output size of a <see cref="SubstanceGraph"/>.
        /// </summary>
        /// <param name="graph">The graph to set the output size for.</param>
        /// <param name="size">The size to set the output size value to. (10 = 1024 x 1024)</param>
        public static Vector2Int SetOutputSize(this SubstanceGraph graph, int size)
        {
            return graph.SetOutputSize(size, size);
        }

        /// <summary>
        /// Sets and returns the output size of a <see cref="SubstanceGraph"/>.
        /// </summary>
        /// <param name="graph">The graph to set the output size for.</param>
        /// <param name="x">Width of the output size. (10 = 1024)</param>
        /// <param name="y">Height of the output size. (10 = 1024)</param>
        public static Vector2Int SetOutputSize(this SubstanceGraph graph, SubstanceOutputSize x, SubstanceOutputSize y)
        {
            return graph.SetOutputSize((int)x, (int)y);
        }

        /// <summary>
        /// Sets and returns the output size of a <see cref="SubstanceGraph"/>.
        /// </summary>
        /// <param name="graph">The graph to set the output size for.</param>
        /// <param name="x">Width of the output size. (10 = 1024)</param>
        /// <param name="y">Height of the output size. (10 = 1024)</param>
        public static Vector2Int SetOutputSize(this SubstanceGraph graph, int x, int y)
        {
            graph.SetInputVector2Int(OutputSizeParameter, x, y);

            return new Vector2Int(x, y);
        }

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