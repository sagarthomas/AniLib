using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Storage;

using Newtonsoft.Json;
using System.Collections.ObjectModel;
using AniLib.Models;
using System.Xml.Serialization;
using System.IO;

namespace AniLib.Util {
	class StorageManager {

		public static ApplicationDataContainer localSettings;
		public static StorageFolder localFolder;

		/// <summary>
		/// Initialize Storage Settings; Must be called before the usage of other StorageManager methods;
		/// Should be called in App.xaml.cs
		/// </summary>
		public static void Initialize() {
			//In order to read and write local app data, we must retrieve it first
			localSettings = ApplicationData.Current.LocalSettings;
			localFolder = ApplicationData.Current.LocalFolder;
		}

		public static void CreateSetting(string key, string value) {
			localSettings.Values[key] = value;
		}

		public static Object RetrieveSetting(string key) {
			return localSettings.Values[key];
		}

		public static async Task SaveObjectData<T>(string key, T data) {
			//ApplicationDataCompositeValue composite = new ApplicationDataCompositeValue();

			var serialData = JsonConvert.SerializeObject(data,Formatting.Indented);
			//composite["data"] = serialData;

			//localSettings.Values[key] = serialData;

			StorageFile dataFile = await localFolder.CreateFileAsync(key + ".txt", CreationCollisionOption.ReplaceExisting);
			await FileIO.WriteTextAsync(dataFile, serialData);
		}

		public static async Task<T> RetrieveObjectData<T>(string key) {
			//ApplicationDataCompositeValue comp = (ApplicationDataCompositeValue)localSettings.Values[key];
			//var serialData = (string)comp["data"];
			//var serialData = (string)localSettings.Values[key];
			//var data = JsonConvert.DeserializeObject<T>(serialData);

			StorageFile dataFile = await localFolder.GetFileAsync(key + ".txt");
			string raw_data = await FileIO.ReadTextAsync(dataFile);
			var data = JsonConvert.DeserializeObject<T>(raw_data);
			return data;
			
		}

		public static void SerializeCollection(ObservableCollection<Anime> anime) {
			XmlSerializer writer = new XmlSerializer(typeof(ObservableCollection<Anime>));

			//StorageFile dataFile = await localFolder.CreateFileAsync("anime-collection.xml", CreationCollisionOption.ReplaceExisting);
			FileStream file = File.Create(localFolder.Path + "//anime-collection.xml");
			writer.Serialize(file, anime);
			file.Dispose();
			
		}

		public static async Task<ObservableCollection<Anime>> ExtractCollection() {
			XmlSerializer reader = new XmlSerializer(typeof(ObservableCollection<Anime>));
			var file = await localFolder.GetFileAsync("anime-collection.xml");
			var stream = await file.OpenStreamForReadAsync();
			//StreamReader sr = new StreamReader(stream);

			ObservableCollection<Anime> anime = (ObservableCollection<Anime>)reader.Deserialize(stream);
			return anime;
		}

		/// <summary>
		/// Checks if a composite object exists at given key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static async Task<bool> CheckObjectData(string key) {
			//ApplicationDataCompositeValue comp = (ApplicationDataCompositeValue)localSettings.Values[key];
			/*
			if(comp == null) {
				return false;
			}
			else {
				return true;
			}
			*/
			var item = await localFolder.TryGetItemAsync(key + ".txt");
			if (item == null)
				return false;
			else
				return true;
		}
	}
}
