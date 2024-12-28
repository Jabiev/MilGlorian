using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MilGlorian.Application.Abstract.Services.Storage.Azure;

namespace MilGlorian.Infrastructure.Services.Storage.Azure;

public class AzureStorage : IAzureStorage
{
    private readonly BlobServiceClient _blobServiceClient;
    private BlobContainerClient _blobContainerClient;
    private readonly string _connectionString;
    private IConfiguration _configuration;

    public AzureStorage(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = $"DefaultEndpointsProtocol=https;AccountName={_configuration["AzureBlobStorageSettings:storageAccountName"]};AccountKey={ _configuration["AzureBlobStorageSettings:storageAccountKey"]};EndpointSuffix=core.windows.net";
        _blobServiceClient = new(_connectionString);
    }

    public async Task DeleteAsync(string containerName, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
        await blobClient.DeleteAsync();
    }

    public List<string> GetFiles(string containerName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        return _blobContainerClient.GetBlobs().Select(f => f.Name).ToList();
    }

    public bool HasFile(string containerName, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        return _blobContainerClient.GetBlobs().Any(f => f.Name == fileName);
    }

    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, IFormFileCollection files)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await _blobContainerClient.CreateIfNotExistsAsync();
        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

        List<(string fileName, string pathOrContainerName)> datas = new();
        foreach (var file in files)
        {
            BlobClient blobClient = _blobContainerClient.GetBlobClient(file.Name);
            await blobClient.UploadAsync(file.OpenReadStream());
            datas.Add((file.Name, containerName));
        }
        return datas;
    }
}