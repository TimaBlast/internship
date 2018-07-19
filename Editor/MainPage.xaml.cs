using System;
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
       // SplitView temp;
        string[] sEN, sAR, sMN, sRU;
        ListBox CaseNameEN, CaseNameAR, CaseNameMN, CaseNameRU, TranslEN, TranslAR, TranslMN, TranslRU;
        List<int> numberEN, numberAR, numberMN, numberRU;
        private async void OpenFile_OnClick(object sender, RoutedEventArgs e)
        {
            FolderPicker folderPicker = new FolderPicker();
            folderPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");
            StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                StorageFile FileEN = await folder.GetFileAsync("Messages.lst");
                if (FileEN != null)
                {
                    var stream = await FileEN.OpenAsync(FileAccessMode.Read);
                    using (StreamReader reader = new StreamReader(stream.AsStream()))
                    {
                        numberEN = new List<int>();
                        CaseNameEN = new ListBox();
                        TranslEN = new ListBox();
                        sEN = reader.ReadToEnd().Split('\n');
                        for(int i=0; i<sEN.Length-1; i++)
                        {
                            if(sEN[i]!=null)
                            {
                                if (sEN[i]!= "\r" && sEN[i][0] != '#')
                                {
                                    string[] temp;
                                    temp = sEN[i].Split('=');
                                    CaseNameEN.Items.Add(temp[0]);
                                    TranslEN.Items.Add(temp[1]);
                                    numberEN.Add(i);
                                }
                            }
                        }
                    }
                }
                StorageFile FileAR = await folder.GetFileAsync("Messages-ar.lst");
                if (FileAR != null)
                {
                    var stream = await FileAR.OpenAsync(FileAccessMode.Read);
                    using (StreamReader reader = new StreamReader(stream.AsStream()))
                    {
                        numberAR = new List<int>();
                        CaseNameAR = new ListBox();
                        TranslAR = new ListBox();
                        sAR = reader.ReadToEnd().Split('\n');
                        for (int i = 0; i < sAR.Length - 1; i++)
                        {
                            if (sAR[i] != null)
                            {
                                if (sAR[i] != "\r" && sAR[i][0] != '#')
                                {
                                    string[] temp;
                                    temp = sAR[i].Split('=');
                                    CaseNameAR.Items.Add(temp[0]);
                                    TranslAR.Items.Add(temp[1]);
                                    numberAR.Add(i);
                                }
                            }
                        }
                    }
                }
                StorageFile FileMN = await folder.GetFileAsync("Messages-mn.lst");
                if (FileMN != null)
                {
                    var stream = await FileMN.OpenAsync(FileAccessMode.Read);
                    using (StreamReader reader = new StreamReader(stream.AsStream()))
                    {
                        numberMN = new List<int>();
                        CaseNameMN = new ListBox();
                        TranslMN = new ListBox();
                        sMN = reader.ReadToEnd().Split('\n');
                        for (int i = 0; i < sMN.Length - 1; i++)
                        {
                            if (sMN[i] != null)
                            {
                                if (sMN[i] != "\r" && sMN[i][0] != '#')
                                {
                                    string[] temp;
                                    temp = sMN[i].Split('=');
                                    CaseNameMN.Items.Add(temp[0]);
                                    TranslMN.Items.Add(temp[1]);
                                    numberMN.Add(i);
                                }
                            }
                        }
                    }
                }
                StorageFile FileRU = await folder.GetFileAsync("Messages-ru.lst");
                if (FileRU != null)
                {
                    var stream = await FileRU.OpenAsync(FileAccessMode.Read);
                    using (StreamReader reader = new StreamReader(stream.AsStream()))
                    {
                        numberRU = new List<int>();
                        CaseNameRU = new ListBox();
                        TranslRU = new ListBox();
                        sRU = reader.ReadToEnd().Split('\n');
                        for (int i = 0; i < sRU.Length - 1; i++)
                        {
                            if (sRU[i] != null)
                            {
                                if (sRU[i] != "\r" && sRU[i][0] != '#')
                                {
                                    string[] temp;
                                    temp = sRU[i].Split('=');
                                    CaseNameRU.Items.Add(temp[0]);
                                    TranslRU.Items.Add(temp[1]);
                                    numberRU.Add(i);
                                }
                            }
                        }
                    }
                }
                for (int i=0; i<CaseNameEN.Items.Count; i++)
                {
                    Button ContentS = new Button();
                    ContentS.Content = CaseNameEN.Items[i].ToString();
                    ContentS.HorizontalAlignment = HorizontalAlignment.Center;
                    ContentS.Click += Button_Click;
                  //  ColumnDefinition colDef1= new ColumnDefinition();
                    Grid PaneS = new Grid();
                  //  PaneS.ColumnDefinitions.Add(colDef1);
                    TextBox WorkEN = new TextBox() { Text = TranslEN.Items[i].ToString() };
                    TextBox WorkAR = new TextBox() { Text = TranslAR.Items[i].ToString() };
                    //TextBox WorkMN = new TextBox() { Text = TranslMN.Items[i].ToString() };
                    TextBox WorkRU = new TextBox() { Text = TranslRU.Items[i].ToString() };
                    PaneS.Children.Add(WorkEN);
                    PaneS.Children.Add(WorkAR);
                    //PaneS.Children.Add(WorkMN);
                    PaneS.Children.Add(WorkRU);
                    Grid.SetColumn(WorkEN, 0);
                    Grid.SetColumn(WorkEN, 1);
                    Grid.SetColumn(WorkEN, 2);
                    Grid.SetColumn(WorkEN, 3);

                    SplitView temp = new SplitView
                    {
                        
                        Background = new SolidColorBrush(Windows.UI.Colors.White),
                        DisplayMode = SplitViewDisplayMode.Inline,
                        CompactPaneLength = 0,
                        OpenPaneLength = 500,
                        PaneBackground = new SolidColorBrush(Windows.UI.Colors.LightBlue),
                        Content = ContentS,
                        Pane = PaneS
                    };
                    Sobject.Items.Add(temp);
                }
            }

        }
        private async void SaveFile_OnClick(object sender, RoutedEventArgs e)
            {
            FolderPicker folderPicker = new FolderPicker();
            folderPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");
            StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                // StorageFile FileRU = await folder.GetFileAsync("Messages-ru.lst");
                for (int i = 0; i < CaseNameEN.Items.Count; i++)
                {
                    string[] temp= sEN[numberEN[i]].Split('=');
                    temp[1] = TranslEN.Items[i].ToString();
                    sEN[numberEN[i]] = temp[0] + "=" + temp[1];

                    temp[1] = TranslAR.Items[i].ToString();
                    sAR[numberAR[i]] = temp[0] + "=" + temp[1];
                    //temp[1] = TranslMN.Items[i].ToString();
                    //sMN[numberMN[i]] = temp[0] + "=" + temp[1];
                    temp[1] = TranslRU.Items[i].ToString();
                    sRU[numberRU[i]] = temp[0] + "=" + temp[1];
                }

                StorageFile saveEN = await folder.CreateFileAsync("Messages1.lst",CreationCollisionOption.ReplaceExisting);
                string text = sEN[0];
                for (int i = 1; i < sEN.Length; i++)
                    text = text + "\n" + sEN[i];
                await FileIO.WriteTextAsync(saveEN, text);
                StorageFile saveAR = await folder.CreateFileAsync("Messages-ar1.lst", CreationCollisionOption.ReplaceExisting);
                text = sAR[0];
                for (int i = 1; i < sAR.Length; i++)
                    text = text + "\n" + sAR[i];
                await FileIO.WriteTextAsync(saveAR, text);
                StorageFile saveMN = await folder.CreateFileAsync("Messages-mn1.lst", CreationCollisionOption.ReplaceExisting);
                text = sMN[0];
                for (int i = 1; i < sMN.Length; i++)
                    text = text + "\n" + sMN[i];
                await FileIO.WriteTextAsync(saveMN, text);
                StorageFile saveRU = await folder.CreateFileAsync("Messages-ru1.lst", CreationCollisionOption.ReplaceExisting);
                text = sRU[0];
                for (int i = 1; i < sRU.Length; i++)
                    text = text + "\n" + sRU[i];
                await FileIO.WriteTextAsync(saveRU, text);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           SplitView temp = (SplitView)Sobject.Items[1];
           temp.IsPaneOpen = !temp.IsPaneOpen;
            Sobject.Items.Insert(1, temp);
        }
    }
}
