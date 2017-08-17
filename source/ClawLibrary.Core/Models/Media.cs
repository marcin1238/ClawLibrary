namespace ClawLibrary.Core.Models
{
    public class Media
    {
        public string Id { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public int ContentSize { get; set; }
        public byte[] Content { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, ContentType: {ContentType}, FileName: {FileName}, ContentSize: {ContentSize}";
        }
    }
}