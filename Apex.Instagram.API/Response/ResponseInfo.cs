using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Apex.Instagram.API.Request.Exception;
using Apex.Instagram.API.Request.Exception.EndpointException;
using Apex.Instagram.API.Response.Serializer;

namespace Apex.Instagram.API.Response
{
    internal class ResponseInfo<T> : IDisposable where T : JsonMap.Response
    {
        private readonly HttpResponseMessage _response;

        private bool IsOk
        {
            get
            {
                if ( !_response.IsSuccessStatusCode || !Response.IsOk() )
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
                foreach ( var exceptionMap in Constants.Response.Instance.StatusCodeMap )
                {
                    if ( exceptionMap.TryGet((int) _response.StatusCode, out var result) )
                    {
                        throw result;
                    }
                }

                if ( _response.Content != null )
                {
                    Response = await JsonSerializer.DeserializeAsync<T>(await _response.Content.ReadAsStreamAsync()
                                                                                       .ConfigureAwait(false), JsonSerializerDefaultOptions.Instance)
                                                   .ConfigureAwait(false);
                }

                if ( Response?.Status == null )
                {
                    throw new JsonException("Response isn't valid.");
                }

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
                            result.Response = Response;

                            throw result;
                        }
                    }
                }

                throw new EndpointException(errors[0])
                      {
                          Response = Response
                      };
            }
            catch (RequestException)
            {
                throw;
            }
            catch (JsonException e)
            {
                switch ( _response.StatusCode )
                {
                    case HttpStatusCode.BadRequest:

                        throw new BadRequestException();
                    case HttpStatusCode.Unauthorized:

                        throw new UnauthorizedException();
                    case HttpStatusCode.Forbidden:

                        throw new ForbiddenException();
                    case HttpStatusCode.NotFound:

                        throw new NotFoundException();
                    case HttpStatusCode.MethodNotAllowed:

                        throw new MethodNotAllowedException();
                    case HttpStatusCode.NotAcceptable:

                        throw new NotAcceptableException();
                    case HttpStatusCode.ProxyAuthenticationRequired:

                        throw new ProxyAuthenticationRequiredException();
                    case HttpStatusCode.RequestTimeout:

                        throw new RequestTimeoutException();
                    case HttpStatusCode.Conflict:

                        throw new ConflictException();
                    case HttpStatusCode.Gone:
                    case HttpStatusCode.LengthRequired:
                    case HttpStatusCode.PreconditionFailed:
                    case HttpStatusCode.RequestEntityTooLarge:
                    case HttpStatusCode.RequestUriTooLong:
                    case HttpStatusCode.UnsupportedMediaType:
                    case HttpStatusCode.RequestedRangeNotSatisfiable:
                    case HttpStatusCode.ExpectationFailed:
                    case HttpStatusCode.UpgradeRequired:
                    case HttpStatusCode.InternalServerError:
                    case HttpStatusCode.NotImplemented:
                    case HttpStatusCode.BadGateway:
                    case HttpStatusCode.ServiceUnavailable:
                    case HttpStatusCode.GatewayTimeout:
                    case HttpStatusCode.HttpVersionNotSupported:

                        throw new EndpointException(_response.StatusCode.ToString());
                    case HttpStatusCode.Continue:
                    case HttpStatusCode.SwitchingProtocols:
                    case HttpStatusCode.OK:
                    case HttpStatusCode.Created:
                    case HttpStatusCode.Accepted:
                    case HttpStatusCode.NonAuthoritativeInformation:
                    case HttpStatusCode.NoContent:
                    case HttpStatusCode.ResetContent:
                    case HttpStatusCode.PartialContent:
                    case HttpStatusCode.MultipleChoices:
                    case HttpStatusCode.MovedPermanently:
                    case HttpStatusCode.Found:
                    case HttpStatusCode.SeeOther:
                    case HttpStatusCode.NotModified:
                    case HttpStatusCode.UseProxy:
                    case HttpStatusCode.Unused:
                    case HttpStatusCode.TemporaryRedirect:
                    case HttpStatusCode.PaymentRequired:

                        throw new EndpointException("Failed to map/parse response: {0}", e, e.Path);
                    default:

                        throw new UnknowEndpointException("Unknow status code: {0}", e, (int) _response.StatusCode);
                }
            }
            catch (System.Exception e)
            {
                throw new RequestException("Unknow exception", e);
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