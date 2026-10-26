using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models
{
    internal class StatusEffectObj
    {
        public bool isFrendly { get; private set; } //that like - or + to parametrs
        public bool isStacking { get; private set; }
       
        public StatusEffect statusEffect { get; private set; }

        public int turnsRamains { get; private set; }

        public int value { get; private set; }

        public StatusEffectObj(bool isFrendly,bool IsStacking, StatusEffect statusEffect, int turnsRamains, int value) 
        {
            this.isFrendly = isFrendly;
            this.isStacking = isStacking;
            this.statusEffect = statusEffect;
            this.turnsRamains = turnsRamains;
            this.value = value;
        }

        public bool DecreamentTunsRamain() 
        {
            turnsRamains--;
            return turnsRamains > 0;
        }

        public void AddTurns(int increament) 
        {
            if (increament <= 0) return;

            turnsRamains += increament;
        }


    }
}
