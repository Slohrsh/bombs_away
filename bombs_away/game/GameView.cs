using OpenTK.Graphics.OpenGL;
using bombs_away.ui.elements.bomb;
using bombs_away.ui.elements.enemy;
using bombs_away.ui.elements.obstacle;
using OpenTK.Graphics;
using bombs_away.game;
using Zenseless.Geometry;
using bombs_away.ui.elements.ground;
using bombs_away.ui.zenseless;

using System.Collections.Generic;
using Zenseless.HLGL;
using System.Drawing;

namespace bombs_away.controller
{
    class GameView
    {
        private ModelView modelView = ModelView.Instance;
        private IList<ITexture> textureList;

        public GameView()
        {
            TextureLoader textureLoader = new TextureLoader();
            textureList = textureLoader.LoadContent();
            GL.ClearColor(Color.White);
            //for transparency in textures we use blending
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend); // for transparency in textures we use blending
            GL.Enable(EnableCap.Texture2D); //todo: only for non shader pipeline relevant -> remove at some point
        }

        internal void DrawScreen()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            //Wichtig für Kamera:
            //GL.Scale(2, 2, 0);
            for (int y = 0; y < (int)StaticValues.GRIDSIZE; y++)
            {
                for (int x = 0; x < (int)StaticValues.GRIDSIZE; x++)
                {
                    Block block = modelView.StaticGrid[x, y];
                    Draw(block.Component, block.IsVisible, block.TextureCoordinates);
                }
            }
            foreach(Block block in modelView.InteractiveObjects)
            {
                Draw(block.Component, block.IsVisible, block.TextureCoordinates);
            }
        }

        private void Draw(Box2D component, bool isVisible, Box2D textureCoordinates)
        {
            if (isVisible)
            {
                if (component != null)
                {
                    /*textureList[0].Activate();
                    GL.Begin(PrimitiveType.Quads);
                    GL.TexCoord2(textureCoordinates.MinX, textureCoordinates.MinY); GL.Vertex2(component.MinX, component.MinY);
                    GL.TexCoord2(textureCoordinates.MaxX, textureCoordinates.MinY); GL.Vertex2(component.MaxX, component.MinY);
                    GL.TexCoord2(textureCoordinates.MaxX, textureCoordinates.MaxY); GL.Vertex2(component.MaxX, component.MaxY);
                    GL.TexCoord2(textureCoordinates.MinX, textureCoordinates.MaxY); GL.Vertex2(component.MinX, component.MaxY);
                    GL.End();
                    textureList[0].Deactivate();*/
                    textureList[1].Activate();
                    GL.Begin(PrimitiveType.Quads);
                    GL.TexCoord2(textureCoordinates.MinX, textureCoordinates.MinY); GL.Vertex2(component.MinX, component.MinY);
                    GL.TexCoord2(textureCoordinates.MaxX, textureCoordinates.MinY); GL.Vertex2(component.MaxX, component.MinY);
                    GL.TexCoord2(textureCoordinates.MaxX, textureCoordinates.MaxY); GL.Vertex2(component.MaxX, component.MaxY);
                    GL.TexCoord2(textureCoordinates.MinX, textureCoordinates.MaxY); GL.Vertex2(component.MinX, component.MaxY);
                    GL.End();
                    textureList[1].Deactivate();

                }
            }
        }
    }
}
