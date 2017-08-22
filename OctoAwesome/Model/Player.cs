using OctoAwesome.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoAwesome.Model
{
    internal sealed class Player : Item, IHaveInventory
    {
        private Input input;
        private Map map;

        public readonly float MAXSPEED = 4f;

        public Vector2 Velocity { get; set; }

        public float Radius { get; private set; }

        public float Angle { get; private set; }

        public PlayerState State { get; private set; }

        public IHaveInventory InteractionPartner { get; set; }

        public List<InventoryItem> InventoryItems { get; private set; }

        public Player(Input input, Map map)
        {
            this.input = input;
            this.map = map;
            Position = new Vector2(0, 0);
            Velocity = new Vector2(0, 0);
            Radius = .1f;
            InventoryItems = new List<InventoryItem>();
        }

        public void Update(TimeSpan frameTime)
        {
            Velocity = new Vector2(
                    (input.Left ? -1f : 0f) + (input.Right ? 1f : 0f),
                    (input.Up ? -1f : 0f) + (input.Down ? 1f : 0f)
                    );

            if (Velocity.Length() > 0f)
            {
                Velocity = Velocity.Normalized() * MAXSPEED;
                State = PlayerState.Walk;
                Angle = Velocity.Angle();
                //Position += (Velocity * MAXSPEED * (float)frameTime.TotalSeconds);
            }
            else
            {
                State = PlayerState.Idle;
            }

            if(input.Interact && InteractionPartner == null)
            {
                int cellX = (int)Position.X;
                int cellY = (int)Position.Y;

                float direction = ((Angle * 360f) / (float)(2 * Math.PI)) + 225;
                int sector = (int)(direction / 90);

                switch (sector)
                {
                    case 1: cellY -= 1; break;
                    case 2: cellX += 1; break;
                    case 3: cellY += 1; break;
                    case 4: cellX -= 1; break;
                }

                InteractionPartner = map.Items.
                    Where(i => (int)i.Position.X == cellX && (int)i.Position.Y == cellY).
                    OfType<IHaveInventory>().
                    FirstOrDefault();
            }
        }
    }

    public enum PlayerState
    {
        Idle,
        Walk
    }
}
