using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestioneVeicoli.Data.Models;
using GestioneVeicoli.Data.Services.Interfaces;
using GestioneVeicoli.Log;
using GestioneVeicoli.ViewModels;
using GestioneVeicoli.Views;

namespace GestioneVeicoli
{
    public class NavigationService
    {
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task NavigateToDettaglioAsync(Veicolo veicolo)
        {
            var viewModel = new VeicoloDettaglioViewModel(
                veicolo,
                _serviceProvider.GetRequiredService<IVeicoliRepository>(),
                _serviceProvider.GetRequiredService<VeicoloViewModel>()
                );
            var page = new VeicoloDettaglioPage
            {
                BindingContext = viewModel
            };
            await App.Current.MainPage.Navigation.PushAsync(page);
        }

        public async Task NavigateBackAsync() 
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }


    }
}
