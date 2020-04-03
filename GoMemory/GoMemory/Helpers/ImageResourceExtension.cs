using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoMemory.Helpers
{
    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return Source == null ? null : ImageSource.FromResource(Source);
        }
    }
}