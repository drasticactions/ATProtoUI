// <copyright file="PostView.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FishyFlip.Lexicon.App.Bsky.Feed;

namespace ATProtoMAUI.Views;

/// <summary>
/// PostView for ThreadViewPost.
/// </summary>
public partial class PostView : ContentView
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PostView"/> class.
    /// </summary>
    public PostView()
    {
        this.InitializeComponent();
    }

    public ThreadViewPost ThreadViewPost
    {
        get => (ThreadViewPost)this.GetValue(ThreadViewPostProperty);
        set => this.SetValue(ThreadViewPostProperty, value);
    }

    public static readonly BindableProperty ThreadViewPostProperty = BindableProperty.Create(
        propertyName: nameof(ThreadViewPost),
        returnType: typeof(ThreadViewPost),
        declaringType: typeof(PostView),
        defaultValue: null,
        propertyChanged: OnThreadViewPostChanged);

    private static void OnThreadViewPostChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is PostView postView)
        {
            postView.OnThreadViewPostChanged((ThreadViewPost)oldValue, (ThreadViewPost)newValue);
        }
    }

    private void OnThreadViewPostChanged(ThreadViewPost oldValue, ThreadViewPost? newValue)
    {
        if (newValue is null)
        {
            return;
        }

        this.BindingContext = newValue;
    }
}