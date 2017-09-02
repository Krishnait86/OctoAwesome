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
        VertexPositionNormalTexture[] vertices;
        BasicEffect effect;

        float rotY = 0f;

        Texture2D sand;

        VertexBuffer vb;
        IndexBuffer ib;

        public Render3DComponent(Game game) : base(game)
        {
            
        }

        protected override void LoadContent()
        {
            vertices = new VertexPositionNormalTexture[]
            {
                new VertexPositionNormalTexture(new Vector3(-1f, 1f, 0f), Vector3.Forward, new Vector2(0, 0)),
                new VertexPositionNormalTexture(new Vector3(1f, 1f, 0f), Vector3.Forward, new Vector2(1, 0)),
                new VertexPositionNormalTexture(new Vector3(1f, -1f, 0f), Vector3.Forward, new Vector2(1, 1)),
                //new VertexPositionNormalTexture(new Vector3(-1f, 1f, 0f), Vector3.Forward, new Vector2(0, 0)),
                //new VertexPositionNormalTexture(new Vector3(1f, -1f, 0f), Vector3.Forward, new Vector2(1, 1)),
                new VertexPositionNormalTexture(new Vector3(-1f, -1f, 0f), Vector3.Forward, new Vector2(0, 1))

            };

            short[] index = new short[]
            {
                0, 1, 2, 0, 2, 3
            };

            vb = new VertexBuffer(GraphicsDevice, VertexPositionNormalTexture.VertexDeclaration, 6, BufferUsage.WriteOnly);
            vb.SetData<VertexPositionNormalTexture>(vertices);

            ib = new IndexBuffer(GraphicsDevice, IndexElementSize.SixteenBits, 6, BufferUsage.WriteOnly);
            ib.SetData<short>(index);

            sand = Game.Content.Load<Texture2D>("Textures/sand_center");

            effect = new BasicEffect(GraphicsDevice);
            effect.EnableDefaultLighting();

            effect.World = Matrix.Identity;
            effect.View = Matrix.CreateLookAt(new Vector3(0, 0, 5), Vector3.Zero, Vector3.Up);
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1f, 10000f);

            effect.TextureEnabled = true;
            effect.Texture = sand;

            RasterizerState rasterazerState = new RasterizerState();
            rasterazerState.CullMode = CullMode.None;
            //rasterazerState.FillMode = FillMode.WireFrame;

            GraphicsDevice.RasterizerState = rasterazerState;
            base.LoadContent(); 
            //rasterazerState.Dispose();
        }

        public override void Update(GameTime gameTime)
        {
            rotY += (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            effect.World = Matrix.CreateRotationY(rotY);

            GraphicsDevice.SetVertexBuffer(vb);
            GraphicsDevice.Indices = ib;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
#pragma warning disable CS0618 // Type or member is obsolete
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
#pragma warning restore CS0618 // Type or member is obsolete
                              //GraphicsDevice.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, vertices, 0, 2);
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