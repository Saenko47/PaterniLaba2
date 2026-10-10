using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using PaterniLab2.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class GameManager
    {
        private readonly Table _table;
        private readonly IMove _move;
        private readonly IFindToAttack _attack;
        private readonly IRender _render;

        private int raceACount;
        private int raceBCount;

        public GameManager(Table table, IMove move, IFindToAttack attack, IRender render)
        {
            _table = table;
            _move = move;
            _attack = attack;
            _render = render;
        }
        private void ShowUnitsBeforeFigth(List<BaseUnit> units) 
        {
            foreach (var unit in units) {
                Console.WriteLine(unit);
            }
        }
        private void InitiaizeTabe() 
        { 
            var res = _table.FirstPlacementOfUnits(new Requests.Table.CreateGameRequest
            {
                raceA = new Requests.Table.PlaceOnBoardRequest
                {
                    race = Tools.Enums.Race.Human,
                    raceAmountFly = 0,
                    raceAmountRide = 0,
                    raceMeleeAmount = 2,
                    raceRangeAmount = 0
                },
                raceB = new Requests.Table.PlaceOnBoardRequest
                {
                    race = Tools.Enums.Race.Orc,
                    raceAmountFly = 0,
                    raceAmountRide = 0,
                    raceMeleeAmount = 4,
                    raceRangeAmount = 0
                }
            });
            this.raceACount = res.raceA.Count();
            this.raceBCount = res.raceB.Count();

            ShowUnitsBeforeFigth(res.raceA);
            ShowUnitsBeforeFigth(res.raceB);

        }

        public void StartGame()
        {
            InitiaizeTabe();
            while (raceACount > 0 && raceBCount > 0)
            {
              _render.RenderTable(_table);

                Console.ReadKey();
            }
            EndGame();
        }

        private void EndGame()
        {
            // Implement game end logic here
            Console.WriteLine("Game ended!");
        }

        private void ProccesCurrentMove(int x, int y) 
        {
            if (x < 0 || x > _table.xLen) return;
            if (y < 0 || y > _table.yLen) return;
        }
    }
}
