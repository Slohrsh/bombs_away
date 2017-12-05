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

namespace bombs_away.controller
{
    class GameView
    {
        private ModelView modelView = ModelView.Instance;
        private ITexture texture;

        public GameView()
        {
            TextureLoader textureLoader = new TextureLoader();
            texture = textureLoader.LoadContent("../../resources/game/map/BasicMap.tmx");
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
            //GL.Translate()
            //GL.Scale()

            for (int y = 0; y < (int)StaticValues.GRIDSIZE; y++)
            {
                for (int x = 0; x < (int)StaticValues.GRIDSIZE; x++)
                {
                    Block block = modelView.StaticGrid[x, y];
                    Draw(block.Component, block.IsVisible, block.Color);
                }
            }
            foreach(Block block in modelView.InteractiveObjects)
            {
                Draw(block.Component, block.IsVisible, block.Color);
            }
        }

        private void Draw(Box2D component, bool isVisible, Color4 color)
        {
            if (isVisible)
            {
                if (component != null)
                {
                    texture.Activate();
                    GL.Begin(PrimitiveType.Quads);
                    GL.Color4(color);
                    GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(component.MinX, component.MinY);
                    GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(component.MaxX, component.MinY);
                    GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(component.MaxX, component.MaxY);
                    GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(component.MinX, component.MaxY);
                    GL.End();
                    texture.Deactivate();

                }
            }
        }
    }
}
