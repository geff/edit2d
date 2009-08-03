using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;
using System.Drawing;

namespace Edit2D
{
    public static class TextureManager
    {
        public static Dictionary<String, Texture2D> ListTexture2D = new Dictionary<string, Texture2D>();
        public static Dictionary<String, Texture2D> ListParticleTexture2D = new Dictionary<string, Texture2D>();

        public static void InitTextureManager(GraphicsDevice graphicsDevice)
        {
            //ListTexture2D = new Dictionary<string, Texture2D>();
            //string[] files = Directory.GetFiles(Environment.CurrentDirectory + @"\Data\Pics\", "*.PNG");

            //foreach (String file in files)
            //{
            //    FileInfo fileInfo = new FileInfo(file);
            //    Texture2D texture = Texture2D.FromFile(graphicsDevice, file);
            //    ListTexture2D.Add(fileInfo.Name.Remove(fileInfo.Name.Length - fileInfo.Extension.Length), texture);
            //}

            LoadTextures(graphicsDevice, @"\Data\Pics\", ref ListTexture2D);
            LoadTextures(graphicsDevice, @"\Data\Pics\Particles\", ref ListParticleTexture2D);
        }

        private static void LoadTextures(GraphicsDevice graphicsDevice, string path, ref Dictionary<String, Texture2D> listTexture)
        {
            listTexture = new Dictionary<String, Texture2D>();
            string[] files = Directory.GetFiles(Environment.CurrentDirectory + path, "*.PNG");

            foreach (String file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                Texture2D texture = Texture2D.FromFile(graphicsDevice, file);
                listTexture.Add(fileInfo.Name.Remove(fileInfo.Name.Length - fileInfo.Extension.Length), texture);
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