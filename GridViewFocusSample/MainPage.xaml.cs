using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GridViewFocusSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public List<String> List;
        public List<String> List2;
        private object PreviousFocusItem;
        public MainPage()
        {
            this.InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            List = new List<string> { "data1_GridView1", "data2_GridView1", "data3_GridView1" };
            List2 = new List<string> { "data1_GridView2", "data2_GridView2", "data3_GridView2" };
            myGridView.ItemsSource = List;
            myGridView2.ItemsSource = List2;
            rootGrid.GotFocus += Grid_GotFocus;
            rootGrid.LostFocus += Grid_LostFocus;
            base.OnNavigatedTo(e);
        }

        private void Grid_LostFocus(object sender, RoutedEventArgs e)
        {
            PreviousFocusItem = e.OriginalSource;
        }

        private void Grid_GotFocus(object sender, RoutedEventArgs e)
        {
            //get the previous focus Element and current focus Element.
            var previous = PreviousFocusItem as UIElement;
            var current = e.OriginalSource as UIElement;

            //got focus logic
            if ((!IsElementInsideGridView(myGridView, previous)) &&IsElementInsideGridView(myGridView,current))
            {
                output.Text += "Got Focus+1 \n";
            }

            //lost focus logic
            if ((!IsElementInsideGridView(myGridView, current)) &&(IsElementInsideGridView(myGridView,previous)) )
            {
                output.Text += "Lost Focus+1 \n";
            }

        }

        private bool IsElementInsideGridView(GridView gridView,UIElement element)
        {
            Point topLeft = gridView.TransformToVisual(this).TransformPoint(new Point());
            Rect rectBounds = new Rect(topLeft.X, topLeft.Y, gridView.ActualWidth, gridView.ActualHeight);
            IEnumerable<UIElement> hits = VisualTreeHelper.FindElementsInHostCoordinates(rectBounds, element);
            if (hits == null || hits.Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
