using LifeBackup.Core.Communication.Bucket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeBackup.Core.Communication.Interfaces
{
    public interface IBucketRepository
    {
        Task<bool> DoesS3BucketExist(string bucketName);
        Task<CreateBucketReponse> CreateBucket(string bucketName);
        Task<IEnumerable<ListS3BucketReponse>> ListBuckets();
        Task DeleteBucket(string bucketName);
    }
}
