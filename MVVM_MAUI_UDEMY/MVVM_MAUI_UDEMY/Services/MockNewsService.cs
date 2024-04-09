using MVVM_MAUI_UDEMY.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_MAUI_UDEMY.Services
{
    public class MockNewsService
    {
        List<News> newslist = new()
        {
            new News(){Id=1, Title="Stellantis, altri 3.500 licenziamenti in arrivo: cala anche la produzione a -10%", Content="L'azienda continua a licenziare dipendenti e i numeri del primo trimestre 2024 mostrano come l'obiettivo di un milione di auto sia tutt'altro che semplice", ImageUrl="https://quifinanza.it/wp-content/uploads/sites/5/2024/04/stellantis-mirafiori.jpg?w=687&h=386&quality=90&strip=all&crop=1"},
            new News(){Id=2, Title="Wanna marchi e la figlia viaggiano sul suv Lamborghini",Content="Wanna Marchi e Stefania Nobile immortalate a bordo di un bolide extra-lusso da 240mila euro (e 666 CV): la regina delle televendite viaggia in Lamborghini Urus", ImageUrl="https://www.virgilio.it/motori/wp-content/uploads/sites/4/2024/04/wanna-marchi-lamborghini.jpeg?w=687&h=386&quality=90&strip=all&crop=1"}
        };

        public List<News> GetNews()
        {
            return newslist;
        }
        public MockNewsService()
        {
            
        }
    }
}
