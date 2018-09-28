﻿using System;
using System.Threading.Tasks;

using Apex.Instagram.Exception;

namespace Apex.Instagram.Request.Instagram.Paginate
{
    internal class AutoPaginateWithRankToken<T> : IAutoPaginate<T> where T : Response.JsonMap.Response, IPaginate
    {
        private readonly Func<string, string, Task<T>> _action;

        private readonly string _rankToken;

        private bool _end;

        private string _maxId;

        public AutoPaginateWithRankToken(Func<string, string, Task<T>> action)
        {
            _action    = action;
            _rankToken = Utils.Instagram.Instance.GenerateUuid();
        }

        public bool HasMore => !_end;

        public async Task<T> Next()
        {
            if ( _end )
            {
                throw new EndOfPageException();
            }

            var response = await _action(_maxId, _rankToken);
            _maxId = response.NextMaxId;

            if ( string.IsNullOrWhiteSpace(_maxId) )
            {
                _end = true;
            }

            return response;
        }
    }
}