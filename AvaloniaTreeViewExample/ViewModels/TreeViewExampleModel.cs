using System.Collections.ObjectModel;
using ReactiveUI;

namespace AvaloniaTreeViewExample.ViewModels
{
	public class TreeViewExampleModel: ViewModelBase
	{
		private TreeViewItemModel? _selectedItem;
		public TreeViewItemModel? SelectedItem
		{
			get => _selectedItem;
			set
			{
				if(value == _selectedItem)
					return;

				_selectedItem = value;
				this.RaisePropertyChanged();
				this.RaisePropertyChanged(nameof(SelectedItemPath));
			}
		}

		public string? SelectedItemPath => _selectedItem?.GetPath();

		public ObservableCollection<TreeViewItemModel> Children { get; } = new();
	}
}
