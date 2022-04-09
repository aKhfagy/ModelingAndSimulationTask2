using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperSellerModels
{
    public class SimulationSystem
    {
        public SimulationSystem()
        {
            DayTypeDistributions = new List<DayTypeDistribution>();
            DemandDistributions = new List<DemandDistribution>();
            SimulationTable = new List<SimulationCase>();
            PerformanceMeasures = new PerformanceMeasures();
        }
        ///////////// INPUTS /////////////
        public int NumOfNewspapers { get; set; }
        public int NumOfRecords { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal ScrapPrice { get; set; }
        public decimal UnitProfit { get; set; }
        public List<DayTypeDistribution> DayTypeDistributions { get; set; }
        public List<DemandDistribution> DemandDistributions { get; set; }
        public static string PATH = "";

        ///////////// OUTPUTS /////////////
        public List<SimulationCase> SimulationTable { get; set; }
        public PerformanceMeasures PerformanceMeasures { get; set; }
        public void ReadInput()
        {
            string[] lines = File.ReadAllLines(PATH);
            this.NumOfNewspapers = int.Parse(lines[1]); // 2
            this.NumOfRecords = int.Parse(lines[4]); // 5
            this.PurchasePrice = decimal.Parse(lines[7]); // 8
            this.ScrapPrice = decimal.Parse(lines[10]); // 11
            this.SellingPrice = decimal.Parse(lines[13]); // 14

            string[] dists = lines[16].Split(','); // 17
            int id = 0;
            DayTypeDistribution prev = null;
            foreach(string dist in dists)
            {
                DayTypeDistribution distribution = new DayTypeDistribution();
                distribution.Probability = decimal.Parse(dist);
                if (id == 0)
                {
                    distribution.DayType = Enums.DayType.Good;
                    distribution.CummProbability = distribution.Probability;
                    distribution.MinRange = 1;
                    distribution.MaxRange = (int)(distribution.CummProbability * 100);
                }
                else
                {
                    distribution.DayType = (id == 1) ? Enums.DayType.Fair : Enums.DayType.Poor;
                    distribution.CummProbability = prev.CummProbability + distribution.Probability;
                    distribution.MinRange = prev.MaxRange + 1;
                    distribution.MaxRange = (int)(distribution.CummProbability * 100);
                }
                prev = distribution;
                this.DayTypeDistributions.Add(distribution);
                id++;
            }

            int start_demand = 19;

            DemandDistribution prev_demand = null;
            while(start_demand < lines.Length)
            {
                DemandDistribution demand = new DemandDistribution();
                dists = lines[start_demand].Split(',');
                
                id = -1;
                foreach(string dist in dists)
                {
                    if(id > -1)
                    {
                       
                        DayTypeDistribution distribution = new DayTypeDistribution();
                        distribution.Probability = decimal.Parse(dist);
                        if(start_demand == 19)
                        {

                            distribution.DayType = (id == 1) ? Enums.DayType.Fair : ((id == 2) ? Enums.DayType.Poor : Enums.DayType.Good);
                            distribution.CummProbability = distribution.Probability;
                            distribution.MinRange = 1;
                            distribution.MaxRange = (int)(distribution.CummProbability * 100);
                        }
                        else
                        {
                            prev = prev_demand.DayTypeDistributions[id];
                            distribution.DayType = (id == 1) ? Enums.DayType.Fair : ((id == 2) ? Enums.DayType.Poor : Enums.DayType.Good);
                            distribution.CummProbability = prev.CummProbability + distribution.Probability;
                            distribution.MinRange = prev.MaxRange + 1;
                            distribution.MaxRange = (int)(distribution.CummProbability * 100);
                        }
                        demand.DayTypeDistributions.Add(distribution);
                        
                    }
                    else
                    {
                        demand.Demand = int.Parse(dist);
                    }
                    id++;
                }
                prev_demand = demand;
                this.DemandDistributions.Add(demand);
                start_demand++;
            }
        }
        public void CalculatePerformanceMeasures()
        {

        }

        public void Simulate()
        {
            // profit = sales_profit - cost - loss + salvage
            Random random = new Random();
        }
    }
}
