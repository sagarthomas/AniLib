using AniLib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AniLib
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AnimeView : Page
    {
        private Anime anime;
        private Grid grid;
        private Image coverImage;
        private Double progress_score;

        private string animeTitle;
        private string score;
        
        public AnimeView()
        {
            this.InitializeComponent();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            anime = (Anime)e.Parameter;
            //animeTitle = anime.canonicalTitle.ToUpper();
            score = "AVG SCORE: " + anime.averageRating;

            RatingBarSetup();
            

            
        }

        private void RatingBarSetup() {
            progress_score = Convert.ToDouble(anime.averageRating);

            //Set colour of bar depending on score
            if (progress_score >= 80.0)
                Rating_Bar.Foreground = new SolidColorBrush(Color.FromArgb(255, 76, 175, 80)); // Green
            else if (progress_score >= 70.0)
                Rating_Bar.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 193, 7)); //Orange
            else
                Rating_Bar.Foreground = new SolidColorBrush(Color.FromArgb(255, 221, 44, 0)); //Red

            Rating_Anim.Begin(); // Starts animation
        }
    }
}
