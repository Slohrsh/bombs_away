﻿using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using System.Drawing;
using Zenseless.HLGL;
using Zenseless.OpenGL;
using TiledSharp;
using SysDraw = System.Drawing.Imaging;
using System.IO;
using bombs_away.resources.game.map;

namespace bombs_away
{
    class TextureLoader
    {
        public IList<ITexture> LoadContent()
        {
            IList<ITexture> textureList = new List<ITexture>();
            textureList.Add(getTextureFromResource(Resources.Tileset));
            textureList.Add(getTextureFromResource(Resources.TilesetCharacters));
            
            return textureList;
        }

        private ITexture getTextureFromResource(Bitmap bitmap)
        {
            var texture = new Texture2dGL()
            {
                Filter = TextureFilterMode.Mipmap
            };
            texture.Activate();
            //todo: 16bit channels
            using (Bitmap bmp = new Bitmap(bitmap))
            {
                bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
                var bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), SysDraw.ImageLockMode.ReadOnly, bmp.PixelFormat);
                var internalFormat = SelectInternalPixelFormat(bmp.PixelFormat);
                var inputPixelFormat = SelectPixelFormat(bmp.PixelFormat);
                texture.LoadPixels(bmpData.Scan0, bmpData.Width, bmpData.Height, internalFormat, inputPixelFormat, PixelType.UnsignedByte);
                bmp.UnlockBits(bmpData);
            }
            texture.Deactivate();

            return texture;
        }

        public static PixelInternalFormat SelectInternalPixelFormat(SysDraw.PixelFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                case SysDraw.PixelFormat.Format8bppIndexed: return PixelInternalFormat.Luminance;
                case SysDraw.PixelFormat.Format24bppRgb: return PixelInternalFormat.Rgb;
                case SysDraw.PixelFormat.Format32bppArgb: return PixelInternalFormat.Rgba;
                default: throw new FileLoadException("Wrong pixel format " + pixelFormat.ToString());
            }
        }

        public static PixelFormat SelectPixelFormat(SysDraw.PixelFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                case SysDraw.PixelFormat.Format8bppIndexed: return PixelFormat.Red;
                case SysDraw.PixelFormat.Format24bppRgb: return PixelFormat.Bgr;
                case SysDraw.PixelFormat.Format32bppArgb: return PixelFormat.Bgra;
                default: throw new FileLoadException("Wrong pixel format " + pixelFormat.ToString());
            }
        }
    }
}
