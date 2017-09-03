using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoAwesome.Components
{
    internal sealed class Render3DComponent : DrawableGameComponent
    {
        private WorldComponent world;
        private Camera3DComponent camera;

        private BasicEffect effect;

        private VertexBuffer vb;
        private IndexBuffer ib;
        private int vertexCount;
        private int indexCount;

        private Texture2D grass;
        private Texture2D sand;
        private Texture2D water;

        public Render3DComponent(Game game, WorldComponent world, Camera3DComponent camera) : base(game)
        {
            this.world = world;
            this.camera = camera;
        }

        protected override void LoadContent()
        {
            int width = world.World.Map.CellCache.GetLength(0);
            int height = world.World.Map.CellCache.GetLength(1);
            vertexCount = width * height * 4;
            indexCount = width * height * 6;

            VertexPositionNormalTexture[] vertices = new VertexPositionNormalTexture[vertexCount];
            short[] index = new short[indexCount];

            for (int z = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    int vertexOffset = (((z * width) + x) * 4);
                    int indexOffset = (((z * width) + x) * 6);
                    vertices[vertexOffset + 0] = new VertexPositionNormalTexture(new Vector3(x, 0, z), Vector3.Up, new Vector2(0, 0));
                    vertices[vertexOffset + 1] = new VertexPositionNormalTexture(new Vector3(x + 1, 0, z), Vector3.Up, new Vector2(1, 0));
                    vertices[vertexOffset + 2] = new VertexPositionNormalTexture(new Vector3(x, 0, z + 1), Vector3.Up, new Vector2(0, 1));
                    vertices[vertexOffset + 3] = new VertexPositionNormalTexture(new Vector3(x + 1, 0, z + 1), Vector3.Up, new Vector2(1, 1));

                    index[indexOffset + 0] = (short)(vertexOffset + 0);
                    index[indexOffset + 1] = (short)(vertexOffset + 1);
                    index[indexOffset + 2] = (short)(vertexOffset + 3);
                    index[indexOffset + 3] = (short)(vertexOffset + 0);
                    index[indexOffset + 4] = (short)(vertexOffset + 3);
                    index[indexOffset + 5] = (short)(vertexOffset + 2);
                }
            }

            vb = new VertexBuffer(GraphicsDevice, VertexPositionNormalTexture.VertexDeclaration, vertexCount, BufferUsage.WriteOnly);
            vb.SetData<VertexPositionNormalTexture>(vertices);

            ib = new IndexBuffer(GraphicsDevice, IndexElementSize.SixteenBits, indexCount, BufferUsage.WriteOnly);
            ib.SetData<short>(index);

            grass = Game.Content.Load<Texture2D>("Textures/grass_center");
            sand = Game.Content.Load<Texture2D>("Textures/sand_center");
            water = Game.Content.Load<Texture2D>("Textures/water_center");

            effect = new BasicEffect(GraphicsDevice);
            //effect.EnableDefaultLighting();

            effect.World = Matrix.Identity;

            effect.Projection = camera.Projection;

            effect.TextureEnabled = true;
            effect.Texture = grass;

            RasterizerState rasterazerState = new RasterizerState();
            rasterazerState.CullMode = CullMode.None;
            //rasterazerState.FillMode = FillMode.WireFrame;

            GraphicsDevice.RasterizerState = rasterazerState;
            base.LoadContent();
            //rasterazerState.Dispose();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            int width = world.World.Map.CellCache.GetLength(0);
            int height = world.World.Map.CellCache.GetLength(1);
            vertexCount = width * height * 4;
            indexCount = width * height * 6;

            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.SetVertexBuffer(vb);
            GraphicsDevice.Indices = ib;

            effect.View = camera.View;

            for (int z = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    Model.CellCache cell = world.World.Map.CellCache[x, z];

                    switch (cell.CellType)
                    {
                        case Model.CellType.Grass:
                            effect.Texture = grass;
                            break;
                        case Model.CellType.Sand:
                            effect.Texture = sand;
                            break;
                        case Model.CellType.Water:
                            effect.Texture = water;
                            break;
                    }

                    int indexOffset = ((z * width) + x) * 6;

                    foreach (var pass in effect.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                        
#pragma warning disable CS0618 // Type or member is obsolete
                        GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertexCount, indexOffset, 2);
#pragma warning restore CS0618 // Type or member is obsolete
                    }
                }
            }
        }
    }
}

//public RasterizerState()
//{
//    CullMode = CullMode.CullCounterClockwiseFace;
//    FillMode = FillMode.Solid;
//    DepthBias = 0;
//    MultiSampleAntiAlias = true;
//    ScissorTestEnable = false;
//    SlopeScaleDepthBias = 0;
//    DepthClipEnable = true;
//}
//#pragma warning disable CS0618 // Type or member is obsolete
//pass.Apply();
//                    effect.View = camera.View;
//                    GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertexCount, 0, indexCount / 3);
//#pragma warning restore CS0618 // Type or member is obsolete