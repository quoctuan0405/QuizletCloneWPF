using QuizletClone.WPF.State;
using QuizletClone.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizletClone.WPF.ViewModels.Factories
{
    public class ViewModelAbstractFactory : IViewModelAbstractFactory
    {
        private readonly IViewModelFactory<HomeViewModel> _listSetViewModelFactory;
        private readonly IViewModelFactory<ProfileViewModel> _profileViewModelFactory;
        private readonly IViewModelFactory<LoginViewModel> _loginViewModelFactory;
        private readonly IViewModelFactory<SetDetailViewModel> _setDetailViewModelFactory;
        private readonly IViewModelFactory<CreateSetViewModel> _createSetViewModelFactory;
        private readonly IViewModelFactory<UpdateSetViewModel> _updateSetViewModelFactory;
        private readonly IViewModelFactory<SignUpViewModel> _signupViewModelFactory;
        private readonly IViewModelFactory<LearningViewModel> _learningViewModelFactory;

        public ViewModelAbstractFactory(
            IViewModelFactory<HomeViewModel> listSetViewModelFactory, 
            IViewModelFactory<ProfileViewModel> profileViewModelFactory, 
            IViewModelFactory<LoginViewModel> loginViewModelFactory, 
            IViewModelFactory<SetDetailViewModel> setDetailViewModelFactory,
            IViewModelFactory<CreateSetViewModel> createSetviewModelFactory,
            IViewModelFactory<UpdateSetViewModel> updateSetViewModelFactory,
            IViewModelFactory<SignUpViewModel> signupViewModelFactory,
            IViewModelFactory<LearningViewModel> learningViewModelFactory
        )
        {
            _listSetViewModelFactory = listSetViewModelFactory;
            _profileViewModelFactory = profileViewModelFactory;
            _loginViewModelFactory = loginViewModelFactory;
            _setDetailViewModelFactory = setDetailViewModelFactory;
            _createSetViewModelFactory = createSetviewModelFactory;
            _updateSetViewModelFactory = updateSetViewModelFactory;
            _signupViewModelFactory = signupViewModelFactory;
            _learningViewModelFactory = learningViewModelFactory;
        }

        public ViewModelBase CreateViewModel(ViewType viewType, INavigator navigator, Store store, object? data)
        {
            switch (viewType)
            {
                case ViewType.Login:
                    return _loginViewModelFactory.CreateViewModel(navigator, this, store, data);

                case ViewType.Profile:
                    return _profileViewModelFactory.CreateViewModel(navigator, this, store, data);

                case ViewType.Home:
                    return _listSetViewModelFactory.CreateViewModel(navigator, this, store, data);

                case ViewType.SetDetail:
                    return _setDetailViewModelFactory.CreateViewModel(navigator, this, store, data);

                case ViewType.CreateSet:
                    return _createSetViewModelFactory.CreateViewModel(navigator, this, store, data);

                case ViewType.UpdateSet:
                    return _updateSetViewModelFactory.CreateViewModel(navigator, this, store, data);

                case ViewType.Signup:
                    return _signupViewModelFactory.CreateViewModel(navigator, this, store, data);

                case ViewType.Learning:
                    return _learningViewModelFactory.CreateViewModel(navigator, this, store, data);

                default:
                    break;
            }

            return default;
        }
    }
}
