using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praktika.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Praktika
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClassesPage : ContentPage
    {
        User currentUser;

        public ClassesPage(User user)
        {
            InitializeComponent();
            currentUser = user;
        }
    }
}