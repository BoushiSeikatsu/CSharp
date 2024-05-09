using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*Storage methods info
         * Indexes
         0 -> Insert(object data) : Int32
         1 -> Delete(Int32 id) : void
         2 -> Select(Type type) : List<Type>
         3 -> SelectById(Int32 id, Type type) : object
         4 -> MemberwiseClone() : Object
         5 -> Finalize() : void
         */
        public MethodInfo[] Methods { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Assembly assembly = Assembly.LoadFrom("Storage.dll");
            Type[] types = assembly.GetTypes();
            Type dataStorage = null;
            foreach(Type t in types)
            {
                if(t.Name == "DataStorage")
                {
                    dataStorage = t;
                }
            }
            Methods = dataStorage.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach(MethodInfo method in Methods)
            {
                Trace.Write(method.Name + ": ");
                ParameterInfo[] parameters = method.GetParameters();
                foreach(ParameterInfo param in parameters)
                {
                    Trace.Write(param.ParameterType.Name + " " + param.Name);
                }
                Trace.WriteLine("");
                Type t = method.ReturnType;
                Trace.WriteLine(t.Name);
            }
        }
    }
}