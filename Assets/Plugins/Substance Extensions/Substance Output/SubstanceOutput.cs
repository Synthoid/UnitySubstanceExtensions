using Substance.Game;

namespace SubstanceExtensions
{
    /// <summary>
    /// Convenience class used to access the outputs of a <see cref="SubstanceGraph"/>.
    /// </summary>
    [System.Serializable]
    public struct SubstanceOutput
    {
        /// <summary>
        /// The <see cref="SubstanceGraph"/> being examined for an output string.
        /// </summary>
        public SubstanceGraph graph;
        /// <summary>
        /// The output being targeted.
        /// </summary>
        public string outputName;

        public SubstanceGraph.TexturePackingItem GetOutputTexturePackingItem()
        {
            return graph.GetTexturePackingItem(outputName);
        }
    }
}