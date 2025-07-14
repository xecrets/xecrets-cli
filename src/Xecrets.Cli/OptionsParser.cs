#region Copyright and GPL License

/*
 * Xecrets Cli - Copyright © 2022-2025 Svante Seleborg, All Rights Reserved.
 *
 * This code file is part of Xecrets Cli, parts of which in turn are derived from AxCrypt as licensed under GPL v3 or later.
 * 
 * However, this code is not derived from AxCrypt and is separately copyrighted and only licensed as follows unless
 * explicitly licensed otherwise. If you use any part of this code in your software, please see https://www.gnu.org/licenses/
 * for details of what this means for you.
 *
 * Xecrets Cli is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 *
 * Xecrets Cli is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
 * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License along with Xecrets Cli.  If not, see <https://www.gnu.org/licenses/>.
 *
 * The source repository can be found at https://github.com/ please go there for more information, suggestions and
 * contributions. You may also visit https://www.axantum.com for more information about the author.
*/

#endregion Copyright and GPL License

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

using AxCrypt.Abstractions;

using NDesk.Options;

using Xecrets.Cli.Public;

namespace Xecrets.Cli;

internal class OptionsParser
{
    private ExportableOptionCollection BuildOptionSet(List<ParsedOp> parsed, List<string> extra)
    {
        var cliVersion = XfExportVersion.CliVersion;

        ExportableOptionCollection optionSet = new ExportableOptionCollection(cliVersion, new RunningArguments(parsed, extra))
        {
            {"a|armor", XfOpCode.AsciiArmor,
                ":Use ASCII armor for encryption and decryption.",
                (ora, op, armor) => ora.Add(armor != null ? op : XfOpCode.NoAsciiArmor) },
            {"b|use-public-key=", XfOpCode.UsePublicKey,
                "{email(s)}:Use selected loaded public key(s) for encryption.",
                (ora, op, email) => ora.AddManyRunning(op, email)},
            {"c|create-key-pair={}", XfOpCode.CreateKeyPair,
                "{email} {encrypted}:Create a key pair for an email moniker, in an encrypted file.",
                (ora, op, email, to) => ora.Add(op, email, to)},
            {"d|decrypt-file-to={}", XfOpCode.DecryptTo,
                "{encrypted} {clear}:Decrypt a file to the given file path.",
                (ora, op, from, to) => ora.Add(op, from, to) },
            {"e|encrypt-file-to={}", XfOpCode.EncryptTo,
                "{clear} {encrypted} [{original}]:Encrypt a file to given file path w/optional original name.",
                (ora, op, from, to) => ora.AddOneRunning(op, from, to) },
            {"f|file=", XfOpCode.OptionsFromFile,
                "{name}:Take options from a file (programmatic).",
                (ora, op, name) => { ora.Add(op); RecursivelyParseFromFile(name, parsed, extra); } },
            {"g|full-license",XfOpCode.GplLicense,
                ":Display the full licenses.",
                (ora, op, gpl) => ora.Add(gpl != null ? op : XfOpCode.None) },
            {"h|?|help", XfOpCode.Help,
                ":Display this helpful help message, use again for details.",
                (ora, op, help) => ora.Add(help != null ? op : XfOpCode.None) },
            {"i|id=", XfOpCode.Id,
                ":Arbitrary id which is returned in JSON-logging.",
                (ora, op, id) => ora.Add(op, id) },
            {"j|json-log", XfOpCode.SdkJsonLog,
                ":Enable JSON console logging (programmatic).",
                (ora, op, json) => ora.Add(json != null ? op : XfOpCode.NoLog) },
            {"k|use-key-pair=", XfOpCode.UseKeyPair,
                "{encrypted}:Use a key-pair, from an encrypted file path. Password is required.",
                (ora, op, file) => ora.Add(op, file) },
            {"l|decrypt-file-to-folder=", XfOpCode.DecryptToFolder,
                "{encrypted} [{folder}]:Decrypt a file with it's original name to a destination folder." +
                ":Default {folder} is the {encrypted}'s folder.",
                (ora, op, from) => ora.AddOneRunning(op, from) },
            { "m|compress", XfOpCode.Compress,
                ":Compress files before encryption (default true).",
                (ora, op, compress) => ora.Add(compress != null ? op : XfOpCode.NoCompress) },
            {"n|environment=", XfOpCode.EnvironmentOption,
                "{variable}:Take options from environment variable (programmatic).",
                (ora, op, variable) => { ora.Add(op); RecursivelyParseFromString(variable, parsed, extra); } },
            {"o|progress", XfOpCode.ProgressLog,
                ":Continuously log progress.",
                (ora, op, progress) => ora.Add(progress != null ? op : XfOpCode.NoProgress) },
            {"p|password=", XfOpCode.Password,
                "{secret}:A password to use for encryption or decryption.",
                (ora, op, pw) => ora.Add(op, pw) },
            {"q|quiet", XfOpCode.Quiet,
                ":Do not display any messages or progress (global).",
                (ora, op, quiet) => {IsQuiet = quiet != null; ora.Add(XfOpCode.NoLog); } },
            {"r|dryrun", XfOpCode.DryRun,
                ":Perform a dry run without actually modifying anything (global).",
                (ora, op, dry) => IsDryRunOnly = dry != null },
            {"s|stdout", XfOpCode.Stdout,
                ":Write log output to stdout instead of stderr (global).",
                (ora, op, text) => ora.Add(op, text != null) },
            {"t|text-log",XfOpCode.TextLog,
                ":Enable text console logging for interactive and simple script use.",
                (ora, op, text) => ora.Add(text != null ? XfOpCode.TextLog : XfOpCode.NoLog) },
            {"u|load-public-keys=", XfOpCode.LoadPublicKeys,
                "{file(s)}:Load public key(s) from file(s).",
                (ora, op, pks) => ora.AddManyRunning(op, pks) },
            {"v|overwrite", XfOpCode.Overwrite,
                ":Overwrite files instead of using alternate target name.",
                (ora, op, ow) => ora.Add(ow != null ? XfOpCode.Overwrite : XfOpCode.NoOverwrite) },
            {"w|wipe=", XfOpCode.Wipe,
                "{file}:Securely wipe and delete a file.",
                (ora, op, wipe) => ora.AddManyRunning(XfOpCode.Wipe, wipe) },
            {"x|export-public-key=", XfOpCode.ExportPublicKey,
                "{email} {file}:Export a public key.",
                (ora, op, email, file) => ora.Add(op, email, file) },
            {"y|load-private-keys=", XfOpCode.LoadPrivateKeys,
                "{in-file} [{out-file}]:Load private keys from file, optionally writing an update.",
                (ora, op, inf) => ora.AddOneRunning(op, inf) },
            {"z|encrypt-like=", XfOpCode.EncryptLike,
                "{file}:Extract password and public keys to use for next encryption",
                (ora, op, file) => ora.Add(op, file)},
            {"platform", XfOpCode.CliPlatform,
                ":Display the platform the program is running on.",
                (ora, op, platform) => {if (platform != null) ora.Add(op); } },
            {"echo", XfOpCode.Echo,
                ":Display the command line received by the program.",
                (ora, op, echo) => {if (echo != null) ora.Add(op, CleanCommandLineProgramName("echo", _commandLine)); } },
            { "<>", XfOpCode.DefaultInternal, (ora, op, v) => ora.RunningAction(v) },
        };

        if (OperatingSystem.IsWindows())
        {
            optionSet.Add("debug-break", XfOpCode.CliDebugBreak, ":?Break into debugger when executing this argument.", (ora, op, dbg) => ora.Add(dbg != null ? op : XfOpCode.None));
            optionSet.Add("debug-break-parse", XfOpCode.CliDebugBreakParse, ":?Break into debugger when parsing this argument.", (ora, op, dbg) => { if (dbg != null) _ = Debugger.Launch(); });
        }

        optionSet.Add("argument-markdown", XfOpCode.ArgumentMarkdown, ":?Display help texts as markdown.", (ora, op, arg) => ora.Add(arg != null ? op : XfOpCode.None));
        optionSet.Add("begin", XfOpCode.Begin, ":?Begin a sequence of operations.", (ora, op, begin) => ora.Add(op));
        optionSet.Add("cli-crash-log=", XfOpCode.CliCrashLog, "{file}:?Write crash log here (global).", (ora, op, log) => ora.Add(op, log));
        optionSet.Add("cli-license=", XfOpCode.CliLicense, "{jwt-license}:?Use this license. Overrides any others found (global).", (ora, op, license) => ora.Add(op, license));
        optionSet.Add("cli-version", XfOpCode.SdkCliVersion, ":?Display the command line tool API version.", (ora, op, arg) => ora.Add(arg != null ? op : XfOpCode.None));
        optionSet.Add("crash=", XfOpCode.Crash, "{parse|dry|real}:?Crash during parse, dry run or real run.", (ora, op, arg) => { if (arg == "parse") HardCrash.Immediately(); else ora.Add(XfOpCode.Crash, arg); });
        optionSet.Add("end", XfOpCode.End, ":?End a sequence of operations.", (ora, op, end) => ora.Add(op));
        optionSet.Add("internal", XfOpCode.Internal, ":?Display internal use help and disable splash (global).", (ora, op, @internal) => Internal = @internal != null);
        optionSet.Add("jwt-audience=", XfOpCode.JwtAudience, "{audience}:?Set audience string or URI for JWT.", (ora, op, audience) => ora.Add(op, audience));
        optionSet.Add("jwt-claims={}", XfOpCode.JwtClaims, "{expiration} {claims}:?Set days until expiration and claims JSON.", (ora, op, days, claims) => ora.Add(op, days, claims));
        optionSet.Add("jwt-create-key-pair={}", XfOpCode.JwtCreateKeyPair, "{private-pem} {public-pem}:?Create JWT keypair as PEM files.", (ora, op, @private, @public) => ora.Add(op, @private, @public));
        optionSet.Add("jwt-issuer=", XfOpCode.JwtIssuer, "{issuer}:?Set issuer email for JWT.", (ora, op, issuer) => ora.Add(op, issuer));
        optionSet.Add("jwt-sign=", XfOpCode.JwtSign, "{signed-jwt}:?Sign and write JWT to file.", (ora, op, file) => ora.Add(op, file));
        optionSet.Add("jwt-private-key=", XfOpCode.JwtPrivateKey, "{private-pem}:?Use a private key PEM file for signing.", (ora, op, @private) => ora.Add(op, @private));
        optionSet.Add("jwt-verify={}", XfOpCode.JwtVerify, "{public-pem} {signed-jwt}:?Verify a signed JWT file using a public PEM file.", (ora, op, @public, token) => ora.Add(op, @public, token));
        optionSet.Add("options-code-export", XfOpCode.OptionsCodeExport, ":?Display C# source code for options.", (ora, op, arg) => ora.Add(arg != null ? op : XfOpCode.None));
        optionSet.Add("sigint=", XfOpCode.SdkSigInt, "{id}:?Send a SIGINT to process id.", (ora, op, id) => ora.Add(op, id));
        optionSet.Add("slip39-combine=", XfOpCode.Slip39Combine, $"{{{nameof(XfOptionKeys.Bip39)}|{nameof(XfOptionKeys.Hex)}|{nameof(XfOptionKeys.Base64)}|{nameof(XfOptionKeys.Text)}}} [{{file}}]:?Combine shares and recover the secret.", (ora, op, format) => ora.AddOneRunning(op, format));
        optionSet.Add("slip39-group={}", XfOpCode.Slip39Group, "{threshold} {count}:?Specify a group to split to.", (ora, op, threshold, count) => ora.Add(op, threshold, count));
        optionSet.Add("slip39-threshold=", XfOpCode.Slip39GroupThreshold, "{threshold}:?Specify the group threshold, default is 1.", (ora, op, threshold) => ora.Add(op, threshold));
        optionSet.Add("slip39-password=", XfOpCode.Slip39Password, "{password} [{iterations}]:?The password to use and the iteration exponent.", (ora, op, password) => ora.AddOneRunning(op, password));
        optionSet.Add("slip39-secret={}", XfOpCode.Slip39Secret, $"{{{nameof(XfOptionKeys.Bip39)}|{nameof(XfOptionKeys.Hex)}|{nameof(XfOptionKeys.Base64)}|{nameof(XfOptionKeys.Text)}}} {{secret}}:?Specify the secret to split.", (ora, op, format, secret) => ora.Add(op, format, secret));
        optionSet.Add("slip39-shares=", XfOpCode.Slip39Shares, "{share(s)}:?Add shares to combine.", (ora, op, share) => ora.AddManyRunning(op, share));
        optionSet.Add("slip39-split=", XfOpCode.Slip39Split, $"{{{nameof(XfOptionKeys.Slip39)}|{nameof(XfOptionKeys.Hex)}|{nameof(XfOptionKeys.Base64)}}}[,...] [{{file}}]:?Split the secret into shares.", (ora, op, format) => ora.AddOneRunning(op, format));
        optionSet.Add("slip39-info:", XfOpCode.Slip39Information, "[{{file}}]:?Verify the shares and output the prefix information.", (ora, op, to) => ora.AddOneRunning(op));
        optionSet.Add("work-folder=", XfOpCode.WorkFolder, "{work-folder}:?A work folder for settings and logs (global).", (ora, op, wf) => WorkFolder = wf);
        return optionSet;
    }

    private readonly string _commandLine;

    public OptionsParser(string commandLine)
    {
        _commandLine = commandLine;
        (_definitions, ParsedOps, Extra) = ArgumentsToOptions(commandLine);
    }

    public IEnumerable<ParsedOp> ParsedOps { get; set; }

    public bool IsDryRunOnly { get; set; } = false;

    public bool IsQuiet { get; set; } = false;

    public bool Internal { get; set; } = false;

    public string WorkFolder { get; set; } = string.Empty;

    public int OpLevel { get; set; } = 0;

    public IEnumerable<string> Extra { get; set; }

    public Status ParseStatus { get; private set; } = Status.Success;

    private readonly ExportableOptionCollection _definitions;

    public IEnumerable<string[]> Descriptions
    {
        get
        {
            var descriptions = new List<string[]>();
            foreach (OptionBase option in _definitions)
            {
                var split = SplitDescription(option);
                if (split.Length > 0)
                {
                    descriptions.Add(split);
                }
            }

            return descriptions;
        }
    }
#if DEBUG
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Can't be static in Release builds.")]
    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "It's not unnecessary in Release mode.")]
#endif
    private string[] SplitDescription(OptionBase option)
    {
#if !DEBUG
        if (!Internal && (option.Description == null || option.Description.Contains(":?")))
        {
            return [];
        }
#endif
        string[] split = option.Description?.Split(':') ?? [string.Empty, string.Empty,];
        if (split[1].StartsWith('?'))
        {
            split[1] = split[1].Substring(1);
        }
        string options = string.Join('|', option.GetNames().Select(n => n.Length == 1 ? $"-{n}" : $"--{n}").ToArray());
        if (options == "--<>")
        {
            return [];
        }
        split[0] = options + ' ' + split[0];
        return split;
    }

    public Version ExportableCliVersion => _definitions.CliVersion;

    public IEnumerable<XfOptionDefinition> ExportableCliDefinitions => _definitions.Export.Select(x => new XfOptionDefinition(x.Item1, x.Item2, x.Item3));

    private (ExportableOptionCollection, IEnumerable<ParsedOp>, IEnumerable<string>) ArgumentsToOptions(string commandLine)
    {
        List<ParsedOp> parsedOps = [];
        List<string> extra = [];

        string[] args = SplitArgs(commandLine).Skip(1).ToArray();

        RecursivleyParseArguments(args, parsedOps, extra);

        return (BuildOptionSet(parsedOps, extra), parsedOps, extra);
    }

    private class RunningArguments
    {
        private readonly IList<string> _extra;

        private readonly IList<ParsedOp> _parsed;

        public RunningArguments(IList<ParsedOp> parsed, IList<string> extra)
        {
            _extra = extra;
            _parsed = parsed;
            _noop = (s) => _extra.Add(s);
            RunningAction = _noop;
        }

        private readonly Action<string> _noop;

        public Action<string> RunningAction { get; private set; }

        public void Add(XfOpCode opCode)
        {
            _parsed.Add(new ParsedOp(opCode));
            RunningAction = _noop;
        }

        public void Add(XfOpCode opCode, string p1)
        {
            _parsed.Add(new ParsedOp(opCode, p1));
            RunningAction = _noop;
        }

        public void Add(XfOpCode opCode, bool flag)
        {
            _parsed.Add(new ParsedOp(opCode, flag));
            RunningAction = _noop;
        }

        public void Add(XfOpCode opCode, string p1, string p2)
        {
            _parsed.Add(new ParsedOp(opCode, p1, p2));
            RunningAction = _noop;
        }

        public void Add(XfOpCode opCode, string p1, string p2, string p3)
        {
            _parsed.Add(new ParsedOp(opCode, p1, p2, p3));
            RunningAction = _noop;
        }

        public void AddOneRunning(XfOpCode opCode)
        {
            var op = new ParsedOp(opCode);
            _parsed.Add(op);
            RunningAction = (s) => { op.Arguments.Add(s); RunningAction = _noop; };
        }

        public void AddOneRunning(XfOpCode opCode, string first)
        {
            var op = new ParsedOp(opCode, first);
            _parsed.Add(op);
            RunningAction = (s) => { op.Arguments.Add(s); RunningAction = _noop; };
        }

        public void AddOneRunning(XfOpCode opCode, string first, string second)
        {
            ParsedOp op = new(opCode, first, second);
            _parsed.Add(op);
            RunningAction = (s) => { op.Arguments.Add(s); RunningAction = _noop; };
        }

        public void AddManyRunning(XfOpCode opCode, string first)
        {
            var op = new ParsedOp(opCode, first);
            _parsed.Add(op);
            RunningAction = (s) => op.Arguments.Add(s);
        }
    }

    private sealed class MyActionOption(string prototype, string description, int count,
        Action<OptionValueCollection> action) : OptionBase(prototype, description, count)
    {
        protected override void OnParseComplete(OptionContext c)
        {
            ArgumentNullException.ThrowIfNull(c);

            action(c.OptionValues);
        }
    }

    private class ExportableOptionCollection(Version cliVersion, RunningArguments ora) : OptionSetCollection
    {
        /// <summary>
        /// This must be updated when the options are updated in an incompatible way.
        /// Increase the minor version, if changes do not change the meaning or syntax of
        /// any pre-existing options, i.e. purely new additional capabilities.
        /// Increase the major version, if changes in any way changes an existing option
        /// so it may not work as expected by an older consumer.
        /// </summary>
        public Version CliVersion { get; } = cliVersion;

        public class ExportableOption(int item1, string item2, string item3) : Tuple<int, string, string>(item1, item2, item3)
        {
        }

        public List<ExportableOption> Export = [];

        public void Add(string prototype, XfOpCode opCode, Action<RunningArguments, XfOpCode, string> action)
        {
            _ = Add(prototype, (p1) => action(ora, opCode, p1));
            Export.Add(new ExportableOption((int)opCode, prototype, string.Empty));
        }

        public void Add(string prototype, XfOpCode opCode, Action<RunningArguments, XfOpCode, string, string> action)
        {
            _ = Add(prototype, (p1, p2) => action(ora, opCode, p1, p2));
            Export.Add(new ExportableOption((int)opCode, prototype, string.Empty));
        }

        public void Add(string prototype, XfOpCode opCode, string description, Action<RunningArguments, XfOpCode, string> action)
        {
            _ = Add(prototype, description, (p1) => action(ora, opCode, p1));
            Export.Add(new ExportableOption((int)opCode, prototype, string.Join(':', (description?.Split(':').Skip(1).ToArray() ?? []))));
        }

        public void Add(string prototype, XfOpCode opCode, string description, Action<RunningArguments, XfOpCode, string, string> action)
        {
            _ = Add(prototype, description, (p1, p2) => action(ora, opCode, p1, p2));
            Export.Add(new ExportableOption((int)opCode, prototype, string.Join(':', (description?.Split(':').Skip(1).ToArray() ?? []))));
        }

        public void Add(string prototype, string description, Action<string, string, string> action)
        {
            ArgumentNullException.ThrowIfNull(action);
            
            OptionBase p = new MyActionOption(prototype, description, 3,
                    delegate (OptionValueCollection v) { action(v[0], v[1], v[2]); });
            Add(p);
        }

        public void Add(string prototype, XfOpCode opCode, string description, Action<RunningArguments, XfOpCode, string, string, string> action)
        {
            Add(prototype, description, (p1, p2, p3) => action(ora, opCode, p1, p2, p3));
            Export.Add(new ExportableOption((int)opCode, prototype, string.Join(':', (description?.Split(':').Skip(1).ToArray() ?? []))));
        }
    }

    private void RecursivleyParseArguments(IEnumerable<string> args, List<ParsedOp> parsedOps, List<string> extra)
    {
        var optionSet = BuildOptionSet(parsedOps, extra);

        try
        {
            extra.AddRange(optionSet.Parse(args));
            if (!ParseStatus.IsSuccess)
            {
                return;
            }

            if (extra.Count != 0)
            {
                ParseStatus = new Status(XfStatusCode.ExtraArguments, "Extra unprocessed arguments '{0}' were found.".Format(string.Join(',', extra)));
                return;
            }

            return;
        }
        catch (OptionException oe)
        {
            ParseStatus = new Status(XfStatusCode.InvalidOption, oe.Message);
            return;
        }
    }

    private void RecursivelyParseFromFile(string path, List<ParsedOp> parsed, List<string> extra)
    {
        foreach (string line in System.IO.File.ReadAllLines(path))
        {
            string trimmed = line.Trim();
            if (trimmed.Length == 0 || trimmed.StartsWith('#'))
            {
                continue;
            }

            string[] argv = SplitArgs(trimmed).ToArray();
            RecursivleyParseArguments(argv, parsed, extra);
        }
    }

    private void RecursivelyParseFromString(string variable, List<ParsedOp> parsed, List<string> extra)
    {
        string? value = Environment.GetEnvironmentVariable(variable);
        if (value == null)
        {
            ParseStatus = new Status(XfStatusCode.UnknownEnvironmentVariable, "No environment variable named '{0}' exists.".Format(variable));
            return;
        }

        string[] argv = SplitArgs(value).ToArray();

        RecursivleyParseArguments(argv, parsed, extra);
    }

    private static string CleanCommandLineProgramName(string echoOptionName, string commandLine)
    {
        string[] args = SplitArgs(commandLine).ToArray();
        string programName = Path.GetFileNameWithoutExtension(args[0]);
        string commandLineWithoutProgramName = commandLine.Substring(commandLine.IndexOf(args[0]) + args[0].Length);
        if (commandLine.StartsWith('"') || commandLine.StartsWith('\''))
        {
            commandLineWithoutProgramName = commandLineWithoutProgramName.Substring(1);
        }

        commandLineWithoutProgramName = commandLineWithoutProgramName.Replace($" --{echoOptionName}", string.Empty);

        return programName + commandLineWithoutProgramName;
    }

    private static IEnumerable<string> SplitArgs(string commandLine)
    {
        var result = new StringBuilder();

        var quoted = false;
        var escaped = false;
        var started = false;
        var allowcaret = false;
        for (int i = 0; i < commandLine.Length; i++)
        {
            var chr = commandLine[i];

            if (chr == '^' && !quoted)
            {
                if (allowcaret)
                {
                    _ = result.Append(chr);
                    started = true;
                    escaped = false;
                    allowcaret = false;
                }
                else if (i + 1 < commandLine.Length && commandLine[i + 1] == '^')
                {
                    allowcaret = true;
                }
                else if (i + 1 == commandLine.Length)
                {
                    _ = result.Append(chr);
                    started = true;
                    escaped = false;
                }
            }
            else if (escaped)
            {
                _ = result.Append(chr);
                started = true;
                escaped = false;
            }
            else if (chr == '"')
            {
                quoted = !quoted;
                started = true;
            }
            else if (chr == '\\' && i + 1 < commandLine.Length && commandLine[i + 1] == '"')
            {
                escaped = true;
            }
            else if (chr == ' ' && !quoted)
            {
                if (started) yield return result.ToString();
                _ = result.Clear();
                started = false;
            }
            else
            {
                _ = result.Append(chr);
                started = true;
            }
        }

        if (started) yield return result.ToString();
    }
}
