﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using OctoAwesome.Components;
using OctoAwesome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoAwesome.Rendering {
    internal class CellTypeRenderer {
        private readonly Texture2D center;
        private readonly Texture2D left;
        private readonly Texture2D right;
        private readonly Texture2D upper;
        private readonly Texture2D lower;
        private readonly Texture2D upperLeft_concave;
        private readonly Texture2D upperRight_concave;
        private readonly Texture2D lowerLeft_concave;
        private readonly Texture2D lowerRight_concave;
        private readonly Texture2D upperLeft_convex;
        private readonly Texture2D upperRight_convex;
        private readonly Texture2D lowerLeft_convex;
        private readonly Texture2D lowerRight_convex;

        public CellTypeRenderer(ContentManager content, string name) {
            center = content.Load<Texture2D>($"Textures/{name}_center");
            left = content.Load<Texture2D>($"Textures/{name}_left");
            right = content.Load<Texture2D>($"Textures/{name}_right");
            upper = content.Load<Texture2D>($"Textures/{name}_upper");
            lower = content.Load<Texture2D>($"Textures/{name}_lower");
            upperLeft_concave = content.Load<Texture2D>($"Textures/{name}_upperLeft_concave");
            upperRight_concave = content.Load<Texture2D>($"Textures/{name}_upperRight_concave");
            lowerLeft_concave = content.Load<Texture2D>($"Textures/{name}_lowerLeft_concave");
            lowerRight_concave = content.Load<Texture2D>($"Textures/{name}_lowerRight_concave");
            upperLeft_convex = content.Load<Texture2D>($"Textures/{name}_upperLeft_convex");
            upperRight_convex = content.Load<Texture2D>($"Textures/{name}_upperRight_convex");
            lowerLeft_convex = content.Load<Texture2D>($"Textures/{name}_lowerLeft_convex");
            lowerRight_convex = content.Load<Texture2D>($"Textures/{name}_lowerRight_convex");
        }

        public void Draw(SpriteBatch g, CameraComponent camera, OctoAwesome.Model.World game, int x, int y) {
            CellType centerType = game.Map.GetCell(x, y);

            DrawTexture(g, camera, x, y, center);

            bool left = x > 0 && game.Map.GetCell(x - 1, y) != centerType;
            bool top = y > 0 && game.Map.GetCell(x, y - 1) != centerType;
            bool right = (x + 1) < game.Map.Columns && game.Map.GetCell(x + 1, y) != centerType;
            bool bottom = (y + 1) < game.Map.Rows && game.Map.GetCell(x, y + 1) != centerType;

            bool upperLeft = x > 0 && y > 0 && game.Map.GetCell(x - 1, y - 1) != centerType;
            bool upperRight = (x + 1) < game.Map.Columns && y > 0 &&
                                game.Map.GetCell(x + 1, y - 1) != centerType;
            bool lowerLeft = x > 0 && y < game.Map.Rows &&
                                game.Map.GetCell(x - 1, y + 1) != centerType;
            bool lowerRight = (x + 1) < game.Map.Columns && (y + 1) < game.Map.Rows &&
                                game.Map.GetCell(x + 1, y + 1) != centerType;

            if (left) DrawTexture(g, camera, x, y, this.left);
            if (top) DrawTexture(g, camera, x, y, upper);
            if (right) DrawTexture(g, camera, x, y, this.right);
            if (bottom) DrawTexture(g, camera, x, y, lower);

            if (left && top) DrawTexture(g, camera, x, y, upperLeft_convex);
            if (left && bottom) DrawTexture(g, camera, x, y, lowerLeft_convex);
            if (right && top) DrawTexture(g, camera, x, y, upperRight_convex);
            if (right && bottom) DrawTexture(g, camera, x, y, lowerRight_convex);

            if (upperLeft && !top && !left) DrawTexture(g, camera, x, y, upperLeft_concave);
            if (upperRight && !top && !right) DrawTexture(g, camera, x, y, upperRight_concave);
            if (lowerLeft && !bottom && !left) DrawTexture(g, camera, x, y, lowerLeft_concave);
            if (lowerRight && !bottom && !right) DrawTexture(g, camera, x, y, lowerRight_concave);
        }

        public static void DrawTexture(SpriteBatch g, CameraComponent camera, int x, int y, Texture2D image) {
            g.Draw(image, new Rectangle(
                (int)(x * camera.SCALE - camera.ViewPort.X),
                (int)(y * camera.SCALE - camera.ViewPort.Y),
                (int)camera.SCALE,
                (int)camera.SCALE),
                Color.White);
        }
    }
}
