using NewspaperSellerModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewspaperSellerSimulation
{
    public partial class DataView : Form
    {
        public DataView(SimulationSystem system)
        {
            InitializeComponent();
            DataTable dt = new DataTable();
            dt.Columns.Add("Day No.", typeof(int)); // 0
            dt.Columns.Add("Random Digit For Type", typeof(int)); // 1
            dt.Columns.Add("Type", typeof(string)); // 2
            dt.Columns.Add("Random Digit for Demand", typeof(int)); // 3
            dt.Columns.Add("Demand", typeof(int)); // 4
            dt.Columns.Add("Sales Revenue", typeof(decimal)); // 5
            dt.Columns.Add("Loss", typeof(decimal)); // 6
            dt.Columns.Add("Salvage", typeof(decimal)); // 7
            dt.Columns.Add("Profit", typeof(decimal)); // 8

            foreach(SimulationCase simulationCase in system.SimulationTable)
            {
                object[] obj = new object[dt.Columns.Count];
                obj[0] = simulationCase.DayNo;
                obj[1] = simulationCase.RandomNewsDayType;
                obj[2] = simulationCase.NewsDayType.ToString();
                obj[3] = simulationCase.RandomDemand;
                obj[4] = simulationCase.Demand;
                obj[5] = simulationCase.SalesProfit;
                obj[6] = simulationCase.LostProfit;
                obj[7] = simulationCase.ScrapProfit;
                obj[8] = simulationCase.DailyNetProfit;
                dt.Rows.Add(obj);
            }
            

            dataGridView1.DataSource = dt;

            dt = new DataTable();
            dt.Columns.Add("Total Sales Profit", typeof(decimal)); // 0
            dt.Columns.Add("Total Cost", typeof(decimal)); // 1
            dt.Columns.Add("Total Lost Profit", typeof(decimal)); // 2
            dt.Columns.Add("Total Scrap Profit", typeof(decimal)); // 3
            dt.Columns.Add("Total Net Profit", typeof(decimal)); // 4
            dt.Columns.Add("Days With More Demand", typeof(int)); // 5
            dt.Columns.Add("Days With Unsold Papers", typeof(int)); // 6
            object[] data = new object[dt.Columns.Count];
            data[0] = system.PerformanceMeasures.TotalSalesProfit;
            data[1] = system.PerformanceMeasures.TotalCost;
            data[2] = system.PerformanceMeasures.TotalLostProfit;
            data[3] = system.PerformanceMeasures.TotalScrapProfit;
            data[4] = system.PerformanceMeasures.TotalNetProfit;
            data[5] = system.PerformanceMeasures.DaysWithMoreDemand;
            data[6] = system.PerformanceMeasures.DaysWithUnsoldPapers;
            dt.Rows.Add(data);
            dataGridView2.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataView_Load(object sender, EventArgs e)
        {

        }
    }
}
