﻿using OctoAwesome.Model.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoAwesome.Model
{
    public class Chunk
    {
        public const int CHUNKSIZE_X = 100;
        public const int CHUNKSIZE_Y = 100;
        public const int CHUNKSIZE_Z = 100;

        public IBlock[,,] Blocks { get; set; }

        public Chunk()
        {
            Blocks = new IBlock[CHUNKSIZE_X, CHUNKSIZE_Y, CHUNKSIZE_Z];

            for (int z = 0; z < CHUNKSIZE_Z; z++)
            {
                for (int y = 0; y < CHUNKSIZE_Y; y++)
                {
                    for (int x = 0; x < CHUNKSIZE_X; x++)
                    {
                        //...
                        if (y < 20)
                        {
                            Blocks[x,y,z] = new GrassBlock();
                        }
                    }
                }
            }
        }
    }
}
