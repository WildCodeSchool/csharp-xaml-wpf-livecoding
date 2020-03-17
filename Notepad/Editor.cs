using System;
using System.Text;
using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Notepad
{

    public class Editor : INotifyPropertyChanged
    {
        public String Text { get; set; }
        public String FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged();
            }
        }

        private String _filePath;

        public const String UNTITLED_FILE_PATH = "[Untitled]";

        public Editor()
        {
            FilePath = UNTITLED_FILE_PATH;
        }

        public void SaveFileText()
        {
            if (!File.Exists(FilePath))
            {
                CreateNewTextFile(FilePath);
                OpenFilePath(FilePath);
            }

            File.WriteAllText(FilePath, Text);
        }

        public void CreateNewTextFile(String filePath=null)
        {
            if (filePath == null)
            {
                filePath = FilePath;
            }
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            File.Create(filePath).Close();
        }

        public void OpenFilePath(String filePath)
        {
            this.FilePath = filePath;
            if (File.Exists(FilePath))  
            {
                Text = File.ReadAllText(filePath);
            }
            else
            {
                Text = String.Empty;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
