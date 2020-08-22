using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Realtor.Styles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomTemplate : ResourceDictionary
    {
        public CustomTemplate()
        {
            InitializeComponent();
        }
    }
}