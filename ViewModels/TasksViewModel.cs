using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using YourTasks.Models;

namespace YourTasks.ViewModels
{
    public class TasksViewModel : ViewModelBase
    {
        private ObservableCollection<TabItemViewModel>? _TabItemViewModels;
        private TabItemViewModel? _selectedTabItemViewModel;

        public ObservableCollection<TabItemViewModel> TabItemViewModels
        {
            get => _TabItemViewModels!;
            set => this.RaiseAndSetIfChanged(ref _TabItemViewModels, value);
        }

        public TabItemViewModel SelectedTabItemViewModel
        {
            get => _selectedTabItemViewModel!;
            set => this.RaiseAndSetIfChanged(ref _selectedTabItemViewModel, value);
        }

        public TasksViewModel()
        {
            TabItemViewModels = new ObservableCollection<TabItemViewModel>(){
                new TabItemViewModel(){
                    Project = new Project(){
                        Id = Guid.NewGuid(),
                        Name = "Project",
                        EllipseColor="Red"
                    },
                    TabContentViewModel = new TabContentViewModel(){
                        Tasks = new TaskGroupViewModel()
                        {
                            GroupName = "Total",
                            Tasks = new ObservableCollection<TaskViewModel>()
                            {
                                new TaskViewModel(){
                                    Task = new Task(){
                                        Id = Guid.NewGuid(),
                                        Text = "Add project",
                                        Description = "",
                                        CreationDateTime = DateTime.Today,
                                        IsCompleted = false
                                    },
                                    SubTasks = new ObservableCollection<Task>()
                                    {
                                        new Task(){
                                            Id = Guid.NewGuid(),
                                            Text = "new files",
                                            Description = "",
                                            CreationDateTime = DateTime.Today,
                                            IsCompleted = false
                                        }
                                    }
                                }
                            }
                        },
                        CompletedTasks = new TaskGroupViewModel()
                        {
                            GroupName = "Completed",
                            Tasks = new ObservableCollection<TaskViewModel>()
                        }
                    }
                },
                new TabItemViewModel(){
                    Project = new Project()
                    {
                        Id = Guid.NewGuid(),
                        Name = "DataBase",
                        EllipseColor = "Green"
                    },
                    TabContentViewModel = new TabContentViewModel()
                    {
                        
                    }                    
                }
            };
        }
    }
}