using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;
using System.Drawing;

namespace Edit2DEngine.Tools
{
    public static class TextureManager
    {
        public static Dictionary<String, Texture2D> ListTexture2D = new Dictionary<string, Texture2D>();
        public static Dictionary<String, Texture2D> ListParticleTexture2D = new Dictionary<string, Texture2D>();
        public static bool IsSimpleMode = true;

        public static void InitTextureManager(GraphicsDevice graphicsDevice)
        {
            InitTextureManager(graphicsDevice, String.Empty, "*.PNG");
        }

        public static void InitTextureManager(GraphicsDevice graphicsDevice, string dataPath, string patternFile)
        {
            LoadTextures(graphicsDevice, dataPath + @"Data\Pics\", patternFile, ref ListTexture2D);

            LoadTextures(graphicsDevice, dataPath + @"Data\Pics\Particles\", patternFile, ref ListParticleTexture2D);
        }

        private static void LoadTextures(GraphicsDevice graphicsDevice, string path, string patternFile, ref Dictionary<String, Texture2D> listTexture)
        {
            listTexture = new Dictionary<String, Texture2D>();
            string[] files = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, path), patternFile);

            foreach (String file in files)
            {
                if ((Path.GetFileNameWithoutExtension(file) == "BigRec" ||
                        Path.GetFileNameWithoutExtension(file) == "BigRec2" ||
                        Path.GetFileNameWithoutExtension(file) == "Pin" ||
                        Path.GetFileNameWithoutExtension(file) == "Pointer" ||
                        Path.GetFileNameWithoutExtension(file) == "Center" ||
                        Path.GetFileNameWithoutExtension(file) == "Empty"
                    ) || !IsSimpleMode)
                {
                    Texture2D texture = Texture2D.FromFile(graphicsDevice, file);
                    listTexture.Add(Path.GetFileNameWithoutExtension(file), texture);
                }
            }
        }

        public static Texture2D LoadTexture2D(string name)
        {
            try
            {
                if (String.IsNullOrEmpty(name))
                    return null;

                if (ListTexture2D.ContainsKey(name))
                    return ListTexture2D[name];
                else
                    return null;
            }
            catch
            {
            }

            return null;
        }

        public static Texture2D LoadParticleTexture2D(string name)
        {
            try
            {
                if (String.IsNullOrEmpty(name))
                    return null;

                if (ListParticleTexture2D.ContainsKey(name))
                    return ListParticleTexture2D[name];
                else return null;
            }
            catch
            {
            }

            return null;
        }

        public static void AddTexture2D(string name, Texture2D texture)
        {
            try
            {
                if (String.IsNullOrEmpty(name))
                    return;

                if (!ListTexture2D.ContainsKey(name))
                    ListTexture2D.Add(name, texture);
            }
            catch
            {
            }
        }

        public static Bitmap GetBitmapFromTexture2D(Texture2D texture)
        {
            byte[] textureData = new byte[4 * texture.Width * texture.Height];
            texture.GetData<byte>(textureData);

            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(
                           texture.Width, texture.Height,
                           System.Drawing.Imaging.PixelFormat.Format32bppArgb
                         );

            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(
                           new System.Drawing.Rectangle(0, 0, texture.Width, texture.Height),
                           System.Drawing.Imaging.ImageLockMode.WriteOnly,
                           System.Drawing.Imaging.PixelFormat.Format32bppArgb
                         );

            IntPtr safePtr = bmpData.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(textureData, 0, safePtr, textureData.Length);
            bmp.UnlockBits(bmpData);

            return bmp;
        }
    }
}