using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace Realtor.Views
{
    public partial class UploadPage : ContentPage
    {
        public UploadPage()
        {
            InitializeComponent();

            //Opendialog.Clicked += Opendialog_Clicked;
        }

        //private async void Opendialog_Clicked(object sender, EventArgs e)
        //{
        //    var choices = new List<string>
        //    {
        //        "choice 1",
        //        "choice 2",
        //        "choice 3",
        //    };

        //    var choice = await MaterialDialog.Instance.SelectChoiceAsync("Select choices", choices);

        //    if (choice < 0)
        //    {
        //        return;
        //    }

        //    DialogResult.Text = choices[choice];
        //}

        private async void Opendialog_Tapped(object sender, EventArgs e)
        {
            var choices = new List<string>
            {
                "true",
                "false"
            };

            var choice = await MaterialDialog.Instance.SelectChoiceAsync("Negotiable", choices, confirmingText: "Ok", dismissiveText: "");

            if (choice < 0)
            {
                return;
            }

            DialogResults.Text = choices[choice];
        }

        private async void PropertyType_Tapped(object sender, EventArgs e)
        {
            var propertyType = new List<string>
            {
                "Land",
                "House",
                "Shop"
            };

            var choice = await MaterialDialog.Instance.SelectChoiceAsync("Property", propertyType);

            if (choice < 0)
            {
                return;
            }

            Property.Text = propertyType[choice];

        }
    }
}
