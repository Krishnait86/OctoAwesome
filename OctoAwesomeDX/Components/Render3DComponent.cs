﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OctoAwesome.Model;
using OctoAwesome.Model.Blocks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoAwesome.Components {
    internal sealed class Render3DComponent : DrawableGameComponent {
        private WorldComponent world;
        private Camera3DComponent camera;

        private BasicEffect effect;

        private Texture2D blockTextures;

        private Texture2D grass;
        private Texture2D sand;
        private Texture2D water;

        private Texture2D sprite;
        private Texture2D tree;
        private Texture2D box;

        private VertexBuffer vb;
        private IndexBuffer ib;
        private int vertexCount;
        private int indexCount;

        public Render3DComponent(Game game, WorldComponent world, Camera3DComponent camera) : base(game) {
            this.world = world;
            this.camera = camera;
        }

        protected override void LoadContent() {

            Bitmap grassTex = GrassBlock.Texture;
            Bitmap sandTex = GrassBlock.Texture;
            Bitmap blocks = new Bitmap(128, 128);

            using (System.Drawing.Graphics g = Graphics.FromImage(blocks)) {
                g.DrawImage(grassTex, new PointF(0, 0));
                g.DrawImage(sandTex, new PointF(64, 0));
            }

            using (MemoryStream stream = new MemoryStream()) {
                blocks.Save(stream, ImageFormat.Png);
                stream.Seek(0, SeekOrigin.Begin);
                blockTextures = Texture2D.FromStream(GraphicsDevice, stream);
            }

            vertexCount = Chunk.CHUNKSIZE_X * Chunk.CHUNKSIZE_Y * Chunk.CHUNKSIZE_Z * 24;
            indexCount = Chunk.CHUNKSIZE_X * Chunk.CHUNKSIZE_Y * Chunk.CHUNKSIZE_Z * 36;
            VertexPositionNormalTexture[] vertices = new VertexPositionNormalTexture[vertexCount];
            int[] index = new int[indexCount];

            for (int z = 0; z < Chunk.CHUNKSIZE_Z; z++) {
                for (int y = 0; y < Chunk.CHUNKSIZE_Y; y++) {
                    for (int x = 0; x < Chunk.CHUNKSIZE_X; x++) {
                        if (world.World.Chunk.Blocks[x, y, z] == null)
                            continue;

                        int offset = x + (y * Chunk.CHUNKSIZE_X) + (z * Chunk.CHUNKSIZE_X * Chunk.CHUNKSIZE_Y);
                        int vertexOffset = offset * 24;
                        int indexOffset = offset * 36;

                        Vector2 textureOffset = new Vector2();
                        Vector2 textureSize = new Vector2(0.5f, 0.5f);

                        if (world.World.Chunk.Blocks[x, y, z] is GrassBlock) {
                            textureOffset = new Vector2(0, 0);
                        }
                        else if (world.World.Chunk.Blocks[x, y, z] is SandBlock) {
                            textureOffset = new Vector2(0.5f, 0);
                        }

                        // offsets // Oben
                        {
                            vertices[vertexOffset + 0] = new VertexPositionNormalTexture(new Vector3(x + 0, y + 1, z + 0), Vector3.Up, textureOffset);
                            vertices[vertexOffset + 1] = new VertexPositionNormalTexture(new Vector3(x + 1, y + 1, z + 0), Vector3.Up, new Vector2(textureOffset.X + textureSize.X, textureOffset.Y));
                            vertices[vertexOffset + 2] = new VertexPositionNormalTexture(new Vector3(x + 0, y + 1, z + 1), Vector3.Up, new Vector2(textureOffset.X, textureOffset.Y + +textureSize.X));
                            vertices[vertexOffset + 3] = new VertexPositionNormalTexture(new Vector3(x + 1, y + 1, z + 1), Vector3.Up, textureOffset + textureSize);
                            index[indexOffset + 0] = vertexOffset + 0;
                            index[indexOffset + 1] = vertexOffset + 1;
                            index[indexOffset + 2] = vertexOffset + 3;
                            index[indexOffset + 3] = vertexOffset + 0;
                            index[indexOffset + 4] = vertexOffset + 3;
                            index[indexOffset + 5] = vertexOffset + 2;

                            // Links
                            vertices[vertexOffset + 4] = new VertexPositionNormalTexture(new Vector3(x + 0, y + 1, z + 0), Vector3.Left, textureOffset);
                            vertices[vertexOffset + 5] = new VertexPositionNormalTexture(new Vector3(x + 0, y + 1, z + 1), Vector3.Left, new Vector2(textureOffset.X + textureSize.X, textureOffset.Y));
                            vertices[vertexOffset + 6] = new VertexPositionNormalTexture(new Vector3(x + 0, y + 0, z + 0), Vector3.Left, new Vector2(textureOffset.X, textureOffset.Y + +textureSize.X));
                            vertices[vertexOffset + 7] = new VertexPositionNormalTexture(new Vector3(x + 0, y + 0, z + 1), Vector3.Left, textureOffset + textureSize);
                            index[indexOffset + 6] = vertexOffset + 4;
                            index[indexOffset + 7] = vertexOffset + 5;
                            index[indexOffset + 8] = vertexOffset + 7;
                            index[indexOffset + 9] = vertexOffset + 4;
                            index[indexOffset + 10] = vertexOffset + 7;
                            index[indexOffset + 11] = vertexOffset + 6;

                            // Vorne
                            vertices[vertexOffset + 8] = new VertexPositionNormalTexture(new Vector3(x + 0, y + 1, z + 1), Vector3.Forward, textureOffset);
                            vertices[vertexOffset + 9] = new VertexPositionNormalTexture(new Vector3(x + 1, y + 1, z + 1), Vector3.Forward, new Vector2(textureOffset.X + textureSize.X, textureOffset.Y));
                            vertices[vertexOffset + 10] = new VertexPositionNormalTexture(new Vector3(x + 0, y + 0, z + 1), Vector3.Forward, new Vector2(textureOffset.X, textureOffset.Y + +textureSize.X));
                            vertices[vertexOffset + 11] = new VertexPositionNormalTexture(new Vector3(x + 1, y + 0, z + 1), Vector3.Forward, textureOffset + textureSize);
                            index[indexOffset + 12] = vertexOffset + 8;
                            index[indexOffset + 13] = vertexOffset + 9;
                            index[indexOffset + 14] = vertexOffset + 11;
                            index[indexOffset + 15] = vertexOffset + 8;
                            index[indexOffset + 16] = vertexOffset + 11;
                            index[indexOffset + 17] = vertexOffset + 10;

                            // Rechts
                            vertices[vertexOffset + 12] = new VertexPositionNormalTexture(new Vector3(x + 1, y + 1, z + 1), Vector3.Right, textureOffset);
                            vertices[vertexOffset + 13] = new VertexPositionNormalTexture(new Vector3(x + 1, y + 1, z + 0), Vector3.Right, new Vector2(textureOffset.X + textureSize.X, textureOffset.Y));
                            vertices[vertexOffset + 14] = new VertexPositionNormalTexture(new Vector3(x + 1, y + 0, z + 1), Vector3.Right, new Vector2(textureOffset.X, textureOffset.Y + +textureSize.X));
                            vertices[vertexOffset + 15] = new VertexPositionNormalTexture(new Vector3(x + 1, y + 0, z + 0), Vector3.Right, textureOffset + textureSize);
                            index[indexOffset + 18] = vertexOffset + 12;
                            index[indexOffset + 19] = vertexOffset + 13;
                            index[indexOffset + 20] = vertexOffset + 15;
                            index[indexOffset + 21] = vertexOffset + 12;
                            index[indexOffset + 22] = vertexOffset + 15;
                            index[indexOffset + 23] = vertexOffset + 14;

                            // Hinten
                            vertices[vertexOffset + 16] = new VertexPositionNormalTexture(new Vector3(x + 1, y + 1, z + 0), Vector3.Backward, textureOffset);
                            vertices[vertexOffset + 17] = new VertexPositionNormalTexture(new Vector3(x + 0, y + 1, z + 0), Vector3.Backward, new Vector2(textureOffset.X + textureSize.X, textureOffset.Y));
                            vertices[vertexOffset + 18] = new VertexPositionNormalTexture(new Vector3(x + 1, y + 0, z + 0), Vector3.Backward, new Vector2(textureOffset.X, textureOffset.Y + +textureSize.X));
                            vertices[vertexOffset + 19] = new VertexPositionNormalTexture(new Vector3(x + 0, y + 0, z + 0), Vector3.Backward, textureOffset + textureSize);
                            index[indexOffset + 24] = vertexOffset + 16;
                            index[indexOffset + 25] = vertexOffset + 17;
                            index[indexOffset + 26] = vertexOffset + 19;
                            index[indexOffset + 27] = vertexOffset + 16;
                            index[indexOffset + 28] = vertexOffset + 19;
                            index[indexOffset + 29] = vertexOffset + 18;

                            // Unten
                            vertices[vertexOffset + 20] = new VertexPositionNormalTexture(new Vector3(x + 0, y + 0, z + 1), Vector3.Down, textureOffset);
                            vertices[vertexOffset + 21] = new VertexPositionNormalTexture(new Vector3(x + 1, y + 0, z + 1), Vector3.Down, new Vector2(textureOffset.X + textureSize.X, textureOffset.Y));
                            vertices[vertexOffset + 22] = new VertexPositionNormalTexture(new Vector3(x + 0, y + 0, z + 0), Vector3.Down, new Vector2(textureOffset.X, textureOffset.Y + +textureSize.X));
                            vertices[vertexOffset + 23] = new VertexPositionNormalTexture(new Vector3(x + 1, y + 0, z + 0), Vector3.Down, textureOffset + textureSize);
                            index[indexOffset + 30] = vertexOffset + 20;
                            index[indexOffset + 31] = vertexOffset + 21;
                            index[indexOffset + 32] = vertexOffset + 23;
                            index[indexOffset + 33] = vertexOffset + 20;
                            index[indexOffset + 34] = vertexOffset + 23;
                            index[indexOffset + 35] = vertexOffset + 22;
                        }
                    }
                }
            }

            vb = new VertexBuffer(GraphicsDevice, VertexPositionNormalTexture.VertexDeclaration, vertexCount, BufferUsage.WriteOnly);
            vb.SetData<VertexPositionNormalTexture>(vertices);

            ib = new IndexBuffer(GraphicsDevice, IndexElementSize.ThirtyTwoBits, indexCount, BufferUsage.WriteOnly);
            ib.SetData<int>(index);

            grass = Game.Content.Load<Texture2D>("Textures/grass_center");
            sand = Game.Content.Load<Texture2D>("Textures/sand_center");
            water = Game.Content.Load<Texture2D>("Textures/water_center");

            tree = Game.Content.Load<Texture2D>("Textures/tree");
            box = Game.Content.Load<Texture2D>("Textures/box");
            sprite = Game.Content.Load<Texture2D>("Textures/sprite");

            effect = new BasicEffect(GraphicsDevice);
            //effect.EnableDefaultLighting();

            effect.World = Matrix.Identity;
            effect.Projection = camera.Projection;
            effect.TextureEnabled = true;

            RasterizerState rasterazerState = new RasterizerState();
            rasterazerState.CullMode = CullMode.None;
            //rasterazerState.FillMode = FillMode.WireFrame;

            GraphicsDevice.RasterizerState = rasterazerState;
            base.LoadContent();
        }

        //public override void Update(GameTime gameTime)
        //{
        //    base.Update(gameTime);
        //}

        public override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            effect.World = Matrix.Identity;
            effect.View = camera.View;
            effect.Texture = blockTextures;
            //effect.VertexColorEnabled = false;

            GraphicsDevice.SetVertexBuffer(vb);
            GraphicsDevice.Indices = ib;

            foreach (var pass in effect.CurrentTechnique.Passes) {
                pass.Apply();

                // minVertexIndex numVertices, DrawIndexedPrimitives is obsolete !"0, vertexCount, "
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, indexCount / 3);

                //for (int z = 0; z < Chunk.CHUNKSIZE_Z; z++) {
                //    for (int y = 0; y < Chunk.CHUNKSIZE_Y; y++) {
                //        for (int x = 0; x < Chunk.CHUNKSIZE_X; x++) {

                //            int offset = x + (y * Chunk.CHUNKSIZE_X) + (z * Chunk.CHUNKSIZE_X * Chunk.CHUNKSIZE_Y);
                //            int indexOffset = offset * 36;

                //            if (world.World.Chunk.Blocks[x, y, z] == null)
                //                continue;
                //        }
                //    }
                //}
            }
        }
    }
}
