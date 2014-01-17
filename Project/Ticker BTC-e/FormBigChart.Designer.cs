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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.ChartMain = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ChartMACD = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.ChartMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartMACD)).BeginInit();
            this.SuspendLayout();
            // 
            // ChartMain
            // 
            this.ChartMain.BackColor = System.Drawing.Color.Transparent;
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
            this.ChartMain.Location = new System.Drawing.Point(0, 0);
            this.ChartMain.Margin = new System.Windows.Forms.Padding(0);
            this.ChartMain.Name = "ChartMain";
            this.ChartMain.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series1.ChartArea = "ChartArea1";
            series1.Color = System.Drawing.Color.Lavender;
            series1.Name = "Area";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series1.YValuesPerPoint = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series2.CustomProperties = "PixelPointWidth=6, MinPixelPointWidth=2, PriceDownColor=Red, PointWidth=2, PriceU" +
    "pColor=Green, MaxPixelPointWidth=20";
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
            series4.Color = System.Drawing.Color.OrangeRed;
            series4.Name = "LineMA1";
            series4.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series4.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.Color = System.Drawing.Color.DarkViolet;
            series5.Name = "LineMA2";
            series5.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series5.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series6.BorderColor = System.Drawing.Color.Transparent;
            series6.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Color = System.Drawing.Color.Gray;
            series6.CustomProperties = "IsXAxisQuantitative=True";
            series6.Name = "LineNow";
            series6.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series6.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.ChartMain.Series.Add(series1);
            this.ChartMain.Series.Add(series2);
            this.ChartMain.Series.Add(series3);
            this.ChartMain.Series.Add(series4);
            this.ChartMain.Series.Add(series5);
            this.ChartMain.Series.Add(series6);
            this.ChartMain.Size = new System.Drawing.Size(800, 295);
            this.ChartMain.TabIndex = 4;
            this.ChartMain.Text = "Chart";
            // 
            // ChartMACD
            // 
            this.ChartMACD.BackColor = System.Drawing.Color.Transparent;
            this.ChartMACD.BorderlineColor = System.Drawing.Color.Gainsboro;
            chartArea2.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea2.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Minutes;
            chartArea2.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Minutes;
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.IsStartedFromZero = false;
            chartArea2.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            chartArea2.AxisX.LineColor = System.Drawing.Color.Gainsboro;
            chartArea2.AxisX.LineWidth = 0;
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea2.AxisX.MajorTickMark.Enabled = false;
            chartArea2.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Gainsboro;
            chartArea2.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea2.AxisX2.IsLabelAutoFit = false;
            chartArea2.AxisX2.IsStartedFromZero = false;
            chartArea2.AxisX2.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            chartArea2.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea2.AxisY.IsLabelAutoFit = false;
            chartArea2.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            chartArea2.AxisY.LineColor = System.Drawing.Color.Gainsboro;
            chartArea2.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            chartArea2.AxisY.LineWidth = 0;
            chartArea2.AxisY.MajorGrid.Enabled = false;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea2.AxisY.MajorTickMark.Enabled = false;
            chartArea2.AxisY.MajorTickMark.LineColor = System.Drawing.Color.Gainsboro;
            chartArea2.AxisY2.InterlacedColor = System.Drawing.Color.Transparent;
            chartArea2.AxisY2.IsLabelAutoFit = false;
            chartArea2.AxisY2.IsStartedFromZero = false;
            chartArea2.AxisY2.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            chartArea2.AxisY2.LabelStyle.ForeColor = System.Drawing.Color.Transparent;
            chartArea2.AxisY2.LineColor = System.Drawing.Color.Transparent;
            chartArea2.AxisY2.LineWidth = 0;
            chartArea2.AxisY2.MajorGrid.Enabled = false;
            chartArea2.AxisY2.MajorTickMark.Enabled = false;
            chartArea2.Name = "ChartArea1";
            this.ChartMACD.ChartAreas.Add(chartArea2);
            this.ChartMACD.Location = new System.Drawing.Point(0, 295);
            this.ChartMACD.Margin = new System.Windows.Forms.Padding(0);
            this.ChartMACD.Name = "ChartMACD";
            this.ChartMACD.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series7.ChartArea = "ChartArea1";
            series7.Color = System.Drawing.Color.Gainsboro;
            series7.Name = "Historgam";
            series7.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series7.YValuesPerPoint = 2;
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series8.Color = System.Drawing.Color.Transparent;
            series8.Name = "Line";
            series8.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series8.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series9.Color = System.Drawing.Color.DarkRed;
            series9.Enabled = false;
            series9.Name = "EMA1";
            series9.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series10.ChartArea = "ChartArea1";
            series10.Enabled = false;
            series10.Name = "EMA2";
            series10.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series11.ChartArea = "ChartArea1";
            series11.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series11.Color = System.Drawing.Color.DarkSlateBlue;
            series11.Name = "MACD";
            series11.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series12.ChartArea = "ChartArea1";
            series12.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series12.Color = System.Drawing.Color.Red;
            series12.Name = "Signal";
            series12.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            this.ChartMACD.Series.Add(series7);
            this.ChartMACD.Series.Add(series8);
            this.ChartMACD.Series.Add(series9);
            this.ChartMACD.Series.Add(series10);
            this.ChartMACD.Series.Add(series11);
            this.ChartMACD.Series.Add(series12);
            this.ChartMACD.Size = new System.Drawing.Size(800, 121);
            this.ChartMACD.TabIndex = 4;
            this.ChartMACD.Text = "MACD";
            // 
            // FormBigChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(792, 654);
            this.Controls.Add(this.ChartMACD);
            this.Controls.Add(this.ChartMain);
            this.Name = "FormBigChart";
            this.ShowIcon = false;
            this.Text = "True Terminal Big Chart";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBigChart_FormClosing);
            this.SizeChanged += new System.EventHandler(this.FormBigChart_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.ChartMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartMACD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataVisualization.Charting.Chart ChartMain;
        public System.Windows.Forms.DataVisualization.Charting.Chart ChartMACD;

    }
}