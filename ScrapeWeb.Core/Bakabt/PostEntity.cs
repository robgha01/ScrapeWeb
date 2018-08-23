using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using ScrapeWeb.Core.Abstraction;
using ScrapeWeb.Core.Annotations;
using ScrapeWeb.Core.Helpers;

namespace ScrapeWeb.Core.Bakabt
{
    public class PostEntity : IPost
    {
        private string _title;
        private byte[] _cover;
        private IList<string> _categories;
        private string _url;
        private IList<string> _generes;

        public string Url
        {
            get { return _url; }
            set
            {
                _url = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public byte[] Cover
        {
            get { return _cover; }
            set
            {
                _cover = value;
                OnPropertyChanged();
            }
        }

        public IList<string> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        public IList<string> Generes
        {
            get { return _generes; }
            set
            {
                _generes = value;
                OnPropertyChanged();
            }
        }

        public string DisplayCategories
        {
            get
            {
                return String.Join(", ", Categories);
            }
        }

        public string DisplayGeneres
        {
            get
            {
                return String.Join(", ", Generes);
            }
        }

        public PostEntity()
        {
            Categories = new List<string>();
            Generes = new List<string>();
        }

        public void SaveCover(string path)
        {
            ImageCodecInfo myImageCodecInfo = ImageHelper.FindJpegEncoder();
            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 90L);

            using (MemoryStream ms = new MemoryStream(Cover))
            using (Image image = Image.FromStream(ms))
            {
                image.Save(path, myImageCodecInfo, encoderParameters);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}