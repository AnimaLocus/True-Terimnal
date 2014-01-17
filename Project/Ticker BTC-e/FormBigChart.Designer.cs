namespace Ticker_BTC_e
{
    partial class FormBigChart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.ChartMain = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.ChartMain)).BeginInit();
            this.SuspendLayout();
            // 
            // ChartMain
            // 
            this.ChartMain.BorderlineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Minutes;
            chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Minutes;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.IsStartedFromZero = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            chartArea1.AxisX.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisX.LineWidth = 0;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.AxisX2.IsLabelAutoFit = false;
            chartArea1.AxisX2.IsStartedFromZero = false;
            chartArea1.AxisX2.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            chartArea1.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            chartArea1.AxisY.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            chartArea1.AxisY.LineWidth = 0;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisY.MajorTickMark.Enabled = false;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisY2.IsLabelAutoFit = false;
            chartArea1.AxisY2.IsStartedFromZero = false;
            chartArea1.AxisY2.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            chartArea1.AxisY2.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisY2.LineWidth = 0;
            chartArea1.AxisY2.MajorGrid.Enabled = false;
            chartArea1.AxisY2.MajorTickMark.Enabled = false;
            chartArea1.Name = "ChartArea1";
            this.ChartMain.ChartAreas.Add(chartArea1);
            this.ChartMain.Location = new System.Drawing.Point(-1, -2);
            this.ChartMain.Name = "ChartMain";
            this.ChartMain.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series1.ChartArea = "ChartArea1";
            series1.Color = System.Drawing.Color.Lavender;
            series1.Name = "Area";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series1.YValuesPerPoint = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series2.CustomProperties = "PixelPointWidth=6, PriceDownColor=Red, PointWidth=2, PriceUpColor=Green";
            series2.Name = "Candlestick";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series2.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series2.YValuesPerPoint = 4;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Enabled = false;
            series3.Name = "Line";
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series3.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Enabled = false;
            series4.Name = "LineMA1";
            series4.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.Enabled = false;
            series5.Name = "LineMA2";
            series5.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series6.Enabled = false;
            series6.Name = "LineMA3";
            series6.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series7.Enabled = false;
            series7.Name = "LineMA4";
            series7.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series8.BorderColor = System.Drawing.Color.Transparent;
            series8.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            series8.BorderWidth = 2;
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series8.Color = System.Drawing.Color.Gray;
            series8.CustomProperties = "IsXAxisQuantitative=True";
            series8.Name = "LineNow";
            series8.XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series8.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series8.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.ChartMain.Series.Add(series1);
            this.ChartMain.Series.Add(series2);
            this.ChartMain.Series.Add(series3);
            this.ChartMain.Series.Add(series4);
            this.ChartMain.Series.Add(series5);
            this.ChartMain.Series.Add(series6);
            this.ChartMain.Series.Add(series7);
            this.ChartMain.Series.Add(series8);
            this.ChartMain.Size = new System.Drawing.Size(804, 482);
            this.ChartMain.TabIndex = 4;
            this.ChartMain.Text = "ChartMain";
            // 
            // FormBigChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(803, 477);
            this.Controls.Add(this.ChartMain);
            this.Name = "FormBigChart";
            this.ShowIcon = false;
            this.Text = "True Terminal Big Chart";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBigChart_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.ChartMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataVisualization.Charting.Chart ChartMain;

    }
}