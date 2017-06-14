using PdfReviewer.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Pdf;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PdfReviewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        

        public MainPage()
        {
            this.InitializeComponent();
            this.Loading += MainPage_Loading;
        }

        private void MainPage_Loading(FrameworkElement sender, object args)
        {   
            this.DataContext = App.ViewModel;
        }
         
        private void OnOpenFileClick(object sender, RoutedEventArgs e)
        {
            BrowseForFileAsync();
        }

        private async void BrowseForFileAsync()
        {
            var picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".pdf");
            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                App.ViewModel.AllDocuments.Clear();
                var pdfDocument = await PdfDocument.LoadFromFileAsync(file);

                var pageCount = pdfDocument.PageCount;

                loadingProgress.Visibility = Visibility.Visible;
                loadingProgress.Minimum = 1;
                loadingProgress.Maximum = pageCount;

                for (int i = 0; i < pageCount; i++)
                {

                    using (PdfPage page = pdfDocument.GetPage((uint)i))
                    {
                        loadingProgress.Value = i + 1;

                        var stream = new InMemoryRandomAccessStream();

                        var options1 = new PdfPageRenderOptions();

                        await page.RenderToStreamAsync(stream);

                        BitmapImage src = new BitmapImage();

                        await src.SetSourceAsync(stream);

                        App.ViewModel.AllDocuments.Add(new Common.RenderedDocument()
                        {
                            PageNumber = i,
                            PageImage = src,
                        });
                    }
                }

            }
            
            loadingProgress.Visibility = Visibility.Collapsed;
        }

        private void OnDocumentClick(object sender, ItemClickEventArgs e)
        {
            App.ViewModel.SelectedDocument = e.ClickedItem as RenderedDocument;

            Frame.Navigate(typeof(ReviewPage));
        }
    }
}
