using System;
using System.Collections.Generic;
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

namespace WpfPons1
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Hier zeigt sich, dass das im Window.DataContext definierte MainViewModel eine Instanz ist,
            //die im Code-Behind verwendet werden kann            
            //ToDo: PropertyChanged-Event der Instanz mvm abonnieren und implementieren.
            //Wenn  PropertyName "GibLaut", soll wav\Jodler.wav via System.Media.SoundPlayer ausgegeben werden
        }

    }
}
