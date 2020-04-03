using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace GoMemory.Helpers
{
    public class ImageHelper
    {
        private readonly Image[] _images;

       

        public ImageHelper() => _images = new[]
            {
                new Image{Source ="apple.png"},
                new Image{Source ="beer.png"},
                new Image{Source ="bell.png"},
                new Image{Source ="bison.png"},
                new Image{Source ="cake.png"},
                new Image{Source ="camera.png"},
                new Image{Source ="carrot.png"},
                new Image{Source ="cheese.png"},
                new Image{Source ="chocolate.png"},
                new Image{Source ="clock.png"},
                new Image{Source ="codfish.png"},
                new Image{Source ="crab.png"},
                new Image{Source ="egg.png"},
                new Image{Source ="frog.png"},
                new Image{Source ="hammer.png"},
                new Image{Source ="lightbulb.png"},
                new Image{Source ="lightning.png"},
                new Image{Source ="lolly.png"},
                new Image{Source ="microphone.png"},
                new Image{Source ="milkshake.png"},
                new Image{Source ="orange.png"},
                new Image{Source ="parrot.png"},
                new Image{Source ="phone.png"},
                new Image{Source ="pig.png"},
                new Image{Source ="portobello.png"},
                new Image{Source ="rabbit.png"},
                new Image{Source ="robots.png"},
                new Image{Source ="sausage.png"},
                new Image{Source ="scissors.png"},
                new Image{Source ="spider.png"},
                new Image{Source ="star.png"},
                new Image{Source ="strawberry.png"},
                new Image{Source ="teapot.png"},
                new Image{Source ="wasp.png"},
                new Image{Source ="watermelon.png"},
                new Image{Source ="wine.png"}

            };


        /// <summary>
        /// zero argument method for getting a unmodified ObservableCollection of Image
        /// </summary>
        /// <returns>
        /// ObservableCollection of Image
        /// </returns>
        public Image[] GetImages(int totalImages)
        {  
            return  ShuffleCollection(_images).Take(totalImages).ToArray();          
        }

        /// <summary>
        /// Randomize the order of a Image Array
        /// </summary>
        /// <returns>
        /// ObservableCollection of Image
        /// </returns>
        public Image[] ShuffleCollection(Image[] imageArray)
        {
            Random rnd = new Random();
            Image[] unsorted = imageArray;
            for (int i = 0; i < unsorted.Length; i++)
            {
                Image temp = unsorted[i];
                int randomIndex = rnd.Next(0, imageArray.Length);
                unsorted[i] = unsorted[randomIndex];
                unsorted[randomIndex] = temp;
            }
            return unsorted;
        }

        public Image[] ToMatchImagesList(int numberOfImagesNeeded, Image[] images)
        {
            return ShuffleCollection(images).Take(numberOfImagesNeeded).ToArray(); 
        }
           



        public Image[] ToMatchImagesArray(Image[] selectFromImages)
        {
            Random rnd = new Random();
            int maxIndex = selectFromImages.Length;
            Image[] matchImages = new Image[maxIndex];
            for (int i = 0; i < selectFromImages.Length; i++)
            {
                Image selectedImage = selectFromImages[rnd.Next(0, maxIndex)];
                if (matchImages.Contains(selectedImage)) continue;
                matchImages[i] = selectedImage;
            }
            return matchImages;

        }
    }
}
