using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Millsnet.DvlApp.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Private Fields
        #endregion

        #region Private Properties
        private bool _isBusy;
        #endregion

        #region Public Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructor
        public ViewModelBase()
        {
        }

        #endregion


        #region Public Properties
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        #endregion

        #region Commands
        //public ICommand AddOrderCommand => new Command(() => {
        //    _navigationService.NavigateModalAsync<OrderEditViewModel>(
        //        new Order { Medications = (_medicationSuggestions ?? new ObservableCollection<SelectableItem<Medication>>()).Where(m => m.IsSelected).Select(m => m.Item) });
        //});

        //public ICommand ToggleMedicationSelected => new Command<SelectableItem<Medication>>((SelectableItem<Medication> item) =>
        //{
        //    SelectableItem<Medication> listItem = item;
        //    listItem.IsSelected = !listItem.IsSelected;
        //    OnPropertyChanged(nameof(MedicationSuggestions));
        //});
        #endregion

        #region Virtual Methods
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual Task InitialiseAsync(object data)
        {
            return Task.FromResult(false);
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;

            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }
        #endregion
    }
}

