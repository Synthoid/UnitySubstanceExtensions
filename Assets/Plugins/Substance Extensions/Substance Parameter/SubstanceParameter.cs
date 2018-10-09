using Substance.Game;

namespace SubstanceExtensions
{
    /// <summary>
    /// Struct used to conveniently access substance parameters via the inspector.
    /// </summary>
    [System.Serializable]
    public struct SubstanceParameter
    {
        /// <summary>
        /// The <see cref="SubstanceGraph"/> being examined for a parameter string.
        /// </summary>
        public SubstanceGraph graph;
        /// <summary>
        /// The parameter being targeted.
        /// </summary>
        public string parameter;
        /// <summary>
        /// The value type of the parameter.
        /// </summary>
        public SubstanceInputType type;
    }
}