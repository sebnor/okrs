using System.IO;

namespace OKRs.Web.Services
{
    public static class StreamExtensions
    {
        public static void Reset(this Stream source)
        {
            if (source != null && source.CanSeek)
            {
                source.Seek(0, SeekOrigin.Begin);
            }
        }
    }
}
