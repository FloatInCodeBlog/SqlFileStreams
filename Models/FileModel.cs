using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace SqlFileStreams.Models
{
    [Table("Files")]
    public class FileModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte[] File { get; set; }
    }

    public class FileViewModel
    {
        public string Title { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}