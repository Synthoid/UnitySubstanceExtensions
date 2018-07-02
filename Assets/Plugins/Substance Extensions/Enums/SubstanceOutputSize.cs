namespace Substance.Game
{
    /// <summary>
    /// Wrapper enum for the int values expected for setting substance output size via code.
    /// Code: substanceGraph.SetInputVector2Int("$outputsize", x, y);
    /// </summary>
	public enum SubstanceOutputSize : byte
	{
        /// <summary>
        /// 32 x 32
        /// </summary>
        _32     = 5,
        /// <summary>
        /// 64 x 64
        /// </summary>
        _64     = 6,
        /// <summary>
        /// 128 x 128
        /// </summary>
        _128    = 7,
        /// <summary>
        /// 256 x 256
        /// </summary>
        _256    = 8,
        /// <summary>
        /// 512 x 512
        /// </summary>
        _512 = 9,
        /// <summary>
        /// 1024 x 1024
        /// </summary>
		_1024 = 10,
        /// <summary>
        /// 2048 x 2048
        /// </summary>
        _2048 = 11,
        /// <summary>
        /// 4096 x 4096
        /// </summary>
        _4096 = 12
	}
}