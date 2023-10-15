# Welcome to CloudFlare.Dns

![CloudFlare.Dns](https://raw.githubusercontent.com/ljchuello/CloudFlare.Dns/master/icon_128.png)

![](https://sonarcloud.io/api/project_badges/measure?project=ljchuello_cloudflare-dns&metric=security_rating) ![](https://sonarcloud.io/api/project_badges/measure?project=ljchuello_cloudflare-dns&metric=bugs) ![](https://sonarcloud.io/api/project_badges/measure?project=ljchuello_cloudflare-dns&metric=vulnerabilities) ![](https://img.shields.io/nuget/v/CloudFlare.Dns) ![](https://img.shields.io/nuget/dt/CloudFlare.Dns) ![](https://sonarcloud.io/api/project_badges/measure?project=ljchuello_cloudflare-dns&metric=reliability_rating) ![](https://img.shields.io/github/languages/code-size/ljchuello/CloudFlare.Dns) ![](https://sonarcloud.io/api/project_badges/measure?project=ljchuello_cloudflare-dns&metric=ncloc) ![](https://img.shields.io/github/languages/top/ljchuello/CloudFlare.Dns) ![](https://sonarcloud.io/api/project_badges/measure?project=ljchuello_cloudflare-dns&metric=sqale_rating) ![](https://img.shields.io/github/contributors/ljchuello/CloudFlare.Dns) ![](https://img.shields.io/github/last-commit/ljchuello/CloudFlare.Dns)

Developed is a C#/.NET library that enables interaction with Cloudflare APIs, allowing for the management of DNS records within the Cloudflare platform. This project proves valuable for the administration of DNS records in various contexts and applications.

## Compatibility

This library is developed in .NET Standard 2.0 and is compatible with all .NET, .NET Core and .NET Framework implementations, it can also be used in Console projects, Web API, Class Library and even with Blazor WASM.

| .NET implementation        	| Version support         	|
|----------------------------	|-------------------------	|
| .NET and .NET Core         	| 3.0, 3.1, 5.0, 6.0, 7.0 	|
| .NET Framework             	| 4.6.1, 4.6.2, 4.7, 4.7.1, 4.7.2, 4.8, 4.8.1 |

## Installation

To install you must go to Nuget package manager and search for "CloudFlare.Dns" and then install.

### [NuGet Package](https://www.nuget.org/packages/CloudFlare.Dns)

    PM> Install-Package CloudFlare.Dns

## Usage

```csharp
// Variables
string xAuthKey = "UltraPrivateSecretKeyCloudFlare"; // Global API Key
string xAuthEmail = "lalolanda@gmail.com"; // Domain owner email in cloudflare
string zoneIdentifier = "Domain identifier"; // Domain identifier

// Client
CloudFlareDnsClient cloudFlareDnsClient = new CloudFlareDnsClient(xAuthKey, xAuthEmail, zoneIdentifier);

// Create record ipv4; with proxied with cloudflare and TTL in 60 seg / 1 min
Record record01 = await cloudFlareDnsClient.Record.Create("test-01.deployrise.com", "8.8.8.8", false, RecordType.A, 60, comment: "This commentary it's optional");

// Create record cname; without proxied and ttl in 120 seg / 2 min
Record record02 = await cloudFlareDnsClient.Record.Create("test-02.deployrise.com", "google.com", false, RecordType.CNAME, 120, comment: "This commentary it's optional");
```

When creating a subdomain, we can either write the complete address or just the subdomain. For example, we can use store.domain.com or just store. Both cases are completely valid

```csharp
// We create a subdomain by specifying the complete address
Record record = await cloudFlareDnsClient.Record.Create("store.deployrise.com", "8.8.8.8", false, RecordType.A, 60);

// We create a subdomain by specifying the short address
Record record = await cloudFlareDnsClient.Record.Create("store", "8.8.8.8", false, RecordType.A, 60);
```

## Wiki
If you like the project and want to understand how it works in depth, you can visit the documentation in [the wiki](https://github.com/ljchuello/CloudFlare.Dns/wiki)

## Implemented functionality

|  | Get All | Get One | Post | Put | Patch | Delete |
|--|:--:|:--:|:--:|:--:|:--:|:--:|
| DNS Records for a Zone | :heavy_check_mark: | :heavy_check_mark: | :heavy_check_mark: | :heavy_check_mark: | :heavy_check_mark: | :heavy_check_mark: |
