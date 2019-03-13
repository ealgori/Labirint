using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serialization.Json;

namespace Labirint
{
    internal class Robot : IRobot
    {
        private readonly RestClient _client = new RestClient("http://5.101.78.193:2045/api/Session/");
        private readonly JsonDeserializer _deserializer = new JsonDeserializer();
        private Guid _sessionId;

        public async Task MoveAsync(Direction direction)
        {
            var request = new RestRequest {Method = Method.PUT, Resource = "Move"};
            request.AddJsonBody(new {sessionId = _sessionId, direction});
            var completionSource = new TaskCompletionSource<IRestResponse>();
            _client.ExecuteAsync(request, x => completionSource.SetResult(x));
            var result = await completionSource.Task;
        }

        public async Task<IEnumerable<Cell>> GetCellsAsync()
        {
            var request = new RestRequest {Method = Method.GET, Resource = $"GetCells/{_sessionId}"};
            var completionSource = new TaskCompletionSource<IRestResponse>();
            _client.ExecuteAsync(request, x => completionSource.SetResult(x));
            return _deserializer.Deserialize<GetCellsResponse>(await completionSource.Task).Cells;
        }

        public async Task Init(string family)
        {
            var request = new RestRequest {Method = Method.POST, Resource = "CreateSession"};
            request.AddJsonBody(new {family});
            var completionSource = new TaskCompletionSource<IRestResponse>();
            _client.ExecuteAsync(request, x => completionSource.SetResult(x));
            _sessionId = _deserializer.Deserialize<SessionCreateResponse>(await completionSource.Task).SessionId;
        }
    }
}