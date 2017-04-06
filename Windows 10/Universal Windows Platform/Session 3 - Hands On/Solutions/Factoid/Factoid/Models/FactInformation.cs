using Factoid.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Factoid.Models
{
    public class FactInformation : Common.ObservableBase
    {
        private Guid _id;
        public Guid Id
        {
            get { return this._id; }
            set { this.SetProperty(ref this._id, value); }
        }

        private string _label;
        public string Label
        {
            get { return this._label; }
            set { this.SetProperty(ref this._label, value); }
        }

        private bool _isFavorite;
        public bool IsFavorite
        {
            get { return this._isFavorite; }
            set { this.SetProperty(ref this._isFavorite, value); }
        }

        public ICommand ReadCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    await Helpers.SpeechHelper.ReadFactAsync(this.Label);
                });
            }
        }

        public ICommand ToggleFavoriteCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    this.IsFavorite = !this.IsFavorite;

                    if (this.IsFavorite)
                    {
                        await Helpers.StorageHelper.SaveFavoriteAsync(this);

                        Helpers.TileHelper.PinFact(App.ViewModel.CurrentFact.Label);
                    }
                    else
                    {
                        await Helpers.StorageHelper.RemoteFavoriteAsync(this);
                    }

                });
            }
        }

        public ICommand AddToFavoritesCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {

                    if (App.ViewModel != null) App.ViewModel.AllFacts.Insert(0, this);

                    await Helpers.StorageHelper.SaveFavoriteAsync(this);

                    App.ViewModel.CurrentFact = this;

                    Helpers.TileHelper.PinFact(App.ViewModel.CurrentFact.Label);


                });
            }
        }
    }
}
