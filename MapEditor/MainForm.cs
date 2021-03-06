﻿using OctoAwesome.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MapEditor
{
    public partial class MainForm : Form
    {
        private Map map;
        private int cellSize = 20;
        private ToolType drawMode = ToolType.CellTypeGrass;

        private bool mouseActive = false;
        private Point mousePosition = new Point();
        private bool mouseDraw = false;

        public MainForm()
        {
            InitializeComponent();
            timer.Enabled = true;
        }

        private void renderControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.CornflowerBlue);

            if (map == null)
                return;

            SolidBrush sandBrush = new SolidBrush(Color.SandyBrown);
            SolidBrush grasBrush = new SolidBrush(Color.DarkGreen);
            SolidBrush treeBrush = new SolidBrush(Color.Brown);
            SolidBrush waterBrush = new SolidBrush(Color.Blue);
            SolidBrush selectionBrush = new SolidBrush(Color.FromArgb(100, Color.White));

            // Zellen malen
            for (int x = 0; x < map.Columns; x++)
            {
                for (int y = 0; y < map.Rows; y++)
                {
                    SolidBrush brush = null;
                    switch (map.GetCell(x, y))
                    {
                        case CellType.Grass:
                            brush = grasBrush;
                            break;
                        case CellType.Sand:
                            brush = sandBrush;
                            break;
                        case CellType.Water:
                            brush = waterBrush;
                            break;
                    }

                    if (brush == null)
                        continue;

                    e.Graphics.FillRectangle
                        (brush, new Rectangle(x * cellSize, y * cellSize, cellSize, cellSize));
                }
            }

            foreach (var item in map.Items)
            {
                if (item is TreeItem)
                {
                    int x = (int)item.Position.X;
                    int y = (int)item.Position.Y;
                    e.Graphics.FillRectangle
                        (treeBrush, new Rectangle(x * cellSize, y * cellSize, cellSize, cellSize));
                }
            }

            using (Pen pen = new Pen(Color.FromArgb(75, Color.White)))
            {
                for (int x = 1; x < map.Columns + 1; x++)
                {
                    e.Graphics.DrawLine(pen, new Point(x * cellSize, 0),
                        new Point(x * cellSize, map.Rows * cellSize));
                }

                for (int y = 1; y < map.Rows + 1; y++)
                {
                    e.Graphics.DrawLine(pen, new Point(0, y * cellSize),
                        new Point(map.Columns * cellSize, y * cellSize));
                }
            }

            if (mouseActive)
            {
                e.Graphics.FillRectangle
                    (selectionBrush, new Rectangle
                    (mousePosition.X * cellSize, mousePosition.Y * cellSize, cellSize, cellSize));
            }

            sandBrush.Dispose();
            grasBrush.Dispose();
            waterBrush.Dispose();
            treeBrush.Dispose();
            selectionBrush.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            renderControl.Invalidate();
            cellPositionLabel.Text = (mouseActive ? mousePosition.X + "/" + mousePosition.Y : string.Empty);
            saveMenu.Enabled = map != null;
        }

        private void smallMapMenu_Click(object sender, EventArgs e)
        {
            map = Map.Generate(20, 20, CellType.Grass);
        }

        private void mediumMapMenu_Click(object sender, EventArgs e)
        {
            map = Map.Generate(40, 40, CellType.Grass);
        }

        private void renderControl_MouseEnter(object sender, EventArgs e)
        {
            mouseActive = true;
        }

        private void renderControl_MouseLeave(object sender, EventArgs e)
        {
            mouseActive = false;
        }

        private void drawCell()
        {
            if (map == null || !mouseDraw || !mouseActive)
                return;

            if (mousePosition.X < 0 || mousePosition.X >= map.Columns ||
                mousePosition.Y < 0 || mousePosition.Y >= map.Rows)
                return;

            switch (drawMode)
            {
                case ToolType.CellTypeGrass:
                    map.SetCell(mousePosition.X, mousePosition.Y, CellType.Grass);
                    break;

                case ToolType.CellTypeSand:
                    map.SetCell(mousePosition.X, mousePosition.Y, CellType.Sand);
                    break;

                case ToolType.CellTypeWater:
                    map.SetCell(mousePosition.X, mousePosition.Y, CellType.Water);
                    map.Items.RemoveAll(i =>
                    (int)i.Position.X == mousePosition.X &&
                    (int)i.Position.Y == mousePosition.Y);
                    break;

                case ToolType.ItemDelete:
                    map.Items.RemoveAll(i =>
                    (int)i.Position.X == mousePosition.X &&
                    (int)i.Position.Y == mousePosition.Y);
                    break;

                case ToolType.ItemTree:
                    if (map.GetCell(mousePosition.X, mousePosition.Y) == CellType.Water)
                        break;

                    if (map.Items.Any(i =>
                     (int)i.Position.X == mousePosition.X &&
                     (int)i.Position.Y == mousePosition.Y))
                        break;

                    map.Items.Add(new TreeItem()
                    {
                        Position = new OctoAwesome.Vector2(
                            mousePosition.X + .5f,
                            mousePosition.Y + .5f)
                    });
                    break;
            }
        }

        private void renderControl_MouseMove(object sender, MouseEventArgs e)
        {
            mousePosition = new Point((int)(e.X / cellSize), (int)(e.Y / cellSize));
            drawCell();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
        }

        private void sandButton_Click(object sender, EventArgs e)
        {
            drawMode = ToolType.CellTypeSand;
            buttonClick(myButtons.Sand);
        }

        private void grassButton_Click(object sender, EventArgs e)
        {
            drawMode = ToolType.CellTypeGrass;
            buttonClick(myButtons.Grass);
        }

        private void waterButton_Click(object sender, EventArgs e)
        {
            drawMode = ToolType.CellTypeWater;
            buttonClick(myButtons.Water);
        }

        private void treeButton_Click(object sender, EventArgs e)
        {
            drawMode = ToolType.ItemTree;
            buttonClick(myButtons.Tree);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            drawMode = ToolType.ItemDelete;
            buttonClick(myButtons.Delete);
        }

        private void buttonClick(myButtons name)
        {
            grassButton.Checked = false;
            sandButton.Checked = false;
            waterButton.Checked = false;
            treeButton.Checked = false;
            deleteButton.Checked = false;

            switch (name)
            {
                case myButtons.Sand: sandButton.Checked = true; break;
                case myButtons.Grass: grassButton.Checked = true; break;
                case myButtons.Water: waterButton.Checked = true; break;
                case myButtons.Tree: treeButton.Checked = true; break;
                case myButtons.Delete: deleteButton.Checked = true; break;
            }
        }

        private void renderControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDraw = true;
                drawCell();
            }
        }

        private void renderControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDraw = false;
            }
        }

        private void saveMenu_Click(object sender, EventArgs e)
        {
            if (map == null)
                return;

            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                if (File.Exists(saveFileDialog.FileName))
                {
                    File.Delete(saveFileDialog.FileName);
                }
                Map.Save(saveFileDialog.FileName, map);
            }
        }

        private void loadMenu_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog(this)== DialogResult.OK)
            {
                map = Map.Load(openFileDialog.FileName);
            }
        }

        private enum myButtons
        {
            Sand,
            Grass,
            Water,
            Tree,
            Delete
        }
    }
}
