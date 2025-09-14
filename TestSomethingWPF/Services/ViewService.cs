using TestSomething.ToNIPI.ViewModel.SystemAnalysis;
using TestSomethingWPF.Views.SystemAnalysis;

namespace TestSomething.ToNIPI.Services
{
    public class ViewService : IViewService
    {
        private IViewModelFactory _viewModelFactory;

        public ViewService(IViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void OpenSystemAnalysisView()
        {
            var viewModel = _viewModelFactory.Creaate<SystemAnalysisViewModel>();
            var view = new SystemAnalysisView { DataContext = viewModel };

            view.Show();
        }
    }
}
