using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using TheDeptBook.Model;
using TheDeptBook.View;
using TheDeptBook.Data;

namespace TheDeptBook.ViewModel
{
    class MainWindowViewModel : BindableBase
    {
        private string filename = "";
        string filePath = "";

        private ObservableCollection<Debtor> debtors;

        public MainWindowViewModel()
        {
            debtors = new ObservableCollection<Debtor>
            {
                new Debtor("1", "Marcus", 0.1),
                new Debtor("2", "Mikkel", 500),
                new Debtor("3", "Anders", 44)
            };
            debtors.First().Debits.Add(new Debit("0", 500));
            debtors.First().UpdateBalance();
        }

        /// Properties
        public ObservableCollection<Debtor> Debtors
        {
            get { return debtors; }
            set { SetProperty(ref debtors, value); }
        }

        private Debtor currentDebtor = null;

        public Debtor CurrentDebtor
        {
            get { return currentDebtor; }
            set
            {
                SetProperty(ref currentDebtor, value);
            }
        }

        int currentIndex = -1;
        public int CurrentIndex
        {
            get { return currentIndex; }
            set
            {
                SetProperty(ref currentIndex, value);
            }
        }

        // Commands
        ICommand _addDebtorCommand;

        public ICommand AddDebtorCommand
        {
            get
            {
                return _addDebtorCommand ?? (_addDebtorCommand = new DelegateCommand(() =>
                {
                    var newDebtor = new Debtor();
                    var addDeptorViewModel = new AddDebtorWindowViewModel(newDebtor);
                    var addDebtor = new AddDebtorWindow
                    {
                        DataContext = addDeptorViewModel
                    };

                    if (addDebtor.ShowDialog() == true)
                    {
                        newDebtor.Debits.Add(new Debit("0", newDebtor.Value));
                        Debtors.Add(newDebtor);
                        currentDebtor = newDebtor;
                    }
                }));
            }
        }

        ICommand _debtorClickedCommand;

        public ICommand DebtorClicked
        {
            get
            {
                return _debtorClickedCommand ?? (_debtorClickedCommand = new DelegateCommand(() =>
                {
                    var vm = new DebtorWindowViewModel(CurrentDebtor);
                    var debtorWindow = new DebtorWindow()
                    {
                        DataContext = vm,
                        Owner = App.Current.MainWindow
                    };
                    debtorWindow.Show();
                }));
            }
        }

        ICommand _saveDebtorCommand;
        public ICommand saveDebtorCommand
        {
            get
            {
                return _saveDebtorCommand ?? (_saveDebtorCommand = new DelegateCommand(SaveFileCommand_Execute));
            }
        }
        private void SaveFileCommand_Execute()
        {
            var dialog = new SaveFileDialog();

            if (filePath == "")
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                dialog.InitialDirectory = Path.GetDirectoryName(filePath);

            if (dialog.ShowDialog(App.Current.MainWindow) == true)
            {
                filePath = dialog.FileName;
                filename = Path.GetFileName(filePath);
                SaveFile();
            }
            
        }


        private void SaveFile()
        {
            try
            {
                Repository.SaveFile(filePath, debtors);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to save file", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        ICommand _loadDebtorCommand;
        public ICommand loadDebtorCommand
        {
            get { return _loadDebtorCommand ?? (_loadDebtorCommand = new DelegateCommand(OpenFileCommand_Execute)); }
        }


        private void OpenFileCommand_Execute()
        {
            var dialog = new OpenFileDialog();

            
            if (filePath == "")
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                dialog.InitialDirectory = Path.GetDirectoryName(filePath);

            if (dialog.ShowDialog(App.Current.MainWindow) == true)
            {
                filePath = dialog.FileName;
                filename = Path.GetFileName(filePath);
                try
                {
                    ObservableCollection<Debtor> tempDebtors;
                    Repository.ReadFile(filePath, out tempDebtors);
                    Debtors = tempDebtors;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unable to open file", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
