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
