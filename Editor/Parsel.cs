using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace Editor
{
    class Parsel
    {
        public static string[] err = new string[4];
        public static string[] SaveText=new string[4];
        public static ArrayList Lang = new ArrayList();
        public static ArrayList Parser(int k)
        {

            Process();
            return Lang;
        }
        public static string[] LOC()
        {
            return SaveText;
        }
            static async void Process()
        {

            FolderPicker folderPicker = new FolderPicker();
            folderPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");
            StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            string[] lang = new string[] {"","-ar","-mn","-ru" };

            if (folder != null)
            {
                for (int j = 0; j < lang.Length; j++)
                {
                    Dictionary<string, string> Library = new Dictionary<string, string>();
                    try
                    {
                        StorageFile File = await folder.GetFileAsync("Messages" + lang[j] + ".lst");
                        if (File != null)
                        {
                            var stream = await File.OpenAsync(FileAccessMode.Read);
                            using (StreamReader reader = new StreamReader(stream.AsStream()))
                            {
                                SaveText[j] = reader.ReadToEnd();
                                string[] text = SaveText[j].Split('\n');
                                for (int i = 0; i < text.Length - 1; i++)
                                {
                                    if (text[i] != null)
                                    {
                                        if (text[i] != "\r" && text[i][0] != '#')
                                        {
                                            string[] temp;
                                            temp = text[i].Split('=');
                                            Library.Add(temp[0], temp[1]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        err[j] = "Messages" + lang[j] + ".lst";
                    }
                    Lang.Add(Library);
                }
            }
        }
    }
}
