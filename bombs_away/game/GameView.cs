using OpenTK.Graphics.OpenGL;
using bombs_away.ui.elements.bomb;
using bombs_away.ui.elements.enemy;
using bombs_away.ui.elements.obstacle;
using OpenTK.Graphics;
using bombs_away.game;
using Zenseless.Geometry;
using bombs_away.ui.elements.ground;
using bombs_away.ui.zenseless;

using Zenseless.HLGL;
using System.Drawing;
using System;
using OpenTK;

namespace bombs_away.controller
{
    class GameView
    {
        private ModelView modelView = ModelView.Instance;
        private ITexture texture;

        public GameView()
        {
            TextureLoader textureLoader = new TextureLoader();
            texture = textureLoader.LoadContent();
            GL.ClearColor(Color.White);
            //for transparency in textures we use blending
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend); // for transparency in textures we use blending
            GL.Enable(EnableCap.Texture2D); //todo: only for non shader pipeline relevant -> remove at some point
        }
        private Camera camera = Camera.Instance;

        internal void DrawScreen()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            //GL.Viewport(0,0, 800, 800);

            camera.beginDraw();

            for (int y = 0; y < (int)StaticValues.GRIDSIZE; y++)
            {
                for (int x = 0; x < (int)StaticValues.GRIDSIZE; x++)
                {
                    Block block = modelView.StaticGrid[x, y];
                    Draw(block.Bounds, block.IsVisible, block.TextureCoordinates);
                }
            }
            foreach(Block block in modelView.InteractiveObjects)
            {
                Draw(block.Bounds, block.IsVisible, block.TextureCoordinates);
            }
            camera.endDraw();
        }

        private void Draw(Box2D component, bool isVisible, Box2D textureCoordinates)
        {
            if (isVisible)
            {
                if (component != null)
                {
                    texture.Activate();
                    GL.Begin(PrimitiveType.Quads);
                    GL.TexCoord2(textureCoordinates.MinX, textureCoordinates.MinY); GL.Vertex2(component.MinX, component.MinY);
                    GL.TexCoord2(textureCoordinates.MaxX, textureCoordinates.MinY); GL.Vertex2(component.MaxX, component.MinY);
                    GL.TexCoord2(textureCoordinates.MaxX, textureCoordinates.MaxY); GL.Vertex2(component.MaxX, component.MaxY);
                    GL.TexCoord2(textureCoordinates.MinX, textureCoordinates.MaxY); GL.Vertex2(component.MinX, component.MaxY);
                    GL.End();
                    texture.Deactivate();
                }
            }
        }
    }
}
