using System.Windows;
using System;
using Microsoft.Win32;

namespace Notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Editor editor;
        
        public MainWindow()
        {
            InitializeComponent();
            editor = new Editor();
            DataContext = editor;
        }

        private void HandleOpenFile(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                editor.OpenFilePath(openFileDialog.FileName);
            }
            ContentTextBox.Text = editor.Text;
        }

        private void HandleSaveFile(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            if (editor.FilePath == Editor.UNTITLED_FILE_PATH)
            {
                saveFileDialog.Title = "Save file";
                if (saveFileDialog.ShowDialog() == true)
                {
                    editor.FilePath = saveFileDialog.FileName;
                }
                editor.CreateNewTextFile();
            }
            editor.SaveFileText();
        }

        private void HandleNewFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                editor.CreateNewTextFile(saveFileDialog.FileName);
                editor.OpenFilePath(saveFileDialog.FileName);
            }
        }

        private void HandleQuitApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }   
}
