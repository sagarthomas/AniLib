using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace AniLib.Models
{
	[Serializable]
	public class Anime
	{
		//Kitsu variable
		public string id { get; set; }
		public string canonicalTitle { get; set; }
		public string synopsis { get; set; }
		public string averageRating { get; set; }
		public string startDate { get; set; }
		public string endDate { get; set; }
		public string showType { get; set; }

		public string posterImage_tiny { get; set; }
		public string posterImage_small { get; set; }
		public string posterImage_medium { get; set; }
		public string posterImage_large { get; set; }
		public string posterImage_orignal { get; set; }

		public string coverImage_tiny { get; set; }
		public string coverImage_small { get; set; }
		public string coverImage_large { get; set; }
		public string coverImage_original { get; set; }

		public int episodeCount { get; set; }

		//Custom
		public StorageFolder folder { get; set; }
		public ObservableCollection<AnimeEpisode> episodes;
		public BitmapImage poster;
		public BitmapImage cover;
		//TODO: Custom object to store episodes
		
		public Anime() {

		}

		public Anime(StorageFolder f) {
			folder = f;
			episodes = new ObservableCollection<AnimeEpisode>();
		}
		public void ExtractFromObject(RootObject anime) {
			try {
				id = anime.data.id;
				var attr = anime.data.attributes;
				canonicalTitle = attr.canonicalTitle;
				synopsis = attr.synopsis;
				averageRating = attr.averageRating;
				startDate = attr.startDate;
				endDate = attr.endDate;
				showType = attr.subtype; //or showType, latter is deprecated but still works

				posterImage_tiny = attr.posterImage.tiny;
				posterImage_small = attr.posterImage.small;
				posterImage_medium = attr.posterImage.medium;
				posterImage_large = attr.posterImage.large;
				posterImage_orignal = attr.posterImage.original;

				//coverImage_tiny = attr.coverImage.tiny; -> Probably not needed
				//coverImage_small = attr.coverImage.small;
				coverImage_large = attr.coverImage.large;
				coverImage_original = attr.coverImage.original;

				episodeCount = attr.episodeCount;

				//Saving PosterImage and CoverImage to Bitmap
				poster = new BitmapImage(new Uri(posterImage_medium));
				cover = new BitmapImage(new Uri(coverImage_original));
			}
			catch(Exception e) {

			}
		}

        public async Task<bool> PopulateEpisodeList() {
			var episodeFiles = await folder.GetFilesAsync();
			var number = 1;
			foreach (var file in episodeFiles) {
				var thumbnail = await file.GetThumbnailAsync(ThumbnailMode.VideosView, 240);
				episodes.Add(new AnimeEpisode(number));
				try {
					episodes[number - 1].thumbnail.SetSource(thumbnail as IRandomAccessStream);

				} catch(Exception e) {
					//TODO: Set Thumbnail to a custom image for the app
				}


				number++;
			}
			//var lel=  await episodeFiles[1].Properties.GetVideoPropertiesAsync();
			//lel.Height.ToString(); //resolution
			return true;
		}


	}//End of Anime class

	[Serializable]
	public class AnimeEpisode {

		public string episodeNumber { get; set; }
		public string officialTitle { get; set; }
		public string resolution { get; set; }

		//public StorageFile episodeFile { get; set; }
		public BitmapImage thumbnail { get; set; }
		//public StorageItemThumbnail pic { get; set; }

		[JsonConstructor]
		public AnimeEpisode( int episodeNum) {
			//episodeFile = file;
			episodeNumber = "Episode " + episodeNum.ToString();
			this.thumbnail = new BitmapImage();
		}

		public AnimeEpisode() {
		}

		public async Task<BitmapImage> SetBitmapSourceAsync(StorageItemThumbnail pic) {
			var bitmap = new BitmapImage();
			await bitmap.SetSourceAsync(pic);
			thumbnail = bitmap;
			return bitmap;
		}
	}

	public static class AnimeManager {
		
		private static ObservableCollection<Anime> anime;

		public static void Init() {
			anime = new ObservableCollection<Anime>();
		}

		public static ObservableCollection<Anime> GetAnime() {
			return anime;
		}

		public static void AddAnime(Anime ani) {
			anime.Add(ani);
		}

		public static void SetAnime(ObservableCollection<Anime> anim) {
			anime = anim;
		}

		public static int Count() {
			return anime.Count;
		}
	}
}
