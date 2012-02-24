namespace OnlineStore.Domain
{
    public class ImageInfo
    {
        public string Path { get; private set; }

        public ImageInfo(string path)
        {
            this.Path = path;
        }

        public string ImageType
        {
            get
            {
                return this.Path.Substring(this.Path.IndexOf(".") + 1);
            }
        }
    }
}