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
        VertexPositionColor[] vertices;
        BasicEffect effect;

        float rotY = 0f;

        public Render3DComponent(Game game) : base(game)
        {
            
        }

        protected override void LoadContent()
        {
            vertices = new VertexPositionColor[]
            {
                new VertexPositionColor(new Vector3(-5f,5f,0f), Color.Red),
                new VertexPositionColor(new Vector3(5f,5f,0f), Color.Green),
                new VertexPositionColor(new Vector3(0f,-5f,0f), Color.Yellow)
            };

            effect = new BasicEffect(GraphicsDevice);
            //effect.EnableDefaultLighting();
            effect.World = Matrix.Identity;
            effect.View = Matrix.CreateLookAt(new Vector3(0, 0, 20), Vector3.Zero, Vector3.Up);
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1f, 10000f);
            effect.VertexColorEnabled = true;

            RasterizerState rasterazerState = new RasterizerState();
            rasterazerState.CullMode = CullMode.None;
            rasterazerState.FillMode = FillMode.WireFrame;

            GraphicsDevice.RasterizerState = rasterazerState;

            base.LoadContent(); 
        }

        public override void Update(GameTime gameTime)
        {
            rotY += (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //GraphicsDevice.RasterizerState.CullMode = CullMode.None;
            //GraphicsDevice.RasterizerState.FillMode = FillMode.WireFrame;

            effect.World = Matrix.CreateRotationY(rotY);
            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, vertices, 0, 1);
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