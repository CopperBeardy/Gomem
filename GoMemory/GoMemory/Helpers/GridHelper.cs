using GoMemory.Models;
using Xamarin.Forms;

namespace GoMemory.Helpers
{
    public static class GridHelper
    {
        /// <summary>
        /// create grid
        /// </summary>
        /// <param name="rowSize"></param>
        /// <param name="columSize"></param>
        /// <returns></returns>
        public static Grid CreateGrid(int rowSize, int columSize)
        {
            Grid Grid = new Grid
            {
                Margin = new Thickness(0, 10, 0, 0),
                ColumnSpacing = 1,
                RowSpacing = 1,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };


            for (int i = 0; i < rowSize; i++)
            {
                Grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }
            for (int i = 0; i < columSize; i++)
            {
                Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            return Grid;
        }


        /// <summary>
        /// Insert images into the Grid
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static Grid InsertGridImages(Grid grid, Image[] images, DifficultySetting difficultySetting)
        {
            grid.Children.Clear();

            int imagecount = 0;
            for (int row = 0; row < difficultySetting.GridRowSize; row++)
            {
                for (int column = 0; column < difficultySetting.GridColumnSize; column++)
                {
                    Image image = new Image
                    {
                        Source = images[imagecount].Source,
                        Aspect = Aspect.AspectFit,
                        Margin = new Thickness(2)
                    };

                    grid.Children.Add(image, row, column);

                    imagecount += 1;
                }
            }
            return grid;
        }
    }
}
