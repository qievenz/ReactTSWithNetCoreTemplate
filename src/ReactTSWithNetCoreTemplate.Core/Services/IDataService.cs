﻿using ReactTSWithNetCoreTemplate.Core.Entities;

namespace ReactTSWithNetCoreTemplate.Core.Services
{
    public interface IDataService
    {
        Task<IEnumerable<Data>> GetAllDatasAsync();
        Task<Data?> GetDataByIdAsync(int id);
        Task<Data> CreateDataAsync(Data data);
        Task<bool> UpdateDataAsync(int id, Data data);
        Task<bool> DeleteDataAsync(int id);
    }
}
