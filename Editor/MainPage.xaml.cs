using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;
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
using Windows.Storage;
using Windows.Storage.Pickers;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace Editor
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        ArrayList Lang = new ArrayList();
        TextBox[] Work1, Work2, Work3, Work4;
        Dictionary<string, string> WorkEN, WorkAR, WorkMN, WorkRU;
        private void OpenFile_OnClick(object sender, RoutedEventArgs e)
        {
            // Dictionary<string, Dictionary<string, string>> Library = new Dictionary<string, Dictionary<string, string>>();
            Lang = Parsel.Parser(0);
            for(int i=0; i<4; i++)
            {
                if(Parsel.err[i]!=null)
                {
                    TextBlock error = new TextBlock { Text = "Не найден файл:"+Parsel.err[i] };
                    Func.Children.Add(error);
                    Grid.SetColumn(error, 0);
                    Grid.SetRow(error, 2+(i+1)*2);

                }
            }
            Zapoln(Lang);
        }
        private async void SaveFile_OnClick(object sender, RoutedEventArgs e)
        {
            FolderPicker folderPicker = new FolderPicker();
            folderPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");
            StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            Lang[0] = WorkEN;
            Lang[1] = WorkAR;
            Lang[2] = WorkMN;
            Lang[3] = WorkRU;
            string[] SaveText = Parsel.LOC();
            if (folder != null)
            {
                string[] lang = new string[] { "", "-ar", "-mn", "-ru" };
                for (int i = 0; i < 4; i++)
                {
                    try
                    {
                        int k = 0;
                        string[] text = SaveText[i].Split('\n');
                        string Save = "";
                        for (int j = 0; j < text.Length - 1; j++)
                        {
                            if (text[j] != null)
                            {
                                if (text[j] != "\r" && text[j][0] != '#')
                                {
                                    string[] temptext;
                                    temptext = text[j].Split('=');
                                    Dictionary<string, string> temp = (Dictionary<string, string>)Lang[i];
                                    if (i == 0) Save = Save + temptext[0] + "=" + Work1[k].Text + "\r\n";
                                    if (i == 1) Save = Save + temptext[0] + "=" + Work2[k].Text + "\r\n";
                                    if (i == 2) Save = Save + temptext[0] + "=" + Work3[k].Text + "\r\n";
                                    if (i == 3) Save = Save + temptext[0] + "=" + Work4[k].Text + "\r\n";
                                    k++;

                                }
                                else
                                    Save = Save + text[j] + "\n";

                            }
                            else
                                Save = Save + null + "\n";
                        }

                        StorageFile save = await folder.CreateFileAsync("Messages1" + lang[i] + ".lst", CreationCollisionOption.ReplaceExisting);
                        if (save != null)
                        {
                            CachedFileManager.DeferUpdates(save);
                            await FileIO.WriteTextAsync(save, Save);
                            await CachedFileManager.CompleteUpdatesAsync(save);
                        }
                    }
                    catch
                    {
                        TextBlock error = new TextBlock { Text = "Файл не сохранён: Messages1" + lang[i] + ".lst" };
                        Func.Children.Add(error);
                        Grid.SetColumn(error, 0);
                        Grid.SetRow(error, 10 + (i + 1) * 2);

                    }
                }
            }
        }

        private void Zapoln(ArrayList Lang)
        {
            
            int k = 0;
            for (int i = 0; i < Lang.Count; i++)
            {
                ICollection<string> c;
                if (i == 0)
                {
                    WorkEN = (Dictionary<string, string>)Lang[i];
                    c = WorkEN.Keys;
                    Work1 = new TextBox[WorkEN.Count];

                }
                else if (i == 1)
                {
                    WorkAR = (Dictionary<string, string>)Lang[i];
                    c = WorkAR.Keys;
                    Work2 = new TextBox[WorkAR.Count];
                }
                else if (i == 2)
                {
                    WorkMN = (Dictionary<string, string>)Lang[i];
                    c = WorkMN.Keys;
                    Work3 = new TextBox[WorkMN.Count];
                }
                else
                {
                    WorkRU = (Dictionary<string, string>)Lang[i];
                    c = WorkRU.Keys;
                    Work4 = new TextBox[WorkRU.Count];
                }
                TextBox[] Work11 = new TextBox[WorkEN.Count];
                StackPanel Column0 = new StackPanel() { Orientation = Orientation.Vertical };
                StackPanel Column1 = new StackPanel() { Orientation = Orientation.Vertical };
                Table.Children.Add(Column0);
                Table.Children.Add(Column1);
                Grid.SetColumn(Column0, k);
                Grid.SetRow(Column0, 1);
                k++;
                Grid.SetColumn(Column1, k);
                Grid.SetRow(Column1, 1);
                int j = 0;
                foreach (string str in c)
                {
                    TextBlock Work = new TextBlock() { Text = str, TextAlignment= TextAlignment.Center};
                    Thickness myThickness = new Thickness();
                    myThickness.Bottom = 6;
                    myThickness.Left = 0;
                    myThickness.Right = 0;
                    myThickness.Top = 6;
                    Work.Margin = myThickness;
                    Column0.Children.Add(Work);
                    if (i == 0)
                    {
                        Work1[j] = new TextBox();
                        Work1[j].Text = WorkEN[str];
                        Column1.Children.Add(Work1[j]);
                    }
                    if (i == 1)
                    {
                        Work2[j] = new TextBox();
                        Work2[j].Text = WorkAR[str];
                        Column1.Children.Add(Work2[j]);
                    }
                    if (i == 2)
                    {
                        Work3[j] = new TextBox();
                        Work3[j].Text = WorkMN[str];
                        Column1.Children.Add(Work3[j]);
                    }
                    if (i == 3)
                    {
                        Work4[j] = new TextBox();
                        Work4[j].Text = WorkRU[str];
                        Column1.Children.Add(Work4[j]);
                    }
                    j++;

                }
                k++;
            }
        }
    }
}