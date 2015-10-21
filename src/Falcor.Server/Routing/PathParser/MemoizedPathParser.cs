using System.Collections.Generic;

namespace Falcor.Server.Routing.PathParser
{
    internal sealed class MemoizedPathParser : IPathParser
    {
        private static IDictionary<string, FalcorPath> PathParsingCache { get; } = new Dictionary<string, FalcorPath>();
        private readonly IPathParser _innnerParser;

        public MemoizedPathParser(IPathParser innnerParser)
        {
            _innnerParser = innnerParser;
        }

        public IReadOnlyList<FalcorPath> ParseMany(string paths) => _innnerParser.ParseMany(paths);

        public FalcorPath ParseSingle(string path)
        {
            FalcorPath parsedPath;
            return PathParsingCache.TryGetValue(path, out parsedPath) ? parsedPath : _innnerParser.ParseSingle(path);
        }
    }
}