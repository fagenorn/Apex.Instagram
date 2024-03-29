﻿using System;
using System.Threading.Tasks;

using Apex.Instagram.API.Exception;

namespace Apex.Instagram.API.Request.Instagram.Paginate
{
    public class AutoPaginate<T> : IAutoPaginate<T> where T : Response.JsonMap.Response, IPaginate
    {
        private readonly Func<string, Task<T>> _action;

        private bool _end;

        private string _maxId;

        public AutoPaginate(Func<string, Task<T>> action) { _action = action; }

        public bool HasMore => !_end;

        public async Task<T> NextAsync()
        {
            if ( _end )
            {
                throw new EndOfPageException();
            }

            var response = await _action(_maxId).ConfigureAwait(false);
            _maxId = response.NextMaxId;

            if ( string.IsNullOrWhiteSpace(_maxId) )
            {
                _end = true;
            }

            return response;
        }
    }
}