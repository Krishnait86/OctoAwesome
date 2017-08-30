using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using OctoAwesome.Components;
using OctoAwesome.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoAwesome.Rendering
{
    internal class CellTypeRenderer
    {
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

        public CellTypeRenderer(ContentManager content, string name)
        {
            center = content.Load<Texture2D>(String.Format("Textures/{0}_center", name));
            left = content.Load<Texture2D>(String.Format("Textures/{0}_left", name));
            right = content.Load<Texture2D>(String.Format("Textures/{0}_right", name));
            upper = content.Load<Texture2D>(String.Format("Textures/{0}_upper", name));
            lower = content.Load<Texture2D>(String.Format("Textures/{0}_lower", name));
            upperLeft_concave = content.Load<Texture2D>(String.Format("Textures/{0}_upperLeft_concave", name));
            upperRight_concave = content.Load<Texture2D>(String.Format("Textures/{0}_upperRight_concave", name));
            lowerLeft_concave = content.Load<Texture2D>(String.Format("Textures/{0}_lowerLeft_concave", name));
            lowerRight_concave = content.Load<Texture2D>(String.Format("Textures/{0}_lowerRight_concave", name));
            upperLeft_convex = content.Load<Texture2D>(String.Format("Textures/{0}_upperLeft_convex", name));
            upperRight_convex = content.Load<Texture2D>(String.Format("Textures/{0}_upperRight_convex", name));
            lowerLeft_convex = content.Load<Texture2D>(String.Format("Textures/{0}_lowerLeft_convex", name));
            lowerRight_convex = content.Load<Texture2D>(String.Format("Textures/{0}_lowerRight_convex", name));
        }

        public void Draw(SpriteBatch g, OctoAwesome.Model.Game game, int x, int y)
        {
            CellType centerType = game.Map.GetCell(x, y);

            DrawTexture(g, game.Camera, x, y, center);

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

            if (left) DrawTexture(g, game.Camera, x, y, this.left);
            if (top) DrawTexture(g, game.Camera, x, y, upper);
            if (right) DrawTexture(g, game.Camera, x, y, this.right);
            if (bottom) DrawTexture(g, game.Camera, x, y, lower);

            if (left && top) DrawTexture(g, game.Camera, x, y, upperLeft_convex);
            if (left && bottom) DrawTexture(g, game.Camera, x, y, lowerLeft_convex);
            if (right && top) DrawTexture(g, game.Camera, x, y, upperRight_convex);
            if (right && bottom) DrawTexture(g, game.Camera, x, y, lowerRight_convex);

            if (upperLeft && !top && !left) DrawTexture(g, game.Camera, x, y, upperLeft_concave);
            if (upperRight && !top && !right) DrawTexture(g, game.Camera, x, y, upperRight_concave);
            if (lowerLeft && !bottom && !left) DrawTexture(g, game.Camera, x, y, lowerLeft_concave);
            if (lowerRight && !bottom && !right) DrawTexture(g, game.Camera, x, y, lowerRight_concave);
        }

        public static void DrawTexture(SpriteBatch g, Camera camera, int x, int y, Texture2D image)
        {
            g.Draw (image, new Rectangle(
                (int)(x * camera.SCALE - camera.ViewPort.X),
                (int)(y * camera.SCALE - camera.ViewPort.Y),
                (int)camera.SCALE,
                (int)camera.SCALE),
                Color.White);
        }
    }
}
