using HarmonyLib;
using I2.Loc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNEScoreboard
{
    public class VictoryStatistics
    {
        public int MissionDuration;

        public string Difficulty;

        public int CultureRating;

        public int ProsperityRating;

        public int KingdomRating;

        public int CityFunds;

        public int Population;

        public int HouseQuantity;

        public int HouseLevel;

        public VictoryStatistics() { }

        public VictoryStatistics(MapGameplay mapGameplay) 
        {
            Population = Traverse.Create(mapGameplay).Field("_populationManager").GetValue<PopulationManager>().TotalPopulation;
            CityFunds = mapGameplay.Treasury;
            KingdomRating = mapGameplay.KingdomRating;
            CultureRating = mapGameplay.GetCultureRating();
            ProsperityRating = mapGameplay.MonthlyProsperityRating;
            var timeManager = Traverse.Create(mapGameplay).Field("_timeManager").GetValue<TimeManager>();
            MissionDuration = timeManager.YearsElapsed * 12 + timeManager.CurrentMonth + 1;
            var level = Traverse.Create(mapGameplay).Field("_level").GetValue<LevelMap>();
            Difficulty = LocalizationManager.GetTranslation($"Menu/Options/#Difficulty_{level.LowestDifficulty}");
            HouseLevel = level.WinCondition.HouseLevel;
            HouseQuantity = level.GetStoredBuildings<House>().Count((House h) => (int)h.CurrentHouseLevel >= level.WinCondition.HouseLevel);
        }
    }
}
