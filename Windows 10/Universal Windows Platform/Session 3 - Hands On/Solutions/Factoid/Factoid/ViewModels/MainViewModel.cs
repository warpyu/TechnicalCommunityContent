using Factoid.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factoid.ViewModels
{
    public class MainViewModel : Common.ObservableBase
    {
        public MainViewModel()
        {
            this.AllFacts = new ObservableCollection<FactInformation>();
        }

        private ObservableCollection<FactInformation> _allFacts;
        public ObservableCollection<FactInformation> AllFacts
        {
            get { return this._allFacts; }
            set { this.SetProperty(ref this._allFacts, value); }
        }

        private FactInformation _currentFact;
        public FactInformation CurrentFact
        {
            get { return this._currentFact; }
            set { this.SetProperty(ref this._currentFact, value); }
        }

        public async void LoadFactsAsync()
        {
            for (int i = 0; i <= 10; i++)
            {
                var fact = await Helpers.FactHelper.GetFactAsync();
                this.AllFacts.Add(new FactInformation() { Id = Guid.NewGuid(), IsFavorite = false, Label = fact });
            }

        }

        public async void LoadFactAsync()
        {
            var fact = await Helpers.FactHelper.GetFactAsync();

            this.CurrentFact = new FactInformation() { Id = Guid.NewGuid(), IsFavorite = false, Label = fact };
        }
    }
}
