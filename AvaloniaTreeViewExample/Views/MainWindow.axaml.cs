using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaTreeViewExample.Views
{
	public class MainWindow : Window
	{
		private readonly TreeView _tree;
		public MainWindow()
		{
			InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
			_tree = this.Get<TreeView>("TreeExample");
			_tree.PointerPressed += _tree_PointerPressed;
		}

		private void _tree_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
		{
			Console.WriteLine("It works?");
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
