// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip;
using FishyFlip.Models;
using FishyFlip.Tools;

Console.WriteLine("ATProtoUI Test");

var atProtocolBuilder = new ATProtocolBuilder();
var atProtocol = atProtocolBuilder.Build();

if (Directory.Exists("json"))
{
    Directory.Delete("json", true);
}

Directory.CreateDirectory("json");

var penisBotDid = ATDid.Create("did:plc:uw7fyearjmke7yp72prgunq7")!;
var videoPostUri = ATUri.Create("at://bakoon.bsky.social/app.bsky.feed.post/3lgcnnttuvs2u")!;
var twoImageUri = ATUri.Create("at://itsclaybaby.bsky.social/app.bsky.feed.post/3lfvzlr4xgc2s")!;
var embeddedPostUri = ATUri.Create("at://did:plc:yhgc5rlqhoezrx6fbawajxlh/app.bsky.feed.post/3lfhzcasydk2a")!;
var postUri = ATUri.Create("at://drasticactions.xn--q9jyb4c/app.bsky.feed.post/3lcmsu5uas22t")!;

(var post, var error) = await atProtocol.Feed.GetPostThreadAsync(postUri);

File.WriteAllText("json/post.json", post!.ToJson());

