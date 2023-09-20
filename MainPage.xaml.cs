using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SQLiteDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Console.WriteLine("+++++++ MainPage Constructor called");
        }

        async void AddItem_Clicked(System.Object sender, System.EventArgs e)
        {
            // 1. get item information from UI
            string title = txtItemName.Text;
            bool isHighPriority = swPriority.IsToggled;

            // 2. Build the todo list item
            ToDoItem itemToAdd = new ToDoItem(title, isHighPriority);

            // 3. Add it to the database
            int results = await App.MyDb.AddItem(itemToAdd);

            if (results == 0)
            {
                Console.WriteLine("++++ ERROR: Item could not be created");
            }
            else
            {
                Console.WriteLine("++++ Item Added!");
            }

            // 4. Clear form fields and prepare for new input
            txtItemName.Text = "";
            swPriority.IsToggled = false;
        }

        async void Update_Clicked(System.Object sender, System.EventArgs e)
        {
            // 1. get item information from UI
            if (!int.TryParse(txtItemId.Text, out int itemId))
            {
                Console.WriteLine("++++ ERROR: Invalid item ID");
                return;
            }

            // 2. Find the item
            ToDoItem itemToUpdate = await App.MyDb.GetItemById(itemId);

            if (itemToUpdate == null)
            {
                Console.WriteLine("++++ ERROR: Item not found");
                return;
            }

            // 3. Get the updated values from the UI
            string updatedTitle = txtItemName.Text;
            bool updatedPriority = swPriority.IsToggled;

            // 4. Set the item's new values
            itemToUpdate.Title = updatedTitle;
            itemToUpdate.IsHighPriority = updatedPriority;

            // 5. Save the changes
            int results = await App.MyDb.UpdateItem(itemToUpdate);

            if (results == 0)
            {
                Console.WriteLine("++++ ERROR: Item could not be updated");
            }
            else
            {
                Console.WriteLine("++++ Item Updated!");
            }

            // 6. Clear form fields and prepare for new input
            txtItemName.Text = "";
            swPriority.IsToggled = false;
        }

        async void Delete_Clicked(System.Object sender, System.EventArgs e)
        {
            // 1. get item information from UI
            if (!int.TryParse(txtItemId.Text, out int itemId))
            {
                Console.WriteLine("++++ ERROR: Invalid item ID");
                return;
            }

            // 2. Delete from the database
            int results = await App.MyDb.DeleteItem(itemId);

            if (results == 0)
            {
                Console.WriteLine("++++ ERROR: Item could not be deleted");
            }
            else
            {
                Console.WriteLine("++++ Item Deleted!");
            }

            // 3. Clear form fields and prepare for new input
            txtItemId.Text = "";
        }

        async void GetItemById_Clicked(System.Object sender, System.EventArgs e)
        {
            // 1. get item information from UI
            if (!int.TryParse(txtItemId.Text, out int itemId))
            {
                Console.WriteLine("++++ ERROR: Invalid item ID");
                return;
            }

            // 2. Retrieve the data from the database
            ToDoItem item = await App.MyDb.GetItemById(itemId);

            if (item != null)
            {
                Console.WriteLine($"Item ID: {item.Id}, Title: {item.Title}, Priority: {item.IsHighPriority}");
            }
            else
            {
                Console.WriteLine("++++ Item not found");
            }

            // 3. Clear all text fields and prepare for new input
            txtItemId.Text = "";
        }

        async void GetAll_Clicked(System.Object sender, System.EventArgs e)
        {
            // Get all items from the database
            List<ToDoItem> items = await App.MyDb.GetAllItems();

            foreach (var item in items)
            {
                Console.WriteLine($"Item ID: {item.Id}, Title: {item.Title}, Priority: {item.IsHighPriority}");
            }
        }

        async void GetItemsByPriority_Clicked(System.Object sender, System.EventArgs e)
        {
            // Get toggle
            bool isHighPriority = swPriority.IsToggled;

            // Get items from the database based on priority
            List<ToDoItem> items = await App.MyDb.GetItemsByPriority(isHighPriority);

            foreach (var item in items)
            {
                Console.WriteLine($"Item ID: {item.Id}, Title: {item.Title}, Priority: {item.IsHighPriority}");
            }

            // Reset form fields
            swPriority.IsToggled = false;
        }
    }
}
