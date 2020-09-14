using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using AnalysisSt.Chart.Parameter;

namespace AnalysisSt.Chart.Uc
{
    public partial class ucChartAreaSetting : UserControl
    {
        public ucChartAreaSetting()
        {
            InitializeComponent();
        }
        // 전역변수
        private ChartArea _oChartArea;
        private ParamChartAttribute _oParamChartAtt = new ParamChartAttribute();
        private Boolean _ChangeChartArea = false;

        public ChartArea oChartArea
        {

            get { return _oChartArea; }
            set { _oChartArea = value;
            if (_oChartArea != null)
            { ViewChartAreaAttribute(); }
            }
        }

        #region Control Event
        private void ucChartAreaSetting_Load(object sender, EventArgs e)
        {
            PropetyGridInit();
            _oParamChartAtt.onChangedCAreaProp += new ParamChartAttribute.ChangedCAreaProp(DoChangedCAreaProp);
        }
        #endregion
        
        #region Event
        public void DoChangedCAreaProp(String CategoryName, ParamChartAttribute.ParamIndex p)
        {
            if (_oChartArea == null || _ChangeChartArea == true)
            { return; }

            switch (p)
            {
                case ParamChartAttribute.ParamIndex.Name:
                    break;
                case ParamChartAttribute.ParamIndex.ChartAreaPosition:
                    if (_oChartArea.Position.X != _oParamChartAtt.ChartAreaPosition.X)
                    { _oChartArea.Position.X = _oParamChartAtt.ChartAreaPosition.X; }
                    if (_oChartArea.Position.Y != _oParamChartAtt.ChartAreaPosition.Y)
                    { _oChartArea.Position.Y = _oParamChartAtt.ChartAreaPosition.Y; }
                    if (_oChartArea.Position.Width != _oParamChartAtt.ChartAreaPosition.Width)
                    { _oChartArea.Position.Width = _oParamChartAtt.ChartAreaPosition.Width; }
                    if (_oChartArea.Position.Height != _oParamChartAtt.ChartAreaPosition.Height)
                    { _oChartArea.Position.Height = _oParamChartAtt.ChartAreaPosition.Height; }
                   
                    break;
                case ParamChartAttribute.ParamIndex.PlottingAreaPosition:
                       if (_oChartArea.InnerPlotPosition.X != _oParamChartAtt.PlottingAreaPosition.X)
                       { _oChartArea.InnerPlotPosition.X = _oParamChartAtt.PlottingAreaPosition.X; }
                       if (_oChartArea.InnerPlotPosition.Y != _oParamChartAtt.PlottingAreaPosition.Y)
                       { _oChartArea.InnerPlotPosition.Y = _oParamChartAtt.PlottingAreaPosition.Y; }
                       if (_oChartArea.InnerPlotPosition.Width != _oParamChartAtt.PlottingAreaPosition.Width)
                       { _oChartArea.InnerPlotPosition.Width = _oParamChartAtt.PlottingAreaPosition.Width; }
                       if (_oChartArea.InnerPlotPosition.Height != _oParamChartAtt.PlottingAreaPosition.Height)
                       { _oChartArea.InnerPlotPosition.Height = _oParamChartAtt.PlottingAreaPosition.Height; }
                    break;
                case ParamChartAttribute.ParamIndex.Visible:
                    _oChartArea.Visible = _oParamChartAtt.Visible;
                    break;
                case ParamChartAttribute.ParamIndex.Enable:
             
                    break;
                default:
                    break;
            }
        }
        #endregion
        
        private void PropetyGridInit()
        {
            ChartAreaProp.SelectedObject = _oParamChartAtt;
        }

        private void ViewChartAreaAttribute()
        {
            _ChangeChartArea = true;

            _oParamChartAtt.Name = _oChartArea.Name.ToString().Trim();
            ParamChartAttribute.stChartAreaPosition stChartAp = new  ParamChartAttribute.stChartAreaPosition();
            ParamChartAttribute.stPlottingAreaPosition stPlottAp = new ParamChartAttribute.stPlottingAreaPosition();

            stChartAp.X = _oChartArea.Position.X;
            stChartAp.Y = _oChartArea.Position.Y;
            stChartAp.Height = _oChartArea.Position.Height;
            stChartAp.Width = _oChartArea.Position.Width;
            _oParamChartAtt.ChartAreaPosition = stChartAp;

            stPlottAp.X = _oChartArea.InnerPlotPosition.X;
            stPlottAp.Y = _oChartArea.InnerPlotPosition.Y;
            stPlottAp.Height = _oChartArea.InnerPlotPosition.Height;
            stPlottAp.Width = _oChartArea.InnerPlotPosition.Width;
            _oParamChartAtt.PlottingAreaPosition = stPlottAp;

            _oParamChartAtt.Visible = _oChartArea.Visible;

            _ChangeChartArea = false;
        }      
    }
}
