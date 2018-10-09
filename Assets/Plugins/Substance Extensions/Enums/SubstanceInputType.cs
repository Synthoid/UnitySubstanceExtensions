namespace SubstanceExtensions
{
    /// <summary>
    /// Wrapper enum for the Global.Input.substanceInputType int value.
    /// </summary>
	public enum SubstanceInputType : byte
	{
		Float       = 0,
        Vector2     = 1,
        Vector3     = 2,
        Vector4     = 3,
        Int_Or_Bool = 4,
        Texture     = 5,
        String      = 6,
        Vector2Int  = 8,
        Vector3Int  = 9,
        Vector4Int  = 10
	}
}