using Microsoft.JSInterop;

namespace BlazingPizza.Dependencias
{

    public class CacheStorageAccessor
    {
        //private readonly InjectServicesApi _injectServices;

        //public CacheStorageAccessor(InjectServices injectServices)
        //{
        //    _injectServices = injectServices;
        //}
        //#region Validação do cache
        //private async Task WaitForReference()
        //{
        //    if (InjectServicesApi._jSRuntimeObjectReference.IsValueCreated is false)
        //    {
        //        _injectServices._jSRuntimeObjectReference = new(await _injectServices._jSRuntime.InvokeAsync<IJSObjectReference>
        //            ("import", "/js/CacheStorageAcesso.js"));
        //    }
        //}

        //public async Task DisposeAsync()
        //{
        //    if (_injectServices._jSRuntimeObjectReference.IsValueCreated)
        //    {
        //        await _injectServices._jSRuntimeObjectReference.Value.DisposeAsync();
        //    }

        //}
        //#endregion Fim da validação do cache
        //#region Metodos de criação do cache
        //public async Task StoreAsync(HttpRequestMessage requestMessage, HttpResponseMessage responseMessage)
        //{
        //    await WaitForReference();
        //    string requestMethod = requestMessage.Method.Method;
        //    string requestBody = await GetRequestBodyAsync(requestMessage);
        //    string responseBody = await responseMessage.Content.ReadAsStringAsync();
        //    await _injectServices._jSRuntimeObjectReference.Value.InvokeVoidAsync("specialsCacheLocal", requestMessage.RequestUri, requestMethod, requestBody, responseBody);
        //}

        //public async Task<string> GetAsync(HttpRequestMessage requestMessage)
        //{
        //    await WaitForReference();
        //    string requestMethod = requestMessage.Method.Method;
        //    string requestBody = await GetRequestBodyAsync(requestMessage);
        //    return await _injectServices._jSRuntimeObjectReference.Value.InvokeAsync<string>("get", requestMessage.RequestUri, requestMethod, requestBody);
        //}
        //#endregion Fim dos metodos de criação do cache
        //#region Metodos de exclusão do cache
        //public async Task RemoveAsync(HttpRequestMessage requestMessage)
        //{
        //    await WaitForReference();
        //    string requestMethod = requestMessage.Method.Method;
        //    string requestBody = await GetRequestBodyAsync(requestMessage);
        //    await _injectServices._jSRuntimeObjectReference.Value.InvokeVoidAsync("remove", requestMessage.RequestUri, requestMethod, requestBody);
        //}

        //public async Task RemoveAllAsync()
        //{
        //    await WaitForReference();
        //    await _injectServices._jSRuntimeObjectReference.Value.InvokeVoidAsync("removeAll");
        //}
        //#endregion Fim dos metodos de exclusão do cache
        //public static async Task <string> GetRequestBodyAsync(HttpRequestMessage requestMessage)
        //{
        //    string requestBody = "";
        //    if (requestMessage.Content is not null)
        //    {
        //        requestBody = await requestMessage.Content.ReadAsStringAsync() ?? "";
        //    }
        //    return requestBody;
        //}
    }
}
