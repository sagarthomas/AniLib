using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AniLib.Models;
using AniLib;
using AniLib.Util;
using System.Collections.ObjectModel;
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AniLib
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class AnimeCollection : Page
	{
		
		private ObservableCollection<Anime> anime;

		public AnimeCollection() {
			this.InitializeComponent();
			anime = new ObservableCollection<Anime>();
		}

		
		protected override async void OnNavigatedTo(NavigationEventArgs e) {
			base.OnNavigatedTo(e);
			/*
			var result = await StorageManager.CheckObjectData("anime-collection");
			if (result) {
				 var currentCollection = await StorageManager.RetrieveObjectData<ObservableCollection<Anime>>("anime-collection");
				AnimeManager.SetAnime(currentCollection);
			}
			*/
			try {
				var currentCollection = await StorageManager.ExtractCollection();
				AnimeManager.SetAnime(currentCollection);
			}
			catch (Exception) {

				//throw;
			}
			
		}
		
		/// <summary>
		/// This button click will only be used for debugging purposes and will be removed later. Currently used for single import
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void Button_Click(object sender, RoutedEventArgs e) {
			var selectedFolder = await ContentImporter.SelectFolderAsync();
			if(selectedFolder != null) {
				var queryName = ContentImporter.FolderNameParse(selectedFolder.DisplayName);
				var id = await ContentImporter.KitsuAPIQueryAsync(queryName);
				var obj = await KitsuInterface.GetAnime(id);
				var ani = new Anime(selectedFolder);
				ani.ExtractFromObject(obj);
				var task = await ani.PopulateEpisodeList();

				AnimeManager.AddAnime(ani);
				anime.Add(ani);
				//AnimeManager.SetAnime(anime);
				Debug.WriteLine("Number of anime in collection: " + AnimeManager.Count());
				//anime = AnimeManager.GetAnime(); //Refreshes list with new Anime
				//anime = new ObservableCollection<Anime>(AnimeManager.GetAnime());
				

			}
		}

        private void Collection_ItemClick(object sender, ItemClickEventArgs e) {
            var anime = (Anime)e.ClickedItem;
            Frame.Navigate(typeof(AnimeAltView), anime);
        }
    }
}
