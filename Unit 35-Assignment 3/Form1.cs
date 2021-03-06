﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace Unit_35_Assignment_3
{
    public partial class Form1 : Form
    {
        class row
        {
            public double time;
            public double Velocity;
            public double accleration;
            public double altitude;

        }

        List<row> table = new List<row>();
        public Form1()
        {
            InitializeComponent();
            chart1.Series.Clear();
        }

        // Calculates velocity
        private void calculateVelocity()
        {
            for (int i = 1; i < table.Count; i++)
            {
                double dh = table[i].altitude - table[i - 1].altitude;
                double dt = table[i].time - table[i - 1].time;
                table[i].Velocity = dh / dt;
            }

        }
        //Calculates acceleration
        private void calculateAcceleration()
        {
            for (int i = 1; i < table.Count; i++)
            {
                double dv = table[i].Velocity - table[i - 1].Velocity;
                double dt = table[i].time - table[i - 1].time;
                table[i].accleration = dv / dt;
            }
        }



        //This allows the user to open CSV files
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "csv files|*csv";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                    {
                        string line = sr.ReadLine();
                        while (!sr.EndOfStream)
                        {
                            table.Add(new row());
                            string[] r = sr.ReadLine().Split(',');
                            table.Last().time = double.Parse(r[0]);
                            table.Last().altitude = double.Parse(r[1]);
                        }


                        //This prevents any user faults
                    }
                    calculateVelocity();
                    calculateAcceleration();
                }
                catch (IOException)
                {
                    MessageBox.Show(openFileDialog1.FileName + "Failed to open.");
                }
                catch (FormatException)
                {
                    MessageBox.Show(openFileDialog1.FileName + "Incorrect format.");
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show(openFileDialog1.FileName + "Incorrect Format.");
                }
                catch (DivideByZeroException)
                {
                    MessageBox.Show(openFileDialog1.FileName + "Rows have the same value.");
                }


            }
        }
        //Draws altitude graph from CSV file
        private void altitudeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
            Series series = new Series
            {
                Name = "altitude",
                Color = Color.Blue,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Spline,
                BorderWidth = 2
            };
            chart1.Series.Add(series);
            foreach (row r in table.Skip(1))
            {
                series.Points.AddXY(r.time, r.altitude);
            }
            chart1.ChartAreas[0].AxisX.Title = "time /s";
            chart1.ChartAreas[0].AxisY.Title = "Altitude /m";
            chart1.ChartAreas[0].RecalculateAxesScale();





        }
        //Saves CSV files
        private void saveCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "csv files|*.csv";
            DialogResult results = saveFileDialog1.ShowDialog();
            if (results == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                    {
                        sw.WriteLine("time /s, altitude /m, accelaration /ms, velocity /v");
                        foreach (row r in table) ;
                    }
                }  
                catch
                {
                    MessageBox.Show(saveFileDialog1.FileName + "Failed to save");
                        
                }
            }







        }
        //Draws velocity graph from CSV file.
        private void velocityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                chart1.Series.Clear();
                chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
                Series series = new Series
                {
                    Name = "velocity",
                    Color = Color.Blue,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Spline,
                    BorderWidth = 2
                };
                chart1.Series.Add(series);
                foreach (row r in table.Skip(1))
                {
                    series.Points.AddXY(r.time, r.Velocity);
                }
                chart1.ChartAreas[0].AxisX.Title = "time /s";
                chart1.ChartAreas[0].AxisY.Title = "Velocity /m/s";
                chart1.ChartAreas[0].RecalculateAxesScale();





            }

        }
        //Draws acceleration graph from CSV file
        private void accelerationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                chart1.Series.Clear();
                chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
                Series series = new Series
                {
                    Name = "acceleration",
                    Color = Color.Blue,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Spline,
                    BorderWidth = 2
                };
                chart1.Series.Add(series);
                foreach (row r in table.Skip(1))
                {
                    series.Points.AddXY(r.time, r.accleration);
                }
                chart1.ChartAreas[0].AxisX.Title = "time /s";
                chart1.ChartAreas[0].AxisY.Title = "Acceleration /m/s";
                chart1.ChartAreas[0].RecalculateAxesScale();





            }

        }
    }
}

    

