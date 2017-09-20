﻿using Organizer.Model;
using Organizer.UI.Data;
using System.Collections.ObjectModel;

namespace Organizer.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IPersonDataService _personDataService;

        public MainViewModel(IPersonDataService personDataService)
        {
            Persons = new ObservableCollection<Person>();
            _personDataService = personDataService;
        }

        public void Load()
        {
            var persons = _personDataService.GetAll();
            Persons.Clear();
            foreach (var p in persons)
            {
                Persons.Add(p);
            }
        }

        public ObservableCollection<Person> Persons { get; set; }

        private Person _selectedPerson;

        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                OnPropertyChanged();
            }
        }
    }
}