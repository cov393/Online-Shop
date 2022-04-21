using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventique.Models;
using Ventique.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ventique.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProducts : ContentPage
    {
        VMProducts vmProduct;
        public AddProducts()
        {
            InitializeComponent();
            vmProduct = new VMProducts();
            this.BindingContext = vmProduct;
        }

        private async void lstProducts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (Products.SelectedItem != null)
                {
                    Products product = (Products)e.SelectedItem;
                    if (product != null)
                    {
                        var display = await DisplayActionSheet(product.productName, "Cancel",
                        null, new string[] { "Edit", "Delete" });
                        if (display.Equals("Edit"))
                        {
                            vmProduct.setProduct(product);
                        }
                        else if (display.Equals("Delete"))
                        {
                            vmProduct.setProduct(product);
                            await vmProduct.trnProducts("DELETE");
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            Products.SelectedItem = null;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            FirebaseClient fc = new FirebaseClient("https://online-shop-26853-default-rtdb.firebaseio.com/");
            var result = await fc
                .Child("Containers")
                //.PostAsync(new Item() { NewsDateTime = DateTime.Now.ToString("dd MMM yyyy"), 
                //                        Text = AdminText.Text,
                //                        Description = AdminText.Text,

                .PostAsync(new Containers(){
                    Name = AdminName.Text,
                    ImageUrl = AdminImageUrl.Text,
                    Status = AdminStatus.Text

                });

        }

        private void btnPick_Clicked(object sender, EventArgs e)
        {

        }

        private void btnStore_Clicked(object sender, EventArgs e)
        {

        }

        async void Button_Clicked_Return(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
