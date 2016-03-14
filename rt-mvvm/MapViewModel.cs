using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime.Layers;

namespace rt_mvvm
{
    class MapViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaiseNotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public void MyMapViewExtentChanged(object parameter)
        {
            var mv = parameter as MapView;
            var extent = mv.Extent;
            //CurrentExtentString = "FOO BAR";
            //Console.Write(extent);
            CurrentExtentString = string.Format("XMin={0:F2} YMin={1:F2} XMax={2:F2} YMax={3:F2}", 
            extent.XMin, extent.YMin, extent.XMax, extent.YMax);
        }


        private Map map;
        public Map IncidentMap
        {
            get { return this.map; }
            set { this.map = value; }
        }

        private string extentString;
        public string CurrentExtentString
        {
            get
            {
                return this.extentString;
            }
            set
            {
                this.extentString = value;
                this.RaiseNotifyPropertyChanged();
            }
        }

        

        public MapViewModel()
        {
            this.map = App.Current.Resources["IncidentMap"] as Map;
            ToggleLayerCommand = new DelegateCommand(ToggleLayer, OkToExecute);
            ExtentChangedCommand = new DelegateCommand(MyMapViewExtentChanged);

        }

        public DelegateCommand ToggleLayerCommand { get; set; }
        public DelegateCommand ExtentChangedCommand { get; set; }

        private void ToggleLayer(object parameter)
        {
            var lyr = this.map.Layers[parameter.ToString()];
            lyr.IsVisible = !(lyr.IsVisible);
        }

        private bool OkToExecute(object parameter)
        {
            var lyr = this.map.Layers[parameter.ToString()] as FeatureLayer;
            return (lyr != null);
        }

        
 
    }
}
