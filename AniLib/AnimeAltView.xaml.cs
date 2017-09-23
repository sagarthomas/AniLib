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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AniLib {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class AnimeAltView : Page {

		private Anime anime;
		private Grid grid;
		private Image coverImage;
		private Double progressScore;

		private string animeTitle;
		private string score;

		public AnimeAltView() {
			this.InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e) {
			base.OnNavigatedTo(e);
			anime = (Anime)e.Parameter;
			RatingBarSetup();
			//var task = await anime.PopulateEpisodeList();

		}

		private void RatingBarSetup() {
			progressScore = Convert.ToDouble(anime.averageRating);
			if (progressScore >= 80.0)
				Rating_Bar.Foreground = new SolidColorBrush(Color.FromArgb(255, 76, 175, 80)); // Green
			else if (progressScore >= 70.0)
				Rating_Bar.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 193, 7)); //Orange
			else
				Rating_Bar.Foreground = new SolidColorBrush(Color.FromArgb(255, 221, 44, 0)); //Red

			Rating_Anim.Begin();
		}

		private void Episode_List_ItemClick(object sender, ItemClickEventArgs e) {
			var episode = (AnimeEpisode)e.ClickedItem;
			//TODO: FIle cannot be serialized, need to find way of getting file without storing it here
			//Frame.Navigate(typeof(VideoPlayer), episode.episodeFile); 

		}
	}

}
