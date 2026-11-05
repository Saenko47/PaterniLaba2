using PaterniLab2.Interfaces.Attack;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests.Units;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Tools
{
    internal class FindUnit:IFindUnit
    {

       
            private List<BaseUnit> FindByCross(ReqiestOnFindingUnit req)
            {
                var foundUnits = new List<BaseUnit>();
                int startX = req.unitThatLooking.x;
                int startY = req.unitThatLooking.y;

           
            
            foreach (var unit in req.allUnits)
                {
                    if (unit == req.unitThatLooking) continue;

                    bool isComrade = (unit.race == req.unitThatLooking.race);

                    if (req.isLookingForComrades && !isComrade) continue;
                    if (!req.isLookingForComrades && isComrade) continue;

                    int distance = Math.Abs(unit.x - startX) + Math.Abs(unit.y - startY);

                    if (distance <= req.rangeOfFindings)
                    {
                        foundUnits.Add(unit);
                    }
                }

                return foundUnits;
            }

            private List<BaseUnit> FindInCircle(ReqiestOnFindingUnit req)
            {
                var foundUnits = new List<BaseUnit>();
                int startX = req.unitThatLooking.x;
                int startY = req.unitThatLooking.y;

                foreach (var unit in req.allUnits)
                {
                    if (unit == req.unitThatLooking) continue;

                    bool isComrade = (unit.race == req.unitThatLooking.race);
                    if (req.isLookingForComrades && !isComrade) continue;
                    if (!req.isLookingForComrades && isComrade) continue;

                    int dx = unit.x - startX;
                    int dy = unit.y - startY;
                    int distanceSquared = (dx * dx) + (dy * dy);

                    if (distanceSquared <= req.rangeOfFindings * req.rangeOfFindings)
                    {
                        foundUnits.Add(unit);
                    }
                }

                return foundUnits;
            }

            private List<BaseUnit> ChooseWhatStrategy(ReqiestOnFindingUnit req)
            {
                switch (req.strategysToFind)
                {
                    case StrategysToFind.Circle:
                        return FindInCircle(req);
                    case StrategysToFind.Cross:
                        return FindByCross(req);
                    default:
                        return new List<BaseUnit>();
                }
            }

            public List<BaseUnit> Find(ReqiestOnFindingUnit req)
            {
                if (req == null || req.unitThatLooking == null || req.allUnits == null)
                    return new List<BaseUnit>();

                return ChooseWhatStrategy(req);
            }
        }
    
}
