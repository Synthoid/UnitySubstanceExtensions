using UnityEngine;

namespace Substance.Game
{
    public static class TextureExtensions
    {
        /// <summary>
        /// Returns true if the texture is using a compressed format.
        /// </summary>
        public static bool IsCompressed(this Texture2D texture)
        {
            switch (texture.format)
            {
                //Uncompressed texture formats
                case TextureFormat.Alpha8:
                case TextureFormat.ARGB32:
                case TextureFormat.ARGB4444:
                case TextureFormat.BGRA32:
                case TextureFormat.R16:
                case TextureFormat.R8:
                case TextureFormat.RFloat:
                case TextureFormat.RG16:
                case TextureFormat.RGB24:
                case TextureFormat.RGB565:
                case TextureFormat.RGB9e5Float:
                case TextureFormat.RGBA32:
                case TextureFormat.RGBA4444:
                case TextureFormat.RGBAFloat:
                case TextureFormat.RGBAHalf:
                case TextureFormat.RGFloat:
                case TextureFormat.RGHalf:
                case TextureFormat.RHalf:
                case TextureFormat.YUY2:
                    return false;
            }

            return true;
        }
    }
}