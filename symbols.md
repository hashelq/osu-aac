```csharp
// \u000e\u000f\u0002
using System.Reflection;
using osu.Framework.IO.Network;
using osu.Game.Online;
using osu.Game.Online.API;

private bool \u0002()
{
	try
	{
		\u000e\u000f\u0002.\u0002 = typeof(WebRequest).GetField(\u000f\u0010\u0002.\u0002(-1506375722), BindingFlags.Instance | BindingFlags.NonPublic);
		\u0005.OnLoadComplete += \u0002;
		APIRequest.Sign = \u0002;
		HubClientConnector.Sign = \u0002;
		\u0008.BindValueChanged(\u0002);
		return true;
	}
	catch
	{
		return false;
	}
```

```csharp
// \u000f\u000f\u0002
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using osu.Framework.Logging;

public async Task \u0003()
{
	try
	{
		HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, \u000f\u0010\u0002.\u0002(-1506375791));
		string content = JsonSerializer.Serialize(new global::\u0002<string, string, \u0002\u0010\u0002.\u0002>(\u000f\u000f\u0002.\u0002(), \u000f, \u0002\u0010\u0002.\u0002()));
		httpRequestMessage.Content = new StringContent(content, Encoding.UTF8, \u000f\u0010\u0002.\u0002(-1506376117));
		string text = \u000e\u000f\u0002.\u0002();
		if (!string.IsNullOrEmpty(text))
		{
                                          // x-token                               *value*
			httpRequestMessage.Headers.Add(\u000f\u0010\u0002.\u0002(-1506376108), \u000f\u0010\u0002.\u0002(-1506376096) + text);
		}
		HttpResponseMessage httpResponseMessage = await this.\u0003.SendAsync(httpRequestMessage, this.\u0005.Token);
		if (!httpResponseMessage.IsSuccessStatusCode)
		{
			return;
		}
		\u0008\u0010\u0002 obj = await httpResponseMessage.Content.ReadFromJsonAsync<\u0008\u0010\u0002>(new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		}, this.\u0005.Token);
		if (obj != null && obj.Url != null)
		{
			using Stream stream = await this.\u0003.GetStreamAsync(obj.Url, this.\u0005.Token);
			\u0002(stream);
			await this.\u0005();
		}
	}
	catch (Exception ex)
	{
		Logger.Log(ex.ToString(), LoggingTarget.Network);
	}
}
```

```csharp
// osu.Game.Online.HubClientConnector
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Connections.Client;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using osu.Framework;

protected override Task<PersistentEndpointClient> BuildConnectionAsync(CancellationToken cancellationToken)
{
	IHubConnectionBuilder builder = ((IHubConnectionBuilder)new HubConnectionBuilder()).WithUrl(endpoint, (Action<HttpConnectionOptions>)delegate(HttpConnectionOptions options)
	{
		if (RuntimeInfo.OS != RuntimeInfo.Platform.iOS)
		{
			options.Proxy = WebRequest.DefaultWebProxy;
			if (options.Proxy != null)
			{
				options.Proxy!.Credentials = CredentialCache.DefaultCredentials;
			}
		}
		options.Headers.Add("Authorization", "Bearer " + API.AccessToken);
		options.Headers.Add("OsuVersionHash", versionHash);
		Sign(options);
	});
```
