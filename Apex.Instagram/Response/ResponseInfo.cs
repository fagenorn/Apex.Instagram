using System;
using System.Net.Http;
using System.Threading.Tasks;

using Apex.Instagram.Request.Exception;

using Utf8Json;

namespace Apex.Instagram.Response
{
    internal class ResponseInfo<T> : IDisposable where T : JsonMap.Response
    {
        private readonly HttpResponseMessage _response;

        private bool IsOk
        {
            get
            {
                if ( !_response.IsSuccessStatusCode || Response.Status != Constants.Response.Instance.StatusOk )
                {
                    return false;
                }

                return true;
            }
        }

        public T Response { get; private set; }

        public void Dispose() { _response?.Dispose(); }

        #region Constructor

        private ResponseInfo(HttpResponseMessage response) { _response = response; }

        private async Task<ResponseInfo<T>> InitializeAsync()
        {
            try
            {
                Response = await JsonSerializer.DeserializeAsync<T>(await _response.Content.ReadAsStreamAsync());

                if ( IsOk )
                {
                    return this;
                }

                var errors = Response.GetErrors();
                foreach ( var error in errors )
                {
                    foreach ( var exceptionMap in Constants.Response.Instance.ExceptionMap )
                    {
                        if ( exceptionMap.TryGet(error, out var result) )
                        {
                            throw result;
                        }
                    }
                }

                throw new EndpointException(errors[0]);
            }
            catch
            {
                throw new RequestException(_response.StatusCode.ToString());
            }
        }

        public static Task<ResponseInfo<TJ>> CreateAsync<TJ>(HttpResponseMessage response) where TJ : JsonMap.Response
        {
            var ret = new ResponseInfo<TJ>(response);

            return ret.InitializeAsync();
        }

        #endregion
    }
}