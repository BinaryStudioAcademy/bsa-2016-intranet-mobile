﻿using System;
using System.Threading.Tasks;

namespace IntranetMobile.Core.Interfaces
{
    [Obsolete("Not used anymore due to optional parameters", true)]
    public interface IRestClient
    {
        Task<T> GetAsync<T>(string resource) where T : new();
        Task<bool> GetAsync(string resource);
        Task<T> GetAsync<T>(string resource, object requestObject) where T : new();
        Task<T> PostAsync<T>(string resource, string contentType) where T : new();
        Task<T> PostAsync<T>(string resource, object requestObject, string contentType) where T : new();
        Task<bool> PostAsync(string resource, object requestObject, string contentType);
        Task<bool> DeleteAsync(string resource);
    }
}