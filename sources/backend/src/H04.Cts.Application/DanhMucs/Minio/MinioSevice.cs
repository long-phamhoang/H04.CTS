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
        private const string BucketNameHistory = "uploadhistory";

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

        public async Task UploadAsync(Stream stream, string objectName, string contentType = "application/octet-stream")
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (string.IsNullOrWhiteSpace(objectName)) throw new ArgumentNullException(nameof(objectName));

            // Ensure bucket exists
            var exists = await _client.BucketExistsAsync(new BucketExistsArgs().WithBucket(BucketNameHistory));
            if (!exists)
            {
                await _client.MakeBucketAsync(new MakeBucketArgs().WithBucket(BucketNameHistory));
            }

            // Ensure stream is seekable and we know length
            Stream uploadStream = stream;
            long size;
            if (!stream.CanSeek)
            {
                var ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                ms.Position = 0;
                uploadStream = ms;
                size = ms.Length;
            }
            else
            {
                if (stream.Position != 0) stream.Position = 0;
                size = stream.Length;
            }

            var putArgs = new PutObjectArgs()
                .WithBucket(BucketNameHistory)
                .WithObject(objectName)
                .WithStreamData(uploadStream)
                .WithObjectSize(size)
                .WithContentType(contentType);

            await _client.PutObjectAsync(putArgs);
        }

        public async Task<Stream> GetObjectStreamAsync(string objectName)
        {
            if (string.IsNullOrWhiteSpace(objectName)) throw new ArgumentNullException(nameof(objectName));
            var ms = new MemoryStream();
            var getArgs = new GetObjectArgs()
                .WithBucket(BucketNameHistory)
                .WithObject(objectName)
                .WithCallbackStream(s => s.CopyTo(ms));
            await _client.GetObjectAsync(getArgs);
            ms.Position = 0;
            return ms;
        }
    }
}
