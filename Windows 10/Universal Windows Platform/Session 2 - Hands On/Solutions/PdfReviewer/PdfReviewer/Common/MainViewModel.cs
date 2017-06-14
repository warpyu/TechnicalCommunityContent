using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace PdfReviewer.Common
{
    public class RenderedDocument
    {
        public int PageNumber { get; set; }
        public BitmapImage PageImage { get; set; }
        public BitmapImage AnnotationImage { get; set; }
    }
    
    public class MainViewModel
    {
        public MainViewModel()
        {
            this.AllDocuments = new ObservableCollection<RenderedDocument>();
        }

        public ObservableCollection<RenderedDocument> AllDocuments { get; set; }
        public RenderedDocument SelectedDocument { get; set; }
    }
}
