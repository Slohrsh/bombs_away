using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using bombs_away.game;
using Zenseless.Geometry;

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
        private Camera camera = Camera.Instance;

        internal void DrawScreen()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            //GL.Viewport(0,0, 800, 800);

            camera.beginDraw();

            for (int y = 0; y < modelView.gridSize; y++)
            {
                for (int x = 0; x < modelView.gridSize; x++)
                {
                    List<Block> blocks = modelView.ConstantGrid[x, y];
                    foreach(Block block in blocks)
                    {
                        Draw(block.Bounds, block.IsVisible, block.TextureCoordinates, block.TextureType);
                    }
                }
            }
            camera.endDraw();
        }

        private void Draw(Box2D component, bool isVisible, Box2D textureCoordinates, string textureType)
        {
            if (isVisible)
            {
                if (component != null)
                {
                    if (textureType.Equals("map"))
                    {
                        textureList[0].Activate();
                        GL.Begin(PrimitiveType.Quads);
                        GL.TexCoord2(textureCoordinates.MinX, textureCoordinates.MinY); GL.Vertex2(component.MinX, component.MinY);
                        GL.TexCoord2(textureCoordinates.MaxX, textureCoordinates.MinY); GL.Vertex2(component.MaxX, component.MinY);
                        GL.TexCoord2(textureCoordinates.MaxX, textureCoordinates.MaxY); GL.Vertex2(component.MaxX, component.MaxY);
                        GL.TexCoord2(textureCoordinates.MinX, textureCoordinates.MaxY); GL.Vertex2(component.MinX, component.MaxY);
                        GL.End();
                        textureList[0].Deactivate();
                    }

                    if (textureType.Equals("char"))
                    {
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
}
