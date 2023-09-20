using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SQLiteDemo
{
    public partial class App : Application
    {
        private static MyDatabase db;
        public static MyDatabase MyDb
        {
            get
            {
                if(db == null)
                {
                    db = new MyDatabase();
                }
                return db;
            }
        }

        public App ()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

