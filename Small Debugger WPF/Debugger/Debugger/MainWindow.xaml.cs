using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Debugger
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSearchFolder_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = false;
            folderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            folderBrowserDialog.Description = "Open you project folder";
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                if (folderBrowserDialog.SelectedPath != "")
                    infFolderPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            string log;

            progress.Value = 0;
            if (Directory.Exists(infFolderPath.Text))
            {
                infFolderPath.IsEnabled = false;
                btnSearchFolder.IsEnabled = false;
                btnStart.IsEnabled = false;
                lbLogger.Items.Add(new System.Windows.Controls.Label() { Content = "🛈 Started pack creation", Background = Brushes.Gold, Width = lbLogger.Width - 15});
                lbLogger.Items.Add(new System.Windows.Controls.Label() { Content = "🛈 Searching for *.mdscript / *.mds main file", Background = Brushes.Gold, Width = lbLogger.Width - 15 });
                string mainFile = DebuggerLib.Debugger.Main.GetMainFile(infFolderPath.Text, out log);
                if (mainFile == null)
                {
                    lbLogger.Items.Add(new System.Windows.Controls.Label() { Content = log, Background = Brushes.Red, Width = lbLogger.Width - 15 });
                    infFolderPath.IsEnabled = true;
                    btnSearchFolder.IsEnabled = true;
                    btnStart.IsEnabled = true;
                    return;
                }
                else
                {
                    lbLogger.Items.Add(new System.Windows.Controls.Label() { Content = log, Background = Brushes.DarkGreen, Width = lbLogger.Width - 15, Foreground = Brushes.White });
                    progress.Value = 33.3;
                }

                lbLogger.Items.Add(new System.Windows.Controls.Label() { Content = "🛈 Started debugging process", Background = Brushes.Gold, Width = lbLogger.Width - 15 });
                DebuggerLib.Debugger.Main.Debug(mainFile, out log);

                if (log != "✓ Debug successful")
                {
                    lbLogger.Items.Add(new System.Windows.Controls.Label() { Content = log, Background = Brushes.Red, Width = lbLogger.Width - 15 });
                    infFolderPath.IsEnabled = true;
                    btnSearchFolder.IsEnabled = true;
                    btnStart.IsEnabled = true;
                    return;
                }
                else
                {
                    lbLogger.Items.Add(new System.Windows.Controls.Label() { Content = log, Background = Brushes.DarkGreen, Width = lbLogger.Width - 15, Foreground = Brushes.White });
                    progress.Value = 66.6;
                }
            }
        }
    }
}
