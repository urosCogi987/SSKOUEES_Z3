using Microsoft.Win32;
using PredmetniZadatak3.Controllers;
using PredmetniZadatak3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PredmetniZadatak3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Doesn't work for some reason, fix if u have time
        #region onPropertyChanged
        private string filePath;
        public string FilePath
        {
            get { return filePath; }
            set
            {
                if (value != filePath)
                {
                    filePath = value;
                    OnPropertyChanged("FilePath");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        MainCamera mainCamera;
        HashSet<PowerEntity> powerEntities = new HashSet<PowerEntity>();
        HashSet<SubstationEntity> substationEntities = new HashSet<SubstationEntity>();        
        HashSet<NodeEntity> nodeEntities = new HashSet<NodeEntity>();
        HashSet<SwitchEntity> switchEntities = new HashSet<SwitchEntity>();
        HashSet<LineEntity> lineEntities = new HashSet<LineEntity>();

        public MainWindow()
        {
            InitializeComponent();
            mainCamera = new MainCamera(myViewport, scaleTransform, rotateX, rotateY, translateTransform, this);
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Find file";
            ofd.Filter = "XML Document|*.xml";

            if (ofd.ShowDialog() == true)
            {
                var onlyFileName = System.IO.Path.GetFileName(ofd.FileName);
                FilePath = onlyFileName;
            }

            XMLParser.LoadSubstations(powerEntities, substationEntities, FilePath);
            XMLParser.LoadNodes(powerEntities, nodeEntities, FilePath);
            XMLParser.LoadSwitches(powerEntities, switchEntities, FilePath);
            XMLParser.LoadLines(lineEntities, FilePath);

            double localLatitude = 0;
        }

        private void DrawBtn_Click(object sender, RoutedEventArgs e)
        {
            
            

            
        }
    }
}
