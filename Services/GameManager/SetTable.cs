using PaterniLab2.Interfaces;
using PaterniLab2.Interfaces.GameManager;
using PaterniLab2.Interfaces.Unit;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Models.Units;
using PaterniLab2.Requests.Table;
using PaterniLab2.Services.AttackAndDamageProccesing;
using PaterniLab2.Services.Behaviour;
using PaterniLab2.Services.MoveHandlers;
using PaterniLab2.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Services.GameManager
{
    internal class SetTable:ISetTable
    {

        private readonly ITable _table;
        private readonly IBehaviour _aiBehavior;
        private readonly IBehaviour _playerBehavior;
        
        public SetTable(ITable table, IBehaviour aiBehaviour, IBehaviour playerBehavior) 
        {
            _table = table;
            _aiBehavior = aiBehaviour;
            _playerBehavior = playerBehavior;
          
        }

        private void ShowUnitsBeforeFigth(List<BaseUnit> units)
        {
            foreach (var unit in units)
            {
                Console.WriteLine(unit);
            }
        }

        private void SetBehaviorForTeam(List<BaseUnit> units, bool isAi)
        {
        
            IBehaviour beha = isAi ? _aiBehavior : _playerBehavior;

            foreach (var unit in units) unit.SetBehaviour(beha);
        }
       


        public List<Army> Set(CreateGameRequest req) 
        {
            req = new Requests.Table.CreateGameRequest
            {
                reqs = new PlaceOnBoardRequest[]
                    {
                        
                    new PlaceOnBoardRequest
                    {
                    race = Race.Human,
                    raceAmountFly = 0,
                    raceAmountRide = 0,
                    raceMeleeAmount = 5,
                    raceRangeAmount = 0,
                    isAi = false,
                    isHandlyPlacement = false,
                    
                    },


                    new PlaceOnBoardRequest
                    {
                    race = Race.Orc,
                    raceAmountFly = 0,
                    raceAmountRide = 0,
                    raceMeleeAmount = 3,
                    raceRangeAmount = 2,
                    isAi = true,
                    isHandlyPlacement = false,

                    }

                    }
            };

            var res = _table.FirstPlacementOfUnits(req);

            foreach (var army in res.placedArmies) SetBehaviorForTeam(army._units, army._isAi);

            foreach (var army in res.placedArmies) ShowUnitsBeforeFigth(army._units);


            return res.placedArmies;
        }
    }
}
