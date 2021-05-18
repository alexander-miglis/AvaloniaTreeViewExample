using System;
using System.Collections.Generic;
using System.Text;

namespace AvaloniaTreeViewExample.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		public TreeViewExampleModel Tree { get; set; } = new();

		public MainWindowViewModel()
		{
			var root = new TreeViewItemModel(null, Tree)
			{
				Header = "Root"
			};
			Tree.Children.Add(root);
		}
	}
}
