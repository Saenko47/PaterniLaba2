using PaterniLab2.Interfaces;
using PaterniLab2.Models.Units;
using PaterniLab2.Requests.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Services.GameManager
{
    internal class NewGameManager: IGameManager
    {
     
        private readonly ISetTable _setTable;
        private readonly ICombatGenerator _combatGenerator;
       

        public NewGameManager(ISetTable setTable, ICombatGenerator combatGenerator)
        {
           
            _setTable = setTable;
            _combatGenerator = combatGenerator;


        }


        public void StartGame(CreateGameRequest req)
        {
            List<Army> armies = _setTable.Set(req);

            _combatGenerator.InitializeArmies(armies);

            _combatGenerator.GenerateCombat();

        }
    }
}
