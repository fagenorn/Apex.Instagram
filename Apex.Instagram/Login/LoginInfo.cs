using System;
using System.Collections.Generic;
using System.Linq;

using Apex.Instagram.Constants;
using Apex.Instagram.Model.Internal;

using MessagePack;

namespace Apex.Instagram.Login
{
    [MessagePackObject]
    public class LoginInfo
    {
        [Key(0)]
        public bool IsLoggedIn { get; internal set; }

        [Key(1)]
        public Dictionary<string, string> ZrRules { get; internal set; }

        [Key(2)]
        public string ZrToken { get; internal set; }

        [Key(3)]
        public int ZrExpires { get; internal set; }

        [Key(4)]
        public LastAction LastLogin { get; internal set; } = new LastAction(Delays.Instance.AppRefreshInterval);

        [Key(5)]
        public Dictionary<string, Dictionary<string, string>> Experiments { get; internal set; }

        [Key(6)]
        public LastAction LastExperiments { get; internal set; } = new LastAction(TimeSpan.FromMinutes(30));

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