using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerApi.Model
{
    public class DocumentInfoFromBody
    {        
        public int SN { get; set; }
        public string UserName { get; set; }
        public string ImportExport { get; set; }
        public int GbNumber { get; set; }
        public string GbState { get; set; }
    }
}
