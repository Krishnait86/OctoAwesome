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
        private readonly Image center;
        private readonly Image left;
        private readonly Image right;
        private readonly Image upper;
        private readonly Image lower;
        private readonly Image upperLeft_concave;
        private readonly Image upperRight_concave;
        private readonly Image lowerLeft_concave;
        private readonly Image lowerRight_concave;
        private readonly Image upperLeft_convex;
        private readonly Image upperRight_convex;
        private readonly Image lowerLeft_convex;
        private readonly Image lowerRight_convex;

        public CellTypeRenderer(string name)
        {
            center = Image.FromFile(String.Format("Assets/{0}_center.png", name));
            left = Image.FromFile(String.Format("Assets/{0}_left.png", name));
            right = Image.FromFile(String.Format("Assets/{0}_right.png", name));
            upper = Image.FromFile(String.Format("Assets/{0}_upper.png", name));
            lower = Image.FromFile(String.Format("Assets/{0}_lower.png", name));
            upperLeft_concave = Image.FromFile(String.Format("Assets/{0}_upperLeft_concave.png", name));
            upperRight_concave = Image.FromFile(String.Format("Assets/{0}_upperRight_concave.png", name));
            lowerLeft_concave = Image.FromFile(String.Format("Assets/{0}_lowerLeft_concave.png", name));
            lowerRight_concave = Image.FromFile(String.Format("Assets/{0}_lowerRight_concave.png", name));
            upperLeft_convex = Image.FromFile(String.Format("Assets/{0}_upperLeft_convex.png", name));
            upperRight_convex = Image.FromFile(String.Format("Assets/{0}_upperRight_convex.png", name));
            lowerLeft_convex = Image.FromFile(String.Format("Assets/{0}_lowerLeft_convex.png", name));
            lowerRight_convex = Image.FromFile(String.Format("Assets/{0}_lowerRight_convex.png", name));
        }

        public void Draw(Graphics g, Game game, int x, int y)
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

        public static void DrawTexture(Graphics g, Camera camera, int x, int y, Image image)
        {
            g.DrawImage(image, new Rectangle(
                (int)(x * camera.SCALE - camera.ViewPort.X),
                (int)(y * camera.SCALE - camera.ViewPort.Y),
                (int)camera.SCALE,
                (int)camera.SCALE));
        }
    }
}
