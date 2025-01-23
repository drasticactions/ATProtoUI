// <copyright file="MainPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using ATProtoUI;
using FishyFlip.Lexicon.App.Bsky.Feed;

namespace ATProtoMAUI;

/// <summary>
/// Main Page.
/// </summary>
public partial class MainPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainPage"/> class.
    /// </summary>
    public MainPage()
    {
        this.InitializeComponent();
        var threadOutput = ExampleItems.GetPost();
        this.PostView.ThreadViewPost = (ThreadViewPost)threadOutput.Thread;
    }
}
