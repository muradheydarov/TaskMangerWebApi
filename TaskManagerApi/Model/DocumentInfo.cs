using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerApi.Model
{
    public class DocumentInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SN { get; set; }
        public User User { get; set; }
        public string ImportExport { get; set; }
        public int GbNumber { get; set; }
        public string GbState { get; set; }        
        public bool Active { get; set; }
    }
}
