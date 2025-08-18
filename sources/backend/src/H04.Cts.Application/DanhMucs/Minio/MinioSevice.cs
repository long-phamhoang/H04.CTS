using Minio;
using Minio.DataModel.Args;
using Volo.Abp.DependencyInjection;
using System.IO;
using System.Threading.Tasks;

using System.Collections.Generic;
using System;

namespace H04.Cts.DanhMucs.Minio
{
    public class MinioService : ITransientDependency
    {
        private readonly IMinioClient _client;
        private const string BucketName = "templatedanhmuc";

        public MinioService()
        {
            _client = new MinioClient()
                .WithEndpoint("localhost", 9000)
                .WithCredentials("minioadmin", "minioadmin")
                .WithSSL(false)
                .Build();
        }
        public async Task<string> DownloadFileAsStreamAsync(string fileName)
        {
            return await _client.PresignedGetObjectAsync(new PresignedGetObjectArgs()
                .WithBucket(BucketName)
                .WithObject(fileName)
                .WithExpiry(3600));
        }
        
    }
}
