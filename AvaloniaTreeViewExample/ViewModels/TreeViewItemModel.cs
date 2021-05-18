using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace AvaloniaTreeViewExample.ViewModels
{
	public class TreeViewItemModel: ViewModelBase
	{
		internal TreeViewExampleModel RootObject { get; set; }
		public TreeViewItemModel? Parent { get; set; }
		public ObservableCollection<TreeViewItemModel> Children { get; } = new();

		public ReactiveCommand<Unit, Unit> CreateCommand { get; }
		public ReactiveCommand<Unit, Unit> DeleteCommand { get; }


		public TreeViewItemModel(TreeViewItemModel? parent, TreeViewExampleModel rootObject)
		{
			Parent = parent;
			RootObject = rootObject;

			CreateCommand = ReactiveCommand.Create(CreateChild);
			DeleteCommand = ReactiveCommand.Create(DeleteMe);
		}

		private void CreateChild()
		{
			var newModel = new TreeViewItemModel(this, RootObject)
			{
				Header = GetNextName("Child "),
			};
			Children.Add(newModel);
			IsExpanded = true;
		}

		private string GetNextName(string baseName)
		{
			if (Children.All(s => s.Header != baseName))
				return baseName;
			int counter = 0;
			while (true)
			{
				string name = $"{baseName} {++counter}";
				if (Children.All(s => s.Header != name))
					return name;
			}
		}

		public void DeleteMe()
		{
			Parent?.DeleteChild(this);
		}

		public void DeleteChild(TreeViewItemModel child)
		{
			for (int i = Children.Count - 1; i >= 0; i--)
			{
				if(Children[i] == child)
					Children.RemoveAt(i);
			}
		}

		public string GetPath()
		{
			var traverser = this;
			string path = string.Empty;
			
			do
			{
				path = $"{traverser.Header}\\{path}";
				traverser = traverser.Parent;
			} while (traverser != null);

			return path;
		}

		private string _header = string.Empty;
		public string Header
		{
			get => _header;
			set
			{
				if (value != _header)
				{
					_header = value;
					this.RaisePropertyChanged();
				}
			}
		}

		private bool _isExpanded;
		public bool IsExpanded
		{
			get => _isExpanded;
			set
			{
				if (value == _isExpanded) 
					return;

				_isExpanded = value;
				
				// Expand all the way up to the root.
				if (_isExpanded && Parent != null)
					Parent.IsExpanded = true;
                    
				this.RaisePropertyChanged();
			}
		}

		private bool _isSelected;
		public bool IsSelected
		{
			get => _isSelected;
			set
			{
				if (value != _isSelected)
				{
					_isSelected = value;
					this.RaisePropertyChanged();
					RootObject.SelectedItem = this;
				}
			}
		}
	}
}
