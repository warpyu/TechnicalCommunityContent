using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PdfReviewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReviewPage : Page
    {
        public ReviewPage()
        {
            this.InitializeComponent();

            inkCanvas.InkPresenter.InputDeviceTypes = CoreInputDeviceTypes.Mouse | CoreInputDeviceTypes.Pen | CoreInputDeviceTypes.Touch;
            
            this.DataContext = App.ViewModel.SelectedDocument;

        }
        
        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            RenderInkImageAsync();
        }

        private async void RenderInkImageAsync()
        {

            IReadOnlyList<InkStroke> currentStrokes = inkCanvas.InkPresenter.StrokeContainer.GetStrokes();

            if (currentStrokes.Count > 0)
            {
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.TemporaryFolder;
                Windows.Storage.StorageFile file = await storageFolder.CreateFileAsync($"{Guid.NewGuid()}.png", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                if (file != null)
                {
                    Windows.Storage.CachedFileManager.DeferUpdates(file);

                    IRandomAccessStream stream = await file.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);

                    using (IOutputStream outputStream = stream.GetOutputStreamAt(0))
                    {
                        await inkCanvas.InkPresenter.StrokeContainer.SaveAsync(outputStream);
                        await outputStream.FlushAsync();
                    }
                    stream.Dispose();
                }

                BitmapImage bm = new BitmapImage(new Uri($"ms-appdata:///temp/{file.Name}"));

                App.ViewModel.SelectedDocument.AnnotationImage = bm;

            }

            Frame.GoBack();

        }

        private void OnUndoClick(object sender, RoutedEventArgs e)
        {
            this.inkCanvas.InkPresenter.StrokeContainer.Clear();
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
