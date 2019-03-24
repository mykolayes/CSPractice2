using System.ComponentModel;
using System.Windows;

namespace NaUKMA.CS.Practice02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new PersonVm();
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            var viewModel = (PersonVm)DataContext;
            if (viewModel.ClosingCommand.CanExecute(null))
                viewModel.ClosingCommand.Execute(null);
        }
    }
}
