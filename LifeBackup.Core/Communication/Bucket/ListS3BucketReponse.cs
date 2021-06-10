using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeBackup.Core.Communication.Bucket
{
    public class ListS3BucketReponse
    {
        public string BucketName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
