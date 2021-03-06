﻿using Microsoft.Xna.Framework;
using OctoAwesome.Components;
using OctoAwesome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OctoAwesome.Model
{
    public sealed class World
    {
        private Dictionary<CellType, CellTypeDefinition> cellTypes;

        public Vector2 PlaygroundSize
        {
            get
            {
                return new Vector2(Map.Columns,Map.Rows);
            }
        }

        public Map Map { get; private set; }
        public Chunk Chunk { get; private set; }
        public Player Player { get; private set; }

        public World(IInputSet input)
        {
            Map = Map.Load(@"Assets\test10.map");
            Chunk = new Chunk();
            Player = new Player(input, Map);
            
            cellTypes = new Dictionary<CellType, CellTypeDefinition>();
            cellTypes.Add(CellType.Grass, new CellTypeDefinition() { CanGoto = true, VelocityFactor = .8f });
            cellTypes.Add(CellType.Sand, new CellTypeDefinition() { CanGoto = true, VelocityFactor = 1f });
            cellTypes.Add(CellType.Water, new CellTypeDefinition() { CanGoto = false, VelocityFactor = 0f });

            // Map cache generieren
            Map.CellCache = new CellCache[Map.Columns, Map.Rows];
            for (int x = 0; x < Map.Columns; x++)
            {
                for (int y = 0; y < Map.Rows; y++)
                {
                    CellType celltype = Map.GetCell(x, y);

                    bool haveItems = Map.Items.Any(i =>
                    (int)i.Position.X == x &&
                    (int)i.Position.Y == y);

                    Map.CellCache[x, y] = new CellCache()
                    {
                        CellType = celltype,
                        CanGoto = cellTypes[celltype].CanGoto && !haveItems,
                        VelocityFactor = cellTypes[celltype].VelocityFactor
                    };
                }
            }
            Map.Items.Add(Player);
        }
        
        public void Update(GameTime frameTime)
        {
            Player.Update(frameTime);

            int cellX = (int)Player.Position.X;
            int cellY = (int)Player.Position.Y;
            CellCache cell = Map.CellCache[cellX, cellY];

            Vector2 velocity = Player.Velocity;
            velocity *= cell.VelocityFactor;
            Vector2 newPosition = Player.Position +
                (velocity * (float)frameTime.ElapsedGameTime.TotalSeconds);

            if (velocity.X < 0)
            {
                float posLeft = newPosition.X - Player.Radius;
                cellX = (int)posLeft;
                cellY = (int)Player.Position.Y;
                cell = Map.CellCache[cellX, cellY];

                if (posLeft < 0)
                {
                    newPosition = new Vector2(cellX + Player.Radius, newPosition.Y);
                }

                if (cellX < 0 || !cell.CanGoto)
                {
                    newPosition = new Vector2((cellX + 1) + Player.Radius, newPosition.Y);
                }
            }

            if (velocity.X > 0)
            {
                float posRight = newPosition.X + Player.Radius;
                cellX = (int)posRight;
                cellY = (int)Player.Position.Y;
                cell = Map.CellCache[cellX, cellY];
                 
                if (cellX >= Map.Columns || !cell.CanGoto)
                {
                    newPosition = new Vector2(cellX - Player.Radius, newPosition.Y);
                }
            }

            if (velocity.Y < 0)
            {
                float posTop = newPosition.Y - Player.Radius;
                cellX = (int)Player.Position.X;
                cellY = (int)posTop;
                cell = Map.CellCache[cellX, cellY];

                if (posTop < 0)
                {
                    newPosition = new Vector2(newPosition.X, cellY + Player.Radius);
                }

                if (cellY < 0 || !cell.CanGoto)
                {
                    newPosition = new Vector2(newPosition.X, (cellY + 1) + Player.Radius);
                }
            }

            if (velocity.Y > 0)
            {
                float posBottom = newPosition.Y + Player.Radius;
                cellX = (int)Player.Position.X;
                cellY = (int)posBottom;
                cell = Map.CellCache[cellX, cellY];

                if (cellY >= Map.Rows || !cell.CanGoto)
                {
                    newPosition = new Vector2(newPosition.X, cellY - Player.Radius);
                }
            }
            Player.Position = newPosition;
        }
    }
}
