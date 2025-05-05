using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using Newtonsoft.Json.Serialization;
namespace IssueTracker.Services
{
    public class GitHubCommitDataModel
    {
        [JsonProperty("sha")]
        public string sha;
        [JsonProperty("node_id")]
        public string node_id;
        [JsonProperty("commit")]
        public commit commit;
        [JsonProperty("url")]
        public string url;
        [JsonProperty("comments_url")]
        public string comments_url;

    }
    public class commit
    {
        [JsonProperty("author")]
        public committer author;
        [JsonProperty("committer")]
        public committer committer;
        [JsonProperty("message")]
        public string message;
        [JsonProperty("tree")]
        public tree tree;
        [JsonProperty("url")]
        public string url;
        [JsonProperty("comment_count")]
        public int comment_count;
        [JsonProperty("verification")]
        public verification verification;

    }
    public class committer
    {
        [JsonProperty("name")]
        public string name;
        [JsonProperty("email")]
        public string email;
        [JsonProperty("date")]
        public string date;
    }
    public class tree
    {
        [JsonProperty("sha")]
        public string sha;
        [JsonProperty("url")]
        public string url;
    }
    public class verification
    {
        [JsonProperty("verified")]
        public bool verified;
        [JsonProperty("reason")]
        public string reason;
        [JsonProperty("signature")]
        public string signature;
        [JsonProperty("payload")]
        public string payload;
        [JsonProperty("verified_at")]
        public string verified_at;
    }
    public class FetechGithubCommitsService
    {
        private readonly HttpClient _httpClient;

        public FetechGithubCommitsService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("DotNetApp");
        }

        public void AddAuthToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token",  token);
        }

        public async Task<List<string>> GetCommitDataAsync(string userName, string repoName)
        {
            /*if (userName == string.Empty || repoName == string.Empty)
            {*/
                var url = $"https://api.github.com/repos/{userName}/{repoName}/commits";
                var httpResp = await _httpClient.GetAsync(url);
                if (httpResp.IsSuccessStatusCode) 
                { 
                    var jsonData = await httpResp.Content.ReadAsStringAsync();
                    if (jsonData != null)
                    {
                        var data = JsonConvert.DeserializeObject<List<GitHubCommitDataModel>>(jsonData);
                        Console.WriteLine(data.Count);
                        List<string> result = new List<string>();
                        result.Add(string.Empty);
                        foreach (var elem in data)
                        {
                            string val = $"message: {elem.commit.message}, commiter: {elem.commit.author.name}, date: {elem.commit.author.date}";
                            result.Add(val);
                        }
                        return result;

                    }
                }
            //}

            return Enumerable.Empty<string>().ToList();
            //List<string> result = new List<string>();
            
        }
    }
}
