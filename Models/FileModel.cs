using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace SqlFileStreams.Models
{
    [Table("Files")]
    public class FileModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte[] File { get; set; } //Column with filestream enabled
        public byte[] File1 { get; set; } //Column with filestream disabled
    }

    public class FileViewModel
    {
        public string Title { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}