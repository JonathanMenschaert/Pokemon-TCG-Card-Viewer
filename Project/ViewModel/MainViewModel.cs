﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project.Model;
using Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private Page currentPage = null;
        public RelayCommand SwitchToDetailPageCommand { get; private set; }
        public RelayCommand SwitchToSearchPageCommand { get; private set; }

        public RelayCommand SwitchToCollectionPageCommand { get; private set; }

        public Page CurrentPage
        {
            get
            {
                return currentPage;
            }
            private set
            {
                currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public OverviewApiPage SearchPage { get; private set; } = new OverviewApiPage();

        public OverviewLocalPage CollectionPage { get; private set; } = new OverviewLocalPage();
        public DetailPage InfoPage { get; private set; } = new DetailPage();

        public MainViewModel()
        {
            CurrentPage = SearchPage;
            SwitchToDetailPageCommand = new RelayCommand(SwitchToDetailPage);
            SwitchToSearchPageCommand = new RelayCommand(SwitchToSearchPage);
            SwitchToCollectionPageCommand = new RelayCommand(SwitchToCollectionPage);
        }

        private void SwitchToDetailPage()
        {
            if (CurrentPage is OverviewApiPage)
            {
                BaseCard selectedCard = (SearchPage.DataContext as OverviewPageApiVM).SelectedCard;
                if (selectedCard == null)
                {
                    return;
                }
                (InfoPage.DataContext as DetailPageVM).CurrentCard = selectedCard;
                CurrentPage = InfoPage;
            }
        }

        private void SwitchToSearchPage()
        {
            if (!(CurrentPage is OverviewApiPage))
            {
                CurrentPage = SearchPage;
            }
        }

        private void SwitchToCollectionPage()
        {
            if (!(CurrentPage is OverviewLocalPage))
            {
                CurrentPage = CollectionPage;
            }
        }
    }
}
