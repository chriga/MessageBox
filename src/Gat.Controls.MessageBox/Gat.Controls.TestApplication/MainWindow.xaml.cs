// -----------------------------------------------------------------------
// <copyright file="MessageBoxView.cs" company="">
//
// The MIT License (MIT)
// 
// Copyright (c) 2016 Christoph Gattnar
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of
// the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
// BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
// CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// </copyright>
// -----------------------------------------------------------------------

using System.Windows;
using System.Windows.Media.Imaging;

namespace Gat.Controls.MessageBox.TestApplication
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Message_Click_1(object sender, RoutedEventArgs e)
		{
			Gat.Controls.MessageBoxView messageBox = new Gat.Controls.MessageBoxView();
			Gat.Controls.MessageBoxViewModel vm = (Gat.Controls.MessageBoxViewModel)messageBox.FindResource("ViewModel");
			vm.Message = "This is a message: MessageMessageMessageMessageMessageMessageMessageMessageMessageMessageMessageMessageMessageMessageMessageMessageEEE";
			vm.Ok = "CancelYesNoOk";
			vm.CancelVisibility = true;
			BitmapImage image = new BitmapImage(new System.Uri("pack://application:,,,/Image.bmp"));
			vm.Image = image;
			vm.Caption = "Test Message";
			Gat.Controls.MessageBoxResult result = vm.Show();
			if(result == Gat.Controls.MessageBoxResult.Ok)
			{
				vm.MessageBoxButton = Gat.Controls.MessageBoxButton.YesNoCancel;
				vm.Show("Msg!!!", "Caption", Gat.Controls.MessageBoxImage.Question);
			}

			vm.Position = MessageBoxPosition.NoCenter;
			vm.Show("Message..", "Caption", Gat.Controls.MessageBoxButton.OkCancel, Gat.Controls.MessageBoxImage.Question);

			vm.Message = "This is a message: MessageMessageMessageMessageMessageMessageMessageMessageMessageMessageMessageMessageMessageMessageMessageMessageEEE";
			vm.Ok = "Left";
			vm.Cancel = "Right";
			vm.Yes = "Top";
			vm.No = "Bottom";
			vm.OkVisibility = true;
			vm.CancelVisibility = true;
			vm.YesVisibility = true;
			vm.NoVisibility = true;
			vm.Image = new BitmapImage(new System.Uri("pack://application:,,,/Image.bmp"));
			vm.Caption = "Test Message";
			vm.Show();

			vm.Show("OkMessage", "Msg", MessageBoxButton.Ok);

			vm.Position = MessageBoxPosition.CenterScreen;
			vm.Show("Center Screen");

			vm.Position = MessageBoxPosition.CenterOwner;
			vm.Owner = this;
			vm.Show("Center Owner");
		}
	}
}
