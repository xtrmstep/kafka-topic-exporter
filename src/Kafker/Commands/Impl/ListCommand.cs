﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Kafker.Configurations;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kafker.Commands
{
    public class ConfigCommand : IConfigCommand
    {
        private readonly IConsole _console;
        private readonly KafkerSettings _settings;

        public ConfigCommand(IConsole console, KafkerSettings settings)
        {
            _console = console;
            _settings = settings;
        }

        public async Task<int> InvokeAsync()
        {
            var json = JObject.FromObject(_settings);
            var txt = json.ToString(Formatting.Indented);
            await _console.Out.WriteLineAsync(txt);
            return await Task.FromResult(Constants.RESULT_CODE_OK).ConfigureAwait(false);
        }
    }
}