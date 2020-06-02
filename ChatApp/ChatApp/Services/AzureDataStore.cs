using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using ChatApp.Models;
using Core.Models;

namespace ChatApp.Services
{
    public class AzureDataStore : IDataStore
    {
        HttpClient client;
        IEnumerable<Item> items;

        public AzureDataStore()
        {
            #if DEBUG
                var handler = GetInsecureHandler();
                client = new HttpClient(handler);

            #else
                client = new HttpClient();
            #endif
            
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/api/chat/");

            items = new List<Item>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task<User> Login(string username, string password)
        {
            if (IsConnected)
            {
                var json = await client.GetStringAsync($"login?username={username}&password={password}");
                return await Task.Run(() => JsonConvert.DeserializeObject<User>(json));
            }

            return null;
        }

        public async Task<Message> SendMessage(long userSenderId, long userReceiverId, string text)
        {
            if (!IsConnected)
            return null;
                
            var json = await client.GetStringAsync($"sendmessage?userSenderId={userSenderId}&userReceiverId={userReceiverId}&text={text}");
            return await Task.Run(() => JsonConvert.DeserializeObject<Message>(json));
        }

        public async Task<IEnumerable<Chat>> GetChats(long userId)
        {
            if (!IsConnected)
                return null;

            var json = await client.GetStringAsync($"getchats?userId={userId}");
            return await Task.Run(() => JsonConvert.DeserializeObject<List<Chat>>(json));
        }

        public async Task<Chat> GetChatById(long userSenderId, long userReceiverId)
        {
            if (!IsConnected)
                return null;

            var json = await client.GetStringAsync($"sendmessage?userSenderId={userSenderId}&userReceiverId={userReceiverId}");
            return await Task.Run(() => JsonConvert.DeserializeObject<Chat>(json));
        }

        public async Task<User> GetUserById(long userId)
        {
            if (!IsConnected)
                return null;

            var json = await client.GetStringAsync($"getuser?userId={userId}");
            return await Task.Run(() => JsonConvert.DeserializeObject<User>(json));
        }
   
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            if (!IsConnected)
                return null;

            var json = await client.GetStringAsync($"getusers");
            return await Task.Run(() => JsonConvert.DeserializeObject<List<User>>(json));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/item");
                items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Item>>(json));
            }

            return items;
        }

        public async Task<Item> GetItemAsync(string id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/item/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Item>(json));
            }

            return null;
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            if (item == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"api/item", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            if (item == null || item.Id == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/item/{item.Id}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id) && !IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/item/{id}");

            return response.IsSuccessStatusCode;
        }

        //For debug we dont need ssl cert
        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                return true;
            };
            return handler;
        }

        public Task<User> Register(string username, string password)
        {
            throw new NotImplementedException();
        }

       
    }
}
