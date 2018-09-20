using System;
using System.Collections.Generic;
using System.Linq;

using Apex.Instagram.Constants;
using Apex.Instagram.Model.Internal;

using MessagePack;

namespace Apex.Instagram.Login
{
    [MessagePackObject]
    internal class LoginInfo
    {
        [Key(0)]
        public bool IsLoggedIn { get; set; }

        [Key(1)]
        public Dictionary<string, string> ZrRules { get; set; }

        [Key(2)]
        public string ZrToken { get; set; }

        [Key(3)]
        public int ZrExpires { get; set; }

        [Key(4)]
        public LastAction LastLogin { get; set; } = new LastAction(Delays.Instance.AppRefreshInterval);

        [Key(5)]
        public Dictionary<string, Dictionary<string, string>> Experiments { get; set; }

        [Key(6)]
        public LastAction LastExperiments { get; set; } = new LastAction(TimeSpan.FromMinutes(120));

        public bool IsExperimentEnabled(string experiment, string param, bool @default = false)
        {
            string[] goodValues =
            {
                "enabled",
                "true",
                "1"
            };

            if ( Experiments.ContainsKey(experiment) && Experiments[experiment]
                     .ContainsKey(param) )
            {
                return goodValues.Contains(Experiments[experiment][param]);
            }

            return @default;
        }

        public string GetExperimentParam(string experiment, string param, string @default = null)
        {
            if ( Experiments.ContainsKey(experiment) && Experiments[experiment]
                     .ContainsKey(param) )
            {
                return Experiments[experiment][param];
            }

            return @default;
        }
    }
}