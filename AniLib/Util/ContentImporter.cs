using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace AniLib.Util
{
	public static class ContentImporter
	{
		//For Large Scale 1 dir depth: selected folder -> other tv/anime folders inside
		public static async Task<bool> SelectCollectionFolderAsync(FolderPicker fp) {

			/*
			folderPicker = fp;
			folderPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
			folderPicker.FileTypeFilter.Add(".sometext"); //Use to prevent Error
			StorageFolder collectionsFolder = await folderPicker.PickSingleFolderAsync();
			folder = collectionsFolder;
			*/
			

			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fp"></param>
		/// <returns></returns>
		public static async Task<StorageFolder> SelectFolderAsync() {
			FolderPicker folderPicker = new FolderPicker();
			folderPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
			folderPicker.FileTypeFilter.Add(".sometext"); //Use to prevent Error
			StorageFolder folder = await folderPicker.PickSingleFolderAsync();
			

			return folder;
		}

		/// <summary>
		/// Parses folder name into API-friendly name
		/// </summary>
		/// <param name="raw_name"></param>
		/// <returns></returns>
		public static string FolderNameParse(string raw_name) {
			var regex = new Regex("^[\\[\\]()]$"); //Looks for '[' or ']'
			var result = "";
			var isWriting = true;
			for (int i = 0; i < raw_name.Length; i++) {
				if (regex.IsMatch(raw_name[i].ToString()))
					isWriting = !isWriting; // Toggle Writing
				if(isWriting && (raw_name[i] != ']' && raw_name[i] != ')')) {
					if (raw_name[i] == ' ')
						result += "%20";
					else
						result += raw_name[i];
				}
			}
			return result;//.Trim('%','2','0'); // Trims out %20 from begin and end of result -> Trim not needed
		}//End of FolderNameParse

		/// <summary>
		/// Queries the Kitsu API with a search name and returns the ID of the first search result
		/// </summary>
		/// <param name="query"></param>
		/// <returns>ID</returns>
		public static async Task<string> KitsuAPIQueryAsync(string query) {
			//TODO:Implement Try-Catch
			var http = new HttpClient();
			var url = "https://kitsu.io/api/edge/anime?filter[text]=" + query;
			var response = await http.GetAsync(url);
			var result = await response.Content.ReadAsStringAsync(); // Gets the query in JSON string format
			//Since ID is the first occuring number, we extract that. Note: gets the ID # of the very first search result
			var firstDigits = result.SkipWhile(c => !Char.IsDigit(c)).TakeWhile(Char.IsDigit).ToArray();
			var id = new String(firstDigits);

			return id;
		}
	}

}
