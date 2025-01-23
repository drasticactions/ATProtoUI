// <copyright file="FacetedLabel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Lexicon.App.Bsky.Richtext;

namespace ATProtoMAUI.Controls;

/// <summary>
/// Faceted Label.
/// </summary>
public class FacetedLabel : Label
{
    /// <summary>
    /// The Facets bindable property.
    /// </summary>
    public static readonly BindableProperty FacetsProperty = BindableProperty.Create(
        nameof(Facets), typeof(List<Facet>), typeof(FacetedLabel), propertyChanged: OnFacetsChanged);

    /// <summary>
    /// Gets or sets the facets for the label.
    /// </summary>
    public List<Facet> Facets
    {
        get => (List<Facet>)this.GetValue(FacetsProperty);
        set => this.SetValue(FacetsProperty, value);
    }

    private static void OnFacetsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is FacetedLabel label)
        {
            label.UpdateFormattedText();
        }
    }

    private void UpdateFormattedText()
    {
        var formattedString = new FormattedString();
        if (string.IsNullOrEmpty(this.Text))
        {
            return;
        }

        var utf8Bytes = System.Text.Encoding.UTF8.GetBytes(this.Text);
        var currentByteIndex = 0;

        foreach (var facet in this.Facets.OrderBy(f => f.Index.ByteStart))
        {
            // Add text before facet
            if (facet.Index.ByteStart > currentByteIndex)
            {
                var beforeText = System.Text.Encoding.UTF8.GetString(
                    utf8Bytes[currentByteIndex..(int)facet.Index.ByteStart]);
                formattedString.Spans.Add(new Span { Text = beforeText });
            }

            // Add faceted text
            var facetText = System.Text.Encoding.UTF8.GetString(
                utf8Bytes[(int)facet.Index.ByteStart..(int)facet.Index.ByteEnd]);

            var span = new Span
            {
                Text = facetText,
                TextDecorations = TextDecorations.Underline,
                TextColor = this.GetColorForFacetType(facet.Features.FirstOrDefault()?.Type ?? string.Empty),
            };

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) => this.HandleFacetTapped(facet);
            span.GestureRecognizers.Add(tapGesture);
            formattedString.Spans.Add(span);
            currentByteIndex = (int)facet.Index.ByteEnd;
        }

        // Add remaining text
        if (currentByteIndex < utf8Bytes.Length)
        {
            var remainingText = System.Text.Encoding.UTF8.GetString(
                utf8Bytes[currentByteIndex..]);
            formattedString.Spans.Add(new Span { Text = remainingText });
        }

        this.FormattedText = formattedString;
    }

    private Color? GetColorForFacetType(string type) => type switch
    {
        Mention.RecordType => Colors.Blue,
        Tag.RecordType => Colors.Blue,
        Link.RecordType => Colors.Blue,
        _ => null,
    };

    private void HandleFacetTapped(Facet facet)
    {
        var facetFeature = facet.Features.FirstOrDefault();
        var facetType = facetFeature?.Type ?? string.Empty;
        switch (facetType)
        {
            case Mention.RecordType:
                var mention = (Mention)facetFeature!;
                break;
            case Tag.RecordType:
                var tag = (Tag)facetFeature!;
                break;
            case Link.RecordType:
                var link = (Link)facetFeature!;
                Browser.Default.OpenAsync(link.Uri, BrowserLaunchMode.SystemPreferred);
                break;
        }
    }
}