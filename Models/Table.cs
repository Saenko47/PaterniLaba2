using PaterniLab2.Interfaces;
using PaterniLab2.Requests.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Models
{
    internal class Table
    {

       public Cell[,] _board { get; set; }
        private readonly IPlaceRaceOnBoard _placeRaceOnBoard;

        public int xLen;
        public int yLen;
        public Table(int xLen, int yLen, IPlaceRaceOnBoard placeRaceOnBoard)
        {
            _placeRaceOnBoard = placeRaceOnBoard;
            _board = new Cell[xLen, yLen];

            this.xLen = xLen;
            this.yLen = yLen;

           InitializeBoard(xLen, yLen);

        }
        private void InitializeBoard(int xLen, int yLen)
        {
            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    _board[i, j] = new Cell(i, j);
                }
            }
        }

        


        public UnitOnTheBoardByRace FirstPlacementOfUnits(CreateGameRequest req) 
        { 
         throw new NotImplementedException();
        }
    }
}
